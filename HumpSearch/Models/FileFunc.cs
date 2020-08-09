using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumpSearch.Models
{
    public class FileFunc
    {
        public List<FileProp> GetFileList(FileSearchCriteria fileSearchCriteria)
        {
            List<FileProp> fileProps = new List<FileProp>();

            if (fileSearchCriteria.isNameSearch && !fileSearchCriteria.isContentSearch)
            {
                string[] allFiles = Directory.GetFiles(fileSearchCriteria.folderPath, $"*{fileSearchCriteria.searchKeyWord}*", SearchOption.AllDirectories);

                GetNameSearchResult(allFiles, fileProps, fileSearchCriteria);

            }
            else if(fileSearchCriteria.isNameSearch && fileSearchCriteria.isContentSearch)
            {
                string[] allFiles = Directory.GetFiles(fileSearchCriteria.folderPath, "*", SearchOption.AllDirectories);

                GetNameSearchResult(allFiles, fileProps, fileSearchCriteria);

                GetContentSearchResult(allFiles, fileProps, fileSearchCriteria);

            }
            else if(!fileSearchCriteria.isNameSearch && fileSearchCriteria.isContentSearch)
            {
                string[] allFiles = Directory.GetFiles(fileSearchCriteria.folderPath, "*", SearchOption.AllDirectories);

                GetContentSearchResult(allFiles, fileProps, fileSearchCriteria);
            }
            else
            {
                // nothing to do
            }

            return fileProps;

        }

        private void GetNameSearchResult(string[] allFiles, List<FileProp> fileProps, FileSearchCriteria fileSearchCriteria)
        {
            foreach (string eachFile in allFiles.Where(x => x.ToLower().Contains(fileSearchCriteria.searchKeyWord.ToLower())))
            {
                FileInfo fileInfo = new FileInfo(eachFile);

                fileProps.Add(new FileProp()
                {
                    FullPath = eachFile,
                    Name = GetCleanFileName(eachFile, fileSearchCriteria.folderPath),
                    Size = GetReadableSize(fileInfo.Length),
                    ModifiedDate = fileInfo.LastWriteTime.ToString(GetDefaultDateFormat())
                });
            }
        }

        private void GetContentSearchResult(string[] allFiles, List<FileProp> fileProps, FileSearchCriteria fileSearchCriteria)
        {
            foreach (string eachFile in allFiles)
            {
                if (isFileContainsKeyWord(eachFile, fileSearchCriteria.searchKeyWord))
                {
                    FileInfo fileInfo = new FileInfo(eachFile);

                    fileProps.Add(new FileProp()
                    {
                        FullPath = eachFile,
                        Name = GetCleanFileName(eachFile, fileSearchCriteria.folderPath),
                        Size = GetReadableSize(fileInfo.Length),
                        ModifiedDate = fileInfo.LastWriteTime.ToString(GetDefaultDateFormat())
                    });
                }
            }
        }

        private bool isFileContainsKeyWord(string filePath, string keyWord)
        {
            return File.ReadAllText(filePath).ToLower().Contains(keyWord.ToLower());
            
        }

        private string GetCleanFileName(string fullPath, string folderPath)
        {
            if (folderPath.EndsWith("\\"))
                return fullPath.Substring(folderPath.Length);
            else
                return fullPath.Substring(folderPath.Length + 1);
        }

        private string GetReadableSize(long fileSize)
        {
            if (fileSize >= 1024 * 1024 * 1024)
            {
                return $"{fileSize / (1024 * 1024 * 1024)}GB";
            }
            else if (fileSize >= 1024 * 1024)
            {
                return $"{fileSize / (1024 * 1024)}MB";
            }
            else if (fileSize >= 1024)
            {
                return $"{fileSize / (1024)}KB";
            }
            else
            {
                return "0KB";
            }
        }

        private string GetDefaultDateFormat()
        {
            return "dd-MM-yyyy HH:mm:ss";
        }
    }
}
