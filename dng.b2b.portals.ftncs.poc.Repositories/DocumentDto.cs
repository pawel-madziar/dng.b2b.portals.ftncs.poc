using System.Text.Json.Serialization;

namespace dng.b2b.portals.ftncs.poc.Repositories;

public class DocumentDto
{
    [JsonPropertyName("documentType")]
    public string DocumentType { get; set; } = string.Empty;
    [JsonPropertyName("countryCode")]
    public string CountryCode { get; set; } = string.Empty;
    [JsonPropertyName("companyCode")]
    public string CompanyCode { get; set; } = string.Empty;
    [JsonPropertyName("schemeCode")]
    public string SchemeCode { get; set; } = string.Empty;
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
}