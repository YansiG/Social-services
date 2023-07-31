using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TalentedYouthProgect
{
    /// <summary>
    /// Логика взаимодействия для ReportsPage.xaml
    /// </summary>
    public partial class ReportsPage : Window
    {
        public ReportsPage()
        {
            InitializeComponent();
        }

        private void buttonReport_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("name2");
            dt.Columns.Add("birthday");
            dt.Columns.Add("studentGroup1");
            dt.Columns.Add("admissionYear");
            dt.Columns.Add("сurator");
            dt.Columns.Add("residentialAddress");
            dt.Columns.Add("registrationAddress");
            dt.Columns.Add("mobile");
            List<int> size;

            DateTime startDate = datePickerStart.SelectedDate ?? DateTime.MinValue;
            DateTime endDate = datePickerEnd.SelectedDate ?? DateTime.MaxValue;
            try
            {
                size = DataBase.findByids(startDate, endDate, (districtComboBox.SelectedItem as ComboBoxItem)?.Content.ToString());
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Неверная дата отчета! " + ex, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (!string.IsNullOrEmpty((sCategory.SelectedItem as ComboBoxItem)?.Content.ToString()))
            {
                IEnumerable<int> CategoryRes;
                string query = "";
                switch ((sCategory.SelectedItem as ComboBoxItem)?.Content.ToString())
                {
                    case "ИПР":
                        query += "ipr_ID";
                        break;
                    case "СОП":
                        query += "sop_ID";
                        break;
                    case "Дети - сироты":
                        query += "orphan_ID";
                        break;
                    case "Дети - инвалиды":
                        query += "disabled_ID";
                        break;
                    case "Социальное расследование":
                        query += "investigation_ID";
                        break;
                    case "Инностранные граждане":
                        query += "foreign_ID";
                        break;
                    default:
                        break;
                }
                CategoryRes = DataBase.SearchNull("Hub", query);
                size = size.Intersect(CategoryRes).ToList();
            }

            for (int i = 0; i < size.Count; i++)
            {
                var resultBD = DataBase.Search("Students", "ID", size[i].ToString());// DataBase.Read("Students", "*", "ID", size[i].ToString());
                List<List<string>> result = resultBD.ToList();
                DataRow row = dt.NewRow();
                row["name2"] = result[0][1];
                row["birthday"] = result[0][2];
                row["studentGroup1"] = result[0][4];
                row["admissionYear"] = DateTime.ParseExact(result[0][3], "yyyy/MM/dd", null).ToString("dd/MM/yyyy");
                row["сurator"] = result[0][5];
                row["residentialAddress"] = result[0][6];
                row["registrationAddress"] = result[0][7];
                row["mobile"] = result[0][9];
                dt.Rows.Add(row);
            }

            reportView.LocalReport.DataSources.Clear();
            reportView.LocalReport.ReportPath = "StudentReport.rdlc";
            reportView.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("ReportHat", $"Отчет о студентах поступивших c {startDate.ToShortDateString()} по {endDate.ToShortDateString()}."));
            reportView.LocalReport.SetParameters(reportParameters);

            reportView.LocalReport.Refresh();
            reportView.RefreshReport();
        }
    }
}
