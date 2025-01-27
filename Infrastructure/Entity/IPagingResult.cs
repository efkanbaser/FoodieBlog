using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entity
{
    public interface IPagingResult<T>
        where T : BaseEntity, new()
    {
        List<T> Data { get; }
        int TotalItemCount { get; }
        int TotalCount { get; }
    }
}
