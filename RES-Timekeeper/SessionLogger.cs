using System;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Diagnostics;
using System.Management;
using System.Security.Principal;

namespace RES_Timekeeper
{
    public class SessionLogger
    {

        public static void Log(string connectionString, int softwareId)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SessionRegister", connection))
                {
                    var currentUser = WindowsIdentity.GetCurrent();

                    command.CommandType = CommandType.StoredProcedure;

                    var output = new SqlParameter("@SessionID", 0);
                    output.Direction = ParameterDirection.Output;
                    command.Parameters.Add(output);
                    command.Parameters.Add(new SqlParameter("@SoftwareID", softwareId));
                    command.Parameters.Add(new SqlParameter("@MajorVersion", fileVersionInfo.ProductMajorPart));
                    command.Parameters.Add(new SqlParameter("@MinorVersion", fileVersionInfo.ProductMinorPart));
                    command.Parameters.Add(new SqlParameter("@Build", fileVersionInfo.ProductBuildPart));
                    command.Parameters.Add(new SqlParameter("@Revision", fileVersionInfo.ProductPrivatePart));
                    command.Parameters.Add(new SqlParameter("@Username", currentUser.Name));
                    command.Parameters.Add(new SqlParameter("@ProcessName", assembly.ManifestModule.Name));
                    command.Parameters.Add(new SqlParameter("@ExecutingFrom", assembly.Location));
                    command.Parameters.Add(new SqlParameter("@MachineName", Environment.MachineName));
                    command.Parameters.Add(new SqlParameter("@OSVersion", WindowsVersion()));
                    command.Parameters.Add(new SqlParameter("@RemoteUser", false));

                    command.ExecuteNonQuery();
                }
            }
        }

        private static string WindowsVersion()
        {
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            var name = searcher.Get().OfType<ManagementObject>()
                .Select(x => x.GetPropertyValue("Caption"))
                .FirstOrDefault();

            return name != null ? name.ToString() : "Unknown";
        }
    }
}

