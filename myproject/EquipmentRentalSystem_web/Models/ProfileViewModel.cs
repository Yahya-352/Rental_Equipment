using System.ComponentModel.DataAnnotations;

namespace EquipmentRentalSystem_web.Models
{
    public class ProfileViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
