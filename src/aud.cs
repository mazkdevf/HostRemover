using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace HostsRemover
{
    class aud
    {
        public static void Host_Unlocker(string path)
        {
            Title($"HostUnlocker x KeyAuth | Unlock / Delete | File: {path}");
            if (!File.Exists(path))
            {
                WriteColor("[HostUnlocker]: ", ("{[HostUnlocker]:}", ConsoleColor.Red));
                Console.WriteLine($"File Doesn't Exist...");
                Thread.Sleep(3500);
                Environment.Exit(0);
            }
            if (!Program.hasAdministrativeRight)
            {
                Program.RunElevated(Application.ExecutablePath);
                Environment.Exit(0);
            }
            WriteColor("[HostUnlocker]: ", ("{[HostUnlocker]:}", ConsoleColor.Cyan));
            Console.WriteLine("Please Wait Unlocking....");
            FileInfo insInfo = new FileInfo(path);
            FileSecurity insFileSecurity = insInfo.GetAccessControl();
            insFileSecurity.AddAccessRule(new FileSystemAccessRule(System.Environment.UserDomainName + "\\" + System.Environment.UserName, FileSystemRights.FullControl, AccessControlType.Allow));
            insInfo.SetAccessControl(insFileSecurity);
            File.Delete(path);
            Sleep(3);
            if (!File.Exists(path))
            {
                WriteColor("[HostUnlocker]: ", ("{[HostUnlocker]:}", ConsoleColor.Green));
                Console.WriteLine($"File have been successfully Unlocked and Deleted!");
            }
            Console.WriteLine("\n\n Closing in 10 Seconds...");
            Sleep(10);
            Environment.Exit(0);
        }

        public static void Title(string title)
        {
            Console.Title = title;
        }

        private static void Sleep(int segs)
        {
            if (segs < 1) return;
            DateTime _desired = DateTime.Now.AddSeconds(segs);
            while (DateTime.Now < _desired)
            {
                Application.DoEvents();
            }
        }

        private static void WriteColor(string str, params (string substring, ConsoleColor color)[] colors)
        {
            var words = Regex.Split(str, @"( )");

            foreach (var word in words)
            {
                (string substring, ConsoleColor color) cl = colors.FirstOrDefault(x => x.substring.Equals("{" + word + "}"));
                if (cl.substring != null)
                {
                    Console.ForegroundColor = cl.color;
                    Console.Write(cl.substring.Substring(1, cl.substring.Length - 2));
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(word);
                }
            }
        }
    }
}
