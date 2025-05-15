using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.EntitiyModels.BaseEntitityModels;

namespace Domain.EntitiyModels.AuditLogModels
{
    public class AuditLog:BaseEntityModel
    {
        public int ID { get; set; }
        [Display(Name = "Tablo Adı")]
        public string TableName { get; set; }
        [Display(Name = "Kayıt ID")]
        public int RecordID { get; set; }
        [Display(Name = "Operasyon türü")]
        public string OperationType { get; set; } // Örn: "Create", "Update", "Delete"
        [Display(Name = "Değişen içerik bilgisi")]
        public string Changes { get; set; }
        [Display(Name = "Eski Veriler")]
        public string OldValues { get; set; }
        [Display(Name = "Yeni Veriler")]
        public string NewValues { get; set; }
    }
}
