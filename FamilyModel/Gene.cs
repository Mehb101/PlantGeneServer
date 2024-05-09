using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FamilyModel;

[Table("Gene")]

public partial class Gene
{
    [Key]
    public int GeneId { get; set; }

    public string Size { get; set; } = null!;

   
    public string Charecteristic { get; set; } = null!;
    public int cost { get; set; }

    public int FamilyId { get; set; }
    public required string Name { get; set; }

    [ForeignKey("FamilyId")]
    [InverseProperty("Gene")]
    public virtual Family Family { get; set; } = null!;
}
