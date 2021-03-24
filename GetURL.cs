using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mnfy
{
    public class UrlFinder
    {
        public async Task<string> GetURL(string shortURL)
        {
            var longURL = "";
            if (shortURL != null)
            {
                var found = await DynaDB.ReadingItem(shortURL, false);
                if (found)
                {
                    var toJSON = DynaDB.mnfyUrlRecord.ToJson();
                    var obj = JsonSerializer.Deserialize<MnfyUrl>(toJSON);
                    longURL = obj.LongURL;
                }
            }
            return longURL;
        }
    }
}