using Assignment_UKHO.Data;
using Assignment_UKHO.Data.Repository;
using Assignment_UKHO.Dto;
using AutoMapper;

namespace Assignment_UKHO.Services
{
    public class BatchService 
    {
        private readonly BatchRepository repository;
        private readonly IMapper mapper;

        public BatchService(AppDbContext context)
        {
            this.repository = new BatchRepository(context);
        }
        public BatchService(AppDbContext context,IMapper mapper) {
            this.repository = new(context);
            this.mapper = mapper;
        }

        public async Task<BatchDto> Create(BatchDto batch)
        {
           
            var result = await repository.CreateBatch(mapper.Map<Batch>(batch));

            return mapper.Map<BatchDto>(result);
        }

        public async Task<BatchDto> GetBatchById(Guid id)
        {
            var result = await repository.GetBatchById(id);
            return mapper.Map<BatchDto>(result);
        }

    }
}
