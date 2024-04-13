using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ToniAuto2003.Data.DataConstraints;

namespace ToniAuto2003.Data
{
    public class Catalogue
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxCatalogName)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;
        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
