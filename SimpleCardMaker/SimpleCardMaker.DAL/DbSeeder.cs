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
            // adding keywords
            var keywords = new Keyword[]
            {
                new Keyword{Name="Last Breath", Icon="fas fa-heart-broken", Description="These abilities take effect when the unit dies"},
                new Keyword{Name="Fearsome",Icon="fas fa-skull", Description="Can only be blocked by enemies with 3 or more Attack."},
                new Keyword{Name="Challenger",Icon="far fa-hand-point-right", Description="Can choose which enemy unit blocks."},
                new Keyword{Name="Tough",Icon="fas fa-shield-alt",Description="Takes 1 less damage from all sources."},
            };

            context.Keywords.AddRange(keywords);
            context.SaveChanges();
            // adding unit types
            // for some reason they are seeded backwards
            var unitTypes = new UnitType[]
            {
                new UnitType{Name="Mistwraith",Description="Ghoustly cloud of glowing Mist"},
                new UnitType{Name="Dragon",Description="Thematically associated with dragons"},
                new UnitType{Name="Spider",Description="Thematically associated with spiders"},
                new UnitType{Name="Elite",Description="Brave warriors"},
            };

            context.UnitTypes.AddRange(unitTypes);
            context.SaveChanges();
            // adding cards
            var cards = new Card[]
            {
                new Card{
                    Name="Commander Ledros",
                    Description="Last Breath: Return me to hand.", 
                    Attack=9, 
                    Defence=6, 
                    ManaCost=9, 
                    ImageFileLink="https://cdn-lor.mobalytics.gg/production/images/set1/en_us/img/card/game/01SI033-full.webp", 
                    ArtDescription="Most spirits lost themselves as the passing years eroded their memories. But anguish anchored Ledros to his past. Some things, even time cannot absolve.",
                    KeywordId=1
                },
                new Card{
                    Name="Mistwraith",
                    Description="When I'm summoned, grant other allied Mistwraiths everywhere +1|+0.", 
                    Attack=2, 
                    Defence=2, 
                    ManaCost=2, 
                    ImageFileLink="https://cdn-lor.mobalytics.gg/production/images/set1/en_us/img/card/game/01SI014-full.webp", 
                    ArtDescription="These specters of the Isles shed their identities long ago to become amalgamations of pure, unappeasable hunger.",
                    KeywordId=2, 
                    UnitTypeId=4},
                new Card{
                    Name="Screeching Dragon",
                    Description="",
                    Attack=4,
                    Defence=5,
                    ManaCost=5,
                    ImageFileLink="https://cdn-lor.mobalytics.gg/production/images/set3/en_us/img/card/game/03DE006-full.webp",
                    ArtDescription="All creatures are fiercely protective of their young, but few are as well equipped to deal with would-be threats as a dragon.",
                    KeywordId=3,
                    UnitTypeId=3},
                  new Card{
                    Name="Arachnoid Host",
                    Description="When I'm summoned, grant other Spider allies +2|+0.",
                    Attack=5,
                    Defence=3,
                    ManaCost=5,
                    ImageFileLink="https://cdn-lor.mobalytics.gg/production/images/set1/en_us/img/card/game/01NX023-full.webp",
                    ArtDescription="A single glance and you're hers. A single bite and you're theirs.",
                    KeywordId=2,
                    UnitTypeId=2},
                   new Card{
                    Name="Vanguard Defender",
                    Description="When you summon an Elite, reduce my cost by 1.",
                    Attack=2,
                    Defence=2,
                    ManaCost=2,
                    ImageFileLink="https://cdn-lor.mobalytics.gg/production/images/set1/en_us/img/card/game/01DE020-full.webp",
                    ArtDescription="We didn't know who or what those creatures were. But we knew the faces of our fellow soldiers by our sides, and that was all we needed.",
                    KeywordId=4,
                    UnitTypeId=1},
           };

            context.Cards.AddRange(cards);
            context.SaveChanges();
        }
    }
}
