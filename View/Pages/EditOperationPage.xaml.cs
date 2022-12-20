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

namespace MM_MoneyFy.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditOperationPage.xaml
    /// </summary>
    public partial class EditOperationPage : Page
    {
        public EditOperationPage()
        {
            InitializeComponent();
           DataContext = new MoneyFY_VM();
        }
    }
}
