using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Diagnostics;
using System.Management;


namespace RES_Timekeeper
{
    public static class SQLServerQueries
    {
        private readonly static string CONNECTION_STRING = "Server=KL-SQL-001;DataBase=RESSoftware;Integrated Security=SSPI";
        private readonly static int SOFTWARE_ID = 1310;
        
        public static void LogSession()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SessionRegister", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter output = new SqlParameter("@SessionID", 0);
                    output.Direction = ParameterDirection.Output;
                    command.Parameters.Add(output);
                    command.Parameters.Add(new SqlParameter("@SoftwareID", SOFTWARE_ID));
                    command.Parameters.Add(new SqlParameter("@MajorVersion", fileVersionInfo.ProductMajorPart));
                    command.Parameters.Add(new SqlParameter("@MinorVersion", fileVersionInfo.ProductMinorPart));
                    command.Parameters.Add(new SqlParameter("@Build", fileVersionInfo.ProductBuildPart));
                    command.Parameters.Add(new SqlParameter("@Revision", fileVersionInfo.ProductPrivatePart));
                    command.Parameters.Add(new SqlParameter("@Username", System.Security.Principal.WindowsIdentity.GetCurrent().Name));
                    command.Parameters.Add(new SqlParameter("@ProcessName", assembly.ManifestModule.Name));
                    command.Parameters.Add(new SqlParameter("@ExecutingFrom", assembly.Location));
                    command.Parameters.Add(new SqlParameter("@MachineName", System.Environment.MachineName));
                    command.Parameters.Add(new SqlParameter("@OSVersion", WindowsVersion()));
                    command.Parameters.Add(new SqlParameter("@RemoteUser", false));

                    command.ExecuteNonQuery();
                }
            }
        }

        static string WindowsVersion()
        {
            var name = (from x in new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get().OfType<ManagementObject>()
                        select x.GetPropertyValue("Caption")).FirstOrDefault();
            return name != null ? name.ToString() : "Unknown";
        }

        static public bool IsLatestVersion(out string executingVersion, out string availableVersion)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                string commandText = "SELECT VersionMajor, VersionMinor, VersionRevision FROM tblSoftwareVersions WHERE IsLive=1 AND SoftwareID=" + SOFTWARE_ID.ToString();
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        int dbMajor = reader.GetInt32(0);
                        int dbMinor = reader.GetInt32(1);
                        int dbRevision = reader.GetInt32(2);

                        executingVersion = string.Format("{0}.{1}.{2}", fileVersionInfo.ProductMajorPart, fileVersionInfo.ProductMinorPart, fileVersionInfo.ProductPrivatePart);
                        availableVersion = string.Format("{0}.{1}.{2}", dbMajor, dbMinor, dbRevision);

                        return (executingVersion == availableVersion);
                    }
                }
            }
        }
    }
}
