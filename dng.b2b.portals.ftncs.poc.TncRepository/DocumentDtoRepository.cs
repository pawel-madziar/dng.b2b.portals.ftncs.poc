using System.Diagnostics;
using System.Xml.Linq;
using Amazon.DynamoDBv2;
using Amazon.Runtime.Documents;
using dng.b2b.portals.ftncs.poc.DynamoDbHelper;
using dng.b2b.portals.ftncs.poc.Repositories;

namespace dng.b2b.portals.ftncs.poc.TncRepository;

public class DocumentDtoRepository : DynamoDbCrudl<DocumentDynamoDbDto>, IDocumentDtoRepository
{
    public DocumentDtoRepository(IAmazonDynamoDB dynamoDb): base(dynamoDb)
    {
            
    }
    protected override string TableName { get; } = "FrenchTnCsDocuments";

    public new async IAsyncEnumerable<DocumentDto> GetAllAsync()
    {
        var result = base.GetAllAsync();
        await foreach (var dto in result)
        {
            yield return (DocumentDto)dto;
        }
    }

    public async Task<DocumentDto?> GetAsync(string countryCode, string schemeCode, string documentType)
    {
        var dDto = await base.GetAsync($"{documentType}", $"{countryCode}_{schemeCode}");
        return (DocumentDto)dDto;
    }

    public async Task<bool> DeleteAsync(string countryCode, string schemeCode, string documentType)
    {
        var result = await base.DeleteAsync($"{documentType}", $"{countryCode}_{schemeCode}");
        return result;
    }

    public async Task<bool> CreateAsync(DocumentDto dto)
    {
        var result = await base.CreateAsync((DocumentDynamoDbDto)dto);
        return result;
    }

    public async Task<bool> UpdateAsync(DocumentDto dto)
    {
        var result = await base.UpdateAsync((DocumentDynamoDbDto)dto);
        return result;
    }


}