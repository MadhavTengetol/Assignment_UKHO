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
            //context.Batches.Add(batch);
            //context.SaveChanges();
            return batch;
        }

        public Batch GetBatchById(Guid id)
        {
            //return context.Batches.FirstOrDefault(b=>b.BatchId == id)!;
            return null;
        }
    }
}
