using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {



        private Agent _currentAgent = new Agent();

        public AddEditPage(Agent SelectedService)
        {   
            InitializeComponent();
            ComboType.SelectedItem = 0;
            if (SelectedService != null)
            {

                _currentAgent = SelectedService;
                ComboType.SelectedIndex = _currentAgent.AgentTypeID-1;
            }
            DataContext = _currentAgent;
            //var currentTrade = SafinGlazkiSaveEntities.GetContext().ProductSale.Where(p => p.AgentID == SelectedService.ID).ToList();
            //TradeListView.ItemsSource = currentTrade;
            UpdateTradeList();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentAgent.Title))
                errors.AppendLine("Укажите наименование агента");
            if (string.IsNullOrWhiteSpace(_currentAgent.Address))
                errors.AppendLine("Укажите адресс агента");
            if (string.IsNullOrWhiteSpace(_currentAgent.DirectorName))
                errors.AppendLine("Укажите наименование ФИО директора");
            if (ComboType.SelectedItem == null)
                errors.AppendLine("Укажите тип агента");
            if (string.IsNullOrWhiteSpace(_currentAgent.Priority.ToString()))
                errors.AppendLine("Укажите приоритет агента");
            if(_currentAgent.Priority<=0)
                errors.AppendLine("Укажите положительнй приоритет");
            if (string.IsNullOrWhiteSpace(_currentAgent.INN))
                errors.AppendLine("Укажите ИНН агента");
            if (string.IsNullOrWhiteSpace(_currentAgent.KPP))
                errors.AppendLine("Укажите КПП агента");
            if (string.IsNullOrWhiteSpace(_currentAgent.Phone))
                errors.AppendLine("Укажите телефон агента");
            else
            {
/*                string ph = _currentAgent.Phone.Replace("(", "").Replace("-", "").Replace("+", "").Replace(")", "").Replace(" ","");
                if (((ph[1] == '9' || ph[1] == '4' || ph[1] == '8') && ph.Length != 11) || (ph[1] == '3' && ph.Length != 12))
                    errors.AppendLine("Укажите правильно номер телефона");*/
            }
            if (string.IsNullOrWhiteSpace(_currentAgent.Email))
                errors.AppendLine("Укажите почта агента");
            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            _currentAgent.AgentTypeID = ComboType.SelectedIndex + 1;

            if (_currentAgent.ID == 0)
            {
                SafinGlazkiSaveEntities.GetContext().Agent.Add(_currentAgent);
            }
            try
            {
                SafinGlazkiSaveEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var currentAgent = (sender as Button).DataContext as Agent;

            var currentProductSale = SafinGlazkiSaveEntities.GetContext().ProductSale.ToList();
            currentProductSale = currentProductSale.Where(p => p.AgentID == _currentAgent.ID).ToList();

            var currentAgentPriorityHistory = SafinGlazkiSaveEntities.GetContext().AgentPriorityHistory.ToList();
            var currentShop = SafinGlazkiSaveEntities.GetContext().Shop.ToList();
            currentAgentPriorityHistory = currentAgentPriorityHistory.Where(p => p.AgentID == _currentAgent.ID).ToList();
            currentShop = currentShop.Where(p => p.AgentID == _currentAgent.ID).ToList();
            if (currentProductSale.Count != 0)
                MessageBox.Show("Невозможно выполнить удаление, так как существует история реализации продуктов");
            else
            {
                if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        SafinGlazkiSaveEntities.GetContext().Agent.Remove(_currentAgent);

                        if (currentAgentPriorityHistory.Count != 0)
                        {
                            for (int i = 0; currentAgentPriorityHistory.Count == i; i++)
                                SafinGlazkiSaveEntities.GetContext().AgentPriorityHistory.Remove(currentAgentPriorityHistory[i]);
                        }
                        if (currentShop.Count != 0)
                        {
                            for (int i = 0; currentShop.Count == i; i++)
                                SafinGlazkiSaveEntities.GetContext().Shop.Remove(currentShop[i]);
                        }
                        SafinGlazkiSaveEntities.GetContext().SaveChanges();

                        MessageBox.Show("Информация удалена!");
                        Manager.MainFrame.GoBack();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void ChangePictureBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myOpenfileDialog=new OpenFileDialog();
            if (myOpenfileDialog.ShowDialog() == true)
            {
                _currentAgent.Logo = myOpenfileDialog.FileName;
                LogoImage.Source = new BitmapImage(new Uri(myOpenfileDialog.FileName));
            }
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void HistoryTrade_Click(object sender, RoutedEventArgs e)
        {

            TradeHistoryWindow HistoryWindow = new TradeHistoryWindow(_currentAgent);
            HistoryWindow.ShowDialog();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPageTrade((sender as Button).DataContext as Agent));
            UpdateTradeList();

        }
        private void UpdateTradeList()
        {
            
            var currentTrade = SafinGlazkiSaveEntities.GetContext().ProductSale.Where(p => p.AgentID == _currentAgent.ID).ToList();
            TradeListView.ItemsSource = currentTrade;
        }
        private void DeleteDtn_Click(object sender, RoutedEventArgs e)
        {

            var currentService = (sender as Button).DataContext as ProductSale;

            if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!",
                                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    SafinGlazkiSaveEntities.GetContext().ProductSale.Remove(currentService);
                    SafinGlazkiSaveEntities.GetContext().SaveChanges();
                    MessageBox.Show("Производим удаление...");
                    UpdateTradeList();
                    //Manager.MainFrame.GoBack();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateTradeList();
        }
    }
}
