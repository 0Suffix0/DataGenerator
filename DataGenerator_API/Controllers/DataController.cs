using DataGenerator_Core.Entites;
using DataGenerator_Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataGenerator_API.Controllers
{
    [ApiController]
    [Route("api/generatedata")]
    public class DataController : ControllerBase
    {
        private readonly Generator _generator;
        private readonly Converter _converter;

        public DataController(Generator generator, Converter converter)
        {
            _generator = generator;
            _converter = converter;
        }

        [HttpPost]
        public async Task<IActionResult> CreateData([FromBody] IEnumerable<Column> columns, string convertTo = "SQL", int count = 2)
        {
            string result = String.Empty;

            if (columns == null)
                throw new ArgumentNullException("Required fields is null");

            foreach (Column column in columns)
                if (column.Type == null || column.ColumnName == null)
                    throw new ArgumentNullException("Required fields in columns is null");

            
            IEnumerable<IEnumerable<string>> generatorResult = _generator.Start(columns, count);

            switch (convertTo)
            {
                case "SQL":
                    result = _converter.toSQL(columns, generatorResult);
                    break;
            }

            return await Task.FromResult(Ok(result));
        }
    }
}