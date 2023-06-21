using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_UKHO.Data.Repository
{
    public interface IRepository
    {
        public Batch CreateBatch(Batch batch);
        public Task<Batch> GetBatchById(Guid id);
    }
}
