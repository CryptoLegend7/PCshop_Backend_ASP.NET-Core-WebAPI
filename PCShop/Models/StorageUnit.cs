using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PCShop.Models;

[Table("StorageUnit")]
public partial class StorageUnit
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(10)]
    public string? Unit { get; set; }

    [InverseProperty("MemoryUnitNavigation")]
    public virtual ICollection<Pc> Pcs { get; set; } = new List<Pc>();
}
