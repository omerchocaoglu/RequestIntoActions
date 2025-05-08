using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum ObjectStatus
    {
        [Display(Name = "Silinmiş")]
        Delete = 1,
        [Display(Name = "Silinmemiş")]
        NonDelete = 0
    }
}
