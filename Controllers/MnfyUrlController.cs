using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using Microsoft.AspNetCore.Mvc;
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
    }
}