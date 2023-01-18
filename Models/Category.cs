using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Models
{
    public enum Category
    {
        [Display(Name="Single Home")]
        SingleHome,
        [Display(Name = "Town Home")]
        TownHome,
        Apartment
    }
}
