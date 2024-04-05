using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PCShop.Models;

[Table("PC")]
public partial class Pc
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public int? Memory { get; set; }

    [Column("Num_USB_2")]
    public int? NumUsb2 { get; set; }

    [Column("Num_USB_3")]
    public int? NumUsb3 { get; set; }

    [Column("Num_USB_C")]
    public int? NumUsbC { get; set; }

    public double? Weight { get; set; }

    public int? WeightUnit { get; set; }

    [Column("PSU")]
    [StringLength(50)]
    public string? Psu { get; set; }

    [Column("RenderID")]
    public int? RenderId { get; set; }

    [Column("ProcessorID")]
    public int? ProcessorId { get; set; }

    public int? StorageType { get; set; }

    public int? StorageCap { get; set; }

    public int? StorageUnit { get; set; }

    public int? MemoryUnit { get; set; }

    [ForeignKey("MemoryUnit")]
    [InverseProperty("Pcs")]
    public virtual StorageUnit? MemoryUnitNavigation { get; set; }
}
