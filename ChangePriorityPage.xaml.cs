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
    /// Логика взаимодействия для ChangePriorityPage.xaml
    /// </summary>
    public partial class ChangePriorityPage : Window
    {
        public ChangePriorityPage( int max)
        {
            InitializeComponent();
            TextBoxPriority.Text= max.ToString();
            
        }

        private void SetPriortyBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



    }
}
