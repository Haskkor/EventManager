using System.ComponentModel.DataAnnotations;

namespace _3NetEventManagerS.Models
{
    public class EventStatus
    {
        [Key]
        public int EventStatusId { get; set; }

        [Required(ErrorMessage = "Field required."), MaxLength(20, ErrorMessage = "Max 20 caracters.")]
        public string Name { get; set; }
    }
}