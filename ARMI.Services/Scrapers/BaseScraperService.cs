using System.IO;
using System.Net.Http;

namespace ARMI.Services.Scrapers
{
    public class BaseScraperService
    {
        public static async void GetFileFromUrl(string url, string fileUploadPath)
        {
            using (var httpClient = new HttpClient())
            using (var contentStream = await httpClient.GetStreamAsync(url))
            using (var fileStream = new FileStream(fileUploadPath, FileMode.Create, FileAccess.Write, FileShare.None, 1048576, true))
            {
                await contentStream.CopyToAsync(fileStream);
            }
        }
    }
}
