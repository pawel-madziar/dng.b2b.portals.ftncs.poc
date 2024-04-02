using System.Diagnostics;
using System.Xml.Linq;
using Amazon.DynamoDBv2;
using Amazon.Runtime.Documents;
using dng.b2b.portals.ftncs.poc.Repositories;

namespace dng.b2b.portals.ftncs.poc.TncRepository;

public class DocumentDtoRepository(IAmazonDynamoDB dynamoDb) : IDocumentDtoRepository
{
    DocumentDynamoDbCrudl crudl = new(dynamoDb);

    public async IAsyncEnumerable<DocumentDto> GetAllAsync()
    {
        var result = crudl.GetAllAsync();
        await foreach (var ddto in result)
        {
            yield return (DocumentDto)ddto;
        }
    }

    public async Task<DocumentDto?> GetAsync(string countryCode, string schemeCode, string documentType)
    {
        var dDto = await crudl.GetAsync(countryCode!, schemeCode!, documentType!);
        return (DocumentDto)dDto;
    }

    public async Task<bool> DeleteAsync(string countryCode, string schemeCode, string documentType)
    {
        var result = await crudl.DeleteAsync(countryCode!, schemeCode!, documentType!);
        return result;
    }

    public async Task<bool> CreateAsync(DocumentDto dto)
    {
        var result = await crudl.CreateAsync((DocumentDynamoDbDto)dto);
        return result;
    }

    public async Task<bool> UpdateAsync(DocumentDto dto)
    {
        var result = await crudl.UpdateAsync((DocumentDynamoDbDto)dto);
        return result;
    }
}