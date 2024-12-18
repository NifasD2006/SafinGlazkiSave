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
        int CountRecords;
        int CountPage;
        int CurrentPage = 0;
        List<Agent> CurrentPageList = new List<Agent>();
        List<Agent> TableList;
        public AgentPage()
        {
            InitializeComponent();

            var currentAgents = SafinGlazkiSaveEntities.GetContext().Agent.ToList();
            AgentListView.ItemsSource = currentAgents;
            ComboType.SelectedIndex = 0;
            ComboAgentType.SelectedIndex = 0;
            ChangePriorityBtn.Visibility = Visibility.Hidden;
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

            currentAgents = currentAgents.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower())|| p.Email.ToLower().Contains(TBoxSearch.Text.ToLower()) || p.Phone.Replace("+","").Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "").Contains(TBoxSearch.Text.ToLower())).ToList();

            AgentListView.ItemsSource=currentAgents;
            TableList = currentAgents;
            ChangePage(0, 0);

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
        private void ChangePage(int direction, int? selectedPage)
        {
            CurrentPageList.Clear();
            CountRecords = TableList.Count;

            if (CountRecords % 10 > 0)
            {
                CountPage = CountRecords / 10 + 1;
            }
            else
            {
                CountPage = CountRecords / 10;
            }

            Boolean Ifupdate = true;

            int min;

            if (selectedPage.HasValue)
            {
                if (selectedPage >= 0 && selectedPage <= CountPage)
                {
                    CurrentPage = (int)selectedPage;
                    min = CurrentPage * 10+10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                    for (int i = CurrentPage * 10; i < min; i++)
                    {
                        CurrentPageList.Add(TableList[i]);
                    }
                }
            }
            else
            {
                switch (direction)
                {
                    case 1:
                        if (CurrentPage > 0)
                        {
                            CurrentPage--;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;
                    case 2:
                        if (CurrentPage < CountPage - 1)
                        {
                            CurrentPage++;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;
                }
               

            }
            if (Ifupdate)
            {
                PageListBox.Items.Clear();
                for (int i = 1; i <= CountPage; i++)
                {
                    PageListBox.Items.Add(i);
                }
                PageListBox.SelectedIndex = CurrentPage;



                AgentListView.ItemsSource = CurrentPageList;
                AgentListView.Items.Refresh();
            }
        }
        private void LeftDirBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(1, null);
        }

        private void RigthDirBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(2, null);

        }

        private void PageListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChangePage(0, Convert.ToInt32(PageListBox.SelectedItem.ToString()) - 1);

        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Agent));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                SafinGlazkiSaveEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                AgentListView.ItemsSource = SafinGlazkiSaveEntities.GetContext().Agent.ToList();
            }
            UpdateAgents();
        }

        private void AgentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AgentListView.SelectedItems.Count > 1)
                ChangePriorityBtn.Visibility = Visibility.Visible;
            else
                ChangePriorityBtn.Visibility = Visibility.Hidden;
        }

        private void ChangePriorityBtn_Click(object sender, RoutedEventArgs e)
        {
            int maxPriorty = 0;
            foreach (Agent agent in AgentListView.SelectedItems) 
            {
                if(agent.Priority > maxPriorty)
                    maxPriorty = agent.Priority;
            }

            ChangePriorityPage SetWindow=new ChangePriorityPage(maxPriorty);
            SetWindow.ShowDialog();
            string str = SetWindow.TextBoxPriority.Text;
            int digit = 0;
            if (string.IsNullOrEmpty(SetWindow.TextBoxPriority.Text) || !int.TryParse(str, out digit) )
            {
                MessageBox.Show("Необходимо ввести число для приоритета");
            }
            else
            {
                int NewP = Convert.ToInt32(SetWindow.TextBoxPriority.Text);
                if (NewP < 0)
                {
                    MessageBox.Show("Приоритет должен быть больше нуля");
                }
                else
                {
                    foreach (Agent agent in AgentListView.SelectedItems)
                    {
                        agent.Priority = NewP;
                    }
                    try
                    {
                        SafinGlazkiSaveEntities.GetContext().SaveChanges();
                        MessageBox.Show("Сохранено");
                        UpdateAgents();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }

                }



            }
        }

        
    }
}
