using MM_MoneyFy.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для UserSettingsView.xaml
    /// </summary>
    public partial class UserSettingsView : Page
    {
        public static DataGrid IncOperationOfCurrentCategory;
        public static DataGrid WstOperationOfCurrentCategory;
        public static DataGrid IncomeCategories;
        public static DataGrid WasteCategories;
        public static GroupBox GroupBoxOperation;
        public static GroupBox GroupBoxOperationWst;
        public static TextBox TextBox;
        public static TextBox TextBox2;
        public static Image Icon;
        public static Image Icon2;
        public UserSettingsView()
        {
            InitializeComponent();
            DataContext = new UserSettings_VM();
            IncOperationOfCurrentCategory = OperationDG;
            WstOperationOfCurrentCategory = OperationDGWst;
            GroupBoxOperation = OperationGB;
            GroupBoxOperationWst = OperationGBWst;
            TextBox = NameBlockIncCategory;
            TextBox2 = NameBlockWstCategory;
            Icon = CategoryIcon;
            Icon2 = CategoryIconWst;
            IncomeCategories = IncomesGridCategories;
            WasteCategories = WasteGridCategories;
            App.LanguageChanged += LanguageChanged;
            CultureInfo currLang = App.Language;
            //Заполняем меню смены языка:
            //menuLanguage.Items.Clear();
            foreach (var lang in App.Languages)
            {
                MenuItem menuLang = new MenuItem();
                menuLang.Header = lang.DisplayName;
                menuLang.Tag = lang;
                menuLang.IsChecked = lang.Equals(currLang);
                menuLang.Click += ChangeLanguageClick;
                menuLang.IsChecked = false;
               // menuLanguage.Items.Add(menuLang);
            }
        }
        private void LanguageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language;

            //Отмечаем нужный пункт смены языка как выбранный язык
            //foreach (MenuItem i in //menuLanguage.Items)
           // {
             //   CultureInfo ci = i.Tag as CultureInfo;
            //    i.IsChecked = ci != null && ci.Equals(currLang);
//}
        }


        private void ChangeLanguageClick(Object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi != null)
            {
                CultureInfo lang = mi.Tag as CultureInfo;
                if (lang != null)
                {
                    App.Language = lang;
                }
            }
        }

        private void PB1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                if (((PasswordBox)sender).Name == "PB1")
                {
                    ((dynamic)this.DataContext).OldPassword = ((PasswordBox)sender).Password;
                }
                else if (((PasswordBox)sender).Name == "PB2")
                {
                    ((dynamic)this.DataContext).NewPassword = ((PasswordBox)sender).Password;
                }
                else if (((PasswordBox)sender).Name == "PB3")
                {
                    ((dynamic)this.DataContext).RepeatNewPassword = ((PasswordBox)sender).Password;
                }
            }
        }
    }
}
