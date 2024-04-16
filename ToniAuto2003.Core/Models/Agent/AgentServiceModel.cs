using System.ComponentModel.DataAnnotations;

namespace ToniAuto2003.Core.Models.Agent
{
    public class AgentServiceModel
    {
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
