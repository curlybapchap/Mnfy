using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mnfy.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MnfyUrlController : ControllerBase
    {
        public MnfyUrlController()
        {
            DynaDB.Client = new AmazonDynamoDBClient(new StoredProfileAWSCredentials("default"), RegionEndpoint.EUWest1);
            DynaDB.urlsTable = Table.LoadTable(DynaDB.Client, "MnfyUrls");
        }

        [HttpGet]
        public async Task<string> Create(string longUrl)
        {
            var newKey = await DynaDB.CreateAndStore(longUrl);
            return newKey;
        }

        [HttpGet]
        public async Task<ActionResult> Get(string u)
        {
            var longURL = "";
            RedirectResult redirectResult;
            if (u != null)
            {
                var found = await DynaDB.ReadingItem(u, false);
                if (found)
                {
                    var toJSON = DynaDB.mnfyUrlRecord.ToJson();
                    var obj = JsonSerializer.Deserialize<MnfyUrl>(toJSON);
                    longURL = obj.LongURL;
                    redirectResult = new RedirectResult(obj.LongURL, false);
                    return redirectResult;
                }
            }
            redirectResult = new RedirectResult(longURL, false);
            return redirectResult;
        }
    }
}