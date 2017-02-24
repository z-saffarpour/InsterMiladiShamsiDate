using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace InsterMiladiShamsiDate
{
    class Program
    {
        static void Main(string[] args)
        {
            var date = new DateTime(1960, 1, 1);
            var calender = new PersianCalendar();
            using (var connection = new SqlConnection("Data Source=127.0.0.1;Initial Catalog=FasicoMang;Persist Security Info=True;User ID=sa;Password=123456"))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    while (date <= new DateTime(2050, 1, 1))
                    {
                        var persianDate = calender.GetYear(date).ToString("0000") + "/" + calender.GetMonth(date).ToString("00") + "/" + calender.GetDayOfMonth(date).ToString("00");
                        command.CommandText = "INSERT INTO MANG.TBL_DateConverter( miladi, shamsi ) VALUES  ( CONVERT(DATE,'" + date.ToShortDateString() + "'),'" + persianDate + "'  )";
                        command.ExecuteNonQuery();
                        date = date.AddDays(1);
                    }
                }
                connection.Close();
            }
            Console.ReadLine();
        }
    }
}
