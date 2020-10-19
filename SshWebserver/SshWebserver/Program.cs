using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Renci.SshNet;
using System.Text.RegularExpressions;


namespace SshWebserver
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            string username = "root";
            string usernameamazon = "ubuntu";
            string hostname = "178.62.16.93";
            string hostname2 = "159.89.97.41";
            string hostname3 = "ec2-3-16-67-223.us-east-2.compute.amazonaws.com";
            //string password = "bAu60Lg52zk8zx6N";
            //int port = 22;
            string privatekeylocalfilepath = @"C:\Users\cerbe\source\repos\SshWebserver\SshWebserver\id_ed25519";
            string privatekeyamazon = @"C:\Users\cerbe\AppData\Local\Packages\CanonicalGroupLimited.UbuntuonWindows_79rhkp1fndgsc\LocalState\rootfs\home\ubuntu\.ssh\cerby.pem";
            string pathremotefile = "/var/log/nginx/accesslogcombined.log";
            string pathremotefile2 = "/var/log/nginx/accesslogcombined2.log";
            string pathremotefileamazon = "/var/log/nginx/accesslogcombined3.log";
            //string localfile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "access.log");
            string regexspression = "(?P<ipaddress>\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}) - - \\[(?P<dateandtime>\\d{2}\\/[a-z]{3}\\/\\d{4}:\\d{2}:\\d{2}:\\d{2} (\\+|\\-)\\d{4})\\] ((\"(GET|POST) )(?P<url>.+)(http\\/1\\.1")) (? P < statuscode >\d{ 3}) (? P < bytessent >\d +) (["](?P<refferer>(\-)|(.+))["])(["](?P<useragent>.+)["])";
            List<string> loglines = new List<string>();
            try
            {
                //ConnectAndDownload(privatekeylocalfilepath, username, hostname2, pathremotefile, "accesslogcombined.log");
                //ConnectAndDownload(privatekeylocalfilepath, username, hostname, pathremotefile2, "accesslogcombined2.log");
                //ConnectAndDownload(privatekeyamazon, usernameamazon, hostname3, pathremotefileamazon, "accesslogcombined3.log");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public static void ConnectAndDownload(string keypath, string username, string hostname, string remotefilepath, string localfilename)
        {
            PrivateKeyFile keyFile = new PrivateKeyFile(keypath);
            PrivateKeyFile[] keyFiles = new[] { keyFile };
            List<AuthenticationMethod> methods = new List<AuthenticationMethod>();
            methods.Add(new PrivateKeyAuthenticationMethod(username, keyFiles));
            ConnectionInfo con = new ConnectionInfo(hostname, username, methods.ToArray());
            using (SftpClient client = new SftpClient(con))
            {
                client.Connect();
                Console.WriteLine("connected to {0}", hostname);
                //methods
                using (Stream filestream = File.Create(@"C:\yek\"+localfilename))
                {
                    client.DownloadFile(remotefilepath, filestream);
                }
                client.Disconnect();
                Console.WriteLine("disconnected");
            }
        }
        
 
    }
}
