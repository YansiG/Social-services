using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;

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
            var yearsArr = Enumerable.Range(1995, DateTime.Now.Year-1994).Reverse();
            y1.ItemsSource = yearsArr;
            y2.ItemsSource = yearsArr;
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
            try
            {
                size = DataBase.findByids(int.Parse(y1.SelectedValue.ToString()), int.Parse(y2.SelectedValue.ToString()));
            }
            catch(System.Exception ex)
            {
                MessageBox.Show("Неверная дата отчета! "+ex,"Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            for (int i = 0; i < size.Count; i++)
            {
                var resultBD = DataBase.Search("Students", "ID", size[i].ToString());// DataBase.Read("Students", "*", "ID", size[i].ToString());
                List<List<string>> result = resultBD.ToList();
                DataRow row = dt.NewRow();
                row["name2"] = result[0][1];
                row["birthday"] = result[0][2];
                row["studentGroup1"] = result[0][4];
                row["admissionYear"] = result[0][3];
                row["сurator"] = result[0][5];
                row["residentialAddress"] = result[0][6];
                row["registrationAddress"] = result[0][7];
                row["mobile"] = result[0][8];
                dt.Rows.Add(row);
            }

            reportView.LocalReport.DataSources.Clear();
            reportView.LocalReport.ReportPath = "StudentReport.rdlc";
            reportView.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("ReportHat", $"Отчет о студентах состоящих на учёте за {y1.SelectedValue.ToString()}-{y2.SelectedValue.ToString()} учебный год."));
            reportView.LocalReport.SetParameters(reportParameters);

            reportView.LocalReport.Refresh();
            reportView.RefreshReport();
        }
    }
}
