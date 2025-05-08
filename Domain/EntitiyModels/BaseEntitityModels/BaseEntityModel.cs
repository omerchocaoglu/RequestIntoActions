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
        [Key] //Bu özellik (property) birincil anahtardır. Entity Framework bu özelliği veritabanında tabloyu tanımlarken PRIMARY KEY olarak işaretler.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Bu özellik, veritabanının bu alanı otomatik olarak artırarak oluşturmasını söyler. Yani, ID alanı için elle değer vermezsin — veritabanı her yeni kayıt eklediğinde 1, 2, 3, ... gibi değerleri otomatik atar.
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
