using System;
using System.Threading;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Amazon;

namespace Mnfy
{
    public static partial class DynaDB
    {
        public static bool operationSucceeded;
        public static bool operationFailed;
        public static AmazonDynamoDBClient Client;
        public static Table urlsTable;
        public static TableDescription urlsTableDescription;
        public static CancellationTokenSource source = new CancellationTokenSource();
        public static CancellationToken token = source.Token;
        public static Document mnfyUrlRecord;

        public static async Task<bool> ReadingItem(string key, bool report)
        {

            DynaDB.Client = new AmazonDynamoDBClient(new StoredProfileAWSCredentials("default"), RegionEndpoint.EUWest1);
            DynaDB.urlsTable = Table.LoadTable(DynaDB.Client, "MnfyUrls");

            operationSucceeded = false;
            operationFailed = false;
            try
            {
                Task<Document> readUrl = urlsTable.GetItemAsync(key, token);
                mnfyUrlRecord = await readUrl;
                if (mnfyUrlRecord == null)
                {
                    return (false);
                }
                else
                {
                    operationSucceeded = true;
                    return (true);
                }
            }
            catch (Exception ex)
            {
                operationFailed = true;
            }
            return (false);
        }
    }
}