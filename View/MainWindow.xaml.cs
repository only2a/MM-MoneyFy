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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MM_MoneyFy.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Grid RecGrid;
        public static Grid RecBackgroundGrid;
        public static Grid RecGridWithFrame;

        public static Frame RecFrame;

        public static StackPanel GetMessage;
        public static StackPanel ConfirmCode;
        public static StackPanel ChangePassw;

        public MainWindow()
        {
            InitializeComponent();
            DataContext =new  AuthorizationVM();
            RecGrid = RecoveryGridView;
            RecBackgroundGrid = BackgroundGrid;
            RecGridWithFrame = RecoveryGridWithFrame;
            RecFrame = RecoveryFrameView;
            GetMessage = Btn1;
            ConfirmCode = Btn2;
            ChangePassw = Btn3;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).UserPassword = ((PasswordBox)sender).Password; }
        }
    }
}
