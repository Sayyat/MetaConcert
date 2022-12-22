using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Agora
{
    public class RequestToken
    {
        private const string URL = "https://agora-token-generator-beryl.vercel.app/api/generate";
        public static string GetToken()
        {
            var token = GetAsync(URL).Result;
            return token;
        }


        private static async Task<string> GetAsync(string uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using var response = (HttpWebResponse)await request.GetResponseAsync();
            await using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }
    }
}