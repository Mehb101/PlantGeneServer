using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyModel;
[Table("Search")]

public partial class Search
{
    [Key]

    public int Cost { get; set; }

    public string Size { get; set; } = null!;
    public string Characteristic { get; set; } = null!;
}
