using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCardMaker.DAL.DBO
{
    public class UnitType : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Unit Type Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Unit Type Description")]

        public string Description { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}
