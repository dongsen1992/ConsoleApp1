using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        private String m_szFeedback; // hold feedback data
        private Object m_objLock; // lock object
        private Boolean m_blnDoRead; // boolean value keeping up the read (may be used to interrupt the reading process)

        static void Main(string[] args)
        {
        //    ProcessStartInfo psi = new ProcessStartInfo(@"C:\Windows\System32\cmd");
        //    psi.RedirectStandardInput = true;
        //    psi.RedirectStandardOutput = true;
        //    psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
        //    psi.UseShellExecute = false;
        //    psi.CreateNoWindow = false;

        //    Process process = Process.Start(psi);

        ////   Program.m_objLock = new Object();
        // //   m_blnDoRead = true;

        //    //string cmdForTunnel = "pscp -pw deltatau D:\\资料\\混联机器人\\上位PC端\\prog11.pmc root@192.168.0.200:\"/var/ftp/usrflash/Project/prog11.pmc\"";
        //    string cmdForTunnel = "C:\\plink.exe -batch -pw deltatau root@192.168.0.200 \"/var/ftp/usrflash/Project/pmac_cmd prog11.pmc\"";
        //    process.StandardInput.WriteLine(cmdForTunnel);
        //    // process.WaitForExit();
        //    Thread.Sleep(30000);
        //    string output = process.StandardOutput.ReadToEnd();

        //    Console.WriteLine(output);



            ProcessStartInfo psi = new ProcessStartInfo()
            {
                ///FileName = PLINK_PATH, // A const or a readonly string that points to the plink executable
                //Arguments = String.Format("-ssh {0}@{1} -pw {2}", userName, remoteHost, password),
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process p = Process.Start(psi);

            m_objLock = new Object();
            m_blnDoRead = true;

            AsyncReadFeedback(p.StandardOutput); // start the async read of stdout
            AsyncReadFeedback(p.StandardError); // start the async read of stderr

            StreamWriter strw = p.StandardInput;

           
                strw.WriteLine(cmd); // send commands
            
            strw.WriteLine("exit"); // send exit command at the end

            p.WaitForExit(); // block thread until remote operations are done
        }
    }
}
