using Amazon.DynamoDBv2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;
using System.Text.Json;
using Amazon.DynamoDBv2.DocumentModel;
using System.Net;

namespace dng.b2b.portals.ftncs.poc.DynamoDbHelper
{
    public abstract class DynamoDbCrudl<T> where T : class, IDynamoDbDto, new()
    {
        protected abstract string TableName { get; }

        private readonly IAmazonDynamoDB _dynamoDb;
        protected DynamoDbCrudl(IAmazonDynamoDB dynamoDb)
        {
            this._dynamoDb = dynamoDb;
        }

        protected async Task<T?> GetAsync(string pk, string sk)
        {
            var request = new GetItemRequest
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "pk", new AttributeValue { S = pk } },
                    { "sk", new AttributeValue { S = sk } },
                }
            };

            var response = await _dynamoDb.GetItemAsync(request);
            if (response.Item.Count == 0)
            {
                return null;
            }

            var itemAsDocument = Document.FromAttributeMap(response.Item);
            return JsonSerializer.Deserialize<T>(itemAsDocument.ToJson());
        }

        public async IAsyncEnumerable<T> GetAllAsync()
        {
            var request = new ScanRequest
            {
                TableName = TableName
            };
            Dictionary<string, AttributeValue>? lastKeyEvaluated = null;

            do
            {
                ScanResponse? response = await _dynamoDb.ScanAsync(request);
                foreach (var item in response.Items)
                {
                    var itemAsDocument = Document.FromAttributeMap(item);
                    yield return JsonSerializer.Deserialize<T>(itemAsDocument.ToJson())!;
                }
                lastKeyEvaluated = response.LastEvaluatedKey;
            } while (lastKeyEvaluated != null && lastKeyEvaluated.Count != 0);
        }

        public async Task<bool> CreateAsync(T dto)
        {
            var customerAsJson = JsonSerializer.Serialize(dto);
            var itemAsDocument = Document.FromJson(customerAsJson);
            var itemAsAttributes = itemAsDocument.ToAttributeMap();

            var createItemRequest = new PutItemRequest
            {
                TableName = TableName,
                Item = itemAsAttributes
            };

            var response = await _dynamoDb.PutItemAsync(createItemRequest);
            return response.HttpStatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> UpdateAsync(T dto)
        {
            var customerAsJson = JsonSerializer.Serialize(dto);
            var itemAsDocument = Document.FromJson(customerAsJson);
            var itemAsAttributes = itemAsDocument.ToAttributeMap();

            var updateItemRequest = new PutItemRequest
            {
                TableName = TableName,
                Item = itemAsAttributes
            };

            var response = await _dynamoDb.PutItemAsync(updateItemRequest);
            return response.HttpStatusCode == HttpStatusCode.OK;
        }

        protected async Task<bool> DeleteAsync(string pk, string sk)
        {
            var deleteItemRequest = new DeleteItemRequest
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "pk", new AttributeValue { S = pk } },
                    { "sk", new AttributeValue { S = sk } }
                }
            };

            var response = await _dynamoDb.DeleteItemAsync(deleteItemRequest);
            return response.HttpStatusCode == HttpStatusCode.OK;
        }
    }
}


