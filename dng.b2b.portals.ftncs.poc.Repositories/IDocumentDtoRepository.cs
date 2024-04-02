namespace dng.b2b.portals.ftncs.poc.Repositories;

public interface IDocumentDtoRepository
{
    IAsyncEnumerable<DocumentDto> GetAllAsync();
    Task<DocumentDto?> GetAsync(string countryCode, string schemeCode, string documentType);
    Task<bool> DeleteAsync(string countryCode, string schemeCode, string documentType);
    Task<bool> CreateAsync(DocumentDto dto);
    Task<bool> UpdateAsync(DocumentDto dto);

}