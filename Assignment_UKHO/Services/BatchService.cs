using Assignment_UKHO.Data;
using Assignment_UKHO.Data.Repository;

namespace Assignment_UKHO.Services
{
    public class BatchService 
    {
        private readonly BatchRepository repository;

        public BatchService(AppDbContext context) {
            this.repository = new(context);
        }

        public Batch Create(Batch batch)
        {
            return repository.CreateBatch(batch);
        }

        public async Task<Batch> GetBatchById(Guid id)
        {
            return await repository.GetBatchById(id);
        }
    }
}
