using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Layer.Services
{
    public interface IFileService
    {
        public string GetFile();
        
        public string FileRead(string filePath);
       
        public string CopyFile(string filePath, string directory);
        public void DeleteFiles(List<string> files);
       
    }
}
