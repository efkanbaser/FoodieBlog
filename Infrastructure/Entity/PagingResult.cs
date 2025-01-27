using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entity
{
    public class PagingResult<T> : IPagingResult<T>
where T : BaseEntity, new()
    {

        public PagingResult(List<T> Data,int TotalItemCount,int TotalCount)
        {
            this.Data = Data;
            this.TotalItemCount = TotalItemCount;
            this.TotalCount = TotalCount;
                
        }
        public List<T> Data {get;}

        public int TotalItemCount { get;}

        public int TotalCount { get; }
    }
}
