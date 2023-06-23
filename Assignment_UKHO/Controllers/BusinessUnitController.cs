using Assignment_UKHO.Data;
using Assignment_UKHO.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_UKHO.Controllers
{
    
    [ApiController]
    public class BusinessUnitController : ControllerBase
    {
        private readonly BusinessUnitServices services;
        public BusinessUnitController(AppDbContext context)
        {
            services = new BusinessUnitServices(context);
        }

        [HttpPost("bu")]
        public async Task<IActionResult> CreateBusinessUnit([FromBody]BusinessUnit unit)
        {
            var result =await services.CreateBusinessUnit(unit);
            return Ok(new { UnitName = result});
        }
    }
}
