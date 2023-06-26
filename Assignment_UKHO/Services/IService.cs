using Assignment_UKHO.Dto;

namespace Assignment_UKHO.Services
{
    public interface IService
    {
        public Task<BatchDto> Create(BatchDto batch);
        public Task<BatchResponseDto> GetBatchById(Guid id);
    }
}
