using System.ComponentModel.DataAnnotations;

namespace _3NetEventManagerS.Models
{
    public class EventType
    {
        [Key]
        public int EventTypeId { get; set; }

        [Required(ErrorMessage = "Field required."), MaxLength(20, ErrorMessage = "Max 20 caracters.")]
        public string Name { get; set; }
    }
}