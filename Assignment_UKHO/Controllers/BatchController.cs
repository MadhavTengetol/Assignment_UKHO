using Assignment_UKHO.Data;
using Assignment_UKHO.Dto;
using Assignment_UKHO.ErrorHandling;
using Assignment_UKHO.Services;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Assignment_UKHO.Controllers
{
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly BatchService service;
      
        private readonly IValidator<BatchDto> validator;
        private readonly IMapper mapper;
        private readonly ILogger<BatchController> logger;
        private readonly BusinessUnitServices unitServices;
        

       
        public BatchController(AppDbContext repo,IValidator<BatchDto> validator,IMapper mapper,ILogger<BatchController> logger)
        {
            this.service = new(repo,mapper);
            this.validator = validator;
            this.mapper = mapper;
            this.logger = logger;
            this.unitServices = new(repo);
        }

        [SwaggerOperation(summary:"Create a new batch to upload files into.")]
        [Produces("application/json")]
        [SwaggerResponse(statusCode: 400,description: "Bad request - there are one or more errors in the specified parameters")]
        [SwaggerResponse(statusCode: 201,description: "Created")]
        [HttpPost]
        [Route("batch")]
        public async Task<IActionResult> CreateBatch(BatchDto batch)
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
         
            if (unitServices.IsExists(batch.BusinessUnit).Result)
            {
                var createdBatch = await service.Create(batch);
                logger.LogInformation("New Batch is Created");
                return Created("", new { BatchId = createdBatch.BatchId });
            }
            logger.LogInformation("Business unit does not exists");
            errorResponse.CorrelationId = Guid.NewGuid().ToString();
            errorResponse.Errors = new List<Errors> { new Errors() { Source="Business Unit",Description="Business Unit Does Not Exists"} };
            return BadRequest(errorResponse);


        }


        [SwaggerOperation(summary:"Get details of the batch including links to all the files in the batch",
                          description:"This get will include full details of the batch, for example it's status, the files in the batch"  )]
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
                logger.LogInformation("Get Batch By Id is called");
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

                var response = mapper.Map<BatchResponseDto>(result);
                    
                return Ok(response);
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
