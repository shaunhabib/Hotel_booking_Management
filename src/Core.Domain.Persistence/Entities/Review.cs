using Core.Domain.Persistence.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Persistence.Entities
{
    public class Review : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }

        [ForeignKey("Hotel")]
        public int HotelId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public virtual Hotel Hotel { get; set; }
    }
}
