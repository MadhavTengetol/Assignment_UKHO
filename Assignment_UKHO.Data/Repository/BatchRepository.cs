using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_UKHO.Data.Repository
{
    public class BatchRepository : IRepository
    {
        private readonly AppDbContext context;

        public BatchRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Batch CreateBatch(Batch batch)
        {
            context.Batches.Add(batch);
            context.SaveChanges();
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
