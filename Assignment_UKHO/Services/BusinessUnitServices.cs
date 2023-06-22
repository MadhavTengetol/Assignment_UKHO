using Assignment_UKHO.Data;
using Assignment_UKHO.Data.Repository;

namespace Assignment_UKHO.Services
{
    public class BusinessUnitServices
    {
        private readonly BusinessUnitRepository repository;
        public BusinessUnitServices(AppDbContext context)
        {
            repository = new BusinessUnitRepository(context);
        }

        public async Task<bool> IsExists(string unitName)
        {
           return await repository.IsExists(unitName);
        }


        public async Task<string> CreateBusinessUnit(BusinessUnit unit)
        {
           return await repository.CreateBusinessUnit(unit);
        }
    }
}
