using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamilyModel;

public partial class Genu
{
    [Key]
    public int GenusId { get; set; }

    [Unicode(false)]
    public string Size { get; set; } = null!;

    [Unicode(false)]
    public string Charecteristic { get; set; } = null!;

    public int FamilyId { get; set; }
    public string Name { get; set; } = null!;

    [ForeignKey("FamilyId")]
    [InverseProperty("Genus")]
    public virtual Family Family { get; set; } = null!;
}
