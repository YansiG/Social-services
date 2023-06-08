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
    /// Логика взаимодействия для AddCurators.xaml
    /// </summary>
    public partial class AddCurators : Window
    {
        PropStudent propStudent;
        public AddCurators(PropStudent ps)
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
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                DataBase.Write("Curators", "Name, Surname, Lastname", textBox1.Text, textBox2.Text, textBox3.Text);
                var ures = System.Windows.MessageBox.Show("Данные обновленны.", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                if (ures == MessageBoxResult.OK)
                {
                    this.Close();
                }
                propStudent.Update();
            }
            else
            {
                System.Windows.MessageBox.Show("Не заполнены обязательные к заполнению поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
