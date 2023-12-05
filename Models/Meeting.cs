namespace SacramentPlanner.Models
{

    public class Talk
    {
        public int Id { get; set; }

        public string? Subject { get; set; }

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
        public DateTime Date { get; set; }

        // Conducting leader
        public string? ConductingLeader  { get; set; }

        // Songs
        public string? OpeningHymn { get; set; }
        public string? SacramentHymn { get; set; }
        public string? ClosingHymn { get; set; }

        // Intermediate number or musical number (optional)
        public string? IntermediateHymn { get; set; }
        public string? MusicalNumber { get; set; }

        // Opening and closing prayers
        public string? OpeningPrayer { get; set; }
        public string? ClosingPrayer { get; set; }

        // Speakers
        public List<Talk> Talks { get; set; } = new List<Talk>();
    }



}
