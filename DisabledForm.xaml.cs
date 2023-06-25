using System.Windows;


namespace TalentedYouthProgect
{
    /// <summary>
    /// Логика взаимодействия для DisabledForm.xaml
    /// </summary>
    public partial class DisabledForm : Window
    {
        PropStudent propStudent;
        public DisabledForm(PropStudent ps)
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
                DataBase.Write("disabled", "category, time, chernobyl", textBox1.Text, textBox2.Text, Chernobyl.IsChecked.ToString());
            }
            else
            {
                System.Windows.MessageBox.Show("Не заполнены обязательные к заполнению поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Closed -= Window_Closed;
            this.Close();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            propStudent.disabledPerson.IsChecked = false;
        }
    }
}
