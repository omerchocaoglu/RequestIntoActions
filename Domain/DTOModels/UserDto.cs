using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOModels
{
    public class UserDto
    {
        public int ID { get; set; }
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
    }
    public class RequestCreateDTO
    {
        [Required]
        public string Title { get; set; }   
        [Required]
        public string Description { get; set; }
    }
}
