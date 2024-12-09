using Microsoft.Win32;
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
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Agent _currentAgents = new Agent();
        public AddEditPage(Agent SelectedService)
        {
            InitializeComponent();

            if (SelectedService != null)
                _currentAgents = SelectedService;
            DataContext = _currentAgents;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentAgents.Title))
                errors.AppendLine("Укажите наименование агента");
            if (string.IsNullOrWhiteSpace(_currentAgents.Address))
                errors.AppendLine("Укажите адресс агента");
            if (string.IsNullOrWhiteSpace(_currentAgents.DirectorName))
                errors.AppendLine("Укажите наименование ФИО директора");
            if (ComboType.SelectedItem == null)
                errors.AppendLine("Укажите тип агента");
            if (string.IsNullOrWhiteSpace(_currentAgents.Priority.ToString()))
                errors.AppendLine("Укажите приоритет агента");
            if(_currentAgents.Priority<=0)
                errors.AppendLine("Укажите положительнй приоритет");
            if (string.IsNullOrWhiteSpace(_currentAgents.INN))
                errors.AppendLine("Укажите ИНН агента");
            if (string.IsNullOrWhiteSpace(_currentAgents.KPP))
                errors.AppendLine("Укажите КПП агента");
            if (string.IsNullOrWhiteSpace(_currentAgents.Phone))
                errors.AppendLine("Укажите телефон агента");
            else
            {
                string ph = _currentAgents.Phone.Replace("(", "").Replace("-", "").Replace("+", "").Replace(")", "");
                if (((ph[1] == '9' || ph[1] == '4' || ph[1] == '8') && ph.Length != 11) || (ph[1] == '3' && ph.Length != 12))
                    errors.AppendLine("Укажите правильно номер телефона");
            }
            if (string.IsNullOrWhiteSpace(_currentAgents.Email))
                errors.AppendLine("Укажите почта агента");
            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentAgents.ID == 0)
                SafinGlazkiSaveEntities.GetContext().Agent.Add(_currentAgents);
            try
            {
                SafinGlazkiSaveEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var currentAgent = (sender as Button).DataContext as Agent;


            if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!",
                               MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    SafinGlazkiSaveEntities.GetContext().Agent.Remove(currentAgent);
                    SafinGlazkiSaveEntities.GetContext().SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void ChangePictureBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myOpenfileDialog=new OpenFileDialog();
            if (myOpenfileDialog.ShowDialog() == true)
            {
                _currentAgents.Logo = myOpenfileDialog.FileName;
                LogoImage.Source = new BitmapImage(new Uri(myOpenfileDialog.FileName));
            }
        }
    }
}
