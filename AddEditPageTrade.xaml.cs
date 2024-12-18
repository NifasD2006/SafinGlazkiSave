using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
    /// Логика взаимодействия для AddEditPageTrade.xaml
    /// </summary>
    public partial class AddEditPageTrade : Page
    {
        private Agent _currentAgent= new Agent();

        public AddEditPageTrade(Agent SelectedService)
        {
            InitializeComponent();
            if (SelectedService != null)
            {

                this._currentAgent = SelectedService;
            }
            DataContext = _currentAgent;


            var _currentClient = SafinGlazkiSaveEntities.GetContext().Product.ToList();
            ComboProductSaleTitle.Items.Clear();

            ComboProductSaleTitle.ItemsSource = _currentClient;
        }
        private ProductSale _currentProductSale = new ProductSale();

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            
            StringBuilder errors = new StringBuilder();
            if (ComboProductSaleTitle.SelectedItem == null)
                errors.AppendLine("Укажите товар");
            if (TradeData.SelectedDate == null)
                errors.AppendLine("Укажите дату услуги");
            
            if (!int.TryParse(TBProductCount.Text, out int productCount) || productCount <= 0)
                errors.AppendLine("Укажите корректное количество товара (больше 0).");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            _currentProductSale.ProductCount = productCount;
            _currentProductSale.ProductID = ComboProductSaleTitle.SelectedIndex + 1;
            _currentProductSale.SaleDate = TradeData.SelectedDate.Value;
            _currentProductSale.AgentID = _currentAgent.ID;

            if (_currentProductSale.ID == 0)
            {
                SafinGlazkiSaveEntities.GetContext().ProductSale.Add(_currentProductSale);
            }
            try
            {
                SafinGlazkiSaveEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();

                 
                Manager.MainFrame.GoBack();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


    }
}
