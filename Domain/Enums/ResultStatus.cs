using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum ResultStatus
    {
        [Display(Name = "Başarılı")]
        Success = 1,
        [Display(Name = "Başarısız")]
        Failed = 2,
    }
}
