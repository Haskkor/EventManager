using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _3NetEventManagerS.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Field required."), MaxLength(20, ErrorMessage = "Max 20 caracters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Field required."), MaxLength(20, ErrorMessage = "Max 20 caracters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Field required."), MaxLength(20, ErrorMessage = "Max 20 caracters."), MinLength(4, ErrorMessage = "Min 4 caracters.")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Field required."), MaxLength(20, ErrorMessage = "Max 20 caracters.")]
        public string NickName { get; set; }

        public int RoleId { get; set; }

        public ICollection<int> FriendsListId { get; set; }
    }
}