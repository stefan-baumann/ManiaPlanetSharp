using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ManiaPlanetSharp.Utils
{
    internal static class WebUtils
    {
        public static async Task<TResult> FetchJsonObject<TResult>(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = await client.GetStringAsync(url);
                using (StringReader stringReader = new StringReader(json))
                using (JsonTextReader jsonReader = new JsonTextReader(stringReader))
                {
                    return new JsonSerializer().Deserialize<TResult>(jsonReader);
                }
            }
        }
    }
}
