using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CardMaker.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the CardMakerUser class
    public class CardMakerUser : IdentityUser
    {
    
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string NickName { get; set; }

    }
}
