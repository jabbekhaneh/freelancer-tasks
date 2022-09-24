using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Portal.ApplicationServices.Common;

public class UploadHelper
{
    public static string Upload(IFormFile file, string path)
    {
        try
        {
            var fileName = string.Empty;
            var newFileName = string.Empty;
            fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var myUniqueFileName = Convert.ToString(Guid.NewGuid());
            var FileExtension = Path.GetExtension(fileName);
            newFileName = myUniqueFileName + FileExtension;
            fileName = Path.Combine(Directory.GetCurrentDirectory(), path, newFileName);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            using (FileStream fs = File.Create(fileName))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            return newFileName;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
