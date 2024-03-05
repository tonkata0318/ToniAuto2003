using System.ComponentModel.DataAnnotations;

namespace ToniAuto2003.Services.Client.Models
{
    public class ClientIndexModel
    {
        [Required]
        [Display(Name="Money")]
        [Range(3000,250000)]
        public decimal amount { get; set; }

    }
}
