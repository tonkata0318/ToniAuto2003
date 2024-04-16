using ToniAuto2003.Core.Models.Agent;

namespace ToniAuto2003.Core.Models.Car
{
    public class CarDetailsServiceModel : CarServiceModel
    {
        public string Description { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string Leasing { get; set; } = null!;
        
        public AgentServiceModel Agent { get; set; }
    }
}
