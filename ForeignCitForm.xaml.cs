
using System.Windows;

using System.Windows.Forms;


namespace TalentedYouthProgect
{
    /// <summary>
    /// Логика взаимодействия для ForeignCitForm.xaml
    /// </summary>
    public partial class ForeignCitForm : Window
    {
        PropStudent propStudent;
        public ForeignCitForm(PropStudent ps)
        {
            InitializeComponent();
            propStudent = ps;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            propStudent.foreignСitizens.IsChecked = false;
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text != "")
            {
                DataBase.Write("foreignC", "citizenship, term, parents", textBox1.Text, textBox2.Text, textBox3.Text);
            }
            else
            {
                System.Windows.MessageBox.Show("Не заполнены обязательные к заполнению поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Close();
        }
    }
}
