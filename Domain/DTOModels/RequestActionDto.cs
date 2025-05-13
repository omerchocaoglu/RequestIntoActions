using System;
using System.Collections.Generic;
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
        public string Description { get; set; }
    }
}
