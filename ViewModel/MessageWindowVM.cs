using MM_MoneyFy.Model;
using MM_MoneyFy.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MM_MoneyFy.ViewModel
{

    class MessageWindowVM 
    {
      

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
    }
}
