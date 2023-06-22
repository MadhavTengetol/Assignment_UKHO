using Assignment_UKHO.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_UKHO.Controllers
{
    public interface IController
    {
        public Task<IActionResult> CreateBatch(BatchDto batch);
        public Task<IActionResult> GetBatchById([FromRoute] Guid batchId);
    }
}
