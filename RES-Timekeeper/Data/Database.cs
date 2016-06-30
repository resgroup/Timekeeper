using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;

namespace RES_Timekeeper.Data
{
    public class Database
    {
        public string DatabasePath { get; }

        public Database() : this(GetDefaultDatabase()) { }
        public Database(string databasePath)
        {
            this.DatabasePath = databasePath;
            if (!File.Exists(this.DatabasePath))
            {
                CreateDatabase();
            }
        }

        public static string GetDefaultDatabase()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
#if DEBUG
            string filename = Path.Combine(folder, "RES-TimekeeperTEST.db");
#else
            string filename = System.IO.Path.Combine(folder, "RES-Timekeeper.db");
#endif
            return filename;
        }

        public IEnumerable<ProjectData> GetMostRecentUsedProjects()
        {
            string sqlText = @"SELECT DISTINCT ID, Code, Title FROM tblProjects P 
                                    INNER JOIN tblItems I 
                                        ON P.ID=I.ProjectID WHERE P.Visible=1
                               ORDER BY I.StartTime DESC LIMIT 10;
            ";


            using (var connection = GetAndOpenConnection())
            using (var command = new SQLiteCommand(sqlText, connection))
            using (var reader = command.ExecuteReader())
            {
                var projects = new List<ProjectData>();
                while (reader.Read())
                {
                    projects.Add(new ProjectData(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), true));
                }

                return projects;
            }
        }


        public IEnumerable<ProjectData> GetProjects(bool visibleProjectsOnly)
        {
            string sqlText;
            if (visibleProjectsOnly)
            {
                sqlText = string.Format("SELECT ID, Code, Title, Visible FROM tblProjects WHERE Visible = 1 ORDER BY CODE");
            }
            else
            {
                sqlText = string.Format("SELECT ID, Code, Title, Visible FROM tblProjects ORDER BY CODE");
            }

            using (var connection = GetAndOpenConnection())
            using (var cmd = new SQLiteCommand(sqlText, connection))
            using (var reader = cmd.ExecuteReader())
            {
                var projects = new List<ProjectData>();
                while (reader.Read())
                {
                    projects.Add(new ProjectData(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetBoolean(3)));
                }
                return projects;
            }
        }

        private static DateTime GetDateTime(string val)
        {
            return DateTime.Parse(val);
        }

        private static DateTime GetDateTime(double val)
        {
            return DateTime.FromOADate(val);
        }

        private static DateTime GetDateTime(long val)
        {
            return DateTime.FromFileTime(val);
        }

        public void CorrectStringDates()
        {
            string sqlText = "SELECT ProjectID, StartTime, EndTime, Confirmed, Notes FROM tblItemsOLD";
            using (var connection = GetAndOpenConnection())
            using (var command = new SQLiteCommand(sqlText, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int projectID = reader.GetInt32(0);
                    string startStr = reader.GetString(1);
                    string endStr = reader.GetString(2);
                    DateTime startDT = GetDateTime(startStr);
                    DateTime endDT = GetDateTime(endStr);
                    bool confirmed = reader.GetBoolean(3);
                    string notes = reader.GetString(4);

                    InsertItem(connection, projectID, startDT, endDT, confirmed, notes);
                }
            }
        }

        public IEnumerable<ItemData> GetItems(DateTime startTime, DateTime endTime)
        {
            string sqlText = "SELECT ProjectID, StartTime, EndTime, Confirmed, Notes FROM tblItems WHERE StartTime>=@startTime AND EndTime<=@endTime";

            using (var connection = GetAndOpenConnection())
            using (var command = new SQLiteCommand(sqlText, connection))
            {
                command.Parameters.AddWithValue("@startTime", startTime.ToFileTime());
                command.Parameters.AddWithValue("@endTime", endTime.ToFileTime());
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    var items = new List<ItemData>();
                    while (reader.Read())
                    {
                        var start = GetDateTime(reader.GetInt64(1));
                        var end = GetDateTime(reader.GetInt64(2));
                        items.Add(new ItemData(reader.GetInt32(0), start, end, reader.GetBoolean(3), reader.GetString(4)));
                    }
                    return items;
                }
            }
        }

        public IEnumerable<ItemData> GetItems(bool unconfirmedItemsOnly)
        {
            string sqlText;
            if (unconfirmedItemsOnly)
            {
                sqlText = "SELECT ProjectID, StartTime, EndTime, Confirmed, Notes FROM tblItems WHERE Confirmed = 0 ORDER BY EndTime DESC";
            }
            else
            {
                sqlText = "SELECT ProjectID, StartTime, EndTime, Confirmed, Notes FROM tblItems ORDER BY EndTime DESC";
            }
            using (var connection = GetAndOpenConnection())
            using (var command = new SQLiteCommand(sqlText, connection))
            using (var reader = command.ExecuteReader())
            {
                var items = new List<ItemData>();
                while (reader.Read())
                {
                    items.Add(new ItemData(reader.GetInt32(0), GetDateTime(reader.GetInt64(1)), GetDateTime(reader.GetInt64(2)), reader.GetBoolean(3), reader.GetString(4)));
                }
                return items;
            }
        }


        public ItemData GetMostRecentItem()
        {
            return GetItem("SELECT ProjectID, StartTime, EndTime, Confirmed, Notes FROM tblItems ORDER BY EndTime DESC LIMIT 1");
        }

        public ItemData GetLeastRecentItem()
        {
            return GetItem("SELECT ProjectID, StartTime, EndTime, Confirmed, Notes FROM tblItems ORDER BY EndTime LIMIT 1");
        }

        private ItemData GetItem(string sqlCommand)
        {
            using (var connection = GetAndOpenConnection())
            using (var command = new SQLiteCommand(sqlCommand, connection))
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new ItemData(reader.GetInt32(0), GetDateTime(reader.GetInt64(1)), GetDateTime(reader.GetInt64(2)), reader.GetBoolean(3), reader.GetString(4));
                }
                else
                {
                    return null;
                }
            }
        }


        public void InsertProject(string code, string title)
        {
            string sqlText = "INSERT INTO tblProjects VALUES (NULL, @code, @title, 1)";
            using (var connection = GetAndOpenConnection())
            using (var command = new SQLiteCommand(sqlText, connection))
            {
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@title", title);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateProject(int ID, string code, string title, bool visible)
        {
            string sqlText = "UPDATE tblProjects SET Code=@code, Title=@title, Visible=@visible WHERE ID=@ID";
            using (var connection = GetAndOpenConnection())
            using (var command = new SQLiteCommand(sqlText, connection))
            {
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@visible", visible);
                command.Parameters.AddWithValue("@ID", ID);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteProject(int ID)
        {
            string sqlText = "DELETE FROM tblProjects WHERE ID=@ID";
            using (var connection = GetAndOpenConnection())
            using (var command = new SQLiteCommand(sqlText, connection))
            {
                command.Parameters.AddWithValue("@ID", ID);

                command.ExecuteNonQuery();
            }
        }

        public void InsertItem(int projectID, DateTime start, DateTime end, bool confirmed, string notes)
        {
            using (var connection = GetAndOpenConnection())
            {
                InsertItem(connection, projectID, start, end, confirmed, notes);
            }
        }

        public void InsertItem(SQLiteConnection connection, int projectID, DateTime start, DateTime end, bool confirmed, string notes)
        {
            string sqlText = "INSERT INTO tblItems VALUES (@projectID, @startTime, @endTime, @confirmed, @notes)";
            using (var command = new SQLiteCommand(sqlText, connection))
            {
                command.Parameters.AddWithValue("@projectID", projectID);
                command.Parameters.AddWithValue("@startTime", start.ToFileTime());
                command.Parameters.AddWithValue("@endTime", end.ToFileTime());
                command.Parameters.AddWithValue("@confirmed", confirmed);
                command.Parameters.AddWithValue("@notes", notes == null ? string.Empty : notes);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateItem(int projectID, DateTime originalStart, DateTime originalEnd, DateTime newStart, DateTime newEnd, bool confirmed, string notes)
        {
            string sqlText = "UPDATE tblItems SET ProjectID=@projectID, Confirmed=@confirmed, Notes=@notes, StartTime=@startTime, EndTime=@endTime WHERE StartTime=@originalStartTime AND EndTime=@originalEndTime";
            using (var connection = GetAndOpenConnection())
            using (var command = new SQLiteCommand(sqlText, connection))
            {
                command.Parameters.AddWithValue("@projectID", projectID);
                command.Parameters.AddWithValue("@originalStartTime", originalStart.ToFileTime());
                command.Parameters.AddWithValue("@originalEndTime", originalEnd.ToFileTime());
                command.Parameters.AddWithValue("@startTime", newStart.ToFileTime());
                command.Parameters.AddWithValue("@endTime", newEnd.ToFileTime());
                command.Parameters.AddWithValue("@confirmed", confirmed);
                command.Parameters.AddWithValue("@notes", notes == null ? string.Empty : notes);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteItem(DateTime startTime, DateTime endTime)
        {
            string sqlText = "DELETE FROM tblItems WHERE StartTime=@startTime AND EndTime=@endTime";
            using (var connection = GetAndOpenConnection())
            using (var command = new SQLiteCommand(sqlText, connection))
            {
                command.Parameters.AddWithValue("@startTime", startTime.ToFileTime());
                command.Parameters.AddWithValue("@endTime", endTime.ToFileTime());

                command.ExecuteNonQuery();
            }
        }


        private void CreateDatabase()
        {
            using (var connection = GetAndOpenConnection())
            {
                CreateProjectsTable(connection);
                CreateItemsTable(connection);
            }
        }

        private void CreateProjectsTable(SQLiteConnection connection)
        {
            string sqlCreate = "CREATE TABLE tblProjects(ID INTEGER PRIMARY KEY, Code nvarchar(16) NOT NULL, Title nvarchar(256) NOT NULL, Visible Bit NOT NULL)";
            ExecuteNonQuery(sqlCreate, connection);

            var sqlInsertDefaultProject = string.Format("INSERT INTO tblProjects VALUES (NULL, 'LUNCH', 'Non working time', 1)");
            ExecuteNonQuery(sqlInsertDefaultProject, connection);
        }

        private void CreateItemsTable(SQLiteConnection connection)
        {
            string sqlText = "CREATE TABLE tblItems(ProjectID INTEGER NOT NULL, StartTime INTEGER NOT NULL, EndTime INTEGER NOT NULL, Confirmed Bit NOT NULL, Notes nvarchar(2000) NOT NULL, CONSTRAINT PK PRIMARY KEY (StartTime, EndTime), FOREIGN KEY(ProjectID) REFERENCES tblProjects(ID))";
            ExecuteNonQuery(sqlText, connection);
        }

        private SQLiteConnection GetAndOpenConnection()
        {
            var connection = new SQLiteConnection("Data Source=" + DatabasePath);
            connection.Open();
            return connection;
        }

        private void ExecuteNonQuery(string sqlText, SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(sqlText, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
