﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FileConductor;
using FileConductor.Helpers;
using FileConductor.Operations;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = GetFileList();

            string myRegex = "*.csv";
            Regex pattern = new Regex("^" + myRegex.Replace("*", ".*") + "$");
           

          /*  List<string> result = files.Where(x=>pattern.IsMatch(x)).ToList();*/
            Send(new TargetTransformData("ftp://localhost/", "", "", ""),
                new List<string>()
                {
                    @"C:\Git\FileConductor\FileConductorNew\FileConductor\src\FileConductor\bin\Debug\Files\Files123.csv"
                });
            
        // Receive(new TargetTransformData("ftp://localhost/", "", "", ""),ProxyFile.ProxyPath,"*csv");
        }




        public static void Send(TargetTransformData targetData, List<string> files)
        {
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                var pathName = targetData.IpAddress;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(pathName+ fileName);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential();

                System.IO.FileInfo fi = new System.IO.FileInfo(file);
                request.ContentLength = fi.Length;
                byte[] buffer = new byte[4097];
                int bytes = 0;
                int total_bytes = (int)fi.Length;
                using (System.IO.FileStream fs = fi.OpenRead())
                using (System.IO.Stream rs = request.GetRequestStream())
                {
                    while (total_bytes > 0)
                    {
                        bytes = fs.Read(buffer, 0, buffer.Length);
                        rs.Write(buffer, 0, bytes);
                        total_bytes = total_bytes - bytes;
                    }
                }

                using (FtpWebResponse uploadResponse = (FtpWebResponse) request.GetResponse())
                {
                    var response = uploadResponse.StatusDescription;
                }

                File.Delete(file);
                              
            }

        }

        public static List<string> Receive(TargetTransformData sourceData, string targetPath, string regex)
        {

            var allFiles = GetFileList();

            string myRegex = "*elo.csv";
            Regex pattern = new Regex("^" + myRegex.Replace("*", ".*") + "$");

            List<string> files = allFiles.Where(x => pattern.IsMatch(x)).ToList();

            string ip = sourceData.IpAddress;

            List<string> result = new List<string>();
            foreach (var file in files)
            {
                var pathName = ip + file;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(pathName);

                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential();

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream))
                using (StreamWriter destination = new StreamWriter(@"C:\Destiny\"+file))
                {
                    destination.Write(reader.ReadToEnd());
                    destination.Flush();
                }

                request = (FtpWebRequest)WebRequest.Create(pathName);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential();
                using ((FtpWebResponse)request.GetResponse()){}
            }
            return result;
        }

        public static string[] GetFileList()
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            WebResponse response = null;
            StreamReader reader = null;

            try
            {
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://localhost/"));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential();
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                reqFTP.Proxy = null;
                reqFTP.KeepAlive = false;
                reqFTP.UsePassive = false;
                response = reqFTP.GetResponse();
                reader = new StreamReader(response.GetResponseStream());

                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                return result.ToString().Split('\n');
            }
            catch(Exception)
            {
                reader?.Close();
                response?.Close();
                downloadFiles = null;
                return downloadFiles;
            }
        }

    }
}
