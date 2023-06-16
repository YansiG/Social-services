﻿using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Windows;

namespace TalentedYouthProgect
{
    /// <summary>
    /// Логика взаимодействия для PropStudent.xaml
    /// </summary>
    public partial class PropStudent : Window
    {
        MainWindow mainWindow;
        private int _studentID;
        public int? disabled_id, orphan_id, ipr_id, sop_id, investigation_id, foreign_id;

        public PropStudent(MainWindow mw)
        {
            InitializeComponent();
            mainWindow = mw;
            Update();
        }

        private List<string> curators()
        {
            string sqlExpressionCur = "SELECT * FROM Curators";
            List<string> resultCur = new List<string>();
            using (var connection = new SqliteConnection("Data Source=data.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpressionCur, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //resultCur.Clear();
                            string name = "";
                            for (int i = 1; i < 4; i++)
                            {
                                name += reader.GetString(i) + " ";
                            }
                            resultCur.Add(name);
                        }
                    }
                }
            }
            return resultCur;
        }
        private List<string> Sgroup()
        {
            string sqlExpressionCur = "SELECT * FROM StudentGroup";
            List<string> resultGroup = new List<string>();
            using (var connection = new SqliteConnection("Data Source=data.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpressionCur, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //resultCur.Clear();
                            string name = "";
                            for (int i = 1; i < 2; i++)
                            {
                                name += reader.GetString(i) + " ";
                            }
                            resultGroup.Add(name);
                        }
                    }
                }
            }
            return resultGroup;
        }

        public void Update()
        {
            comboBox1.Items.Clear();
            for (int i = 0; i < curators().Count; i++) { comboBox1.Items.Add(curators().ToArray()[i]); }
            groupBox.Items.Clear();
            for (int i = 0; i < Sgroup().Count; i++) { groupBox.Items.Add(Sgroup().ToArray()[i]); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text != "" && comboBox1.SelectedIndex > -1 && groupBox.SelectedIndex > -1)
            {
                int Student_ID = Convert.ToInt32(DataBase.Read("SQLITE_SEQUENCE", "seq", "name", "Students")) + 1;

                DataBase.Write("Students", "name, birthday, admissionYear, studentGroup, сurator, residentialAddress, registrationAddress, mobile, Hub_ID", textBox1.Text, dateTimePicker1.Text, dateTimePicker2.SelectedDate.Value.ToString("yyyy.MM.dd"), groupBox.SelectedItem.ToString(), comboBox1.SelectedItem.ToString(), textBox6.Text, textBox7.Text, textBox8.Text, Student_ID.ToString());

                DataBase.Write("Hub", "Student_ID", Student_ID.ToString());

                if ((bool)disabledPerson.IsChecked)
                {
                    DataBase.Update("Hub", "disabled_ID", DataBase.GetID("disabled"), "Student_ID", Convert.ToString(Student_ID));
                }
                if ((bool)orphan.IsChecked)
                {
                    DataBase.Update("Hub", "orphan_ID", DataBase.GetID("orphan"), "Student_ID", Convert.ToString(Student_ID));
                }
                if ((bool)preventiveWork.IsChecked)
                {
                    DataBase.Update("Hub", "ipr_ID", DataBase.GetID("ipr"), "Student_ID", Convert.ToString(Student_ID));
                }
                if ((bool)socialDanger.IsChecked)
                {
                    DataBase.Update("Hub", "sop_ID", DataBase.GetID("sop"), "Student_ID", Convert.ToString(Student_ID));
                }
                if ((bool)socialInvestigation.IsChecked)
                {
                    DataBase.Update("Hub", "investigation_ID", DataBase.GetID("investigation"), "Student_ID", Convert.ToString(Student_ID));
                }
                if ((bool)foreignСitizens.IsChecked)
                {
                    DataBase.Update("Hub", "foreign_ID", DataBase.GetID("foreignC"), "Student_ID", Convert.ToString(Student_ID));
                }
                mainWindow.UpdateData();
                mainWindow.ResetComboBox();
                var ures = System.Windows.MessageBox.Show("Данные обновленны.", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                if (ures == MessageBoxResult.OK)
                {
                    this.Close();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Не заполнены обязательные к заполнению поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void EditData(int id)
        {
            _studentID = id;
            string sqlExpression = $"SELECT * FROM Students WHERE ID = '{_studentID}'";
            List<string> result = new List<string>();
            using (var connection = new SqliteConnection("Data Source=data.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Clear();
                            for (int i = 0; i < 9; i++)
                            {
                                result.Add(reader.GetString(i));
                            }
                        }
                    }
                }
            }
            textBox1.Text = result[1].ToString();
            dateTimePicker1.Text = result[2].ToString();
            dateTimePicker2.Text = result[3].ToString();
            comboBox1.SelectedValue = result[5].ToString();
            groupBox.SelectedValue = result[4].ToString();
            textBox6.Text = result[6].ToString();
            textBox7.Text = result[7].ToString();
            textBox8.Text = result[8].ToString();

            SaveBut.Click -= Button_Click_1;
            SaveBut.Click += Edit_Click;

            List<string> hub = DataBase.ReadS("Hub", "*", "Student_ID", _studentID.ToString());
            if (hub[1] != "NULL")
            {
                disabledPerson.IsChecked = true;
            }
            if (hub[2] != "NULL")
            {
                orphan.IsChecked = true;
            }
            if (hub[3] != "NULL")
            {
                preventiveWork.IsChecked = true;
            }
            if (hub[4] != "NULL")
            {
                socialDanger.IsChecked = true;
            }
            if (hub[5] != "NULL")
            {
                foreignСitizens.IsChecked = true;
            }
            if (hub[6] != "NULL")
            {
                socialInvestigation.IsChecked = true;
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)//РЕАЛИЗОВАТЬ
        {
            DataBase.Update("Students", "name", textBox1.Text, "ID", _studentID.ToString());
            DataBase.Update("Students", "birthday", dateTimePicker1.Text, "ID", _studentID.ToString());
            DataBase.Update("Students", "admissionYear", dateTimePicker2.Text, "ID", _studentID.ToString());
            DataBase.Update("Students", "studentGroup", groupBox.SelectedValue.ToString(), "ID", _studentID.ToString());
            DataBase.Update("Students", "сurator", comboBox1.SelectedValue.ToString(), "ID", _studentID.ToString());
            DataBase.Update("Students", "residentialAddress", textBox6.Text, "ID", _studentID.ToString());
            DataBase.Update("Students", "registrationAddress", textBox7.Text, "ID", _studentID.ToString());
            DataBase.Update("Students", "mobile", textBox8.Text, "ID", _studentID.ToString());

            List<string> hub = DataBase.ReadS("Hub", "*", "Student_ID", _studentID.ToString());

            if (!(bool)disabledPerson.IsChecked)
            {
                DataBase.Delete("disabled", "", "ID", hub[1].ToString());
                DataBase.SetNull("Hub", "disabled_ID", "Student_ID", _studentID.ToString());
                //string id = DataBase.GetID("disabled");
                //DataBase.Update("Hub", "disabled_ID", id, "Student_ID", _studentID.ToString());
            }
            else
            {
                string id = DataBase.GetID("disabled");
                DataBase.Update("Hub", "disabled_ID", id, "Student_ID", _studentID.ToString());
            }
            if (!(bool)orphan.IsChecked)
            {
                DataBase.Delete("orphan", "", "ID", hub[2].ToString());
                string id = DataBase.GetID("orphan");
                if (id != "")
                {
                    DataBase.Update("Hub", "orphan_ID", id, "Student_ID", _studentID.ToString());
                }
                
            }
            if (!(bool)preventiveWork.IsChecked)
            {
                DataBase.Delete("ipr", "", "ID", hub[3].ToString());
                string id = DataBase.GetID("ipr");
                if (id != "")
                {
                    DataBase.Update("Hub", "ipr_ID", id, "Student_ID", _studentID.ToString());
                }
                
            }
            if (!(bool)socialDanger.IsChecked)
            {
                DataBase.Delete("sop", "", "ID", hub[4].ToString());
                string id = DataBase.GetID("sop");
                if (id != "")
                {
                    DataBase.Update("Hub", "sop_ID", id, "Student_ID", _studentID.ToString());
                }
                
            }
            if (!(bool)socialInvestigation.IsChecked)
            {
                DataBase.Delete("investigation", "", "ID", hub[6].ToString());
                string id = DataBase.GetID("investigation");
                if (id != "")
                {
                    DataBase.Update("Hub", "investigation_ID", id, "Student_ID", _studentID.ToString());
                }
                
            }
            if (!(bool)foreignСitizens.IsChecked)
            {
                DataBase.Delete("foreignC", "", "ID", hub[5].ToString());
                string id = DataBase.GetID("foreignC");
                if (id != "")
                {
                    DataBase.Update("Hub", "foreign_ID", id, "Student_ID", _studentID.ToString());
                }
                
            }

            var ures = System.Windows.MessageBox.Show("Данные обновленны.", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
            if (ures == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

        private void preventiveWork_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)preventiveWork.IsChecked)
            {
                IPR ipr = new IPR(this);
                ipr.Show();
            }
        }

        private void socialDanger_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)socialDanger.IsChecked)
            {
                SOP sop = new SOP(this);
                sop.Show();
            }
        }

        private void orphan_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)orphan.IsChecked)
            {
                OrphanForm of = new OrphanForm(this);
                of.Show();
            }
        }

        private void disabledPerson_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)disabledPerson.IsChecked)
            {
                DisabledForm df = new DisabledForm(this);
                df.Show();
            }
        }

        private void socialInvestigation_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)socialInvestigation.IsChecked)
            {
                InvestigationForm iform = new InvestigationForm(this);
                iform.Show();
            }
        }

        private void foreignСitizens_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)foreignСitizens.IsChecked)
            {
                ForeignCitForm fc = new ForeignCitForm(this);
                fc.Show();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AddGroup addGroup = new AddGroup(this);
            addGroup.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AddCurators ac = new AddCurators(this);
            ac.Show();
        }
    }
}
