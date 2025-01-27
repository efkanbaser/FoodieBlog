using Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkMarket.Infrastructure.Interceptors
{


    // SaveChangesInterceptor
    public class AuditInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            var entiries = eventData.Context.ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entiries)
            {
                if (entry.State == EntityState.Added)
                {
                    // İnsert işlemi
                    entry.Entity.PublicationDate = DateTime.UtcNow;


                }
                if (entry.State == EntityState.Modified)
                {
                    // Update İşlemi Yapımaya çalışılıyor
                    entry.Entity.ModifiedDate = DateTime.UtcNow;
                }

            }
            return base.SavingChanges(eventData, result);

        }
    }
}
