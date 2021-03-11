using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCardMaker.Models
{
    public class Keyword
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Keyword Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Keyword Description")]

        public string Description { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}
