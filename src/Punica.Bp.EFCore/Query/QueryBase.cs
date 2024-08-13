using Microsoft.EntityFrameworkCore;
using Punica.Bp.Application.Query;

namespace Punica.Bp.EFCore.Query
{
    public class QueryBase
    {
        private readonly DbContext _dbContext;
       

        public QueryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // TODO: add where facility again
        protected List<dynamic> GetList<TEntity>(QueryOptions<TEntity> options) where TEntity : class
        {
            var query = _dbContext.Set<TEntity>().AsNoTracking();
            return options.ApplyTo(query);
        }

    }


}
