using Amazon.DynamoDBv2.DocumentModel;
using System;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using System.Threading.Tasks;

namespace Mnfy
{
    public static partial class DynaDB
    {
        public static async Task<string> CreateAndStore(string longUrl)
        {
            //DynaDB.Client = new AmazonDynamoDBClient(new StoredProfileAWSCredentials("default"), RegionEndpoint.EUWest1);
            //DynaDB.urlsTable = Table.LoadTable(DynaDB.Client, "MnfyUrls");
            string newKey = Guid.NewGuid().ToString("N").Substring(0, 6).ToLower();

            var alreadyExists = true;
            while (alreadyExists)
            {
                alreadyExists = await DynaDB.ReadingItem(newKey, false);
                if (!alreadyExists)
                {
                    Document newItemDocument = new Document();
                    newItemDocument["UrlKey"] = newKey;
                    newItemDocument["LongURL"] = longUrl;
                    newItemDocument["TwitterHandle"] = "@PaulLogan";
                    newItemDocument["DeletionDate"] = "2021/30/25";
                    DynaDB.WritingNewItem(newItemDocument).Wait();
                }
                else
                {
                    newKey = Guid.NewGuid().ToString("N").Substring(0, 6).ToLower();
                }
            }
            return newKey;
        }
    }
}
