using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.EntitiyModels.BaseEntitityModels;
using Domain.EntitiyModels.RequestModels;
using Domain.EntitiyModels.UserModels;
using Domain.Enums;

namespace Domain.EntitiyModels.ActionModels
{
    public class RequestAction:BaseEntityModel
    {
        public string Message { get; set; }
        public Request Request { get; set; } // Navigation
        public int RequestID { get; set; } // Foreign key
        public int? UserID { get; set; } // Foreign key
        public User? User { get; set; } // Navigation
        public string? Description { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime FinishedDate { get; set; }
    }
}
