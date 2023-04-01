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
    public class Room : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("RoomType")]
        public int RoomTypeId { get; set; }

        [ForeignKey("Hotel")]
        public int HotelId { get; set; }
        public int RoomNumber { get; set; }
        public float Capacity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public virtual RoomType RoomType { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual ICollection<RoomFeature> Features { get; set; }
    }
}
