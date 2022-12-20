﻿using MM_MoneyFy.ViewModel;
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
    /// Логика взаимодействия для OperationsPageView.xaml
    /// </summary>
    public partial class OperationsPageView : Page
    {
        public static DataGrid OperationsDataGridView;
        public OperationsPageView()
        {
            InitializeComponent();
            DataContext = new OperationsPage_VM();
            OperationsDataGridView = OperationsDataGrid;
        }
    }
}
