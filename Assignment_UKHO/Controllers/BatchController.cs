using Assignment_UKHO.Data;
using Assignment_UKHO.Data.Repository;
using Assignment_UKHO.ErrorHandling;
using Assignment_UKHO.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        [SwaggerOperation(summary:"Create a new batch to upload files into.")]
        [SwaggerResponse(statusCode: 400,description: "Bad request - there are one or more errors in the specified parameters")]
        [SwaggerResponse(statusCode: 201,description: "Created")]
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
            batch.BatchId = Guid.NewGuid();
            var createdBatch = service.Create(batch);
            return Created("", new { BatchId = createdBatch.BatchId });

        }


        [SwaggerResponse(statusCode: 200, description: "Ok - Details about the batch")]
        [SwaggerResponse(statusCode: 400, description: "Bad request - could be an invalid batch ID format. Batch IDs should be a GUID.")]
        [SwaggerResponse(statusCode: 404, description:"Not Found - Could be there the batch ID doesn't exist.")]
        [HttpGet]
        [Route("batch/{batchId}")]
        public async Task<IActionResult> GetBatchById([FromRoute]Guid batchId)
        {
            if(batchId != Guid.Empty)
            {
                var result =await service.GetBatchById(batchId);
                if(result == null)
                {
                    var errorRes = new ErrorModel
                    {
                        CorrelationId = Guid.NewGuid().ToString(),
                        Errors = new List<Errors>
                {
                    new Errors{Source="Batch Id",Description="Batch Id does not exists."}
                }
                    };
                    return NotFound(errorRes);
                }
                    
                return Ok(result);
            }
            var errorResponse = new ErrorModel
            {
                CorrelationId = Guid.NewGuid().ToString(),
                Errors = new List<Errors>
                {
                    new Errors{Source="Batch Id",Description="Batch Id is not valid."}
                }
            };
            return BadRequest(errorResponse);
        }
    }
}
