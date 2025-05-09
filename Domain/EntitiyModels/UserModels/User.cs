using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.EntitiyModels.BaseEntitityModels;
using Domain.EntitiyModels.RequestModels;

namespace Domain.EntitiyModels.UserModels
{
    public class User: BaseEntityModel
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        [NotMapped]
        public string FullName => $"{Name} {SurName}";
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Request> Requests { get; set; } // İstekleri bağladık
    }
}
