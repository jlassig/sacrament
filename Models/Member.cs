namespace SacramentPlanner.Models
{
    public class Member
    {
        public int Id { get; set; }


        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Boolean Conducting { get; set; }

        public List<Talk> MemberTalks { get; set; } = new List<Talk>();


    }
}
