using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCardMaker.DAL.DBO
{
    public class Card : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, Set {0} for your card")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please, Set {0} for your card")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please, Set {0} value for your card")]
        [Range(0, int.MaxValue, ErrorMessage = "You do not want to have negative value here")]
        [DisplayName("Mana Cost")]
        public int ManaCost { get; set; } = 0;

        [Required(ErrorMessage = "Please, Set {0} value for your card")]
        [Range(0, int.MaxValue, ErrorMessage = "You do not want to have negative value here")]
        public int Attack { get; set; } = 0;

        [Required(ErrorMessage = "Please, Set {0} value for your card")]
        [Range(0, int.MaxValue, ErrorMessage = "You do not want to have negative value here")]
        public int Defence { get; set; } = 0;

        public string ImageFileName { get; set; }

        [Required(ErrorMessage = "Please, Set {0} for your card")]
        [NotMapped]
        [DisplayName("Image")]

        public IFormFile ImageFile { get; set; }

        [DisplayName("Keyword")]
        public int? KeywordId { get; set; }
        [DisplayName("Unit Type")]
        public int? UnitTypeId { get; set; }
 
        public virtual Keyword Keyword { get; set; }
        public virtual UnitType UnitType { get; set; }
    }
}
