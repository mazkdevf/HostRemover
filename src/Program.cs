using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HostsRemover
{
    class Program
    {
        private static WindowsPrincipal pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
        public static bool hasAdministrativeRight = pricipal.IsInRole(WindowsBuiltInRole.Administrator);
        static void Main(string[] args)
        {

            if (!hasAdministrativeRight)
            {
                RunElevated(Application.ExecutablePath);
                Environment.Exit(0);
            }
            aud.Host_Unlocker(@"C:\Windows\System32\drivers\etc\hosts");
        }

        public static bool RunElevated(string fileName)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.UseShellExecute = true;
            processInfo.Verb = "runas";
            processInfo.FileName = fileName;
            try
            {
                Process.Start(processInfo);
                return true;
            }
            catch (Win32Exception)
            {

            }
            return false;
        }
    }
}
