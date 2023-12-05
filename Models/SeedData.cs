using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SacramentPlanner.Models;
using System.Linq;
using SacramentPlanner.Data;


namespace SacramentPlanner.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SacramentPlannerContext(
                serviceProvider.GetRequiredService<DbContextOptions<SacramentPlannerContext>>()))
            {

                var members = new Member[]
                {
                new Member{FirstName = "Bob", LastName="Jones", Conducting=true },
                new Member{FirstName = "Jane", LastName="Smith", Conducting=false },
                new Member{FirstName = "Jill", LastName="Garcia", Conducting=false },
                new Member{FirstName = "Joe", LastName="Garcia", Conducting=false },
                new Member{FirstName = "Jack", LastName="Lakey", Conducting=false },
                new Member{FirstName = "Trudy", LastName="Moons", Conducting=false },
                new Member{FirstName = "Dave", LastName="Jenkins", Conducting=false },

                };

                foreach (Member m in members)
                {
                    context.Member.Add(m);
                }
                context.SaveChanges();




                // Look for any meetings
                if (context.Meeting.Any())
                {
                    return;   // DB has been seeded
                }
                context.Meeting.AddRange(

                new Meeting
                {
                    Date = DateTime.Now.AddDays(7),
                    ConductingLeader = "Bishop John Doe",
                    OpeningHymn = "2 - The Spirit of God",
                    SacramentHymn = "194 - There is a Green Hill Far Away",
                    ClosingHymn = "301 - I am a Child of God",
                    IntermediateHymn = "",
                    MusicalNumber = "",
                    OpeningPrayer = "Sister Jane Doe",
                    ClosingPrayer = "Brother Bob Smith",
                    //Talks = new List<Talk>
                    //{
                    //    new Talk { MemberId=1, MeetingId=1, Subject = "Christmas in our hearts" },
                    //    new Talk { MemberId=2, MeetingId=1, Subject = "The meaning of Christmas" }
                    //}
                },
                new Meeting
                {
                    Date = DateTime.Now.AddDays(14),
                    ConductingLeader = "Bishop John Doe",
                    OpeningHymn = "204 - Silent Night",
                    SacramentHymn = "193 - I Stand All Amazed",
                    ClosingHymn = "209 - Hark the Herald Angels Sing",
                    IntermediateHymn = "",
                    MusicalNumber = "Special song by the Ward Choir",
                    OpeningPrayer = "Sister Jill Garcia",
                    ClosingPrayer = "Brother Joe Garcia",
                    //Talks = new List<Talk>
                    //{
                    //    new Talk { MemberId=members.Single(i=>i.LastName =="Lakey").Id, MeetingId=2, Subject="Everybody loves Christmas" },
                    //    new Talk { MemberId=4, MeetingId=2, Subject = "Christmas all year round" },
                    //    new Talk { MemberId=3, MeetingId=2, Subject = "The Joy of Christmas" }
                    //}
                },

                new Meeting
                {
                    Date = DateTime.Now.AddDays(21),
                    ConductingLeader = "Bishop John Doe",
                    OpeningHymn = "3- Now let us Rejoice",
                    SacramentHymn = "173 - While of these Emblems",
                    ClosingHymn = "215 - Ring Out Wild Bells",
                    IntermediateHymn = "225 - We are Marching On to Glory",
                    MusicalNumber = "",
                    OpeningPrayer = "Sister Jane Doe",
                    ClosingPrayer = "Brother Bob Smith",
                    //Talks = new List<Talk>
                    //{
                    //    new Talk { MemberId=6, MeetingId=3, Subject = "New Year, New Me" },
                    //    new Talk { MemberId=7, MeetingId=3, Subject = "Goals that bring me closer to God" }
                    //}
                }
                );

                context.SaveChanges();



                
            }
        }




    }
}
