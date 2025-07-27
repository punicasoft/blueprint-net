using System.Linq.Expressions;
using System.Text;
using Punica.Linq.Dynamic;

namespace Punica.Bp.Application.Query
{
    public class QueryOptions<TEntity> where TEntity : class
    {
        public string? Select { get; private set; }
        public string? Filter { get; private set; }
        public string? OrderBy { get; private set; }
        public int? Skip { get; private set; }
        public int? Take { get; private set; }

        public QueryOptions(string? select, string? filter, string? orderBy, int? skip, int? take)
        {
            Select = select;
            Filter = filter;
            OrderBy = orderBy;
            Skip = skip;
            Take = take;
        }

        public List<dynamic> ApplyTo(IQueryable<TEntity> query)
        {
            var expression = "";

            var sb = new StringBuilder();

            if (Select != null)
            {
                sb.AppendFormat("Select(new {{{0}}})", Select);
            }

            if (Filter != null)
            {
                if (sb.Length > 0)
                {
                    sb.Append(".");

                }
                  
                sb.AppendFormat("Where({0})", Filter);
            }

            if (OrderBy != null)
            {
                if (sb.Length > 0)
                {
                    sb.Append(".");
                }
                sb.AppendFormat("OrderBy({0})", OrderBy);
            }

            if (Skip != null)
            {
                if (sb.Length > 0)
                {
                    sb.Append(".");
                }
                sb.AppendFormat("Skip({0})", Skip);
            }

            if (Take != null)
            {
                if (sb.Length > 0)
                {
                    sb.Append(".");
                }
                sb.AppendFormat("Take({0})", Take);
            }

            expression = sb.ToString();


            var result = new Evaluator()
                .AddStartParameter(typeof(IQueryable<TEntity>))
                .Evaluate(expression, query);

        
            var queryable = (IQueryable)result;

            return queryable.ToDynamicList();

          
        }

    }
}
