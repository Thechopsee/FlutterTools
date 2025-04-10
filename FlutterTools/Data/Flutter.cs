using FlutterTools.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterTools.Data
{
    public class Flutter
    {
        public string Version { get; set; }

        public string DartVersion { get; set; }

        public string DevToolsVersion { get; set; }


        public void parseVersionFromCMDOutput(string cmdOutput)
        {
            string[] splitted=cmdOutput.Split(' ');
            if (splitted.Length > 0)
            {
                if (splitted[0].Equals("Flutter")){
                    int last=splitted.Length-1;
                    Version = splitted[1];
                    DevToolsVersion = splitted[last];
                    DartVersion = splitted[last-3];
                }
                else
                {
                    throw new VersionCantBeResolvedException();
                }
            }
            else {
                throw new VersionCantBeResolvedException();
            }
        }
        public void parseDoctorFromCMDOutput(string cmdOutput)
        {
            string[] splitted = cmdOutput.Split("\n");
        }
        public void PrintInfo()
        {
            Console.WriteLine("Flutter version: " + Version);
            Console.WriteLine("Dart version: " + DartVersion);
            Console.WriteLine("DevTools version: " + DevToolsVersion);
        }
    }
}
