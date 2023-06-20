using Assignment_UKHO.Data;
using Assignment_UKHO.Data.Repository;
using Assignment_UKHO.ErrorHandling;
using Assignment_UKHO.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_UKHO.Controllers
{
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly BatchService service;
        private readonly IValidator<Batch> validator;

        public BatchController(AppDbContext repo,IValidator<Batch> validator)
        {
            this.service = new(repo);
            this.validator = validator;
        }

        [HttpPost]
        [Route("batch")]
        public IActionResult CreateBatch(Batch batch)
        {
            var result = validator.Validate(batch);
            var errorResponse = new ErrorModel();
            if (!result.IsValid)
            {
                var errors = result.ToDictionary();
                List<Errors> errors1 = new();
                foreach (var error in errors)
                {
                   errors1.Add(new Errors { Source = error.Key, Description = error.Value.FirstOrDefault()! });
                }

                errorResponse.CorrelationId =Guid.NewGuid().ToString();
                errorResponse.Errors = errors1;
                return BadRequest(errorResponse);
            }
            return Created("", new { BatchId = Guid.NewGuid() });
        }
    }
}
