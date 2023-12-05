using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SacramentPlanner.Models;
using System.Linq;
using SacramentPlanner.Data;
using System.Collections.Generic;

namespace SacramentPlanner.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SacramentPlannerContext(
                serviceProvider.GetRequiredService<DbContextOptions<SacramentPlannerContext>>()))
            {
                List<Member> members = new List<Member>
                {
                    new Member { FirstName = "Bob", LastName = "Jones", Conducting = true },
                    new Member { FirstName = "Jane", LastName = "Smith", Conducting = false },
                    new Member { FirstName = "Jill", LastName = "Garcia", Conducting = false },
                    new Member { FirstName = "Joe", LastName = "Johnson", Conducting = false },
                    new Member { FirstName = "Jack", LastName = "Lakey", Conducting = false },
                    new Member { FirstName = "Trudy", LastName = "Moons", Conducting = false },
                    new Member { FirstName = "Dave", LastName = "Jenkins", Conducting = false },
                };

                // Check if any members exist in the database
                if (!context.Member.Any())
                {
                    // Add members to the database
                    context.Member.AddRange(members);
                    // Save changes to the database
                    context.SaveChanges();
                }

                // Check if any meetings exist in the database
                if (!context.Meeting.Any())
                {
                    // Add meetings to the database
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
                            Talks = new List<Talk>
                            {
                                new Talk { MemberId = context.Member.Single(i => i.LastName == "Lakey").Id, Subject = "Christmas in our hearts" },
                                new Talk { MemberId = context.Member.Single(i => i.LastName == "Garcia").Id, Subject = "The meaning of Christmas" }
                            }
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
                            Talks = new List<Talk>
                            {
                                new Talk { MemberId = context.Member.Single(i => i.LastName == "Lakey").Id, Subject = "Everybody loves Christmas" },
                                new Talk { MemberId = context.Member.Single(i => i.LastName == "Garcia").Id, Subject = "Christmas all year round" },
                                new Talk { MemberId = context.Member.Single(i => i.LastName == "Johnson").Id, Subject = "The Joy of Christmas" }
                            }
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
                            Talks = new List<Talk>
                            {
                                new Talk { MemberId = context.Member.Single(i => i.LastName == "Moons").Id, Subject = "New Year, New Me" },
                                new Talk { MemberId = context.Member.Single(i => i.LastName == "Jenkins").Id, Subject = "Goals that bring me closer to God" }
                            }
                        }
                    );

                    // Save changes to the database
                    context.SaveChanges();
                }
            }
        }
    }
}