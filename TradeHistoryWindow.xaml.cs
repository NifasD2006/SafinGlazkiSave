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

namespace SafinGlazkiSave
{
    /// <summary>
    /// Логика взаимодействия для TradeHistoryWindow.xaml
    /// </summary>
    public partial class TradeHistoryWindow : Window
    {
        //public Agent currentAgent;
        public TradeHistoryWindow(Agent agent)
        {
            InitializeComponent();
            TradeFrame.Navigate(new TradeHistoryPage(agent));
            Manager.TradeFrame = TradeFrame;
        }

        private void TradeFrame_ContentRendered(object sender, EventArgs e)
        {
            if (TradeFrame.CanGoBack)
            {
                BtnBack.Visibility = Visibility.Visible;
            }
            else
            {
                BtnBack.Visibility = Visibility.Hidden;
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.TradeFrame.GoBack();
        }
    }
}
