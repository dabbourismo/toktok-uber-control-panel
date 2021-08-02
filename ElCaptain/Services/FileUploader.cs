using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ElCaptain.Services
{
    public static class FileUploader
    {
        public static void UploadFTPFile(
            string fileName, int lastIdInserted, HttpPostedFileBase file, string folderPath)
        {
            string FileExt = Path.GetExtension(fileName).ToUpper();
            string fullPath = Path.Combine(folderPath, lastIdInserted.ToString() + FileExt);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            file.SaveAs(fullPath);
        }


        public static void UploadFTPFileStringName(
           string fileName, string lastIdInserted, HttpPostedFileBase file, string folderPath)
        {
            string FileExt = Path.GetExtension(fileName).ToUpper();
            string fullPath = Path.Combine(folderPath, lastIdInserted.ToString() + FileExt);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            file.SaveAs(fullPath);
        }

        public static void DeleteFTPFile(int imageId, string folderPath)
        {
            string fullPath = Path.Combine(folderPath, imageId.ToString() + ".JPG");
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }

        public static void DeleteFTPFileStringName(string imageId, string folderPath)
        {
            string fullPath = Path.Combine(folderPath, imageId + ".JPG");
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
    }
}