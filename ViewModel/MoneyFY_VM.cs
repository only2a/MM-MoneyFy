using MM_MoneyFy.Model;
using MM_MoneyFy.View;
using MM_MoneyFy.View.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MM_MoneyFy.ViewModel
{
    class MoneyFY_VM : INotifyPropertyChanged,IDataErrorInfo
    {

        public MoneyFY_VM()
        {

            Incomes = new ObservableCollection<Operation>();
            Wastes = new ObservableCollection<Operation>();

            foreach (var el in DataWorker.GetAllOperationsByUserId(DataWorker.CurrentUser.Id))
            {
                if (el.Sum > 0)
                    Incomes.Add(el);
                else Wastes.Add(el);
            }

            foreach (var el in Wastes)
            {
                el.Sum *= -1;
            }

        }

        public ObservableCollection<Operation> Incomes { get; private set; }
        public ObservableCollection<Operation> Wastes { get; private set; }

        public void FirstColor()
        {
            var Uri = new Uri("Themes/PurpleDark.xaml", UriKind.Relative);
            ResourceDictionary resource = Application.LoadComponent(Uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resource);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region ADDITIONAL

        private void SetRedBlockControll(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        private void ShowMessageToUser(string message)
        {
            MessageWindow messageView = new MessageWindow(message);
            messageView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            messageView.ShowDialog();
        }

        //все счета
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

        //all currency
        private ObservableCollection<Currency> allCurrency = DataWorker.GetAllCurrency();
        public ObservableCollection<Currency> AllCurrency
        {
            get { return allCurrency; }
            set
            {
                allCurrency = value;
                NotifyPropertyChanged("AllCurrency");
            }
        }

        //все categories
        private List<Category> allIncomesCategories = DataWorker.GetAllCategories(true);
        public List<Category> AllIncomesCategories
        {
            get { return allIncomesCategories; }
            set
            {
                allIncomesCategories = value;
                NotifyPropertyChanged("AllCategories");
            }
        }

        private List<Category> allWastesCategories = DataWorker.GetAllCategories(false);
        public List<Category> AllWastesCategories
        {
            get { return allWastesCategories; }
            set
            {
                allWastesCategories = value;
                NotifyPropertyChanged("AllWastesCategories");
            }
        }

        private List<Category> allCategories = DataWorker.GetAllCategories();
        public List<Category> AllCategories
        {
            get { return allCategories; }
            set
            {
                allCategories = value;
                NotifyPropertyChanged("AllCategories");
            }
        }


        //All Icons
        private ObservableCollection<Icon> allIcons = DataWorker.GetAllIcons();
        public ObservableCollection<Icon> AllIcons
        {
            get => allIcons;
            set
            {
                allIcons = value;
                NotifyPropertyChanged("AllIcons");
            }
        }

        private ObservableCollection<Icon> allAccountIcons = DataWorker.GetAllAccountIcons();
        public ObservableCollection<Icon> AllAccountIcons
        {
            get => allAccountIcons;
            set
            {
                allAccountIcons = value;
                NotifyPropertyChanged("AllAccountIcons");
            }
        }



        //All operations
        private ObservableCollection<Operation> alloperations = DataWorker.GetAllOperationsByUserId(DataWorker.CurrentUser.Id);
        public ObservableCollection<Operation> AllOperations
        {
            get => alloperations;
            set
            {
                alloperations = value;
                NotifyPropertyChanged("AllOperations");
            }
        }


        //Properties for add Incomes
        private void UpdateAllOperationsView()
        {
            AllOperations = DataWorker.GetAllOperationsByUserId(DataWorker.CurrentUser.Id);
            MoneyFy.OperationView.ItemsSource = null;
            MoneyFy.OperationView.Items.Clear();
            MoneyFy.OperationView.ItemsSource = AllOperations;
            MoneyFy.OperationView.Items.Refresh();
        }

        private void UpdatePieChart()
        {
            Incomes = null;
            Incomes = new ObservableCollection<Operation>();
            Wastes = null;
            Wastes = new ObservableCollection<Operation>();

            foreach (var el in DataWorker.GetAllOperationsByUserId(DataWorker.CurrentUser.Id))
            {
                if (el.Sum > 0)
                    Incomes.Add(el);
                else Wastes.Add(el);
            }
            foreach (var el in Wastes)
            {
                el.Sum *= -1;
            }
            MoneyFy.PieChartIncomeView.ItemsSource = null;
            MoneyFy.PieChartIncomeView.Items.Clear();
            MoneyFy.PieChartIncomeView.ItemsSource = Incomes;
            MoneyFy.PieChartIncomeView.Items.Refresh();

            MoneyFy.PieChartWasteView.ItemsSource = null;
            MoneyFy.PieChartWasteView.Items.Clear();
            MoneyFy.PieChartWasteView.ItemsSource = Wastes;
            MoneyFy.PieChartWasteView.Items.Refresh();
        }

        private void UpdateAllAccountsView()
        {
            AllAccountsOfCurrentUser = DataWorker.GetAllAccountsByUserId(DataWorker.CurrentUser.Id);
            MoneyFy.AccountView.ItemsSource = null;
            MoneyFy.AccountView.Items.Clear();
            MoneyFy.AccountView.ItemsSource = AllAccountsOfCurrentUser;
            MoneyFy.AccountView.Items.Refresh();
        }

        //Logout
        private RelayCommand logOut;
        public RelayCommand LogOut
        {
            get
            {
                return logOut ?? new RelayCommand(obj => {
                    Window wnd = obj as Window;
                    MainWindow mwnd = new MainWindow();
                    mwnd.Show();
                    wnd.Close();

                });
            }
        }

        private void SetNullValuesToProperties()
        {
            //для пользователя
            AccountName = null;
            AccountIcon = null;
            AccountCurrency = null;
            AccountBalance = null;
            SelectedAccount = null;
            //for operation
            OperationName = null;
            OperationAccount = null;
            OperationSum = null;
            OperationCategory = null;
            SelectedOperation = null;

        }
        #endregion

        #region OPERATION

        //private string operatonName;
        //public string OperationName
        //{
        //    get => operatonName;
        //    set
        //    {
        //        operatonName = value;
        //        NotifyPropertyChanged("OperationName");
        //    }
        //}
        private static string operationName;
        public string OperationName 
        { 
            get => operationName; 
            set 
            {
                operationName = value;
                NotifyPropertyChanged("OperationName");
            } 
        }
        //private string operationSum;
        //public string OperationSum
        //{
        //    get => operationSum;
        //    set
        //    {
        //        operationSum = value;
        //        NotifyPropertyChanged("OperationSum");
        //    }
        //}
        private static string operationSum;
        public  string OperationSum
        {
            get => operationSum;
            set
            {
                operationSum = value;
                NotifyPropertyChanged("OperationSum");
            }
        }


        private static Category operationCategory;
        public  Category OperationCategory
        {
            get => operationCategory;
            set
            {
                operationCategory = value;
                NotifyPropertyChanged("OperationCategory");
            }
        }
        public static Account OperationAccount { get; set; }
        public static TabItem SelectedTabItem { get; set; }
        public static Operation SelectedOperation { get; set; }

        private static Currency operationCurrency;
        public  Currency OperationCurrency
        {
            get => operationCurrency;
            set
            {
                operationCurrency = value;
                NotifyPropertyChanged("OperationCurrency");
            }
        }
 

        public RelayCommand addOperation;
        public RelayCommand AddOperation
        {
            get
            {
                return addOperation ?? new RelayCommand(obj =>
                {
                    MoneyFy wnd = obj as MoneyFy;
                    string resultStr = "";
                    if (OperationName == null || OperationName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlockIncome");
                    }
                    else if (OperationSum == null || OperationSum.Replace(" ", "").Length == 0) SetRedBlockControll(wnd, "SumBlockIncome");
                    else if (OperationCategory == null) SetRedBlockControll(wnd, "CategoryBlockIncome");
                    else if (OperationAccount == null) SetRedBlockControll(wnd, "AccountBlockIncome");
                    else
                    {
                        if (SelectedTabItem.Name == "Income")
                        {
                            resultStr = DataWorker.CreateOperation(OperationName, OperationSum, OperationCategory, OperationAccount,OperationCurrency);
                            ShowMessageToUser(resultStr);
                            UpdateAllAccountsView();
                            UpdateAllOperationsView();
                            SetNullValuesToProperties();

                            
                            UpdatePieChart();

                        }
                        else if (SelectedTabItem.Name == "Waste")
                        {
                            resultStr = DataWorker.CreateOperation(OperationName, OperationSum, OperationCategory, OperationAccount, OperationCurrency, false);
                            ShowMessageToUser(resultStr);
                            UpdateAllAccountsView();
                            UpdateAllOperationsView();
                            SetNullValuesToProperties();
                            UpdatePieChart();
                            
                        }



                    }
                });
            }
        }


        private RelayCommand deleteOperation;
        public RelayCommand DeleteOperation
        {
            get
            {
                return deleteOperation ?? new RelayCommand(obj =>
                {
                    MoneyFy wnd = obj as MoneyFy;
                    string resultStr = "Ничего не выбрано";
                    //если сотрудник
                    if (SelectedOperation != null)
                    {
                        resultStr = DataWorker.DeleteOperation(SelectedOperation);
                        UpdateAllOperationsView();
                        UpdateAllAccountsView();
                        UpdatePieChart();
                    }
                    //обновление
                    SetNullValuesToProperties();
                    ShowMessageToUser(resultStr);
                }
                    );
            }
        }


        private RelayCommand editOperation;
        public RelayCommand EditOperation
        {
            get
            {
                return editOperation ?? new RelayCommand(obj =>
                {
                    MoneyFy window = obj as MoneyFy;
                    string resultStr = "Не выбрана операция";
                    string noCategoryStr = "Не выбрана новая категория";
                    string noAccountStr = "Не выбран новый счёт";
                    string noAccountIcon = "Не выбрана новая иконка счёта";
                    string noAccountCurrency = "Не выбрана новая валюта счёта";
                    if (SelectedOperation != null)
                    {
                        if (OperationAccount != null)
                        {
                            if (OperationCategory != null)
                            {
                                resultStr = DataWorker.EditOperation(SelectedOperation, OperationName, OperationSum, OperationCategory, OperationAccount);

                                UpdateAllOperationsView();
                                UpdateAllAccountsView();
                                UpdatePieChart();
                                SetNullValuesToProperties();
                                ShowMessageToUser(resultStr);
                                MoneyFy.GridForEditPagesView.Visibility = Visibility.Collapsed;
                                MoneyFy.EditPagesFrameView.Content = null;
                            }
                            else ShowMessageToUser(noCategoryStr);
                        }
                        else ShowMessageToUser(noAccountStr);
                    }
                    else if(SelectedAccount!=null)
                    {
                        if (AccountIcon != null)
                        {
                            if(AccountCurrency !=null)
                            {
                                resultStr = DataWorker.EditAccount(SelectedAccount, AccountName, AccountIcon, AccountBalance, AccountCurrency);
                                UpdateAllOperationsView();
                                UpdateAllAccountsView();
                                UpdatePieChart();
                                SetNullValuesToProperties();
                                ShowMessageToUser(resultStr);
                                MoneyFy.GridForEditPagesView.Visibility = Visibility.Collapsed;
                                MoneyFy.EditPagesFrameView.Content = null;
                            }
                            else ShowMessageToUser(noAccountCurrency);

                        }
                        else ShowMessageToUser(noAccountIcon);
                    }
                    else ShowMessageToUser(resultStr);

                }
                );
            }
        }
        #endregion

        #region OPENPAGES

        // Operations
        private RelayCommand openOperationsPage;
        public RelayCommand OpenOperationsPage
        {
            get
            {
                return openOperationsPage ?? new RelayCommand(obj =>
                {
                    MoneyFy wnd = obj as MoneyFy;
                    wnd.MainGrid.Visibility = Visibility.Collapsed;
                    wnd.frameForPages.Visibility = Visibility.Visible;
                    wnd.frameForPages.Content = new OperationsPageView();
                });
            }
        }


        //Assessment
        private RelayCommand openAssessmentPage;
        public RelayCommand OpenAssessmentPage
        {
            get
            {
                return openAssessmentPage ?? new RelayCommand(obj =>
                {
                    MoneyFy wnd = obj as MoneyFy;
                    wnd.MainGrid.Visibility = Visibility.Collapsed;
                    wnd.frameForPages.Visibility = Visibility.Visible;
                    wnd.frameForPages.Content = new AssessmentsPageView();
                });
            }
        }

        //UserSettings
        private RelayCommand openUserSettingsPage;
        public RelayCommand OpenUserSettingsPage
        {
            get
            {
                return openUserSettingsPage ?? new RelayCommand(obj =>
                {
                    MoneyFy wnd = obj as MoneyFy;
                    wnd.MainGrid.Visibility = Visibility.Collapsed;
                    wnd.frameForPages.Visibility = Visibility.Visible;
                    wnd.frameForPages.Content = new UserSettingsView();
                });
            }
        }

        //AdminSettings
        private RelayCommand openServicesSettingsPage;
        public RelayCommand OpenServicesSettingsPage
        {
            get
            {
                return openServicesSettingsPage ?? new RelayCommand(obj =>
                {
                    MoneyFy wnd = obj as MoneyFy;
                    wnd.MainGrid.Visibility = Visibility.Collapsed;
                    wnd.frameForPages.Visibility = Visibility.Visible;
                    wnd.frameForPages.Content = new ServicesSettingsPage();
                });
            }
        }

        //Home
        private RelayCommand openHomeGrid;
        public RelayCommand OpenHomeGrid
        {
            get
            {
                return openHomeGrid ?? new RelayCommand(obj =>
                {
                    MoneyFy wnd = obj as MoneyFy;
                    wnd.frameForPages.Content = null;
                    wnd.frameForPages.Visibility = Visibility.Collapsed;
                    wnd.MainGrid.Visibility = Visibility.Visible;
                    UpdateAllOperationsView();
                    UpdatePieChart();
                    UpdateAllAccountsView();
                    


                });
            }
        }


        //Add Account
        private RelayCommand openAddAccountPage;
        public RelayCommand OpenAddAccountPage
        {
            get
            {
                return openAddAccountPage ?? new RelayCommand(obj =>
                {
                    MoneyFy wnd = obj as MoneyFy;
                    wnd.AccountsListBox.Visibility = Visibility.Collapsed;
                    wnd.AccountAddGrid.Visibility = Visibility.Visible;
                    wnd.AccountAddFrame.Content = new AddAccountPage();
                });
            }
        }


        //Close add Account
        private RelayCommand closeAddAccountPage;
        public RelayCommand CloseAddAccountPage
        {
            get
            {
                return closeAddAccountPage ?? new RelayCommand(obj =>
                {
                    MoneyFy wnd = obj as MoneyFy;
                    wnd.AccountsListBox.Visibility = Visibility.Visible;
                    wnd.AccountAddGrid.Visibility = Visibility.Collapsed;
                    wnd.AccountAddFrame.Content = null;
                });
            }
        }

        private RelayCommand start;
        public RelayCommand Start
        {
            get => start ?? new RelayCommand(obj =>
             {
                 MoneyFy wnd = obj as MoneyFy;
                 DoubleAnimation animtion2 = new DoubleAnimation()
                 {
                     From = 1,
                     To = 0,
                     Duration = TimeSpan.FromSeconds(1),
                     FillBehavior = FillBehavior.HoldEnd,
                     AccelerationRatio = .5,
                     BeginTime = TimeSpan.FromSeconds(0)
                 };
                 
                 wnd.StartPage.BeginAnimation(ContentControl.OpacityProperty, animtion2);



                 ObjectAnimationUsingKeyFrames animate = new ObjectAnimationUsingKeyFrames();

                 animate.Duration = new TimeSpan(0, 0, 1);

                 DiscreteObjectKeyFrame kf1 = new DiscreteObjectKeyFrame(
                     Visibility.Collapsed,
                     new TimeSpan(0, 0, 0, 1));
                 animate.KeyFrames.Add(kf1);

                 wnd.StartPage.BeginAnimation(ContentControl.VisibilityProperty, animate);
             });

        }

        private RelayCommand openEditOperationPage;
        public RelayCommand OpenEditOperationPage
        {
            get
            {
                return openEditOperationPage ?? new RelayCommand(obj =>
                {

                    MoneyFy wnd = obj as MoneyFy;
                    MoneyFy.GridForEditPagesView.Visibility = Visibility.Visible;
                    MoneyFy.EditPagesFrameView.Content = new EditOperationPage();
                    ThicknessAnimation animtion = new ThicknessAnimation()
                    {
                        From = new Thickness(400),
                        To = new Thickness(0),
                        Duration = TimeSpan.FromSeconds(0.5),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .5,
                        BeginTime = TimeSpan.FromSeconds(0)
                    };

                    MoneyFy.BackGridView.BeginAnimation(ContentControl.MarginProperty, animtion);



                    DoubleAnimation animtion2 = new DoubleAnimation()
                    {
                        From = 0,
                        To = 500,
                        Duration = TimeSpan.FromSeconds(1),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .5,
                        BeginTime = TimeSpan.FromSeconds(0.5)
                    };
                    MoneyFy.GridWithEditPagesView.BeginAnimation(ContentControl.WidthProperty, animtion2);
                });
            }
        }



        private RelayCommand openEditAccountPage;
        public RelayCommand OpenEditAccountPage
        {
            get
            {
                return openEditOperationPage ?? new RelayCommand(obj =>
                {

                    MoneyFy wnd = obj as MoneyFy;
                    MoneyFy.GridForEditPagesView.Visibility = Visibility.Visible;
                    MoneyFy.EditPagesFrameView.Content = new EditAccountPage();
                    ThicknessAnimation animtion = new ThicknessAnimation()
                    {
                        From = new Thickness(400),
                        To = new Thickness(0),
                        Duration = TimeSpan.FromSeconds(0.5),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .5,
                        BeginTime = TimeSpan.FromSeconds(0)
                    };

                    MoneyFy.BackGridView.BeginAnimation(ContentControl.MarginProperty, animtion);



                    DoubleAnimation animtion2 = new DoubleAnimation()
                    {
                        From = 0,
                        To = 500,
                        Duration = TimeSpan.FromSeconds(1),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .5,
                        BeginTime = TimeSpan.FromSeconds(0.5)
                    };
                    MoneyFy.GridWithEditPagesView.BeginAnimation(ContentControl.WidthProperty, animtion2);
                });
            }
        }

        private RelayCommand closeEditGrid;
        public RelayCommand CloseEditGrid
        {
            get
            {
                return closeEditGrid ?? new RelayCommand(obj =>
                {
                    MoneyFy wnd = obj as MoneyFy;
                    DoubleAnimation animtion2 = new DoubleAnimation()
                    {
                        From = 500,
                        To = 0,
                        Duration = TimeSpan.FromSeconds(0.3),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .5,
                        BeginTime = TimeSpan.FromSeconds(0)
                    };
                    MoneyFy.GridWithEditPagesView.BeginAnimation(ContentControl.WidthProperty, animtion2);
                    MoneyFy.GridForEditPagesView.Visibility = Visibility.Collapsed;

                    MoneyFy.EditPagesFrameView.Content = null;

                });
            }
        }
        #endregion

        #region ACCOUNT

        //private string accountName;
        //public string AccountName
        //{
        //    get => accountName;
        //    set
        //    {
        //        accountName = value;
        //        NotifyPropertyChanged("AccountName");
        //    }
        //}

        public static string AccountName { get; set; }
        public static Icon AccountIcon { get; set; }
        public static Currency AccountCurrency { get; set; }
        //private string accountBalance;
        //public string AccountBalance
        //{
        //    get => accountBalance;
        //    set
        //    {
        //        accountBalance = value;
        //        NotifyPropertyChanged("AccountBalance");
        //    }
        //}

        public static string AccountBalance { get; set; }
        public static Account SelectedAccount { get; set; }

        public RelayCommand addAccount;
        public RelayCommand AddAccount
        {
            get
            {
                return addAccount ?? new RelayCommand(obj =>
                {
                    MoneyFy wnd = obj as MoneyFy;
                    string resultStr = "";
                    if(AccountName!=null&&AccountBalance!=null && AccountIcon != null && AccountCurrency!= null)
                    {
                        resultStr = DataWorker.CreateAccount(AccountName, AccountBalance, AccountIcon, AccountCurrency);
                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        UpdateAllAccountsView();
                        wnd.AccountsListBox.Visibility = Visibility.Visible;
                        wnd.AccountAddGrid.Visibility = Visibility.Collapsed;
                        wnd.AccountAddFrame.Content = null;
                    }
                    

                });
            }
        }

        private RelayCommand deleteAccount;
        public RelayCommand DeleteAccount
        {
            get
            {
                return deleteAccount ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    //если сотрудник
                    if (SelectedAccount != null)
                    {
                        resultStr = DataWorker.DeleteAccount(SelectedAccount);
                        UpdateAllOperationsView();
                        UpdateAllAccountsView();
                        Incomes = null;
                        Incomes = new ObservableCollection<Operation>();
                        Wastes = null;
                        Wastes = new ObservableCollection<Operation>();

                        foreach (var el in DataWorker.GetAllOperationsByUserId(DataWorker.CurrentUser.Id))
                        {
                            if (el.Sum > 0)
                                Incomes.Add(el);
                            else Wastes.Add(el);
                        }
                        foreach (var el in Wastes)
                        {
                            el.Sum *= -1;
                        }
                        UpdatePieChart();
                    }
                    //обновление
                    SetNullValuesToProperties();
                    ShowMessageToUser(resultStr);
                }
                    );
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    
                    case "OperationName":
                        if (!Regex.IsMatch(OperationName, @"[A-Z][a-z]{2,}(-[A-z][a-z]{2,})*"))
                        {
                            error = "Operation's name is not correct";
                        }
                        break;
                    
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }


        #endregion





    }
}
