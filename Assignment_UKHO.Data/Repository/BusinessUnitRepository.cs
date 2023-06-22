using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_UKHO.Data.Repository
{
    public class BusinessUnitRepository
    {
        private readonly AppDbContext context;

        public BusinessUnitRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> IsExists(string unitName)
        {
            var result = await context.BusinessUnit.FirstOrDefaultAsync(x => x.UnitName == unitName);
            if (result is null)
                return false;
            return true;
        }


        public async Task<string> CreateBusinessUnit(BusinessUnit unit)
        {
            await context.BusinessUnit.AddAsync(unit);
            context.SaveChanges();
            return unit.UnitName;
        }
    }
}
