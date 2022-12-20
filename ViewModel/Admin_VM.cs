using Microsoft.Win32;
using MM_MoneyFy.Model;
using MM_MoneyFy.View;
using MM_MoneyFy.View.AdminPages;
using MM_MoneyFy.View.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace MM_MoneyFy.ViewModel
{
    class Admin_VM : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private static ObservableCollection<User> allUsers = DataWorker.GetAllUsers();
        public ObservableCollection<User> AllUsers
        {
            get => allUsers;
            set
            {
                allUsers = value;
                NotifyPropertyChanged("AllUsers");
            }
        }
        

        private static User selectedUser;
        public User SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                NotifyPropertyChanged("SelectedUser");
            }
        }
        

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

        private ObservableCollection<Icon> allIcons = DataWorker.GetAllIcons();
        public ObservableCollection<Icon> AllIcons
        {
            get { return allIcons; }
            set
            {
                allIcons = value;
                NotifyPropertyChanged("AllIcons");
            }
        }

        private ObservableCollection<Account> allAccounts = DataWorker.GetAllAccounts();
        public ObservableCollection<Account> AllAccounts
        {
            get { return allAccounts; }
            set
            {
                allAccounts = value;
                NotifyPropertyChanged("AllAccounts");
            }
        }


        private ObservableCollection<Currency> currencies = DataWorker.GetAllCurrency();
        public ObservableCollection<Currency> Currencies
        {
            get { return currencies; }
            set
            {
                currencies = value;
                NotifyPropertyChanged("Currencies");
            }
        }

        private ObservableCollection<Icon> allCategoryIcons = DataWorker.GetAllCategoryIcons();
        public ObservableCollection<Icon> AllCategoryIcons
        {
            get => allCategoryIcons;
            set
            {
                allCategoryIcons = value;
                NotifyPropertyChanged("AllCategoryIcons");
            }
        }

        public static Icon CategoryIcon { get; set; }

        private static Currency selectedCurrency;
        public Currency SelectedCurrency
        {
            get => selectedCurrency;
            set
            {
                selectedCurrency = value;
                NotifyPropertyChanged("SelectedCurrency");
            }
        }

        private static Category selectedCategory;
        public Category SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                NotifyPropertyChanged("SelectedCategory");
            }
        }

        private static Icon selectedIcon;
        public Icon SelectedIcon
        {
            get => selectedIcon;
            set
            {
                selectedIcon = value;
                NotifyPropertyChanged("SelectedIcon");
            }
        }

        private static Account selectedAccount;
        public Account SelectedAccount
        {
            get => selectedAccount;
            set
            {
                selectedAccount = value;
                NotifyPropertyChanged("SelectedAccount");
            }
        }

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


        #region OPENCOMMANDS

        private RelayCommand openAddCategory;
        public RelayCommand OpenAddCategory
        {
            get
            {
                return openAddCategory ?? new RelayCommand(obj => {

                    AdminMoneyFy.AddCard.Visibility = Visibility.Visible;
                    
                    DoubleAnimation animtion2 = new DoubleAnimation()
                    {
                        From = 0,
                        To = 250,
                        Duration = TimeSpan.FromSeconds(.7),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .5,
                        BeginTime = TimeSpan.FromSeconds(0)
                    };
                    AdminMoneyFy.AddCard.BeginAnimation(ContentControl.WidthProperty, animtion2);

                });
            }
        }

        private RelayCommand openAddIcon;
        public RelayCommand OpenAddIcon
        {
            get
            {
                return openAddIcon ?? new RelayCommand(obj => {

                    AdminMoneyFy.AddCardIcon.Visibility = Visibility.Visible;

                    DoubleAnimation animtion2 = new DoubleAnimation()
                    {
                        From = 0,
                        To = 250,
                        Duration = TimeSpan.FromSeconds(.7),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .5,
                        BeginTime = TimeSpan.FromSeconds(0)
                    };
                    AdminMoneyFy.AddCardIcon.BeginAnimation(ContentControl.WidthProperty, animtion2);

                });
            }
        }


        private RelayCommand openAddCurrency;
        public RelayCommand OpenAddCurrency
        {
            get
            {
                return openAddCurrency ?? new RelayCommand(obj => {

                    AdminMoneyFy.AddCardCurrency.Visibility = Visibility.Visible;

                    DoubleAnimation animtion2 = new DoubleAnimation()
                    {
                        From = 0,
                        To = 250,
                        Duration = TimeSpan.FromSeconds(.7),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .5,
                        BeginTime = TimeSpan.FromSeconds(0)
                    };
                    AdminMoneyFy.AddCardCurrency.BeginAnimation(ContentControl.WidthProperty, animtion2);

                });
            }
        }
        public void Close(object sender, RoutedEventArgs e)// add the proper parameters
        {
            DoubleAnimation animtion2 = new DoubleAnimation()
            {
                From = 250,
                To = 0,
                Duration = TimeSpan.FromSeconds(.7),
                FillBehavior = FillBehavior.HoldEnd,
                AccelerationRatio = .5,
                BeginTime = TimeSpan.FromSeconds(0)
            };
            AdminMoneyFy.AddCard.BeginAnimation(ContentControl.WidthProperty, animtion2);
            AdminMoneyFy.AddCardCurrency.BeginAnimation(ContentControl.WidthProperty, animtion2);
            AdminMoneyFy.AddCardIcon.BeginAnimation(ContentControl.WidthProperty, animtion2);
        }
        public void Close2(object sender, RoutedEventArgs e)// add the proper parameters
        {
            DoubleAnimation animtion2 = new DoubleAnimation()
            {
                From = 250,
                To = 0,
                Duration = TimeSpan.FromSeconds(.7),
                FillBehavior = FillBehavior.HoldEnd,
                AccelerationRatio = .5,
                BeginTime = TimeSpan.FromSeconds(0)
            };
            AdminMoneyFy.AddCardCurrency.BeginAnimation(ContentControl.WidthProperty, animtion2);
        }
        public void Close3(object sender, RoutedEventArgs e)// add the proper parameters
        {
            DoubleAnimation animtion2 = new DoubleAnimation()
            {
                From = 250,
                To = 0,
                Duration = TimeSpan.FromSeconds(.7),
                FillBehavior = FillBehavior.HoldEnd,
                AccelerationRatio = .5,
                BeginTime = TimeSpan.FromSeconds(0)
            };
            AdminMoneyFy.AddCardIcon.BeginAnimation(ContentControl.WidthProperty, animtion2);
        }

        
        private RelayCommand addIcon;
        public RelayCommand AddIcon
        {
            get
            {
                return addIcon ?? new RelayCommand(obj =>
                {

                    OpenFileDialog openFileDialog1 = new OpenFileDialog();
                    openFileDialog1.ShowDialog();
                    string filename = openFileDialog1.FileName;
                    if (Regex.IsMatch(filename, $".jpg|.jfif|.jpg|.png"))
                    {

                        IconPath = filename;
                    }
                    else
                    {
                        MessageBox.Show("Неверный формат файла!");
                    }
                });
            }
        }

        private RelayCommand openEditCategory;
        public RelayCommand OpenEditCategory
        {
            get
            {
                return openEditCategory ?? new RelayCommand(obj => {

                    AdminMoneyFy.MainGrid.Visibility = Visibility.Visible;

                    AdminMoneyFy.Frame.Content = new EditCategoryPage();
                    if (SelectedCategory != null)
                    {

                        CategoryName = SelectedCategory.Name;
                        IconPath = SelectedCategory.IconPath;
                        Types = SelectedCategory.Type;
                    }
                    ThicknessAnimation animtion = new ThicknessAnimation()
                    {
                        From = new Thickness(400),
                        To = new Thickness(0),
                        Duration = TimeSpan.FromSeconds(0.5),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .5,
                        BeginTime = TimeSpan.FromSeconds(0)
                    };

                    AdminMoneyFy.BackgroundGr.BeginAnimation(ContentControl.MarginProperty, animtion);



                    DoubleAnimation animtion2 = new DoubleAnimation()
                    {
                        From = 0,
                        To = 300,
                        Duration = TimeSpan.FromSeconds(.7),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .5,
                        BeginTime = TimeSpan.FromSeconds(0.5)
                    };
                    AdminMoneyFy.PagesGrid.BeginAnimation(ContentControl.HeightProperty, animtion2);

                });
            }
        }


        private RelayCommand openEditIcon;
        public RelayCommand OpenEditIcon
        {
            get
            {
                return openEditIcon ?? new RelayCommand(obj => {

                    AdminMoneyFy.MainGrid.Visibility = Visibility.Visible;

                    AdminMoneyFy.Frame.Content = new EditIconPage();
                    if (SelectedIcon != null)
                    {

                        IconName = SelectedIcon.Name;
                        IconPath = SelectedIcon.IconPath;
                        IconType= SelectedIcon.Type;
                       
                    }
                    ThicknessAnimation animtion = new ThicknessAnimation()
                    {
                        From = new Thickness(400),
                        To = new Thickness(0),
                        Duration = TimeSpan.FromSeconds(0.5),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .5,
                        BeginTime = TimeSpan.FromSeconds(0)
                    };

                    AdminMoneyFy.BackgroundGr.BeginAnimation(ContentControl.MarginProperty, animtion);



                    DoubleAnimation animtion2 = new DoubleAnimation()
                    {
                        From = 0,
                        To = 300,
                        Duration = TimeSpan.FromSeconds(.7),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .5,
                        BeginTime = TimeSpan.FromSeconds(0.5)
                    };
                    AdminMoneyFy.PagesGrid.BeginAnimation(ContentControl.HeightProperty, animtion2);

                });
            }
        }

        private RelayCommand openEditCurrency;
        public RelayCommand OpenEditCurrency
        {
            get
            {
                return openEditCurrency ?? new RelayCommand(obj => {

                    AdminMoneyFy.MainGrid.Visibility = Visibility.Visible;

                    AdminMoneyFy.Frame.Content = new EditCurrencyPage();
                    if (SelectedCurrency != null)
                    {

                        CurrencyName = SelectedCurrency.Name;
                        
                    }
                    ThicknessAnimation animtion = new ThicknessAnimation()
                    {
                        From = new Thickness(400),
                        To = new Thickness(0),
                        Duration = TimeSpan.FromSeconds(0.5),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .5,
                        BeginTime = TimeSpan.FromSeconds(0)
                    };

                    AdminMoneyFy.BackgroundGr.BeginAnimation(ContentControl.MarginProperty, animtion);



                    DoubleAnimation animtion2 = new DoubleAnimation()
                    {
                        From = 0,
                        To = 300,
                        Duration = TimeSpan.FromSeconds(.7),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .5,
                        BeginTime = TimeSpan.FromSeconds(0.5)
                    };
                    AdminMoneyFy.PagesGrid.BeginAnimation(ContentControl.HeightProperty, animtion2);

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
                      

                      AdminMoneyFy.Frame.Content =null;
                      ThicknessAnimation animtion = new ThicknessAnimation()
                      {
                          From = new Thickness(0),
                          To = new Thickness(400),
                          Duration = TimeSpan.FromSeconds(0.5),
                          FillBehavior = FillBehavior.HoldEnd,
                          AccelerationRatio = .5,
                          BeginTime = TimeSpan.FromSeconds(0)
                      };

                      AdminMoneyFy.BackgroundGr.BeginAnimation(ContentControl.MarginProperty, animtion);



                      DoubleAnimation animtion2 = new DoubleAnimation()
                      {
                          From = 300,
                          To = 0,
                          Duration = TimeSpan.FromSeconds(.7),
                          FillBehavior = FillBehavior.HoldEnd,
                          AccelerationRatio = .5,
                          BeginTime = TimeSpan.FromSeconds(0.5)
                      };
                      AdminMoneyFy.PagesGrid.BeginAnimation(ContentControl.HeightProperty, animtion2);
                      AdminMoneyFy.MainGrid.Visibility = Visibility.Collapsed;
                      SetNullValuesToProperties();
                  });
            }
        }
        #endregion



        #region DELETE
        private RelayCommand deleteUser;
        public RelayCommand DeleteUser
        {
            get
            {
                return deleteUser ?? new RelayCommand(obj => {

                    string resultStr = "Пользователь не выбран";
                    if (SelectedUser != null)
                    {

                        //если сотрудник

                        resultStr = DataWorker.DeleteUser(SelectedUser);
                        Update();
                        SetNullValuesToProperties();
                    }

                    ShowMessageToUser(resultStr);
                });
            }
        }




        private RelayCommand deleteCategory;
        public RelayCommand DeleteCategory
        {
            get
            {
                return deleteCategory ?? new RelayCommand(obj => {

                    string resultStr = "Категория не выбрана";
                    if (SelectedCategory != null)
                    {

                        resultStr = DataWorker.DeleteCategory(SelectedCategory);
                        Update();
                        SetNullValuesToProperties();
                    }

                    ShowMessageToUser(resultStr);
                });
            }
        }


        private RelayCommand deleteIcon;
        public RelayCommand DeleteIcon
        {
            get
            {
                return deleteIcon ?? new RelayCommand(obj =>
                {

                    string resultStr = "Иконка не выбрана";
                    if (SelectedIcon != null)
                    {

                        resultStr = DataWorker.DeleteIcon(SelectedIcon);
                        Update();
                        SetNullValuesToProperties();
                    }

                    ShowMessageToUser(resultStr);
                });
            }
        }

        private RelayCommand deleteCurrency;
        public RelayCommand DeleteCurrency
        {
            get
            {
                return deleteCurrency ?? new RelayCommand(obj => {

                    string resultStr = "Валюта не выбрана";
                    if (SelectedCurrency != null)
                    {

                        resultStr = DataWorker.DeleteCurrency(SelectedCurrency);
                        Update();
                        SetNullValuesToProperties();
                    }

                    ShowMessageToUser(resultStr);
                });
            }
        }

        #endregion



        #region ADDCATEGORY
        private static string categoryName;
        public string CategoryName
        {
            get => categoryName;
            set
            {
                categoryName = value;
                NotifyPropertyChanged("CategoryName");
            }
        }
        private static string iconPath;
        public string IconPath
        {
            get => iconPath;
            set
            {
                iconPath = value;
                NotifyPropertyChanged("IconPath");
            }
        }

        private static bool types;
        public bool Types
        {
            get => types;
            set
            {
                types = value;
                NotifyPropertyChanged("Types");
            }
        }

        private RelayCommand addCategory;
        public RelayCommand AddCategory
        {
            get
            {
                return addCategory ?? new RelayCommand(obj =>
                {
                    if (CategoryName != null)
                    {
                        if (CategoryIcon != null)
                        {
                            if (Types != null)
                            {
                               string result= DataWorker.CreateCategory(CategoryName,Types, CategoryIcon);
                                Update();
                                SetNullValuesToProperties();
                                ShowMessageToUser(result);
                            }
                            else ShowMessageToUser("Не указан тип");
                        }
                        else ShowMessageToUser("Не выбрана иконка");
                    }
                    else ShowMessageToUser("Не указано имя");
                });
            }
        }

        private RelayCommand edit;
        public RelayCommand Edit
        {
            get
            {
                return edit ?? new RelayCommand(obj =>
                {
                    if (SelectedCategory != null)
                    {
                        if (CategoryName != null)
                        {
                            if (IconPath != null)
                            {
                                if (Types != null)
                                {
                                    string result = DataWorker.EditCategory(SelectedCategory, CategoryName,Types, CategoryIcon);
                                    Update();
                                    SetNullValuesToProperties();
                                    ShowMessageToUser(result);
                                }
                                else ShowMessageToUser("Не указан тип");
                            }
                            else ShowMessageToUser("Не выбрана иконка");
                        }
                        else ShowMessageToUser("Не указано имя");
                    }
                    else if (SelectedIcon != null)
                    {
                        if (IconName != null)
                        {
                            if (IconPath != null)
                            {
                                if(IconType != null)
                                {
                                    string result = DataWorker.EditIcon(SelectedIcon, IconName, IconPath,IconType);   /////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                                    Update();
                                    SetNullValuesToProperties();
                                    ShowMessageToUser(result);
                                }
                                else ShowMessageToUser("Не указан тип");
                            }
                            else ShowMessageToUser("Не выбрана иконка");
                        }
                        else ShowMessageToUser("Не указано имя");
                    }
                    else if (SelectedCurrency != null)
                    {
                        if (CurrencyName != null)
                        {
                            
                                string result = DataWorker.EditCurrency(SelectedCurrency, CurrencyName);
                                Update();
                                SetNullValuesToProperties();
                                ShowMessageToUser(result);

                            
                        }
                        else ShowMessageToUser("Не указано имя");
                    }
                });
            }
        }
        #endregion

        #region AddIcon
        private static string iconName;
        public string IconName
        {
            get => iconName;
            set
            {
                iconName = value;
                NotifyPropertyChanged("IconName");
            }
        }

        private static string iconType;
        public string IconType
        {
            get => iconType;
            set
            {
                iconType = value;
                NotifyPropertyChanged("IconType");
            }
        }

        private RelayCommand addBankIcon;
        public RelayCommand AddBankIcon
        {
            get
            {
                return addBankIcon ?? new RelayCommand(obj =>
                {
                    if (IconName != null)
                    {
                        if (IconPath != null)
                        {
                            if(IconType != null)
                            {
                                string result = DataWorker.CreateIcon(IconName, IconPath, IconType);
                                Update();
                                SetNullValuesToProperties();
                                ShowMessageToUser(result);
                            }
                            else ShowMessageToUser("Не указан тип");
                        }
                        else ShowMessageToUser("Не выбрана иконка");
                    }
                    else ShowMessageToUser("Не указано имя");
                });
            }
        }
        #endregion

        #region ADDCURRENCY
        private static string currencyName;
        public string CurrencyName
        {
            get => currencyName;
            set
            {
                currencyName = value;
                NotifyPropertyChanged("CurrencyName");
            }
        }

       

        private RelayCommand addCurrency;
        public RelayCommand AddCurrency
        {
            get
            {
                return addCurrency ?? new RelayCommand(obj =>
                {
                    if (CurrencyName != null)
                    {
                        
                            string result = DataWorker.CreateCurrency(CurrencyName);
                            Update();
                            SetNullValuesToProperties();
                            ShowMessageToUser(result);

                      
                    }
                    else ShowMessageToUser("Не указано имя");
                });
            }
        }

        #endregion



        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {

                    case "CategoryName":
                        if (!Regex.IsMatch(CategoryName, @"(([A-Z][a-z]{2,}(-[A-z][a-z]{2,})*)|([А-Я][а-я]{2,}(-[А-я][а-я]{2,})*))"))
                        {
                            error = "Categoy's name is not correct";
                        }
                        break;
                    case "Types":
                        if (!Regex.IsMatch(Types.ToString(), @"((true)|(false)|(True)|(False))"))
                        {
                            error = "Category's type is not correct";
                        }
                        break;

                }
                return error;
            }
        }

        private void SetNullValuesToProperties()
        {
            SelectedAccount = null;
            SelectedCategory = null;
            SelectedUser = null;
            SelectedIcon = null;
            SelectedCurrency = null;
          

            //Categories
            CategoryName = null;
            IconPath = null;
            Types = false;

            //Icons
            IconName = null;
            IconType = null;

            //Currencies
            CurrencyName = null;
            
        }

        private void ShowMessageToUser(string message)
        {
            MessageWindow messageView = new MessageWindow(message);
            messageView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            messageView.ShowDialog();
        }

        private void UpdateUsers()
        {
            AllUsers = DataWorker.GetAllUsers();
            AdminMoneyFy.Users.ItemsSource = null;
            AdminMoneyFy.Users.Items.Clear();
            AdminMoneyFy.Users.ItemsSource = AllUsers;
            AdminMoneyFy.Users.Items.Refresh();
        }

        


        private void UpdateIncomes()
        {
            AllIncomesCategories = DataWorker.GetAllCategories(true);
            AdminMoneyFy.Incomes.ItemsSource = null;
            AdminMoneyFy.Incomes.Items.Clear();
            AdminMoneyFy.Incomes.ItemsSource = AllIncomesCategories;
            AdminMoneyFy.Incomes.Items.Refresh();
        }

        private void UpdateWastes()
        {
            AllWastesCategories = DataWorker.GetAllCategories(false);
            AdminMoneyFy.Wastes.ItemsSource = null;
            AdminMoneyFy.Wastes.Items.Clear();
            AdminMoneyFy.Wastes.ItemsSource = AllWastesCategories;
            AdminMoneyFy.Wastes.Items.Refresh();
        }

        private void UpdateIcons()
        {
            AllIcons = DataWorker.GetAllIcons();
            AdminMoneyFy.Icons.ItemsSource = null;
            AdminMoneyFy.Icons.Items.Clear();
            AdminMoneyFy.Icons.ItemsSource = AllIcons;
            AdminMoneyFy.Icons.Items.Refresh();
            AllCategoryIcons = DataWorker.GetAllCategoryIcons();
            AdminMoneyFy.CategoryIcons.ItemsSource = null;
            AdminMoneyFy.CategoryIcons.Items.Clear();
            AdminMoneyFy.CategoryIcons.ItemsSource = AllCategoryIcons;
            AdminMoneyFy.CategoryIcons.Items.Refresh();
        }

        private void UpdateCurrency()
        {
            Currencies = DataWorker.GetAllCurrency();
            AdminMoneyFy.Currency.ItemsSource = null;
            AdminMoneyFy.Currency.Items.Clear();
            AdminMoneyFy.Currency.ItemsSource = Currencies;
            AdminMoneyFy.Currency.Items.Refresh();
        }

        private void Update()
        {
            UpdateCurrency();
            UpdateIcons();
            UpdateWastes();
            UpdateIncomes();
            UpdateUsers();
        }
    }
}
