using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HW_10_Delegates_Events_Async
{
    internal class ImageDownloader
    {
        public event Action ImageStarted;
        public event Action ImageCompleted;

        private string remoteUri;

        public ImageDownloader(string uri)
        {
            remoteUri = uri;
        }

        public async Task Download(string fileName)
        {
            ImageStarted?.Invoke();
            Console.WriteLine($"Качаю \"{fileName}\" из \"{remoteUri}\" .......\n");

            using (var myWebClient = new WebClient())
            {
                await myWebClient.DownloadFileTaskAsync(remoteUri, fileName);
            }

            ImageCompleted?.Invoke();
            Console.WriteLine($"Успешно скачал \"{fileName}\" из \"{remoteUri}\"");
        }
    }
}
