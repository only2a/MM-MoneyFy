using MM_MoneyFy.Model;
using MM_MoneyFy.View;
using MM_MoneyFy.View.RecoveryPasswordPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MM_MoneyFy.ViewModel
{
    class AuthorizationVM : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

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

        #region OpenAndCloseWindows
        //Открыть окно регистрации пользователя
        private void OpenRegistrationWindow()
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            SetCenterPositionandOpen(registrationWindow);
        }

        


        private void SetCenterPositionandOpen(Window window)
        {
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
        }

        private  RelayCommand openRegistrationWnd;
        public RelayCommand OpenRegistrationWnd
        {
            get
            {
                return openRegistrationWnd ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    OpenRegistrationWindow();
                    window.Close();
                });
            }
        }



        private RelayCommand closeWnd;
        public RelayCommand CloseWnd
        {
            get
            {
                return closeWnd ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;

                    if (window != null)
                    {
                        window.Close();
                    }


                }
                );
            }
        }
        #endregion


        #region USERAUTHORIZATION
        //User verification
        
        private static string userEmail;
        public string UserEmail
        {
            get => userEmail;
            set
            {
                userEmail = value;
                NotifyPropertyChanged("UserEmail");
            }
        }

        private static string userPassword;
        public string UserPassword
        {
            get => userPassword;
            set
            {
                userPassword = value;
                NotifyPropertyChanged("UserPassword");
            }
        }

        private RelayCommand userVerification;
        public RelayCommand UserVerification
        {
            get
            {
                return userVerification ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    var Users = DataWorker.AllUsers();
                    
                    if (Users != null && UserEmail.Length!=0 && UserPassword.Length!=0)
                    {

                        var resUser = Users.Where(x => x.Login == UserEmail && x.Password == DataWorker.GetHash(UserPassword)).SingleOrDefault();
                        if (resUser != null)
                        {
                            DataWorker.CurrentUser = resUser;
                            if (resUser.IsAdmin)
                            {
                                AdminMoneyFy adminMoneyFy = new AdminMoneyFy();
                                adminMoneyFy.Show();
                                window.Close();
                                ShowMessageToUser($"Здравствуйте, {resUser.Name}");
                            }
                            else
                            {
                                MoneyFy moneyFy = new MoneyFy();
                                moneyFy.Show();
                                window.Close();
                                ShowMessageToUser($"Здравствуйте, {resUser.Name}");
                            }
                            
                        }
                        

                    }
                    
                });
            }
        }
        #endregion

        #region UserRegistration

        //User registration
        public string UserRegName { get; set; }
        public string UserRegEmail { get; set; }
        public string UserRegPassword { get; set; }

        private RelayCommand userReg;
        public RelayCommand UserReg
        {
            get
            {
                return userReg ?? new RelayCommand(obj=> 
                {
                    RegistrationWindow wnd = obj as RegistrationWindow;
                    string resultStr = "";
                    if (UserRegName == null || UserRegName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                    }
                    else if(UserRegEmail == null || UserRegEmail.Replace(" ", "").Length == 0) SetRedBlockControll(wnd, "EmailBlock");
                    else if(UserRegPassword == null || UserRegPassword.Replace(" ", "").Length == 0) SetRedBlockControll(wnd, "PasswordBlock");
                    else
                    {
                        if (Regex.IsMatch(UserRegPassword, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{1,16}$"))
                        {
                            
                            resultStr = DataWorker.CreateUser(UserRegName, UserRegEmail, UserRegPassword);


                            ShowMessageToUser(resultStr);
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            wnd.Close();
                        }
                        else 
                        {
                            
                            ShowMessageToUser("Пароль слишком лёгкий. В пароле должна быть минимум одна цифра, одна буква(английская), большая буква и любой знак. Максимальная длина пароль 16 символов."); 
                        }
                    }
                });
            }
        }
        #endregion


        #region RECOVERYPASSWORD

        public void OpenRecPasswGrid(object sender, RoutedEventArgs e)// add the proper parameters
        {
            MainWindow.RecGrid.Visibility = Visibility.Visible;
            MainWindow.RecFrame.Content = new EnterEmail();
            ThicknessAnimation animtion = new ThicknessAnimation()
            {
                From = new Thickness(400),
                To = new Thickness(0),
                Duration = TimeSpan.FromSeconds(0.5),
                FillBehavior = FillBehavior.HoldEnd,
                AccelerationRatio = .5,
                BeginTime = TimeSpan.FromSeconds(0)
            };

            MainWindow.RecBackgroundGrid.BeginAnimation(ContentControl.MarginProperty, animtion);



            DoubleAnimation animtion2 = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1),
                FillBehavior = FillBehavior.HoldEnd,
                AccelerationRatio = .5,
                BeginTime = TimeSpan.FromSeconds(0.5)
            };
            MainWindow.RecGridWithFrame.BeginAnimation(ContentControl.OpacityProperty, animtion2);
        }


        private RelayCommand closeRecGrid;
        public RelayCommand CloseRecGrid
        {
            get
            {
                return closeRecGrid ?? new RelayCommand(obj =>
                {
                    MainWindow wnd = obj as MainWindow;
                    wnd.RecoveryGridView.Visibility = Visibility.Collapsed;
                    DoubleAnimation animtion2 = new DoubleAnimation()
                    {
                        From = 1,
                        To = 0,
                        Duration = TimeSpan.FromSeconds(0.1),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .5,
                        BeginTime = TimeSpan.FromSeconds(0)
                    };
                    wnd.RecoveryGridWithFrame.BeginAnimation(ContentControl.OpacityProperty, animtion2);
                    wnd.RecoveryFrameView.Content = null;
                });
            }
        }

        private RelayCommand getCode;
        public RelayCommand GetCode
        {
            get
            {
                return getCode ?? new RelayCommand(obj =>
                 {
                     MainWindow wnd = obj as MainWindow;

                     if (toAdress == null || toAdress.Replace(" ","").Length==0)
                     {
                         ShowMessageToUser("Введите Email");
                     }
                     else if(!Regex.IsMatch(toAdress, @"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)"))
                     {
                         ShowMessageToUser("Вы ввели некорректный Email");
                     }
                     else
                     {
                         if (DataWorker.UserExist(toAdress))
                         {
                             Random rnd = new Random();
                             Code = rnd.Next(1000, 10000).ToString();
                             MailAddress fromadress = new MailAddress("r3m.a@mail.ru", "Sender");
                             MailAddress toadress = new MailAddress(toAdress, "Dima");
                             MailMessage message = new MailMessage(fromadress, toadress);
                             message.Body = "Введите этот код для восстановления пароля:" + Code;
                             message.Subject = "Восстановление пароля";

                             SmtpClient smtpClient = new SmtpClient();
                             smtpClient.Host = "smtp.mail.ru";
                             smtpClient.Port = 465;
                             smtpClient.EnableSsl = true;
                             smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                             smtpClient.UseDefaultCredentials = false;
                             smtpClient.Credentials = new NetworkCredential(fromadress.Address, "YGU0d8Aa8bwBNjK4A5fi");

                             smtpClient.Send(message);
                             wnd.RecoveryFrameView.Content = null;
                             wnd.RecoveryFrameView.Content = new EnterSentCode();
                             wnd.Btn1.Visibility = Visibility.Collapsed;
                             wnd.Btn2.Visibility = Visibility.Visible;
                         }
                         else
                         {
                             ShowMessageToUser("Пользователя с таким Email не существует");
                         }
                         
                     }
                 });
            }
        }

        private RelayCommand checkCode;
        public RelayCommand CheckCode
        {
            get
            {
                return checkCode ?? new RelayCommand(obj =>
                {
                    MainWindow wnd = obj as MainWindow;


                    if (UserCode == null || UserCode.Replace(" ", "").Length == 0)
                    {
                        ShowMessageToUser("Введите полученный код");
                    }
                    else
                    {
                        if (UserCode == Code)
                        {
                            wnd.RecoveryFrameView.Content = null;
                            wnd.RecoveryFrameView.Content = new RecPasswordChange();
                            wnd.Btn2.Visibility = Visibility.Collapsed;
                            wnd.Btn3.Visibility = Visibility.Visible;
                        }
                        else ShowMessageToUser("Вы ввели неверный код");
                    }
                    
                        
                    
                });
            }
        }

        private RelayCommand passwordChange;
        public RelayCommand PasswordChange
        {
            get => passwordChange ?? new RelayCommand(obj =>
            {
                string result = "Заполните все поля!";
                MainWindow wnd = obj as MainWindow;
                if (RecNewPassword == null || RecNewPassword.Replace(" ", "").Length == 0) ShowMessageToUser("Введите новый пароль!");
                else if (RecRepeatNewPassword != RecRepeatNewPassword) ShowMessageToUser("Пароли не совпадают!");
                else
                {
                    result = DataWorker.ChangeUserPassword(RecNewPassword, toAdress);
                    wnd.RecoveryGridView.Visibility = Visibility.Collapsed;
                    DoubleAnimation animtion2 = new DoubleAnimation()
                    {
                        From = 1,
                        To = 0,
                        Duration = TimeSpan.FromSeconds(1),
                        FillBehavior = FillBehavior.HoldEnd,
                        AccelerationRatio = .5,
                        BeginTime = TimeSpan.FromSeconds(0.5)
                    };
                    wnd.RecoveryGridWithFrame.BeginAnimation(ContentControl.OpacityProperty, animtion2);
                    wnd.RecoveryFrameView.Content = null;
                    result = "Сделано!";
                }
                ShowMessageToUser(result);
            });
        }

        public static string toAdress {get;set;}

        private static string Code { get; set; }
        public string UserCode { get; set; }
        public static string RecNewPassword { get; set; }
        public static string RecRepeatNewPassword { get; set; }






        #endregion

        private RelayCommand goback;
        private RelayCommand goback2;
        public RelayCommand GoBack
        {
            get => goback ?? new RelayCommand(obj =>
              {
                  RegistrationWindow window = obj as RegistrationWindow;
                  MainWindow mainWindow = new MainWindow();
                  mainWindow.Show();
                  window.Close();
              });
        }

   






        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "UserEmail":
                        if (!Regex.IsMatch(UserEmail, @"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)"))
                        {
                            error = "Email is not correct";
                        }
                        break;
                    case "UserRegName":
                        if (!Regex.IsMatch(UserRegName, @"[A-Z][a-z]{2,}(-[A-z][a-z]{2,})*"))
                        {
                            error = "User's name is not correct";
                        }
                        break;
                    case "UserRegEmail":
                        if (!Regex.IsMatch(UserRegEmail, @"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)"))
                        {
                            error = "Email is not correct";
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

    }
}
