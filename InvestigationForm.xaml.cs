
using System.Windows;
using System.Windows.Forms;


namespace TalentedYouthProgect
{
    /// <summary>
    /// Логика взаимодействия для InvestigationForm.xaml
    /// </summary>
    public partial class InvestigationForm : Window
    {
        PropStudent propStudent;
        public InvestigationForm(PropStudent ps)
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
            if (dateTimePicker1.Text != "")
            {
                DataBase.Write("investigation", "date, date_fin, sourceinf, solution", dateTimePicker1.Text, dateTimePicker2.Text, textBox2.Text, textBox3.Text);
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
            propStudent.socialInvestigation.IsChecked = false;
        }
    }
}
