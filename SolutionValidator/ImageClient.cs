using System;
using System.IO;
using System.Net.Http;

namespace SolutionValidator
{
    internal class ImageClient
    {
        private readonly string serviceUrl;

        public ImageClient(string serviceUrl)
        {
            this.serviceUrl = serviceUrl;
            client = new HttpClient();
        }

        public byte[] ProcessImage(string path, string coords, string transform)
        {
            var image = File.ReadAllBytes(path);

            var result = client.SendAsync(new HttpRequestMessage(HttpMethod.Post, $"{serviceUrl}/process/{transform}/{coords}") { Content = new ByteArrayContent(image) } ).Result;
            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Request failed with code " + result.StatusCode);
                return null;
            }

            return result.Content.ReadAsByteArrayAsync().Result;
        }

        private readonly HttpClient client;
    }
}