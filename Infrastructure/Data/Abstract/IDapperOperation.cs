using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Abstract
{
    public interface IDapperOperation<T>
    {
        List<T> SelectList<T>(string sql, object parameters,out string hata);

        T SelectFirstOrDefault<T>(string sql, object parameters,out  string hata);

        T SelectSingleOrDefault<T>(string sql, object parameters, out string hata);

        int SqlCommand(string sql, object parameters, out string hata);

    }
}
