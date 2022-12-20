using MM_MoneyFy.Model;
using MM_MoneyFy.View.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM_MoneyFy.ViewModel
{
    public class OperationsPage_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private int countOfOperations=DataWorker.GetAllOperationsByUserId(DataWorker.CurrentUser.Id).Count();
        public int CountOfOperations
        {
            get => countOfOperations;
            set
            {
                countOfOperations = value;
                NotifyPropertyChanged("CountOfOperations");
            }
        }


        private decimal sumOfIncomes = OperationSum("income");
        public decimal SumOfIncomes
        {
            get => sumOfIncomes;
            set
            {
                sumOfIncomes = value;
                NotifyPropertyChanged("SumOfIncomes");
            }
        }

        private decimal sumOfWastes = OperationSum("waste");
        public decimal SumOfWastes
        {
            get => sumOfWastes;
            set
            {
                sumOfWastes = value;
                NotifyPropertyChanged("SumOfWastes");
            }
        }


        private static decimal OperationSum(string type)
        {
            ObservableCollection<Operation> operations = DataWorker.GetAllOperationsByUserId(DataWorker.CurrentUser.Id);
            decimal income = 0;
            decimal waste = 0;
            foreach(var el in operations)
            {
                if (el.Sum > 0) income += el.Sum;
                else waste += el.Sum;
            }
            switch (type)
            {
                case "income": return income;
                case "waste": return waste;
                default:return 0;
            }
        }



        private ObservableCollection<Operation> allOperations = DataWorker.GetAllOperationsByUserId(DataWorker.CurrentUser.Id);
        public ObservableCollection<Operation> AllOperations
        {
            get => allOperations;
            set
            {
                allOperations = value;
                NotifyPropertyChanged("AllOperations");
            }
        }


        #region SEARCHandSORTS
        private string searchByName;
        public string SearchByName
        {
            get => searchByName;
            set
            {
                searchByName = value;
                AllOperations = DataWorker.GetAllOperationsByName(searchByName);
                UpdateDataGrid();
                NotifyPropertyChanged("SearchByName");
            }
        }

        private string searchByAccount;
        public string SearchByAccount
        {
            get => searchByAccount;
            set
            {
                searchByAccount = value;
                AllOperations = DataWorker.GetAllOperationsByAccountName(searchByAccount);
                UpdateDataGrid();
                NotifyPropertyChanged("SearchByAccount");
            }
        }

        private string searchByCategory;
        public string SearchByCategory
        {
            get => searchByCategory;
            set
            {
                searchByCategory = value;
                AllOperations = DataWorker.GetAllOperationsByCategoryName(searchByCategory);
                UpdateDataGrid();
                NotifyPropertyChanged("SearchByCategory");
            }
        }

        
        private void UpdateDataGrid()
        {
            OperationsPageView.OperationsDataGridView.ItemsSource = null;
            OperationsPageView.OperationsDataGridView.Items.Clear();
            OperationsPageView.OperationsDataGridView.ItemsSource = AllOperations;
            OperationsPageView.OperationsDataGridView.Items.Refresh();
        }
        private void setnull()
        {
            SearchByAccount = null;
            SearchByCategory = null;
            SearchByName = null;
        }
        #endregion

        #region TYPEofOPERATIONS
        private bool showAll;
        public bool ShowAll
        {
            get
            {
                return showAll;
            }
            set
            {
                showAll = value;
                if (showAll == true)
                {
                    AllOperations = DataWorker.GetAllOperationsByUserId(DataWorker.CurrentUser.Id);
                    UpdateDataGrid();
                }
                NotifyPropertyChanged("ShowAll");

            }
        }

        private bool showIncomes;
        public bool ShowIncomes
        {
            get
            {
                return showIncomes;
            }
            set
            {
                showIncomes = value;
                if (showIncomes == true)
                {
                    AllOperations = DataWorker.GetAllIncomeOperations();
                    UpdateDataGrid();
                }
                NotifyPropertyChanged("ShowIncomes");

            }
        }

        private bool showWastes;
        public bool ShowWastes
        {
            get
            {
                return showWastes;
            }
            set
            {
                showWastes = value;
                if (showWastes == true)
                {
                    AllOperations = DataWorker.GetAllWastesOperations();
                    UpdateDataGrid();
                }
                NotifyPropertyChanged("ShowWastes");

            }
        }
        #endregion
    }
}
