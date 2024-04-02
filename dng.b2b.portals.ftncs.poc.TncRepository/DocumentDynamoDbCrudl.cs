using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using dng.b2b.portals.ftncs.poc.DynamoDbHelper;

namespace dng.b2b.portals.ftncs.poc.TncRepository
{
    internal class DocumentDynamoDbCrudl: DynamoDbCrudl<DocumentDynamoDbDto>
    {
        public DocumentDynamoDbCrudl(IAmazonDynamoDB dynamoDb) : base(dynamoDb)
        {
        }
        protected override string TableName { get; } = "FrenchTnCsDocuments";

        public Task<DocumentDynamoDbDto?> GetAsync(string countryCode, string schemeCode, string documentType)
        {
            return GetAsync($"{documentType}", $"{countryCode}_{schemeCode}");
        } 

        public Task<bool> DeleteAsync(string countryCode, string schemeCode, string documentType)
        {
            return DeleteAsync($"{documentType}", $"{countryCode}_{schemeCode}");
        }
    }
}
