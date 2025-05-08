using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.EntitiyModels.BaseEntitityModels;
using Domain.EntitiyModels.RequestModels;
using Domain.Enums;

namespace Domain.EntitiyModels.ActionModels
{
    public class Action:BaseEntityModel
    {
        public string Message { get; set; }
        public Request Request { get; set; }
        public int RequestID { get; set; }
        public string? Description { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime FinishedDate { get; set; }
    }
}
