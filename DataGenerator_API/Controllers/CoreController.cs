using DataGenerator_Core.Entites;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DataGenerator_API.Controllers
{
    [ApiController]
    [Route("api/generate")]
    public sealed class CoreController : ControllerBase
    {
        private readonly string _connectionString = System.Configuration.ConfigurationManager.AppSettings.Get("connectionString");

        [HttpPost]
        public async Task<string> CreateData(string convertTo, int count)
        {
            string result = String.Empty;
            DataGenerator_Core.Services.Controller _controller = new(_connectionString);

            List<Column> columns = new List<Column>();
            try
            {
                columns = await Request.ReadFromJsonAsync<List<Column>>();
            }
            catch
            {
                columns.Add(await Request.ReadFromJsonAsync<Column>());
            }

            result = _controller.GenerateData(columns, convertTo, count);


            return result;
        }
    }
}
