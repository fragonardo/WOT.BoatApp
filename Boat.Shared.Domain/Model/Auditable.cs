using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApp.Shared.Domain.Model
{
    public class Auditable
    {
        public int CreatedBy { get; protected set; }
        public DateTime CreatedAt { get; protected  set; }

        public int? UpdatedBy { get; protected  set; }
        public DateTime UpdatedAt { get; protected  set; }
    }
}
