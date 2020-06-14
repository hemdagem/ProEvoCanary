using System.ComponentModel.DataAnnotations;

namespace ProEvoCanary.Web.Models
{
    public enum TournamentType
    {
	    [Display(Name = "League")]
        League = 1,
        Friendly = 2,
        Knockout = 3,
    }
}