using System.ComponentModel.DataAnnotations;

namespace SacramentPlanner.Models
{

    public class Talk
    {
        public int Id { get; set; }

        public string? Subject { get; set; }

        //Foreign key to link member to a talk to a meeting
        public int MemberId { get; set; }
        public Member? Member { get; set; } 


        // Foreign key to link Speaker to a Meeting
        public int MeetingId { get; set; }
        public Meeting? Meeting { get; set; }
        
    }


    public class Meeting
    {
        public int Id { get; set; }

        // Date of meeting
        [DisplayFormat(DataFormatString ="{0:MMMM d, yyyy}")]
        public DateTime Date { get; set; }

        // Conducting leader
        [Display(Name = "Conducting")]
        public string? ConductingLeader  { get; set; }

        // Songs
        [Display(Name = "Opening Hymn")]
        public string? OpeningHymn { get; set; }
        [Display(Name = "Sacrament Hymn")]
        public string? SacramentHymn { get; set; }
        [Display(Name = "Closing Hymn")]
        public string? ClosingHymn { get; set; }

        // Intermediate number or musical number (optional)
        [Display(Name = "Intermediate Hymn")]
        public string? IntermediateHymn { get; set; }
        [Display(Name = "Musical Number")]
        public string? MusicalNumber { get; set; }

        // Opening and closing prayers
        [Display(Name = "Opening Prayer")]
        public string? OpeningPrayer { get; set; }
        [Display(Name = "Closing Prayer")]
        public string? ClosingPrayer { get; set; }

        // Speakers
        public List<Talk> Talks { get; set; } = new List<Talk>();
    }



}
