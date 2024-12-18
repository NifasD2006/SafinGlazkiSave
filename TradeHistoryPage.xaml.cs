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

namespace SafinGlazkiSave
{
    /// <summary>
    /// Логика взаимодействия для TradeHistoryPage.xaml
    /// </summary>
    public partial class TradeHistoryPage : Page
    {
        private ProductSale _currentAgent = new ProductSale();
        List<ProductSale> CurrentPageList = new List<ProductSale>();
        List<ProductSale> TableList;

        public TradeHistoryPage(Agent agent)
        {
            InitializeComponent();
            var currentTrade = SafinGlazkiSaveEntities.GetContext().ProductSale.Where(p => p.AgentID == agent.ID).ToList();
            TradeListView.ItemsSource = currentTrade;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.TradeFrame.Navigate(new AddEditPageTrade((sender as Button).DataContext as Agent));
        }

        public void UpdateAgents()
        {
            /*var currentTrade = SafinGlazkiSaveEntities.GetContext().ProductSale.Where(p => p.AgentID == agent.ID).ToList();
            currentTrade = currentTrade.Where(p => p.ProductText.ToLower().Contains(TXBox.Text.ToLower())).ToList();
            TradeListView.ItemsSource = currentTrade;*/
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAgents();

        }
    }
}
