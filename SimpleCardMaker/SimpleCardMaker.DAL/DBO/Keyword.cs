using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SimpleCardMaker.DAL.DBO
{
    public class Keyword : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Keyword Name")]
        public string Name { get; set; }

  
        [DisplayName("Keyword Description")]
        public string Description { get; set; }

        [DisplayName("Keyword Icon")]
        public string Icon { get; set; } = "fas fa-bolt";

        [JsonIgnore]
        public virtual ICollection<Card> Cards { get; set; }
    }
}
