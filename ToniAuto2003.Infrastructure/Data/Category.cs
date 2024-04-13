
using System.ComponentModel.DataAnnotations;
using static ToniAuto2003.Infrastructure.Constants.DataConstraints;

namespace ToniAuto2003.Infrastructure.Data
{
    public class Category
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(MaxCategoryName)]
        public string Name { get; set; } = string.Empty;


        public List<Car> Cars=new List<Car>();

    }
}