using Microsoft.Win32;
using MM_MoneyFy.Model;
using MM_MoneyFy.View;
using MM_MoneyFy.View.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    class UserSettings_VM : INotifyPropertyChanged
    {

        


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string userName = DataWorker.CurrentUser.Name;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                NotifyPropertyChanged("UserName");
            }
        }

        private string userEmail = DataWorker.CurrentUser.Login;
        public string UserEmail
        {
            get => userEmail;
            set
            {
                userEmail = value;
                NotifyPropertyChanged("UserEmail");
            }
        }

        public string Message { get; set; }



        #region CATEGORIES
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




        private List<Operation> incOperationsCurrentCategory;
        public List<Operation> IncOperationsCurrentCategory
        {
            get
            {
                return incOperationsCurrentCategory;
            }
            set
            {
                incOperationsCurrentCategory = value;
                NotifyPropertyChanged("OperationsCurrentCategory");
            }
        }


        private List<Operation> wstOperationsCurrentCategory;
        public List<Operation> WstOperationsCurrentCategory
        {
            get
            {
                return wstOperationsCurrentCategory;
            }
            set
            {
                wstOperationsCurrentCategory = value;
                NotifyPropertyChanged("OperationsCurrentCategory");
            }
        }


        private RelayCommand openCreateIncome;
        public RelayCommand OpenCreateIncome
        {
            get
            {
                return openCreateIncome ?? new RelayCommand(obj =>
                {

                    UserSettingsView wnd = obj as UserSettingsView;
                    wnd.CreateIncomeCtgrBlock.Visibility = Visibility.Visible;

                    DoubleAnimation animtion2 = new DoubleAnimation()
                    {
                        From = 0,
                        To = 800,
                        Duration = TimeSpan.FromSeconds(1),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .5,
                        BeginTime = TimeSpan.FromSeconds(0)
                    };
                    wnd.CreateIncomeCtgrBlock.BeginAnimation(ContentControl.WidthProperty, animtion2);
                });
            }
        }


        private RelayCommand openCreateWaste;
        public RelayCommand OpenCreateWaste
        {
            get
            {
                return openCreateWaste ?? new RelayCommand(obj =>
                {

                    UserSettingsView wnd = obj as UserSettingsView;
                    wnd.CreateWasteCtgrBlock.Visibility = Visibility.Visible;

                    DoubleAnimation animtion2 = new DoubleAnimation()
                    {
                        From = 0,
                        To = 800,
                        Duration = TimeSpan.FromSeconds(1),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .5,
                        BeginTime = TimeSpan.FromSeconds(0)
                    };
                    wnd.CreateWasteCtgrBlock.BeginAnimation(ContentControl.WidthProperty, animtion2);
                });
            }
        }

        private RelayCommand back;
        public RelayCommand Back
        {
            get
            {
                return back ?? new RelayCommand(obj =>
                {

                    UserSettingsView wnd = obj as UserSettingsView;
                    wnd.CreateIncomeCtgrBlock.Visibility = Visibility.Collapsed;
                    wnd.CreateWasteCtgrBlock.Visibility = Visibility.Collapsed;
                    wnd.OperationGB.Visibility = Visibility.Collapsed;
                    wnd.OperationGBWst.Visibility = Visibility.Collapsed;
                    SelectedCategory = null;
                    wnd.NameBlockIncCategory.Text = string.Empty;
                    wnd.NameBlockWstCategory.Text = string.Empty;
                });
            }
        }



        private RelayCommand saveIncome;
        public RelayCommand SaveIncome
        {
            get
            {
                return saveIncome ?? new RelayCommand(obj =>
                {
                    UserSettingsView page = obj as UserSettingsView;
                    if (CategoryName != null || CategoryName.Replace(" ", "").Length != 0)
                    {
                        Icon icon = new Icon();
                        DataWorker.CreateCategory(CategoryName, true, icon);
                        UpdateGrid();
                    }
                });
            }
        }

        private RelayCommand saveWaste;
        public RelayCommand SaveWaste
        {
            get
            {
                return saveWaste ?? new RelayCommand(obj =>
                {
                    UserSettingsView page = obj as UserSettingsView;
                    if (CategoryName != null || CategoryName.Replace(" ", "").Length != 0)
                    {
                        Icon icon = new Icon();
                        DataWorker.CreateCategory(CategoryName, false, icon);
                        UpdateGrid();
                    }
                });
            }
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




        private RelayCommand deleteCategory;
        public RelayCommand DeleteCategory
        {
            get
            {
                return deleteCategory ?? new RelayCommand(obj =>
                {

                    UserSettingsView wnd = obj as UserSettingsView;
                    string resultstr = "Ничего не выбрано";
                    if (SelectedCategory != null)
                    {
                        resultstr = DataWorker.DeleteCategory(SelectedCategory);
                        UserSettingsView.TextBox.Text = String.Empty;
                        UserSettingsView.Icon.Source = null;
                        UserSettingsView.TextBox2.Text = String.Empty;
                        UserSettingsView.Icon2.Source = null;
                        Update();
                        UpdateGrid();

                    }
                    ShowMessageToUser(resultstr);

                });
            }
        }

        public void OperationsOfthisCategory(object sender, RoutedEventArgs e)// add the proper parameters
        {
            Update();
        }

        public void SendEmail(object sender, RoutedEventArgs e)// add the proper parameters
        {
            MailAddress fromadress = new MailAddress("makatdy@gmail.com", "Makar");
            MailAddress toadress = new MailAddress("pilipovich.2000@gmail.com", "Bbb");
            MailMessage message = new MailMessage(fromadress, toadress);
            message.Body = Message;
            message.Subject = "Hello";

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(fromadress.Address, "20Makar03");

            smtpClient.Send(message);
            ShowMessageToUser("Сделано");
        }
        private void UpdateGrid()
        {
            AllIncomesCategories = DataWorker.GetAllCategories(true);
            AllWastesCategories = DataWorker.GetAllCategories(false);
            UserSettingsView.IncomeCategories.ItemsSource = null;
            UserSettingsView.IncomeCategories.Items.Clear();
            UserSettingsView.IncomeCategories.ItemsSource = AllIncomesCategories;
            UserSettingsView.IncomeCategories.Items.Refresh();
            UserSettingsView.WasteCategories.ItemsSource = null;
            UserSettingsView.WasteCategories.Items.Clear();
            UserSettingsView.WasteCategories.ItemsSource = AllWastesCategories;
            UserSettingsView.WasteCategories.Items.Refresh();
        }

        private void Update()
        {
            if (SelectedCategory != null && SelectedCategory.Type == true)
            {
                UserSettingsView.TextBox.Text = SelectedCategory.Name;
                UserSettingsView.Icon.Source = new BitmapImage(new Uri(SelectedCategory.IconPath));
                UserSettingsView.GroupBoxOperation.Visibility = Visibility.Visible;
                //income
                IncOperationsCurrentCategory = DataWorker.GetOperationsOfSelCategory(SelectedCategory.Id);
                UserSettingsView.IncOperationOfCurrentCategory.ItemsSource = null;
                UserSettingsView.IncOperationOfCurrentCategory.Items.Clear();
                UserSettingsView.IncOperationOfCurrentCategory.ItemsSource = IncOperationsCurrentCategory;
                UserSettingsView.IncOperationOfCurrentCategory.Items.Refresh();


            }
            else if (SelectedCategory != null && SelectedCategory.Type == false)
            {
                UserSettingsView.TextBox2.Text = SelectedCategory.Name;
                UserSettingsView.Icon2.Source = new BitmapImage(new Uri(SelectedCategory.IconPath));
                UserSettingsView.GroupBoxOperationWst.Visibility = Visibility.Visible;
                //waste
                WstOperationsCurrentCategory = DataWorker.GetOperationsOfSelCategory(SelectedCategory.Id);
                UserSettingsView.WstOperationOfCurrentCategory.ItemsSource = null;
                UserSettingsView.WstOperationOfCurrentCategory.Items.Clear();
                UserSettingsView.WstOperationOfCurrentCategory.ItemsSource = WstOperationsCurrentCategory;
                UserSettingsView.WstOperationOfCurrentCategory.Items.Refresh();
            }
            else
            {
                UserSettingsView.TextBox.Text = String.Empty;
                UserSettingsView.Icon.Source = null;
            }

        }

        private void ShowMessageToUser(string message)
        {
            MessageWindow messageView = new MessageWindow(message);
            messageView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            messageView.ShowDialog();
        }

        public static Category SelectedCategory { get; set; }

        public static string IconPath { get; set; }
        public static string CategoryName { get; set; }

        private static List<string> lang;
        public  List<string> Languages
        {
            get => lang;
            set
            {
                lang = value;
                NotifyPropertyChanged("Languages");
            }
        }

        #endregion

        #region CHANGECOLOR
        public void Yellow()
        {
            var Uri = new Uri("Themes/Yellow.xaml", UriKind.Relative);
            ResourceDictionary resource = Application.LoadComponent(Uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resource);
        }
        public void Deeporange()
        {
            var Uri = new Uri("Themes/DeepOrange.xaml", UriKind.Relative);
            ResourceDictionary resource = Application.LoadComponent(Uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resource);
        }
        public void Deepskyblue()
        {
            var Uri = new Uri("Themes/DeepSkyBlue.xaml", UriKind.Relative);
            ResourceDictionary resource = Application.LoadComponent(Uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resource);
        }
        public void Purple()
        {
            var Uri = new Uri("Themes/PurpleLight.xaml", UriKind.Relative);
            ResourceDictionary resource = Application.LoadComponent(Uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resource);
        }
        #endregion


        #region PASSWORDCHANGE

        public string OldPassword{get;set;}
        public string NewPassword { get; set; }
        public string RepeatNewPassword { get; set; }
        public static string NewName { get; set; }

        private void SetRedBlockControll(Page wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        private RelayCommand passwordChange;
        public RelayCommand PasswordChange
        {
            get => passwordChange ?? new RelayCommand(obj =>
                {
                    string result = "Заполните все поля!";
                    UserSettingsView settingsView = obj as UserSettingsView;
                    if (OldPassword == null || OldPassword.Replace(" ", "").Length == 0) SetRedBlockControll(settingsView, "PB1");
                    else if(RepeatNewPassword!=NewPassword) SetRedBlockControll(settingsView, "PB3");
                    else
                    {
                        result = DataWorker.ChangeUserPassword(OldPassword, NewPassword, DataWorker.CurrentUser.Login);
                    }
                    ShowMessageToUser(result);
                });
        }


        private RelayCommand nameChange;
        public RelayCommand NameChange
        {
            get => nameChange ?? new RelayCommand(obj =>
            {
                string result = "Заполните все поля!";
                UserSettingsView settingsView = obj as UserSettingsView;
                if (NewName == null || NewName.Replace(" ", "").Length == 0) SetRedBlockControll(settingsView, "TB1");
                else
                {
                    result = DataWorker.ChangeUserName(NewName, DataWorker.CurrentUser.Login);
                    UserName = NewName;
                }
                ShowMessageToUser(result);
            });
        }
        #endregion


    }
}
