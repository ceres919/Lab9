using Avalonia;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineScoreboardOfFlights.Models
{
    public class DataBaseLoader
    {
        public ObservableCollection<DepartureTableItem> departuresCollection = new ObservableCollection<DepartureTableItem>();
        public ObservableCollection<ArrivalTableItem> arrivalsCollection = new ObservableCollection<ArrivalTableItem>();

        public DataBaseLoader() { }
        public void Load()
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=../../../flyyyyy.db"))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM Departure", connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                foreach(DataTable table in dataSet.Tables)
                {
                    foreach(DataRow row in table.Rows)
                    {
                        var cells = row.ItemArray;
                        DepartureTableItem item = new DepartureTableItem(cells[0].ToString(), cells[1].ToString(), cells[2].ToString(), DateTime.Parse(cells[3].ToString()), DateTime.Parse(cells[4].ToString()),
                            cells[5].ToString(), cells[9].ToString(), cells[10].ToString(), cells[12].ToString(), DateTime.Parse(cells[6].ToString()), DateTime.Parse(cells[7].ToString()), cells[8].ToString(), cells[11].ToString());
                        departuresCollection.Add(item);
                    }
                }

                adapter = new SQLiteDataAdapter("SELECT * FROM Arrival", connection);
                dataSet = new DataSet();
                adapter.Fill(dataSet);
                foreach (DataTable table in dataSet.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        var cells = row.ItemArray;
                        ArrivalTableItem item = new ArrivalTableItem(cells[0].ToString(), cells[1].ToString(), cells[2].ToString(), DateTime.Parse(cells[3].ToString()), DateTime.Parse(cells[4].ToString()),
                            cells[5].ToString(), cells[6].ToString(), cells[7].ToString(), cells[9].ToString(), cells[8].ToString());
                        arrivalsCollection.Add(item);
                    }
                }
                connection.Close();
            }
        }

    }
}
