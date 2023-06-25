using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TalentedYouthProgect
{
    /// <summary>
    /// Логика взаимодействия для IPR.xaml
    /// </summary>
    public partial class IPR : Window
    {
        PropStudent propStudent;
        public IPR(PropStudent ps)
        {
            InitializeComponent();
            propStudent = ps;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text != "")
            {
                DataBase.Write("ipr", "cause, date, numdatecause, area, category, terminationdate", textBox1.Text, dateTimePicker1.Text, textBox3.Text, textBox4.Text, textBox5.Text, dateTimePicker2.Text);
                //propStudent.ipr_id = 
            }
            else
            {
                System.Windows.MessageBox.Show("Не заполнены обязательные к заполнению поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Closed -= Window_Closed;
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            propStudent.preventiveWork.IsChecked = false;
        }
    }
}
