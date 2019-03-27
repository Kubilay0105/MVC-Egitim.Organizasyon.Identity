using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimOrg.Entity.Entity
{
    public class RollCall : EntityBase
    {
        public RollCall()
        {
            IsHere = true;
        }
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        public bool IsHere { get; set; }

        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

    }
}
