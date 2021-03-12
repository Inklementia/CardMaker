using SimpleCardMaker.DAL.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCardMaker.DAL
{
    public class DbSeeder
    {
        public static void Seed(CardDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Keywords.Any())
            {
                return;   // DB has been seeded
            }


            var keywords = new Keyword[]
            {

                new Keyword{Name="Taunt",Description="Enemy Units must attack this Unit"},
                new Keyword{Name="Quick Attack",Description="Strikes and deals damage to a Unit first, then takes damage from it"},
                new Keyword{Name="Barrier",Description="Prevents damage for 1 hit"},
                new Keyword{Name="Tough",Description="Reduces damage a Unit takes per hit by 1"},
                new Keyword{Name="Attune",Description="Refills 1 mana"},
            };

            context.Keywords.AddRange(keywords);
            context.SaveChanges();

            var unitTypes = new UnitType[]
            {
      
                new UnitType{Name="Elite",Description="Usually brave warriors"},
                new UnitType{Name="Celestial",Description="Thematically associated with the constelations and etheral beings"},
                new UnitType{Name="Dragon",Description="Thematically associated with dragons"},
                new UnitType{Name="Pirate",Description="Thematically associated with pirates"},
                new UnitType{Name="Demon",Description="Thematically associated with demons"},
            };

            context.UnitTypes.AddRange(unitTypes);
            context.SaveChanges();
        }
    }
}
