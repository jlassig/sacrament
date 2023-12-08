using System.ComponentModel.DataAnnotations;

namespace SacramentPlanner.Models
{
    public class Member
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string? FirstName { get; set; }
        [Display(Name ="Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";

        public Boolean Conducting { get; set; }

        public List<Talk> MemberTalks { get; set; } = new List<Talk>();


    }
}
