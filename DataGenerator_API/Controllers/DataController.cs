using DataGenerator_Core;
using DataGenerator_Core.Entites;
using DataGenerator_Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace DataGenerator_API.Controllers
{
    [ApiController]
    [Route("api/generatedata")]
    public class DataController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public DataController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IResult> CreateData([FromBody] IEnumerable<Column> columns, string convertTo = "SQL", int count = 2)
        {
            string result = String.Empty;

            if (columns == null)
                throw new ArgumentNullException("Required fields is null");

            foreach (Column column in columns)
                if (column.Type == null || column.ColumnName == null)
                    throw new ArgumentNullException("Required fields in columns is null");


            Generator generator = new(_context);
            List<List<string>> generatorResult = generator.Start(columns, count);

            Converter converter = new();
            switch (convertTo)
            {
                case "SQL":
                    result = converter.toSQL(columns, generatorResult);
                    break;
            }

            return Results.Ok(result);
        } 
    }
}
