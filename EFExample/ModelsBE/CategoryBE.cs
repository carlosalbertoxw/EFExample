//Se comentan data annotations por Fluent API
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

using System.Text.Json.Serialization;

namespace EFExample.ModelsBE
{
    //[Table("Categories")]
    public class CategoryBE
    {
        //[Key]
        public Guid CategoryId { get; set; }

        //[Required]
        //[MaxLength(250)]
        public string Name { get; set; }
        public string Description { get; set; }

        public int Order { get; set; }

        [JsonIgnore]
        public virtual ICollection<TaskBE> Tasks { get; set; }
    }
}
