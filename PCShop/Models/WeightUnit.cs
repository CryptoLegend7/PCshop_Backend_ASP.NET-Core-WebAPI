using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PCShop.Models;

[Table("WeightUnit")]
public partial class WeightUnit
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(10)]
    public string? Unit { get; set; }
}
