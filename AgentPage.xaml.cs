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
    /// Логика взаимодействия для AgentPage.xaml
    /// </summary>
    public partial class AgentPage : Page
    {
        public AgentPage()
        {
            InitializeComponent();

            var currentAgents = SafinGlazkiSaveEntities.GetContext().Agent.ToList();
            ComboType.SelectedIndex = 0;
            ComboAgentType.SelectedIndex = 0;
            AgentListView.ItemsSource = currentAgents;
            UpdateAgents();
        }

        public void UpdateAgents()
        {
            var currentAgents = SafinGlazkiSaveEntities.GetContext().Agent.ToList();

            if(ComboType.SelectedIndex == 1)
            {
                currentAgents=currentAgents.OrderBy(p=>p.Title).ToList();
            }
            if (ComboType.SelectedIndex == 2)
            {
                currentAgents = currentAgents.OrderByDescending(p=>p.Title).ToList();
            }

            if (ComboType.SelectedIndex == 3)
            {
                currentAgents = currentAgents.OrderBy(p => p.Discount).ToList();
            }
            if (ComboType.SelectedIndex == 4)
            {
                currentAgents = currentAgents.OrderByDescending(p => p.Discount).ToList();
            }

            if (ComboType.SelectedIndex == 5)
            {
                currentAgents = currentAgents.OrderBy(p => p.Priority).ToList();
            }
            if (ComboType.SelectedIndex == 6)
            {
                currentAgents = currentAgents.OrderByDescending(p => p.Priority).ToList();
            }

            if (ComboAgentType.SelectedIndex == 1)
            {
                currentAgents = currentAgents.Where(p=>p.AgentTypeText.ToString()=="МФО").ToList();
            }
            if (ComboAgentType.SelectedIndex == 2)
            {
                currentAgents = currentAgents.Where(p => p.AgentTypeText.ToString() == "ООО").ToList();
            }
            if (ComboAgentType.SelectedIndex == 3)
            {
                currentAgents = currentAgents.Where(p => p.AgentTypeText.ToString() == "ЗАО").ToList();
            }
            if (ComboAgentType.SelectedIndex == 4)
            {
                currentAgents = currentAgents.Where(p => p.AgentTypeText.ToString() == "МКК").ToList();
            }
            if (ComboAgentType.SelectedIndex == 5)
            {
                currentAgents = currentAgents.Where(p => p.AgentTypeText.ToString() == "ОАО").ToList();
            }
            if (ComboAgentType.SelectedIndex == 6)
            {
                currentAgents = currentAgents.Where(p => p.AgentTypeText.ToString() == "ПАО").ToList();
            }

            currentAgents = currentAgents.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            AgentListView.ItemsSource=currentAgents;
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAgents();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgents();
        }

        private void ComboAgentType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgents();
        }
    }
}
