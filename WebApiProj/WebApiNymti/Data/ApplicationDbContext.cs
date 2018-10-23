using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiNymti.Models.Entities;

namespace WebApiNymti.Data
{
    

    public class ApplicationDbContext : IdentityDbContext<AppUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public   DbSet<Groups> Groups { get; set; }
        public   DbSet<WebApiNymti.Models.Entities.Content> Contents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Groups>().HasData(
                new Groups { GroupsId = 1, GroupsTitle = "Sport" },
                new Groups { GroupsId = 2, GroupsTitle = "Art" },
                new Groups { GroupsId = 3, GroupsTitle = "Politics" }
            );

            modelBuilder.Entity<Content>().HasData(
                new Content { ContentId = 1, GroupsId = 1, ContentText = "Tannehill has quietly gotten 9.3 YPA with seven TD passes this season and should have to throw more than usual as underdogs against a New England team coming off back-to-back losses." },
                new Content { ContentId = 2, GroupsId = 1, ContentText = "After holing a birdie putt early in his afternoon foursomes, McIlroy turned angrily to one spectator who had questioned his technique and shouted: Who can't putt? Who can't putt ? I can putt.I can putt.F * *****come on!" },
                new Content { ContentId = 3, GroupsId = 1, ContentText = "SAINT-QUENTIN-EN-YVELINES, France — The big rumor here Friday morning was that Phil Mickelson would not play at all. Mickelson has had a lousy end to the year. Lately, his tee shots seem to land everywhere but the fairway. And as the morning matches began and Mickelson rode near the first tee in a golf cart with wife Amy in his lap, the best chance to play him had already vanished." },
                new Content { ContentId = 4, GroupsId = 2, ContentText = "The world’s top collectors in 2018—among whose recent acquisitions are a 2011 “Coca-Cola” work by Danh Vo and Arthur Jafa’s widely acclaimed Love Is the Message, The Message Is Death (2016)—include newcomers like Laurene Powell Jobs, the founder of Emerson Collective, a social justice organization, and widow of Steve Jobs; Elizabeth and Phillip Chun, founders of the art-filled resort Paradise City in Incheon, South Korea; philanthropist Suzanne Deal Booth, who recently joined forces with fellow “Top 200” collectors Amanda and Glenn R. Fuhrman to jointly fund an $800,000 artist prize; and two executives from Grupo Televisa, Alfonso de Angoitia Noriega and Bernardo Gómez Martínez." },
                new Content { ContentId = 5, GroupsId = 2, ContentText = "It’s late on a Friday, but some big news just landed in the old ARTnews inbox: the Bronx Museum of the Arts is planning what it’s calling an “artist workspace and exhibition venue” at 80 White Street in the Tribeca section of Lower Manhattan. Slated to open next year, the new location, which measures a hearty 4,500 square feet, will be in the heart of an arts area that has grown rapidly in recent years. Artists Space is working on a major new space, and dealers like Postmasters, Alexander & Bonin, Queer Thoughts, and Bortolami have also set up shop close by." },
                new Content { ContentId = 6, GroupsId = 2, ContentText = "Today’s show: “Maia Cruz Palileo: The Way Back” is on view at Taymour Grahne in London through Sunday, October 7. The solo exhibition, which is the second iteration of the gallery’s nomadic exhibition platform—located now at 93 Piccadilly—presents new work by the New York–based artist." },
                 new Content { ContentId = 7, GroupsId = 3, ContentText = "Politics Back to business: The SEC is off Elon Musk's back. Now he and Tesla can return to that other pesky problem — making and selling cars." },
                new Content { ContentId = 8, GroupsId = 3, ContentText = "Washington (CNN)The FBI investigation into allegations against Supreme Court nominee Brett Kavanaugh is narrowly focused, top officials said in interviews on Sunday, with sources telling CNN that the White House is controlling the scope of the probe." },
                new Content { ContentId = 9, GroupsId = 3, ContentText = "The United States sailed a warship close to disputed islands in the South China Sea on Sunday, a move that is bound to draw the ire of Beijing and comes amid heightened US-China tensions over a broad range of issues." }


            );
            base.OnModelCreating(modelBuilder);



        }

    }
}
