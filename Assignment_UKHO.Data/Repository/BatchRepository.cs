using Microsoft.EntityFrameworkCore;

namespace Assignment_UKHO.Data.Repository
{
    public class BatchRepository : IRepository
    {
        private readonly AppDbContext context;

        public BatchRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Batch> CreateBatch(Batch batch)
        {
            await context.Batches.AddAsync(batch);
            await context.SaveChangesAsync();
            
            return batch;
        }

        public async Task<Batch> GetBatchById(Guid id)
        {
            var data =await context.Batches.Where(x => x.BatchId == id)
                                      .Include(x=>x.Acl.ReadGroups )
                                      .Include(x=>x.Acl.ReadUsers)
                                      .Include(x=>x.Attributes)
                                      .Include(x=>x.Files)
                                      .ThenInclude(x => x.Attributes)
                                      .FirstOrDefaultAsync();
            if(data is null )
            {
                return null;
            }
            return data;
        }
        
    }
}
