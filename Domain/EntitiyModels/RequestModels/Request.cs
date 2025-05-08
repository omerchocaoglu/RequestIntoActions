using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.EntitiyModels.ActionModels;
using Domain.EntitiyModels.BaseEntitityModels;
using Domain.EntitiyModels.UserModels;

namespace Domain.EntitiyModels.RequestModels
{
    public class Request: BaseEntityModel
    {
        [Required]
        [DisplayName("İsteğe bağlı başlık")]
        public string Title { get; set; }
        [DisplayName("Mesaj")]
        public string Message {  get; set; }
        [DisplayName("Kullanıcı")]
        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; } // burada isteği hangi user'ın yaptığını görebileceğiz
        [DisplayName("Açıklama")]
        public string? Description { get; set; }
        [DisplayName("Başlangıç Zamanı")]
        public DateTime StartedDate { get; set; }
        [DisplayName("Bitiş Zamanı")]
        public DateTime FinishedDate { get; set; }
        public ICollection<ActionModels.Action> Actions { get; set; }
    }
}
