using MaterialDesignThemes.Wpf;
using MM_MoneyFy.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MM_MoneyFy.View
{
    /// <summary>
    /// Логика взаимодействия для AdminMoneyFy.xaml
    /// </summary>
    public partial class AdminMoneyFy : Window
    {
        #region d
        public static Card AddCard;
        public static Card AddCardIcon;
        public static Card AddCardCurrency;
        public static Grid BackgroundGr;
        public static Grid PagesGrid;
        public static Grid MainGrid;
        public static Frame Frame;
        public static DataGrid Users;
        public static DataGrid Staffs;
        public static DataGrid Incomes;
        public static DataGrid Wastes;
        public static DataGrid Icons;
        public static DataGrid Currency;
        public static ComboBox CategoryIcons;
        #endregion
        public AdminMoneyFy()
        {
            InitializeComponent();
            DataContext = new Admin_VM();


            #region a
            AddCard = Add;
            AddCardIcon = Add2;
            AddCardCurrency = Add3;
            BackgroundGr = BackGrid;
            PagesGrid = GridWhithPages;
            MainGrid = AdminPagesGrid;
            Frame = Pages;
            Users = UsersDT;
            //Staffs = StaffDT;
            Incomes = IncCategories;
            Wastes = WstCategories;
            Icons = BankIcons;
            Currency = CurrencyDT;
            CategoryIcons = IconBlock;
            #endregion
        }




        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

            if (Tg_Btn.IsChecked == true)
            {
                //tt_settings.Visibility = Visibility.Collapsed;
                tt_signout.Visibility = Visibility.Collapsed;
                //tt_add.Visibility = Visibility.Collapsed;
            }
            else
            {
                //tt_settings.Visibility = Visibility.Visible;
                //tt_add.Visibility = Visibility.Visible;
                tt_signout.Visibility = Visibility.Visible;
            }
        }
    }
}
