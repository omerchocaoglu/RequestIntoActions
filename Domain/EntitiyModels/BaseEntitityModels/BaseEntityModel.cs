using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.EntitiyModels.BaseEntitityModels
{
    public class BaseEntityModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [DisplayName("Bu Kayıt tarihi")]
        public DateTime CreateOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int LastModifiedBy { get; set; }
        [DisplayName("Durum")]
        public Status Status { get; set; }
        public ObjectStatus ObjectStatus { get; set; }
        public ResultStatus ResultStatus { get; set; }
    }
}
