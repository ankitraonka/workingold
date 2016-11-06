using System;

using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;

namespace UploadTest.Controllers
{
    public class FileUploadController : ApiController
    {
        [HttpPost()]
        public string UploadFiles()
        {
            bool isUploaded = false;
            string message = "File upload failed";

            for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
            {
                var myFile = HttpContext.Current.Request.Files[i];

                if (myFile != null && myFile.ContentLength != 0)
                {
                    string pathForSaving = HttpContext.Current.Server.MapPath("~/Uploads");
                    if (this.CreateFolderIfNeeded(pathForSaving))
                    {
                        try
                        {
                            myFile.SaveAs(Path.Combine(pathForSaving, myFile.FileName));
                            isUploaded = true;
                            message = "File uploaded successfully!";
                        }
                        catch (Exception ex)
                        {
                            message = string.Format("File upload failed: {0}", ex.Message);
                        }
                    }
                }

            }


            return message;
        }

        private bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
        }

    }
}