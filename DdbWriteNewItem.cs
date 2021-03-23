using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;

namespace Mnfy
{
    public static partial class DynaDB
    {
        public static async Task WritingNewItem(Document newItem)
        {
            operationSucceeded = false;
            operationFailed = false;

            string key = newItem["UrlKey"];

            if (await ReadingItem(key, false)) { }
            else
            {
                try
                {

                    urlsTable.PutItemAsync(newItem, token).Wait();
                    operationSucceeded = true;
                }
                catch (Exception ex)
                {
                    operationFailed = true;
                }
            }
        }
    }
}