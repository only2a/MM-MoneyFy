using LiveCharts;
using Microsoft.Data.SqlClient;
using MM_MoneyFy.Model.Data;
using MM_MoneyFy.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Xml.Linq;

namespace MM_MoneyFy.Model
{
    public static class DataWorker
    {
        public static string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash);
        }

        // CurrentUser
        public static User CurrentUser { get; set; }
        public static ChartValues<decimal> Incomes { get; set; }
        public static ChartValues<decimal> Wastes { get; set; }



        //1) Get all Users 
        public static ObservableCollection<User> AllUsers()
        {
            string proc = "GetAllUsers";
            List<User> list = new List<User>();
           
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= Unauthuser;Password=Unauthuser"))
            {

                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(proc, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var result = command.ExecuteReader();

                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            User user = new User();
                            user.Id = (int)result.GetValue(0);
                            user.IsAdmin = (bool)result.GetValue(1);
                            user.Name = (string)result.GetValue(2);
                            user.Password = (string)result.GetValue(3);
                            user.Login = (string)result.GetValue(4);
                            list.Add(user);
                        }
                    }


                    
                }
                catch (SqlException error)
                {
                    System.Windows.MessageBox.Show(error.Message);
                }
                var res = new ObservableCollection<User>(list);
                return res;
            }
        }

        //1) Get all Users 
        public static ObservableCollection<User> GetAllUsers() // DONE
        {

            string proc = "GetUsers";
            List<User> list = new List<User>();

            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= Admin;Password=Admin"))
            {

                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(proc, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var result = command.ExecuteReader();

                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            User user = new User();
                            user.Id = (int)result.GetValue(0);
                            user.IsAdmin = (bool)result.GetValue(1);
                            user.Name = (string)result.GetValue(2);
                            user.Password = (string)result.GetValue(3);
                            user.Login = (string)result.GetValue(4);
                            list.Add(user);
                        }
                    }



                }
                catch (SqlException error)
                {
                    System.Windows.MessageBox.Show(error.Message);
                }
                var res = new ObservableCollection<User>(list);
                return res;
            }
        }

        public static ObservableCollection<Account> GetAllAccounts() // DONE
        {
            string proc = "GetAllAccounts";
            List<Account> list = new List<Account>();
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= User;Password=User"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(proc, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var result = command.ExecuteReader();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        Account account = new Account();
                        account.Id = (int)result.GetValue(0);
                        account.Name = (string)result.GetValue(1);
                        account.Balance = (decimal)result.GetValue(2);
                        account.iconId = (int)result.GetValue(3);
                        account.userID = (int)result.GetValue(4);
                        account.CurrencyId = (int)result.GetValue(5);
                        list.Add(account);
                    }
                }


                var res = new ObservableCollection<Account>(list);
                return res;
            }
        }

      

        // 5) Get all Operations
        public static List<Operation> GetAllOperations() // DONE 
        {
            string proc = "GetAllOperations";
            List<Operation> list = new List<Operation>();
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= User;Password=User"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(proc, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var result = command.ExecuteReader();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        //GetAllAccountsByUserId;
                        Operation operation = new Operation();
                        operation.Id = (int)result.GetValue(0);
                        operation.CategoryId = (int)result.GetValue(1);
                        operation.AccountId = (int)result.GetValue(2);
                        operation.Name = (string)result.GetValue(3);
                        operation.date = (DateTime)result.GetValue(4);
                        operation.Sum = (decimal)result.GetValue(5);
                        list.Add(operation);
                    }
                }
                return list;
            }
        }

        public static List<Category> GetAllCategories(bool type) // DONE
        {
            
                List<Category> categories = GetAllCategories().Where(x=>x.Type==type).ToList();
                return categories;


        }
        public static List<Category> GetAllCategories() // DONE
        {
            string proc = "GetAllCategories";
            List<Category> list = new List<Category>();
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= User;Password=User"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(proc, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var result = command.ExecuteReader();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        //GetAllAccountsByUserId;
                        Category category = new Category();
                        category.Id = (int)result.GetValue(0);
                        category.iconId = (int)result.GetValue(1);
                        category.Name = (string)result.GetValue(2);
                        category.Type = (bool)result.GetValue(3);
                        list.Add(category);
                    }
                }
                return list;
            }
        }

        // 7) Get all Icons
        public static ObservableCollection<Icon> GetAllIcons()
        {

            string proc = "GetAllIcons";
            List<Icon> list = new List<Icon>();
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= User;Password=User"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(proc, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var result = command.ExecuteReader();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        
                        Icon icon = new Icon();
                        icon.Id = (int)result.GetValue(0);
                        icon.Name = (string)result.GetValue(1);
                        icon.IconPath = (string)result.GetValue(2);
                        icon.Type = (string)result.GetValue(3);
                        list.Add(icon);
                    }
                }
                return new ObservableCollection<Icon>(list);
            }

        }
        // 8) Get all Currency
        public static ObservableCollection<Currency> GetAllCurrency()
        {

            string proc = "GetAllCurrencies";
            List<Currency> list = new List<Currency>();
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= User;Password=User"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(proc, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var result = command.ExecuteReader();

                if (result.HasRows)
                {
                    while (result.Read())
                    {

                        Currency currency = new Currency();
                        currency.Id = (int)result.GetValue(0);
                        currency.Name = (string)result.GetValue(1);
                        list.Add(currency);
                    }
                }



                return new ObservableCollection<Currency>(list);
            }
        }

        public static bool UserExist(string email)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Users.Any(el => el.Login == email);
                return checkIsExist;
            }
        }

        public static string CreateUser(string name,string email,string password) // DONE
        {
            string result = "Уже существует";
            string proc = "CreateUser";
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= Unauthuser;Password=Unauthuser"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(proc, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Name", name));
                command.Parameters.Add(new SqlParameter("@User_Login", email));
                command.Parameters.Add(new SqlParameter("@User_Password", GetHash(password)));
                command.ExecuteNonQuery();
                connection.Close();
                result = "Сделано!";
                return result;
            }
        }

        public static string CreateCategory(string name, bool type, Icon icon) // DONE
        {
            string result = "Уже существует";
            string proc = "CreateCategory";
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= User;Password=User"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(proc, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@icon_Id", icon.Id));
                command.Parameters.Add(new SqlParameter("@name", name));
                command.Parameters.Add(new SqlParameter("@type", type));
                command.ExecuteNonQuery();
                connection.Close();
                result = "Сделано!";
                return result;
            }
        }

        public static string CreateIcon(string name, string iconpath, string type)
        {
            string result = "Уже существует";
            string proc = "CreateIcon";
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= Admin;Password=Admin"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(proc, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@icon_path", iconpath));
                command.Parameters.Add(new SqlParameter("@name", name));
                command.Parameters.Add(new SqlParameter("@type", type));
                command.ExecuteNonQuery();
                connection.Close();
                result = "Сделано!";
                return result;
            }
        }

        public static string CreateCurrency(string name)
        {
            string result = "Уже существует";
            string proc = "CreateCurrency";
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= Admin;Password=Admin"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(proc, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@name", name));
                command.ExecuteNonQuery();
                connection.Close();
                result = "Сделано!";
                return result;
            }
        }

        public static string CreateOperation(string name, string sum, Category category, Account account, Currency currency, bool isIncome = true) // DONE
        {
            string result = "Уже существует";
            string proc = "CreateOperation";

            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= User;Password=User"))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(proc, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@categoryId", category.Id));
                    command.Parameters.Add(new SqlParameter("@accountId", account.Id));
                    command.Parameters.Add(new SqlParameter("@name", name));
                    command.Parameters.Add(new SqlParameter("@date", DateTime.Now));
                    if (isIncome)
                    {
                        command.Parameters.Add(new SqlParameter("@sum", Convert.ToDecimal(sum)));
                    }
                    else
                    {
                        if (account.Balance < Convert.ToDecimal(sum))
                        {
                            throw new Exception("На счету недостаточно денег!");
                        }
                        command.Parameters.Add(new SqlParameter("@sum", Convert.ToDecimal(sum) * (-1)));
                    }
                    result = "Сделано!";
                    command.ExecuteNonQuery();
                    connection.Close();
                    
                }
                catch ( Exception error)
                {
                    result = "Операция не выполнена!";
                    ShowMessageToUser(error.Message);
                }
                
                return result;
            }

        }

        static void ShowMessageToUser(string message)
        {
            MessageWindow messageView = new MessageWindow(message);
            messageView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            messageView.ShowDialog();
        }
        public static string CreateAccount(string name, string ballance, Icon icon, Currency currency) // DONE
        {
            string result = "Уже существует";

            string proc = "CreateAccount";
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= User;Password=User"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(proc, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Name", name));
                command.Parameters.Add(new SqlParameter("@start_balance", Convert.ToDecimal(ballance)));
                command.Parameters.Add(new SqlParameter("@icon_Id", icon.Id));
                command.Parameters.Add(new SqlParameter("@user_Id", DataWorker.CurrentUser.Id));
                command.Parameters.Add(new SqlParameter("@currency_Id", currency.Id));
                
                command.ExecuteNonQuery();
                connection.Close();
                result = "Сделано!";
                return result;
            }
        }



        //Get accounts of current user
        public static List<Account> GetAllAccountsByUserId(int id) // DONE
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Account> accounts = (from account in GetAllAccounts() where account.userID == id select account).ToList();
                return accounts;
            }
        }

        public static List<Operation> GetOperationsOfSelCategory(int id) // DONE
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Operation> operations = (from operation in GetAllOperationsByUserId(DataWorker.CurrentUser.Id) where operation.CategoryId == id select operation).ToList();
                return operations;
            }
        }

        public static string GetIconPathById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                //Icon pos = db.Icons.FirstOrDefault(p => p.Id == id);
                Icon pos = GetAllIcons().FirstOrDefault(p=> p.Id == id);
                return pos.IconPath;
            }
        }

        public static string GetCurrencyNameById(int id)
        {
            string name = GetAllCurrency().Where(x=>x.Id==id).First().Name;
            return name;
        }

        public static string GetUserNameById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                User pos = db.Users.FirstOrDefault(p => p.Id == id);
                return pos.Name;
            }
        }


        //Get operations of current user
        public static ObservableCollection<Operation> GetAllOperationsByUserId(int id) // DONE
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Operation> operations = (from account in GetAllAccounts()
                                              join user in GetAllUsers() on account.userID equals user.Id
                                              join operation in GetAllOperations() on account.Id equals operation.AccountId
                                              where user.Id == id
                                              select operation).ToList();
                ObservableCollection<Operation> res = new ObservableCollection<Operation>(operations);
                return res;
            }
        }

        // Search by name
        public static ObservableCollection<Operation> GetAllOperationsByName(string name) // DONE
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Operation> operations = (from operation in GetAllOperationsByUserId(DataWorker.CurrentUser.Id)
                                         where operation.Name.Contains(name)
                                         select operation).ToList();
                ObservableCollection<Operation> res = new ObservableCollection<Operation>(operations);
                return res;
            }
        }

        // Search by AccoutName
        public static ObservableCollection<Operation> GetAllOperationsByAccountName(string name) // DONE
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Operation> operations = (from operation in GetAllOperationsByUserId(DataWorker.CurrentUser.Id)
                                              where operation.OperationAccount.Name.Contains(name)
                                              select operation).ToList();
                ObservableCollection<Operation> res = new ObservableCollection<Operation>(operations);
                return res;
            }
        }

        // Search by CategoryName
        public static ObservableCollection<Operation> GetAllOperationsByCategoryName(string name) // DONE
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Operation> operations = (from operation in GetAllOperationsByUserId(DataWorker.CurrentUser.Id)
                                              where operation.OperationCategory.Name.Contains(name)
                                              select operation).ToList();
                ObservableCollection<Operation> res = new ObservableCollection<Operation>(operations);
                return res;
            }
        }

        // Get all incomes
        public static ObservableCollection<Operation> GetAllIncomeOperations() // DONE 
        {
            string proc = "GetAllIncomeOperations";
            List<Operation> list = new List<Operation>();
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= User;Password=User"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(proc, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@userId", (DataWorker.CurrentUser.Id)));
                var result = command.ExecuteReader();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        //GetAllAccountsByUserId;
                        Operation operation = new Operation();
                        operation.Id = (int)result.GetValue(0);
                        operation.CategoryId = (int)result.GetValue(1);
                        operation.AccountId = (int)result.GetValue(2);
                        operation.Name = (string)result.GetValue(3);
                        operation.date = (DateTime)result.GetValue(4);
                        operation.Sum = (decimal)result.GetValue(5);
                        list.Add(operation);
                    }
                }


                ObservableCollection<Operation> res = new ObservableCollection<Operation>(list);
                return res;
            }
        }
        // Get all incomes
        public static ObservableCollection<Operation> GetAllIncomeOperations(int id) // DONE 
        {
            List<Operation> operations = GetAllIncomeOperations().Where(x => x.AccountId == id).ToList();
            return new ObservableCollection<Operation>(operations);
        }

        public static ObservableCollection<Icon> GetAllAccountIcons() // DONE 
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Icon> icons = (from icon in GetAllIcons().ToList() where icon.Type == "account" select icon).ToList();
                ObservableCollection<Icon> res = new ObservableCollection<Icon>(icons);
                return res;
            }
        }

        public static ObservableCollection<Icon> GetAllCategoryIcons() // DONE 
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Icon> icons = (from icon in GetAllIcons().ToList() where icon.Type == "category" select icon).ToList();
                ObservableCollection<Icon> res = new ObservableCollection<Icon>(icons);
                return res;
            }
        }

        public static ObservableCollection<Operation> GetAllWastesOperations() // DONE 
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Operation> operations = (from operation in GetAllOperationsByUserId(DataWorker.CurrentUser.Id)
                                              where operation.Sum < 0
                                              select operation).ToList();
                ObservableCollection<Operation> res = new ObservableCollection<Operation>(operations);
                return res;
            }
        }

        public static ObservableCollection<Operation> GetAllWastesOperations(int id) // DONE 
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Operation> operations = (from operation in GetAllOperationsByUserId(DataWorker.CurrentUser.Id)
                                              where operation.Sum < 0 && operation.AccountId==id
                                              select operation).ToList();
                ObservableCollection<Operation> res = new ObservableCollection<Operation>(operations);
                return res;
            }
        }

        public static List<Operation> GetAllOperationsByAccountId(int id) // DONE 
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Operation> operations = (from  operation in GetAllOperations() where operation.AccountId == id
                                              select operation).ToList();
                return operations;
            }
        }

        public static Category GetCategoryById(int id) //DONe
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Category pos = GetAllCategories().FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }

        public static Account GetAccountById(int id)   // DONE
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Account pos = GetAllAccounts().FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }

        public static string DeleteOperation(Operation operation)
        {
            string result = "Такой позиции не существует";
            string proc = "DeleteOperation";
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= User;Password=User"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(proc, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Operation_Id", operation.Id));

                command.ExecuteNonQuery();
                connection.Close();
                result = "Сделано! Операция удалёна";
            }
            return result;
        }

        public static string DeleteUser(User user)
        {
            string result = "Такой позиции не существует";
            string proc = "DeleteUser";
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= Admin;Password=Admin"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(proc, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@User_Id", user.Id));

                command.ExecuteNonQuery();
                connection.Close();
                result = "Сделано! Пользователь удалёна";
            }
            return result;
        }

        public static string DeleteIcon(Icon icon)
        {
            string result = "";
            string proc = "DeleteIcon";
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= Admin;Password=Admin"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(proc, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Id", icon.Id));

                command.ExecuteNonQuery();
                connection.Close();
                result = "Сделано! Иконка " + icon.Name + " удалёна";
            }
            return result;
        }

        public static string DeleteCurrency(Currency currency)
        {
            string result = "";
            string proc = "DeleteCurrency";
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= Admin;Password=Admin"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(proc, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Id", currency.Id));

                command.ExecuteNonQuery();
                connection.Close();
                result = "Сделано! Валюта " + currency.Name + " удалёна";
            }
            return result;
        }

        public static string DeleteCategory(Category category)
        {
            string result = "Такой категории не существует";
            string proc = "DeleteCategory";
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= User;Password=User"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(proc, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Category_Id", category.Id));

                command.ExecuteNonQuery();
                connection.Close();
                result = "Сделано! Категория " + category.Name + " удалена";
            }
            return result;
        }

        public static string DeleteAccount(Account account)
        {
            string result = "Такой позиции не существует";
        
            string proc = "DeleteAccount";
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= User;Password=User"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(proc, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Account_Id", account.Id));

                command.ExecuteNonQuery();
                connection.Close();
                result = "Сделано! Аккаунт " + account.Name + " удалён";
            }
            return result;
        }


        public static string EditOperation(Operation oldOperation, string newName, string newSum, Category newCategory, Account newAccount)
        {
            string result = "Такой операции не существует";
            string proc = "UpdateOperation";
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= User;Password=User"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(proc, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Id", oldOperation.Id));
                command.Parameters.Add(new SqlParameter("@categoryId", newCategory.Id));
                command.Parameters.Add(new SqlParameter("@accountId", newAccount.Id));
                command.Parameters.Add(new SqlParameter("@name", newName));
                command.Parameters.Add(new SqlParameter("@date", DateTime.Now));
                if(oldOperation.Sum<0) command.Parameters.Add(new SqlParameter("@sum", Convert.ToDecimal(newSum) * (-1)));
                else command.Parameters.Add(new SqlParameter("@sum", Convert.ToDecimal(newSum)));
                command.ExecuteNonQuery();
                connection.Close();
                result = "Сделано! Категория изменена";
            }
            return result;
        }

        public static string EditCategory(Category oldCategory, string newName,bool type, Icon newIcon )
        {
            string result = "Такой категории не существует";
            string proc = "UpdateCategory";
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= Admin;Password=Admin"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(proc, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Id", oldCategory.Id));
                command.Parameters.Add(new SqlParameter("@icon_Id", newIcon.Id));
                command.Parameters.Add(new SqlParameter("@name", newName));
                command.Parameters.Add(new SqlParameter("@type", type));
                command.ExecuteNonQuery();
                connection.Close();
                result = "Сделано! Категория изменена";
            }
            return result;
        }


        public static string EditIcon(Icon oldIcon,string name, string iconpath,string type)
        {
            string result = "Такой операции не существует";
            string proc = "UpdateIcon";
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= Admin;Password=Admin"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(proc, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Id", oldIcon.Id));
                command.Parameters.Add(new SqlParameter("@icon_path", iconpath));
                command.Parameters.Add(new SqlParameter("@name", name));
                command.Parameters.Add(new SqlParameter("@type", type));
                command.ExecuteNonQuery();
                connection.Close();
                result = "Сделано! Иконка изменена";
            }
            return result;
        }


        public static string EditCurrency(Currency oldCurrency, string name)
        {
            string result = "Такой операции не существует";
            string proc = "UpdateCurrency";
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= Admin;Password=Admin"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(proc, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Id", oldCurrency.Id));
                command.Parameters.Add(new SqlParameter("@name", name));
                command.ExecuteNonQuery();
                connection.Close();
                result = "Сделано! Валюта изменена";
            }
            return result;
        }


        public static string EditAccount(Account oldAccount, string newName,Icon newIcon, string newBalance, Currency currency)
        {
            string result = "Такого аккаунта не существует";
            string proc = "UpdateAccount";
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= User;Password=User"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(proc, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Id", oldAccount.Id));
                command.Parameters.Add(new SqlParameter("@Name", newName));
                command.Parameters.Add(new SqlParameter("@Start_Balance", Convert.ToDecimal(newBalance)));
                command.Parameters.Add(new SqlParameter("@icon_Id", newIcon.Id));
                command.Parameters.Add(new SqlParameter("@currency_Id", currency.Id));
                command.ExecuteNonQuery();
                connection.Close();
                result = "Сделано! Валюта изменена";
            }
            return result;
        }



        public static string ChangeUserPassword(string oldPassword,string newPassword,string email)
        {
            string result = "Вами введён неверный старый пароль";

            string proc = "ChangeUserPassword";
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= User;Password=User"))
            {
                User user = GetAllUsers().FirstOrDefault(u => u.Login == email && u.Password == GetHash(oldPassword));
                if (user!=null)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(proc, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@id", user.Id));
                    command.Parameters.Add(new SqlParameter("@new_password", GetHash(newPassword)));
                    command.ExecuteNonQuery();
                    connection.Close();
                    result = "Сделано! Пароль изменён!";
                }
                
            }
            return result;
        }
        public static string ChangeUserPassword( string newPassword, string email)
        {
            string result = "Нет пользователя с таким email";
            string proc = "ChangeUserPassword";
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= User;Password=User"))
            {
                User user = GetAllUsers().FirstOrDefault(u => u.Login == email);
                if (user != null)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(proc, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@id", user.Id));
                    command.Parameters.Add(new SqlParameter("@new_password", GetHash(newPassword)));
                    command.ExecuteNonQuery();
                    connection.Close();
                    result = "Сделано! Пароль изменён!";
                }

            }
            return result;
        }
        public static string ChangeUserName( string newName, string email)
        {
            string result = "";
            string proc = "ChangeUserName";
            using (SqlConnection connection = new SqlConnection("Data Source=DMITRY;Initial Catalog=MoneyFy; User id= User;Password=User"))
            {
                User user = GetAllUsers().FirstOrDefault(u => u.Login == email);
                if (user != null)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(proc, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@id", user.Id));
                    command.Parameters.Add(new SqlParameter("@new_name", newName));
                    command.ExecuteNonQuery();
                    connection.Close();
                    result = "Сделано! Имя изменено!";
                }

            }
            return result;
        }
    }
}
