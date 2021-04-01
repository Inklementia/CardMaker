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

            // Look for any keywords or unitTypes.
            if (context.Keywords.Any() && context.UnitTypes.Any())
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

            var cards = new Card[]
           {

                new Card{Name="Example Card",Description="Just an example of a card", Attack = 3, Defence =3, ManaCost=2, ImageFileName="uploads/default.png", KeywordId=1, UnitTypeId=1},
                new Card{Name="Example Card 2",Description="Just an example of a card 2", Attack = 5, Defence =3, ManaCost=2, ImageFileName="uploads/default.png", KeywordId=2, UnitTypeId=1},

           };

            context.Cards.AddRange(cards);
            context.SaveChanges();
        }
    }
}
