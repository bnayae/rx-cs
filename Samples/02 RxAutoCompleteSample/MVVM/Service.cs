using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WpfRxAutoComplete
{
    public class Service
    {
        public static async Task<string[]> AutoComplete(string text)
        {
            using (var http = new HttpClient())
            {
                string json = await http.GetStringAsync($"http://suggestqueries.google.com/complete/search?client=firefox&q={text}");
                JArray root = JsonConvert.DeserializeObject<JArray>(json);
                JArray content = root[1] as JArray;
                return content.Select(item => item.ToString()).ToArray();
            }
        }
    }
}
