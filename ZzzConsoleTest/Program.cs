using Amazon.DynamoDBv2;
using Bogus;
using dng.b2b.portals.ftncs.poc.Repositories;
using dng.b2b.portals.ftncs.poc.TncRepository;

namespace ZzzConsoleTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            string[] companyCodes = new[] { "A", "B", "C", "D", "E" };
            string[] countryCodes = new[] { "GB", "FR", "PL", "IE", "DE" };
            var date = DateTime.Now;

            var dtoFaker = new Faker<DocumentDto>()
                .RuleFor(f => f.SchemeCode, r => r.Random.AlphaNumeric(3).ToUpper())
                .RuleFor(f => f.DocumentType, r => "Terms and Conditions")
                .RuleFor(f => f.CompanyCode, r => r.Random.String2(1, "ABCDE"))
                .RuleFor(f => f.CountryCode, r => r.PickRandom(countryCodes))
                .RuleFor(f => f.CountryCode, r => r.PickRandom(countryCodes))
                .RuleFor(f => f.ClientName, r => r.Company.CompanyName())
                .RuleFor(f => f.CreatedBy, r => r.Person.FullName)
                .RuleFor(f => f.ModifiedBy, (r, s) => s.CreatedBy)
                .RuleFor(f => f.FileName, r => r.System.FilePath())
                .RuleFor(f => f.SenderEmail, (r, u) => r.Internet.Email())
                .RuleFor(f => f.EmailTemplate, r => r.Random.AlphaNumeric(12))
                .RuleFor(f => f.Created, r => date)
                .RuleFor(f => f.Modified, r => date);


            var dtos = dtoFaker.Generate(10);

            IAmazonDynamoDB dynamoDb = new AmazonDynamoDBClient();


            IDocumentDtoRepository repo = new DocumentDtoRepository(dynamoDb);
            foreach (var dto in dtos)
            {
                await repo.CreateAsync(dto);
            }

            var ttt = repo.GetAllAsync();

            await foreach (var tt in ttt)
            {
                Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
            }

            var result = await repo.GetAllAsync().ToListAsync();

            foreach (var dto in dtos)
            {
                dto.ModifiedBy = "John Doe";
                dto.Modified = DateTime.UtcNow;
                await repo.UpdateAsync(dto);
            }

            foreach (var dto in dtos)
            {

              var xx =  await repo.GetAsync(dto.CountryCode, dto.SchemeCode, dto.DocumentType);
            }

            foreach (var dto in dtos)
            {
                var xx = await repo.DeleteAsync(dto.CountryCode, dto.SchemeCode, dto.DocumentType);
            }
        }
    }
}
