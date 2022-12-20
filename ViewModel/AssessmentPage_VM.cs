using LiveCharts;
using LiveCharts.Wpf;
using MM_MoneyFy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace MM_MoneyFy.ViewModel
{
    class AssessmentPage_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public AssessmentPage_VM()
        {
           

            //incomes
            SeriesCollection = new SeriesCollection();
            LineSeries IncomeLine = new LineSeries
            {
                Values = new ChartValues<decimal>(),
                PointGeometrySize = 15,
                StrokeThickness = 4,
                Fill = Brushes.Transparent,
                Title="+"
            };
            ChartValues<decimal> IncomeValue = new ChartValues<decimal>();
            foreach(var el in DataWorker.GetAllIncomeOperations())
            {
                IncomeValue.Add(el.Sum);
            }
            IncomeLine.Values = IncomeValue;
            SeriesCollection.Add(IncomeLine);

            //wastes
            LineSeries WasteLine = new LineSeries
            {
                Values = new ChartValues<decimal>(),
                PointGeometrySize = 15,
                StrokeThickness = 4,
                Fill = Brushes.Transparent,
                Foreground = Brushes.Red
             
            };
            ChartValues<decimal> WasteValue = new ChartValues<decimal>();
            foreach (var el in DataWorker.GetAllWastesOperations())
            {
                WasteValue.Add(el.Sum*(-1));
            }
            WasteLine.Values = WasteValue;
            SeriesCollection.Add(WasteLine);


            //Dates
            IncomesDate = new List<string>();
            
            foreach(var el in DataWorker.GetAllIncomeOperations())
            {
                IncomesDate.Add(el.date.ToShortDateString());
            }
            WastesDate =new List<string>();
            foreach(var el in DataWorker.GetAllWastesOperations())
            {
                WastesDate.Add(el.date.ToShortDateString());
            }
            






            //incomes only
            SeriesCollection2 = new SeriesCollection();
            LineSeries OnlyIncomeLine = new LineSeries
            {
                Values = new ChartValues<decimal>(),
                PointGeometrySize = 15,
                StrokeThickness = 4,
                Fill = Brushes.Transparent,
                Title = "+"
            };
            ChartValues<decimal> OnlyIncomeValue = new ChartValues<decimal>();
            foreach (var el in DataWorker.GetAllIncomeOperations())
            {
                OnlyIncomeValue.Add(el.Sum);
            }
            OnlyIncomeLine.Values = OnlyIncomeValue;
            SeriesCollection2.Add(OnlyIncomeLine);

            //wastes only
            SeriesCollection3 = new SeriesCollection();
            LineSeries OnlyWasteLine = new LineSeries
            {
                Values = new ChartValues<decimal>(),
                PointGeometrySize = 15,
                StrokeThickness = 4,
                Fill = Brushes.Transparent,
                Foreground = Brushes.Red,
                Title = "-"
            };
            ChartValues<decimal> OnlyWasteValue = new ChartValues<decimal>();
            foreach (var el in DataWorker.GetAllWastesOperations())
            {
                OnlyWasteValue.Add(el.Sum * (-1));
            }
            OnlyWasteLine.Values = OnlyWasteValue;
            SeriesCollection3.Add(OnlyWasteLine);
        }
        public List<string> IncomesDate { get; set; }
        public List<string> WastesDate { get; set; }

        private static SeriesCollection seriesCollection;
        public SeriesCollection SeriesCollection
        {
            get => seriesCollection;
            set
            {
                seriesCollection = value;
                NotifyPropertyChanged("SeriesCollection");
            }
        }

        private static SeriesCollection seriesCollection2;
        public SeriesCollection SeriesCollection2
        {
            get => seriesCollection2;
            set
            {
                seriesCollection2 = value;
                NotifyPropertyChanged("SeriesCollection2");
            }
        }

        private static SeriesCollection seriesCollection3;
        public SeriesCollection SeriesCollection3
        {
            get => seriesCollection3;
            set
            {
                seriesCollection3 = value;
                NotifyPropertyChanged("SeriesCollection3");
            }
        }


        //все счета
        private static Account selectedAccount;
        public  Account SelectedAccount
        {
            get => selectedAccount;
            set
            {
                selectedAccount = value;
                NotifyPropertyChanged("SelectedAccount");
            }
        }


        public void AccountSelectionChenged(object sender, RoutedEventArgs e)// add the proper parameters
        {
            if (SelectedAccount != null)
            {
                //incomes
                SeriesCollection =null;
                SeriesCollection =new SeriesCollection();
                LineSeries IncomeLine = new LineSeries
                {
                    Values = new ChartValues<decimal>(),
                    PointGeometrySize = 15,
                    StrokeThickness = 4,
                    Fill = Brushes.Transparent,
                    Title = "+"
                };
                ChartValues<decimal> IncomeValue = new ChartValues<decimal>();
                foreach (var el in DataWorker.GetAllIncomeOperations(SelectedAccount.Id))
                {
                    IncomeValue.Add(el.Sum);
                }
                IncomeLine.Values = IncomeValue;
                SeriesCollection.Add(IncomeLine);

                //wastes
                LineSeries WasteLine = new LineSeries
                {
                    Values = new ChartValues<decimal>(),
                    PointGeometrySize = 15,
                    StrokeThickness = 4,
                    Fill = Brushes.Transparent,
                    Foreground = Brushes.Red

                };
                ChartValues<decimal> WasteValue = new ChartValues<decimal>();
                foreach (var el in DataWorker.GetAllWastesOperations(SelectedAccount.Id))
                {
                    WasteValue.Add(el.Sum * (-1));
                }
                WasteLine.Values = WasteValue;
                SeriesCollection.Add(WasteLine);


                //Dates
                IncomesDate = new List<string>();

                foreach (var el in DataWorker.GetAllIncomeOperations(SelectedAccount.Id))
                {
                    IncomesDate.Add(el.date.ToShortDateString());
                }
                WastesDate = new List<string>();
                foreach (var el in DataWorker.GetAllWastesOperations(SelectedAccount.Id))
                {
                    WastesDate.Add(el.date.ToShortDateString());
                }

                //incomes only
                SeriesCollection2 =null;
                SeriesCollection2 = new SeriesCollection();
                LineSeries OnlyIncomeLine = new LineSeries
                {
                    Values = new ChartValues<decimal>(),
                    PointGeometrySize = 15,
                    StrokeThickness = 4,
                    Fill = Brushes.Transparent,
                    Title = "+"
                };
                ChartValues<decimal> OnlyIncomeValue = new ChartValues<decimal>();
                foreach (var el in DataWorker.GetAllIncomeOperations(SelectedAccount.Id))
                {
                    OnlyIncomeValue.Add(el.Sum);
                }
                OnlyIncomeLine.Values = OnlyIncomeValue;
                SeriesCollection2.Add(OnlyIncomeLine);

                //wastes only
                SeriesCollection3 = null;
                
                SeriesCollection3 = new SeriesCollection();
                LineSeries OnlyWasteLine = new LineSeries
                {
                    Values = new ChartValues<decimal>(),
                    PointGeometrySize = 15,
                    StrokeThickness = 4,
                    Fill = Brushes.Transparent,
                    Foreground = Brushes.Red,
                    Title = "-"
                };
                ChartValues<decimal> OnlyWasteValue = new ChartValues<decimal>();
                foreach (var el in DataWorker.GetAllWastesOperations(SelectedAccount.Id))
                {
                    OnlyWasteValue.Add(el.Sum * (-1));
                }
                OnlyWasteLine.Values = OnlyWasteValue;
                SeriesCollection3.Add(OnlyWasteLine);
            }
            else
            {
                //incomes
                SeriesCollection = new SeriesCollection();
                LineSeries IncomeLine = new LineSeries
                {
                    Values = new ChartValues<decimal>(),
                    PointGeometrySize = 15,
                    StrokeThickness = 4,
                    Fill = Brushes.Transparent,
                    Title = "+"
                };
                ChartValues<decimal> IncomeValue = new ChartValues<decimal>();
                foreach (var el in DataWorker.GetAllIncomeOperations())
                {
                    IncomeValue.Add(el.Sum);
                }
                IncomeLine.Values = IncomeValue;
                SeriesCollection.Add(IncomeLine);

                //wastes
                LineSeries WasteLine = new LineSeries
                {
                    Values = new ChartValues<decimal>(),
                    PointGeometrySize = 15,
                    StrokeThickness = 4,
                    Fill = Brushes.Transparent,
                    Foreground = Brushes.Red

                };
                ChartValues<decimal> WasteValue = new ChartValues<decimal>();
                foreach (var el in DataWorker.GetAllWastesOperations())
                {
                    WasteValue.Add(el.Sum * (-1));
                }
                WasteLine.Values = WasteValue;
                SeriesCollection.Add(WasteLine);


                //Dates
                IncomesDate = new List<string>();

                foreach (var el in DataWorker.GetAllIncomeOperations())
                {
                    IncomesDate.Add(el.date.ToShortDateString());
                }
                WastesDate = new List<string>();
                foreach (var el in DataWorker.GetAllWastesOperations())
                {
                    WastesDate.Add(el.date.ToShortDateString());
                }







                //incomes only
                SeriesCollection2 = new SeriesCollection();
                LineSeries OnlyIncomeLine = new LineSeries
                {
                    Values = new ChartValues<decimal>(),
                    PointGeometrySize = 15,
                    StrokeThickness = 4,
                    Fill = Brushes.Transparent,
                    Title = "+"
                };
                ChartValues<decimal> OnlyIncomeValue = new ChartValues<decimal>();
                foreach (var el in DataWorker.GetAllIncomeOperations())
                {
                    OnlyIncomeValue.Add(el.Sum);
                }
                OnlyIncomeLine.Values = OnlyIncomeValue;
                SeriesCollection2.Add(OnlyIncomeLine);

                //wastes only
                SeriesCollection3 = new SeriesCollection();
                LineSeries OnlyWasteLine = new LineSeries
                {
                    Values = new ChartValues<decimal>(),
                    PointGeometrySize = 15,
                    StrokeThickness = 4,
                    Fill = Brushes.Transparent,
                    Foreground = Brushes.Red,
                    Title = "-"
                };
                ChartValues<decimal> OnlyWasteValue = new ChartValues<decimal>();
                foreach (var el in DataWorker.GetAllWastesOperations())
                {
                    OnlyWasteValue.Add(el.Sum * (-1));
                }
                OnlyWasteLine.Values = OnlyWasteValue;
                SeriesCollection3.Add(OnlyWasteLine);
            }

            
        }




        private List<Account> allAccountsOfCurrentUser = DataWorker.GetAllAccountsByUserId(DataWorker.CurrentUser.Id);
        public List<Account> AllAccountsOfCurrentUser
        {
            get { return allAccountsOfCurrentUser; }
            set
            {
                allAccountsOfCurrentUser = value;
                NotifyPropertyChanged("AllAccountsOfCurrentUser");
            }
        }

        private int countOfOperations = DataWorker.GetAllOperationsByUserId(DataWorker.CurrentUser.Id).Count();
        public int CountOfOperations
        {
            get => countOfOperations;
            set
            {
                countOfOperations = value;
                NotifyPropertyChanged("CountOfOperations");
            }
        }

        private int countOfAccounts = DataWorker.GetAllAccountsByUserId(DataWorker.CurrentUser.Id).Count();
        public int CountOfAccounts
        {
            get => countOfAccounts;
            set
            {
                countOfAccounts = value;
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
            foreach (var el in operations)
            {
                if (el.Sum > 0) income += el.Sum;
                else waste += el.Sum;
            }
            switch (type)
            {
                case "income": return income;
                case "waste": return waste;
                default: return 0;
            }
        }
    }
}
