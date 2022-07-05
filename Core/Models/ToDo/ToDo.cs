using System.ComponentModel.DataAnnotations;

namespace Core.Models.ToDo
{
    public class ToDo
    {
        [Key]
        public int Id { get; set; } = 0;

        [Required]
        [MinLength(1)]
        [MaxLength(300)]
        public string Data { get; set; }

        [Required]
        public State State { get; set; }

        [Required]
        public DateTime LastTimeEditied { get; set; } = DateTime.Now;

        public override string ToString()
        {
            return $"[Id = \"{Id}\", Data = \"{Data}\", State = \"{State}\"]";
        }
    }
}
