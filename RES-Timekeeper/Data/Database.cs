using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace RES_Timekeeper.Data
{
    public class Database
    {
        public Database()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
#if DEBUG
            string filename = System.IO.Path.Combine(folder, "RES-TimekeeperTEST.db");
#else
            string filename = System.IO.Path.Combine(folder, "RES-Timekeeper.db");
#endif
            Initialise(filename);
        }

        public Database(string filename)
        {
            Initialise(filename);
        }

        public IEnumerable<ProjectData> GetMostRecentUsedProjects()
        {
            List<ProjectData> projects = new List<ProjectData>();
            using (SQLiteConnection connection = GetAndOpenConnection())
            {
                string sqlText = "SELECT DISTINCT ID, Code, Title FROM tblProjects P ";
                sqlText += "INNER JOIN tblItems I ON P.ID=I.ProjectID WHERE P.Visible=1 ";
                sqlText += "ORDER BY I.StartTime DESC LIMIT 10;";

                using (SQLiteCommand cmd = new SQLiteCommand(sqlText, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            projects.Add(new ProjectData(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), true));
                        }
                    }
                }
            }
            return projects;
        }

            
        public IEnumerable<ProjectData> GetProjects(bool visibleProjectsOnly)
        {
            List<ProjectData> projects = new List<ProjectData>();
            using (SQLiteConnection connection = GetAndOpenConnection())
            {
                string sqlText;
                if (visibleProjectsOnly)
                    sqlText = string.Format("SELECT ID, Code, Title, Visible FROM tblProjects WHERE Visible = 1 ORDER BY CODE");
                else
                    sqlText = string.Format("SELECT ID, Code, Title, Visible FROM tblProjects ORDER BY CODE");
                using (SQLiteCommand cmd = new SQLiteCommand(sqlText, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            projects.Add(new ProjectData(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetBoolean(3)));
                        }
                    }
                }
            }
            return projects;
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
            using (SQLiteConnection connection = GetAndOpenConnection())
            {
                string sqlText = "SELECT ProjectID, StartTime, EndTime, Confirmed, Notes FROM tblItemsOLD";
                using (SQLiteCommand cmd = new SQLiteCommand(sqlText, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
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
            }
        }

        public IEnumerable<ItemData> GetItems(DateTime startTime, DateTime endTime)
        {
            List<ItemData> items = new List<ItemData>();
            using (SQLiteConnection connection = GetAndOpenConnection())
            {
                string sqlText = "SELECT ProjectID, StartTime, EndTime, Confirmed, Notes FROM tblItems WHERE StartTime>=@startTime AND EndTime<=@endTime";
                using (SQLiteCommand cmd = new SQLiteCommand(sqlText, connection))
                {
                    cmd.Parameters.AddWithValue("@startTime", startTime.ToFileTime());
                    cmd.Parameters.AddWithValue("@endTime", endTime.ToFileTime());
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime start = GetDateTime(reader.GetInt64(1));
                            DateTime end = GetDateTime(reader.GetInt64(2));
                            items.Add(new ItemData(reader.GetInt32(0), start, end, reader.GetBoolean(3), reader.GetString(4)));
                        }
                    }
                }
            }

            return items;
        }

        public IEnumerable<ItemData> GetItems(bool unconfirmedItemsOnly)
        {
            List<ItemData> items = new List<ItemData>();
            using (SQLiteConnection connection = GetAndOpenConnection())
            {
                string sqlText;
                if (unconfirmedItemsOnly)
                    sqlText = string.Format("SELECT ProjectID, StartTime, EndTime, Confirmed, Notes FROM tblItems WHERE Confirmed = 0 ORDER BY EndTime DESC");
                else
                    sqlText = string.Format("SELECT ProjectID, StartTime, EndTime, Confirmed, Notes FROM tblItems ORDER BY EndTime DESC");
                using (SQLiteCommand cmd = new SQLiteCommand(sqlText, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new ItemData(reader.GetInt32(0), GetDateTime(reader.GetInt64(1)), GetDateTime(reader.GetInt64(2)), reader.GetBoolean(3), reader.GetString(4)));
                        }
                    }
                }
            }
            return items;
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
            using (SQLiteConnection connection = GetAndOpenConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sqlCommand, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return new ItemData(reader.GetInt32(0), GetDateTime(reader.GetInt64(1)), GetDateTime(reader.GetInt64(2)), reader.GetBoolean(3), reader.GetString(4));
                        else
                            return null;
                    }
                }
            }
        }


        public void InsertProject(string code, string title)
        {
            using (SQLiteConnection connection = GetAndOpenConnection())
            {
                string sqlText = "INSERT INTO tblProjects VALUES (NULL, @code, @title, 1)";
                using (SQLiteCommand cmd = new SQLiteCommand(sqlText, connection))
                {
                    cmd.Parameters.AddWithValue("@code", code);
                    cmd.Parameters.AddWithValue("@title", title);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProject(int ID, string code, string title, bool visible)
        {
            using (SQLiteConnection connection = GetAndOpenConnection())
            {
                string sqlText = "UPDATE tblProjects SET Code=@code, Title=@title, Visible=@visible WHERE ID=@ID";
                using (SQLiteCommand cmd = new SQLiteCommand(sqlText, connection))
                {
                    cmd.Parameters.AddWithValue("@code", code);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@visible", visible);
                    cmd.Parameters.AddWithValue("@ID", ID);

                    cmd.ExecuteNonQuery();
                } 
            }
        }

        public void DeleteProject(int ID)
        {
            using (SQLiteConnection connection = GetAndOpenConnection())
            {
                string sqlText = "DELETE FROM tblProjects WHERE ID=@ID";
                using (SQLiteCommand cmd = new SQLiteCommand(sqlText, connection))
                {
                    cmd.Parameters.AddWithValue("@ID", ID);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertItem(int projectID, DateTime start, DateTime end, bool confirmed, string notes)
        {
            using (SQLiteConnection connection = GetAndOpenConnection())
            {
                InsertItem(connection, projectID, start, end, confirmed, notes);
            }
        }

        public void InsertItem(SQLiteConnection connection, int projectID, DateTime start, DateTime end, bool confirmed, string notes)
        {
            string sqlText = "INSERT INTO tblItems VALUES (@projectID, @startTime, @endTime, @confirmed, @notes)";
            using (SQLiteCommand cmd = new SQLiteCommand(sqlText, connection))
            {
                cmd.Parameters.AddWithValue("@projectID", projectID);
                cmd.Parameters.AddWithValue("@startTime", start.ToFileTime());
                cmd.Parameters.AddWithValue("@endTime", end.ToFileTime());
                cmd.Parameters.AddWithValue("@confirmed", confirmed);
                cmd.Parameters.AddWithValue("@notes", notes == null ? string.Empty : notes);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateItem(int projectID, DateTime originalStart, DateTime originalEnd, DateTime newStart, DateTime newEnd, bool confirmed, string notes)
        {
            using (SQLiteConnection connection = GetAndOpenConnection())
            {
                string sqlText = "UPDATE tblItems SET ProjectID=@projectID, Confirmed=@confirmed, Notes=@notes, StartTime=@startTime, EndTime=@endTime WHERE StartTime=@originalStartTime AND EndTime=@originalEndTime";
                using (SQLiteCommand cmd = new SQLiteCommand(sqlText, connection))
                {
                    cmd.Parameters.AddWithValue("@projectID", projectID);
                    cmd.Parameters.AddWithValue("@originalStartTime", originalStart.ToFileTime());
                    cmd.Parameters.AddWithValue("@originalEndTime", originalEnd.ToFileTime());
                    cmd.Parameters.AddWithValue("@startTime", newStart.ToFileTime());
                    cmd.Parameters.AddWithValue("@endTime", newEnd.ToFileTime());
                    cmd.Parameters.AddWithValue("@confirmed", confirmed);
                    cmd.Parameters.AddWithValue("@notes", notes == null ? string.Empty : notes);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteItem(DateTime startTime, DateTime endTime)
        {
            using (SQLiteConnection connection = GetAndOpenConnection())
            {
                string sqlText = "DELETE FROM tblItems WHERE StartTime=@startTime AND EndTime=@endTime";
                using (SQLiteCommand cmd = new SQLiteCommand(sqlText, connection))
                {
                    cmd.Parameters.AddWithValue("@startTime", startTime.ToFileTime());
                    cmd.Parameters.AddWithValue("@endTime", endTime.ToFileTime());

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void Initialise(string filename)
        {
            Filename = filename;

            if (!System.IO.File.Exists(filename))
                CreateDatabase();
        }

        private string Filename
        {
            get;
            set;
        }

        private void CreateDatabase()
        {
            using (SQLiteConnection connection = GetAndOpenConnection())
            {
                CreateProjectsTable(connection);
                CreateItemsTable(connection);
            }
        }

        private void CreateProjectsTable(SQLiteConnection connection)
        {
            string sqlText = "CREATE TABLE tblProjects(ID INTEGER PRIMARY KEY, Code nvarchar(16) NOT NULL, Title nvarchar(256) NOT NULL, Visible Bit NOT NULL)";
            ExecuteNonQuery(sqlText, connection);

            sqlText = string.Format("INSERT INTO tblProjects VALUES (NULL, 'LUNCH', 'Non working time', 1)");
            ExecuteNonQuery(sqlText, connection);
        }

        private void CreateItemsTable(SQLiteConnection connection)
        {
            string sqlText = "CREATE TABLE tblItems(ProjectID INTEGER NOT NULL, StartTime INTEGER NOT NULL, EndTime INTEGER NOT NULL, Confirmed Bit NOT NULL, Notes nvarchar(2000) NOT NULL, CONSTRAINT PK PRIMARY KEY (StartTime, EndTime), FOREIGN KEY(ProjectID) REFERENCES tblProjects(ID))";

            ExecuteNonQuery(sqlText, connection);
        }

        private SQLiteConnection GetAndOpenConnection()
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source=" + Filename);
            connection.Open();
            return connection;
        }

        private void ExecuteNonQuery(string sqlText, SQLiteConnection connection)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(sqlText, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
