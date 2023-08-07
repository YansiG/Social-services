using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TalentedYouthProgect
{
    public partial class MainWindow : Window
    {

        private int? disabled_id, orphan_id, ipr_id, sop_id, investigation_id, foreign_id;
        private List<string> categories;

        private void makeReport_Click(object sender, RoutedEventArgs e)
        {
            ReportsPage rp = new ReportsPage();
            rp.Show();
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
                            for (int i = 1; i < 3; i++)
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

        public void ResetComboBox()
        {
            groupBox.Items.Clear();
            for (int i = 0; i < Sgroup().Count; i++) { groupBox.Items.Add(Sgroup().ToArray()[i]); }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            UpdateData();
            disabled_id = null; orphan_id = null; ipr_id = null; sop_id = null; investigation_id = null; foreign_id = null;
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void moreinfo_Click_1(object sender, RoutedEventArgs e)
        {
            if (listViewStudent.SelectedItems.Count > 0)
            {
                string key1 = "", key2 = "";
                try
                {
                    //key1 = (listViewStudent.SelectedItem as string[]).GetValue(0).ToString();
                    key2 = (listViewStudent.SelectedItem as string[]).GetValue(1).ToString();
                }
                catch (Exception ex) { System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
                string id_temp = DataBase.Read("Students", "Hub_ID", "name", key2);
                //SetId(Convert.ToInt32(DataBase.Read("SQLITE_SEQUENCE", "seq", "name", "Students")));
                SetId(Convert.ToInt32(id_temp));

                string result = "";

                if (disabled_id != null)
                {
                    result += "Инвалидность:\n";
                    using (var connection = new SqliteConnection("Data Source=data.db"))
                    {
                        connection.Open();
                        SqliteCommand command;
                        command = new SqliteCommand($"SELECT * FROM disabled WHERE ID = '{disabled_id}'", connection);
                        using (SqliteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) // если есть данные
                            {
                                while (reader.Read())   // построчно считываем данные
                                {
                                    for (int i = 1; i < 4; i++)
                                    {
                                        if (reader.GetString(i) == "True")
                                        {
                                            result += "ЧАЭС";
                                            continue;
                                        }
                                        if (reader.GetString(i) == "False")
                                        {
                                            continue;
                                        }
                                        result += reader.GetString(i) + " ";
                                    }
                                }
                            }
                        }
                    }
                    result += "\n";
                }
                if (orphan_id != null)
                {
                    result += "Сирота:\n";
                    using (var connection = new SqliteConnection("Data Source=data.db"))
                    {
                        connection.Open();
                        SqliteCommand command;
                        command = new SqliteCommand($"SELECT * FROM orphan WHERE ID = '{orphan_id}'", connection);
                        using (SqliteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) // если есть данные
                            {
                                while (reader.Read())   // построчно считываем данные
                                {
                                    for (int i = 1; i < 4; i++)
                                    {

                                        result += reader.GetString(i) + " ";
                                    }
                                }
                            }
                        }
                    }
                    result += "\n";
                }
                if (ipr_id != null)
                {
                    result += "ИПР:\n";
                    using (var connection = new SqliteConnection("Data Source=data.db"))
                    {
                        connection.Open();
                        SqliteCommand command;
                        command = new SqliteCommand($"SELECT * FROM ipr WHERE ID = '{ipr_id}'", connection);
                        using (SqliteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) // если есть данные
                            {
                                while (reader.Read())   // построчно считываем данные
                                {
                                    for (int i = 1; i < 4; i++)
                                    {

                                        result += reader.GetString(i) + " ";
                                    }
                                }
                            }
                        }
                    }
                    result += "\n";
                }
                if (sop_id != null)
                {
                    result += "СОП:\n";
                    using (var connection = new SqliteConnection("Data Source=data.db"))
                    {
                        connection.Open();
                        SqliteCommand command;
                        command = new SqliteCommand($"SELECT * FROM sop WHERE ID = '{sop_id}'", connection);
                        using (SqliteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) // если есть данные
                            {
                                while (reader.Read())   // построчно считываем данные
                                {
                                    for (int i = 1; i < 4; i++)
                                    {

                                        result += reader.GetString(i) + " ";
                                    }
                                }
                            }
                        }
                    }
                    result += "\n";
                }
                if (investigation_id != null)
                {
                    result += "Соц. расследование:\n";
                    using (var connection = new SqliteConnection("Data Source=data.db"))
                    {
                        connection.Open();
                        SqliteCommand command;
                        command = new SqliteCommand($"SELECT * FROM investigation WHERE ID = '{investigation_id}'", connection);
                        using (SqliteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) // если есть данные
                            {
                                while (reader.Read())   // построчно считываем данные
                                {
                                    for (int i = 1; i < 4; i++)
                                    {

                                        result += reader.GetString(i) + " ";
                                    }
                                }
                            }
                        }
                    }
                    result += "\n";
                }
                if (foreign_id != null)
                {
                    result += "Иностранный гражданин:\n";
                    using (var connection = new SqliteConnection("Data Source=data.db"))
                    {
                        connection.Open();
                        SqliteCommand command;
                        command = new SqliteCommand($"SELECT * FROM foreignC WHERE ID = '{foreign_id}'", connection);
                        using (SqliteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) // если есть данные
                            {
                                while (reader.Read())   // построчно считываем данные
                                {
                                    for (int i = 1; i < 4; i++)
                                    {

                                        result += reader.GetString(i) + " ";
                                    }
                                }
                            }
                        }
                    }
                    result += "\n";
                }
                System.Windows.MessageBox.Show(result, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                disabled_id = null; orphan_id = null; ipr_id = null; sop_id = null; investigation_id = null; foreign_id = null;
            }
        }

        private void search_Click_1(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text == "")
            {
                UpdateData();
            }
            else
            {
                listViewStudent.Items.Clear();

                var res = DataBase.Search("Students", "(name || birthday || admissionYear || studentGroup || сurator || residentialAddress || registrationAddress || mobile)", textBox1.Text);
                foreach (var item in res)
                {
                    listViewStudent.Items.Add(item.ToArray());
                }
            }
        }

        private void checkedListBoxCriteriaExcel_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (checkedListBoxCriteria.SelectedItems.Contains(iprI) && !categories.Contains("ipr_ID")) { categories.Add("ipr_ID"); } else if (!checkedListBoxCriteria.SelectedItems.Contains(iprI)) { categories.Remove("ipr_ID"); }
            if (checkedListBoxCriteria.SelectedItems.Contains(sopI) && !categories.Contains("sop_ID")) { categories.Add("sop_ID"); } else if (!checkedListBoxCriteria.SelectedItems.Contains(sopI)) { categories.Remove("sop_ID"); }
            if (checkedListBoxCriteria.SelectedItems.Contains(orphanI) && !categories.Contains("orphan_ID")) { categories.Add("orphan_ID"); } else if (!checkedListBoxCriteria.SelectedItems.Contains(orphanI)) { categories.Remove("orphan_ID"); }
            if (checkedListBoxCriteria.SelectedItems.Contains(disabledI) && !categories.Contains("disabled_ID")) { categories.Add("disabled_ID"); } else if (!checkedListBoxCriteria.SelectedItems.Contains(disabledI)) { categories.Remove("disabled_ID"); }
            if (checkedListBoxCriteria.SelectedItems.Contains(investigationI) && !categories.Contains("investigation_ID")) { categories.Add("investigation_ID"); } else if (!checkedListBoxCriteria.SelectedItems.Contains(investigationI)) { categories.Remove("investigation_ID"); }
            if (checkedListBoxCriteria.SelectedItems.Contains(foreignI) && !categories.Contains("foreign_ID")) { categories.Add("foreign_ID"); } else if (!checkedListBoxCriteria.SelectedItems.Contains(foreignI)) { categories.Remove("foreign_ID"); }
            UpdateData();
        }

        public MainWindow()
        {
            InitializeComponent();
            categories = new List<string>();
            UpdateData();
            ResetComboBox();
        }

        private void groupBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void curatorBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void ClickEdit_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            if (listViewStudent.SelectedItem == null)
            {
                return;
            }
            string selectId = DataBase.GetID("Students", "name", (listViewStudent.SelectedItem as string[]).GetValue(1).ToString(), "birthday", (listViewStudent.SelectedItem as string[]).GetValue(2).ToString());
            //MessageBox.Show(selectId);
            PropStudent ps = new PropStudent(this);
            try
            {
                ps.EditData(int.Parse(selectId));
                ps.Show();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Повторно выберите учащегося для редактирования или перезагрузите приложение.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetId(int id)
        {
            //disabled_id = Convert.ToInt32(DataBase.Read("Hub", "disabled_ID", "Student_ID", id.ToString()));
            disabled_id = Convert.ToInt32(DataBase.GetID("Hub", "disabled_ID", "Student_ID", id.ToString()));
            orphan_id = Convert.ToInt32(DataBase.GetID("Hub", "orphan_ID", "Student_ID", id.ToString()));
            ipr_id = Convert.ToInt32(DataBase.GetID("Hub", "ipr_ID", "Student_ID", id.ToString()));
            sop_id = Convert.ToInt32(DataBase.GetID("Hub", "sop_ID", "Student_ID", id.ToString()));
            foreign_id = Convert.ToInt32(DataBase.GetID("Hub", "foreign_ID", "Student_ID", id.ToString()));
            investigation_id = Convert.ToInt32(DataBase.GetID("Hub", "investigation_ID", "Student_ID", id.ToString()));
        }

        public void UpdateData()
        {
            if (categories.Count == 0 && groupBox.SelectedValue == null)
            {
                listViewStudent.Items.Clear();
                string sqlExpression = "SELECT * FROM Students";
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
                                for (int i = 0; i < 10; i++)
                                {
                                    result.Add(reader.GetString(i));
                                }
                                listViewStudent.Items.Add(result.ToArray());
                            }
                        }
                    }
                }
            }
            else
            {
                UpdateWithParams();
                //ResetComboBox();
            }

            listViewSecondTab.Items.Clear();
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
                            resultCur.Clear();
                            for (int i = 0; i < 4; i++)
                            {
                                resultCur.Add(reader.GetString(i));
                            }
                            listViewSecondTab.Items.Add(resultCur.ToArray());
                        }
                    }
                }
            }
        }

        public void UpdateWithParams()
        {
            IEnumerable<int> res;
            if (categories.Count > 0 && groupBox.SelectedValue == null)
            {
                listViewStudent.Items.Clear();
                string cat = "";
                for (int i = 0; i < categories.Count; i++)
                {
                    if (i == categories.Count - 1) { cat += categories[i]; continue; }
                    cat += categories[i] + " AND ";
                }
                res = DataBase.SearchNull("Hub", $"({cat})");
                for (int i = 0; i < res.Count(); i++)
                {
                    var result = DataBase.Search("Students", "(ID)", res.ToArray()[i].ToString());

                    foreach (var item in result)
                    {
                        listViewStudent.Items.Add(item.ToArray());
                    }

                }
            }
            else if (categories.Count > 0 && groupBox.SelectedValue != null)
            {
                listViewStudent.Items.Clear();
                string cat = "";
                for (int i = 0; i < categories.Count; i++)
                {
                    if (i == categories.Count - 1) { cat += categories[i]; continue; }
                    cat += categories[i] + " AND ";
                }
                res = DataBase.SearchNull("Hub", $"({cat})");
                for (int i = 0; i < res.Count(); i++)
                {
                    var result = DataBase.Search("Students", "(ID)", res.ToArray()[i].ToString());
                    foreach (var item in result)
                    {
                        if (item.Contains(groupBox.SelectedValue.ToString()))
                        {
                            listViewStudent.Items.Add(item.ToArray());
                        }
                    }
                }
            }
            else if (categories.Count == 0 && groupBox.SelectedValue != null)
            {
                listViewStudent.Items.Clear();
                var items = DataBase.Search("Students", "StudentGroup", groupBox.SelectedValue.ToString());
                foreach (var item in items)
                {
                    listViewStudent.Items.Add(item.ToArray());
                }
            }
            else if (categories.Count == 0 && groupBox.SelectedValue == null)
            {
                ResetComboBox();
            }
            else
            {
                listViewStudent.Items.Clear();
                string cat = "";
                for (int i = 0; i < categories.Count; i++)
                {
                    if (i == categories.Count - 1) { cat += categories[i]; continue; }
                    cat += categories[i] + " AND ";
                }
                res = DataBase.SearchNull("Hub", $"({cat})");
                for (int i = 0; i < res.Count(); i++)
                {
                    var result = DataBase.Search("Students", "(ID)", res.ToArray()[i].ToString());

                    foreach (var item in result)
                    {
                        try
                        {
                            if (item.Contains(groupBox?.SelectedValue.ToString()))
                            {
                                listViewStudent.Items.Add(item.ToArray());
                            }
                        }
                        catch
                        {
                            groupBox.SelectedValue = null;
                            UpdateData();
                        }

                    }

                }
            }
        }

        private void AddNewStudent_Click(object sender, RoutedEventArgs e)
        {
            PropStudent ps = new PropStudent(this);
            ps.Show();
        }

        //    private void DeleteStudent_Click(object sender, RoutedEventArgs e)
        //    {
        //        //MessageBox.Show((listViewStudent.SelectedItem as string[]).GetValue(1).ToString());

        //        if (listViewStudent.SelectedItems.Count > 0)
        //        {
        //            string key1 = "", key2 = "";
        //            try
        //            {
        //                key1 = (listViewStudent.SelectedItem as string[]).GetValue(1).ToString();
        //                key2 = (listViewStudent.SelectedItem as string[]).GetValue(2).ToString();
        //            }
        //            catch (Exception ex)
        //            {
        //                System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //            }

        //            listViewStudent.Items.Remove(listViewStudent.SelectedItem as string[]);

        //            SetId(Convert.ToInt32(DataBase.Read("SQLITE_SEQUENCE", "seq", "name", "Students")));

        //            try
        //            {
        //                DataBase.Delete("Hub", "", "Student_ID", DataBase.GetID("Students", "name", key1, "birthday", key2));
        //                DataBase.Delete("Students", "", "ID", DataBase.GetID("Students", "name", key1, "birthday", key2));
        //            }
        //            catch (Exception ex)
        //            {
        //                System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //            }

        //            //DataBase.Delete("disabled", "", "ID", disabled_id.ToString());
        //            //DataBase.Delete("orphan", "", "ID", orphan_id.ToString());
        //            //DataBase.Delete("ipr", "", "ID", ipr_id.ToString());
        //            //DataBase.Delete("sop", "", "ID", sop_id.ToString());
        //            //DataBase.Delete("investigation", "", "ID", investigation_id.ToString());
        //            //DataBase.Delete("foreignC", "", "ID", foreign_id.ToString());
        //        }

        //        if (listViewSecondTab.SelectedItems.Count > 0)
        //        {
        //            string key1 = "", key2 = "";
        //            try
        //            {
        //                key1 = (listViewSecondTab.SelectedItem as string[]).GetValue(1).ToString();
        //                key2 = (listViewSecondTab.SelectedItem as string[]).GetValue(2).ToString();
        //            }
        //            catch (Exception ex) { System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }

        //            listViewSecondTab.Items.Remove(listViewSecondTab.SelectedItem as string[]);

        //            try
        //            {
        //                DataBase.Delete("Curators", "", "ID", DataBase.GetID("Curators", "Name", key1, "Surname", key2));
        //            }
        //            catch (Exception ex) { System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        //        }
        //        disabled_id = null; orphan_id = null; ipr_id = null; sop_id = null; investigation_id = null; foreign_id = null;
        //    }
    }
}


