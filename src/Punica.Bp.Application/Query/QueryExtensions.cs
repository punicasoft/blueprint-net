using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punica.Bp.Application.Query
{
    public static class QueryExtensions
    {
        public static List<dynamic> ToDynamicList(this IQueryable query)
        {
            return query.ToList<dynamic>();
        }

        public static List<T> ToList<T>(this IQueryable query)
        {
            return query.Cast<T>().ToList();
        }
    }
}

