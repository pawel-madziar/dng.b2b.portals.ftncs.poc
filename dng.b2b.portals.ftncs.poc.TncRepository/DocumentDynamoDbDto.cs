using dng.b2b.portals.ftncs.poc.DynamoDbHelper;
using System.Text.Json.Serialization;
using dng.b2b.portals.ftncs.poc.Repositories;

namespace dng.b2b.portals.ftncs.poc.TncRepository;

internal class DocumentDynamoDbDto : IDynamoDbDto
{
    [JsonPropertyName("pk")]
    public string pk { get; set; } = string.Empty;
    [JsonPropertyName("sk")]
    public string sk { get; set; } = string.Empty;
    [JsonPropertyName("companyCode")]
    public string CompanyCode { get; set; } = string.Empty;
    [JsonPropertyName("clientName")]
    public string ClientName { get; set; } = string.Empty;
    [JsonPropertyName("emailTemplate")]
    public string EmailTemplate { get; set; } = string.Empty;
    [JsonPropertyName("senderEmail")]
    public string SenderEmail { get; set; } = string.Empty;
    [JsonPropertyName("fileName")]
    public string FileName { get; set; } = string.Empty;
    [JsonPropertyName("createdBy")]
    public string CreatedBy { get; set; } = string.Empty;
    [JsonPropertyName("modifiedBy")]
    public string ModifiedBy { get; set; } = string.Empty;
    [JsonPropertyName("created")]
    public DateTime Created { get; set; } = DateTime.Now;
    [JsonPropertyName("modified")]
    public DateTime Modified { get; set; } = DateTime.Now;

    public static explicit operator DocumentDynamoDbDto(DocumentDto dto)
    {
        return new DocumentDynamoDbDto
        {
            pk = dto.DocumentType,
            sk = $"{dto.CountryCode}_{dto.SchemeCode}",
            CompanyCode = dto.CompanyCode,
            ClientName = dto.ClientName,
            EmailTemplate = dto.EmailTemplate,
            SenderEmail = dto.SenderEmail,
            FileName = dto.FileName,
            CreatedBy = dto.CreatedBy,
            ModifiedBy = dto.ModifiedBy,
            Created = dto.Created,
            Modified = dto.Modified
        };
    }

    public static explicit operator DocumentDto(DocumentDynamoDbDto dto)
    {
        return new DocumentDto
        {
            DocumentType = dto.pk,
            CountryCode = dto.sk.Split('_')[0],
            SchemeCode = dto.sk.Split('_')[1],
            CompanyCode = dto.CompanyCode,
            ClientName = dto.ClientName,
            EmailTemplate = dto.EmailTemplate,
            SenderEmail = dto.SenderEmail,
            FileName = dto.FileName,
            CreatedBy = dto.CreatedBy,
            ModifiedBy = dto.ModifiedBy,
            Created = dto.Created,
            Modified = dto.Modified
        };
    }

}
