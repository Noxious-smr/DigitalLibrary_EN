using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;

namespace UI_Layer.Services
{
    public class CategoryImageService : IFileService
    {
        public string CopyFile(string filePath, string directory)
        {
            var fileName = filePath.Split('\\');
            var destFile = Environment.CurrentDirectory + @"\Books\" + @$"{directory}\" + fileName[fileName.Length - 1];
            var newDestFile = Environment.CurrentDirectory + @"\Books\" + @$"{directory}\" + DateOnly.FromDateTime(DateTime.Now).ToString("y_M_d_") + fileName[fileName.Length - 1];
            if (!Directory.Exists(Environment.CurrentDirectory + @"\Books\"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\Books\");
            }
            if (!Directory.Exists(Environment.CurrentDirectory + @"\Books\" + @$"{directory}"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\Books\" + @$"{directory}");
            }
            if (File.Exists(destFile))
            {
                //destFile = (Environment.CurrentDirectory + @"\Books\" + @$"{directory}\" + DateOnly.FromDateTime(DateTime.Now).ToString() + fileName[fileName.Length - 1]);
                File.Copy(filePath, newDestFile);
                string[] newDestFileParts = newDestFile.Split('\\');
                return newDestFileParts[newDestFileParts.Length - 1].Replace("'", "''");
            }
            else
            {
                File.Copy(filePath, destFile);
                return fileName[fileName.Length - 1].Replace("'", "''");
            }
            //catch (Exception)
            //{
            //    File.Replace(filePath, destFile, destFile+".bk");
            //}
            //return fileName[fileName.Length - 1].Replace("'", "''");
        }

        public void DeleteFiles(List<string> files)
        {
            foreach (var item in files)
            {
                File.Delete(item);
            }
        }

        public string FileRead(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrWhiteSpace(filePath))
            {
                return "Nothing To show";
            }
            else
                return Environment.CurrentDirectory + @"\Books\" + @"Categories\" + filePath;
        }

        public string GetFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.AddExtension = true;
            dlg.DefaultExt = ".png";
            dlg.Filter = "Images | *.jpg;*.jpeg;*.png|All Files | *";
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                return dlg.FileName;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
