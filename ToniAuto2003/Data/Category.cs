
using System.ComponentModel.DataAnnotations;
using static ToniAuto2003.Data.DataConstraints;

namespace ToniAuto2003.Data
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxCategoryName)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Catalogue> Catalogues { get; set; } = new List<Catalogue>();
    }
}