using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOModels
{
    public class RequestActionDto
    {
        public int ID { get; set; }
        public int RequestID { get; set; }
        public string Description { get; set; } = null!;
        public  DateTime ActionDate { get; set; }
    }
    public class RequestActionCreateDto
    {
        public int RequestID { get; set; }
        [Required(ErrorMessage = "Mesaj zorunludur")]
        public string Message { get; set; }
        [Required(ErrorMessage ="Açıklama zorunludur")]
        public string Description { get; set; }
        public DateTime StartedDate { get; set; } = DateTime.Now;
        public DateTime FinishedDate { get; set; } = DateTime.Now;
    }
    public class RequestActionListDto
    {
        public string Description { get; set; }
        public DateTime CreatedAt {  get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }
}
