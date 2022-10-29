//Se comentan data annotations por Fluent API
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

namespace EFExample.ModelsBE
{
    //[Table("Tasks")]
    public class TaskBE
    {
        //[Key]
        public Guid TaskId { get; set; }

        //[ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }

        //[Required]
        //[MaxLength(250)]
        public string Title { get; set; }

        public string Description { get; set; }

        public Priority PriorityTask { get; set; }

        public DateTime CreationDate { get; set; }

        public virtual CategoryBE Category { get; set; }

        //[NotMapped]
        public string Summary { get; set; }
    }

    public enum Priority
    {
        Low,
        Medium,
        Hight
    }
}
