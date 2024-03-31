using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FamilyModel;

[Table("Family")]
public partial class Family
{
    [Key]
    public int FamilyId { get; set; }

    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty("Family")]
    public virtual ICollection<Genu> Genus { get; set; } = new List<Genu>();
}
