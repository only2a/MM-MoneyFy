using LiveCharts;
using LiveCharts.Wpf;
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
    /// Логика взаимодействия для MoneyFy.xaml
    /// </summary>
    public partial class MoneyFy : Window
    {
        public static ListBox AccountView;
        public static ListBox OperationView;
        public static De.TorstenMandelkow.MetroChart.ChartSeries PieChartIncomeView;
        public static De.TorstenMandelkow.MetroChart.ChartSeries PieChartWasteView;
        public static Grid GridForEditPagesView;
        public static Grid BackGridView;
        public static Grid GridWithEditPagesView;
        public static Frame EditPagesFrameView;
        public MoneyFy()
        {
            InitializeComponent();
            DataContext = new MoneyFY_VM();
            AccountView = ViewListBoxAccount;
            OperationView = ViewListBoxOperation;
            PieChartIncomeView = PieChartIncome;
            PieChartWasteView = PieChartWaste;
            GridForEditPagesView = GridForEditPages;
            GridWithEditPagesView = GridWhithEditPages;
            BackGridView = BackGrid;
            EditPagesFrameView = EditPagesFrame;
        }















        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

            if (Tg_Btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_contacts.Visibility = Visibility.Collapsed;
                tt_messages.Visibility = Visibility.Collapsed;
                tt_settings.Visibility = Visibility.Collapsed;
                tt_signout.Visibility = Visibility.Collapsed;
                tt_add.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_contacts.Visibility = Visibility.Visible;
                tt_messages.Visibility = Visibility.Visible;
                tt_settings.Visibility = Visibility.Visible;
                tt_add.Visibility = Visibility.Visible;
                tt_signout.Visibility = Visibility.Visible;
            }
        }
       

    }
}
