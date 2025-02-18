using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entity
{
    public class BaseEntity : IBaseEntity
    {

        public int Id { get; set; }
        public Nullable<bool> Active { get; set; }
        public DateTime? PublicationDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public int? CreatorId { get; set; }
        public int? ModifierId { get; set; }

    }
}
