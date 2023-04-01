using Core.Domain.Persistence.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Persistence.Entities
{
    public class RoomFeature : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Room Hotel { get; set; }
    }
}
