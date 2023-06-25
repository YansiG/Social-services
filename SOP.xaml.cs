using System.Windows;


namespace TalentedYouthProgect
{
    /// <summary>
    /// Логика взаимодействия для SOP.xaml
    /// </summary>
    public partial class SOP : Window
    {
        PropStudent propStudent;
        public SOP(PropStudent ps)
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
                DataBase.Write("sop", "famtype, cause, startdate, terminationdate", textBox1.Text, textBox2.Text, dateTimePicker1.Text, dateTimePicker2.Text);
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
            propStudent.socialDanger.IsChecked = false;
        }
    }
}
