using DataGenerator_Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataGenerator_API.Controllers
{
    [ApiController]
    [Route("api/template")]
    public sealed class TemplatesController : ControllerBase
    {
        private readonly TemplateService _templateService;

        public TemplatesController(TemplateService templateService)
        {
            _templateService = templateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return await Task.FromResult(Ok(_templateService.GetAll()));
            }
            catch (ArgumentException ex)
            {
                return await Task.FromResult(NotFound(ex.Message));
            }
        }

        [HttpGet("{data}")]
        public async Task<IActionResult> GetOne(string data)
        {
            try
            {
                return await Task.FromResult(Ok(_templateService.GetOne(data)));
            } 
            catch (ArgumentException ex)
            {
                return await Task.FromResult(NotFound(ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(string data, string typeName)
        {
            try
            {
                return await Task.FromResult(Ok(_templateService.Create(data, typeName)));
            }
            catch (ArgumentException ex)
            {
                return await Task.FromResult(NotFound(ex.Message));
            }
        }

        [HttpPut("data")]
        public async Task<IActionResult> EditData(string currentData, string newData)
        {
            try
            {
                return await Task.FromResult(Ok(_templateService.EditData(currentData, newData)));
            }
            catch (ArgumentException ex)
            {
                return await Task.FromResult(NotFound(ex.Message));
            }
        }

        [HttpPut("type")]
        public async Task<IActionResult> EditType(string currentData, string typeName)
        {
            try
            {
                return await Task.FromResult(Ok(_templateService.EditType(currentData, typeName)));
            }
            catch (ArgumentException ex)
            {
                return await Task.FromResult(NotFound(ex.Message));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string data)
        {
            try
            {
                return await Task.FromResult(Ok(_templateService.Delete(data)));
            }
            catch (ArgumentException ex)
            {
                return await Task.FromResult(NotFound(ex.Message));
            }
        }
    }
}
