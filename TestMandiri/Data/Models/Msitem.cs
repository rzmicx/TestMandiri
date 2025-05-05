using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TestMandiri.Data.Models;

[Table("msitem")]
public partial class Msitem
{
    [Key]
    [Column("id")]
    [StringLength(5)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [Column("namaitem")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Namaitem { get; set; }

    [Column("qty")]
    public int Qty { get; set; }

    [InverseProperty("IdItemNavigation")]
    public virtual ICollection<TrTransaction> TrTransactions { get; set; } = new List<TrTransaction>();
}
