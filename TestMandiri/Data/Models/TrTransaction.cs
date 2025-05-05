using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TestMandiri.Data.Models;

[Table("TrTransaction")]
[Index("IdItem", Name = "noncluster_TrTransaction_idItem")]
[Index("IdUser", Name = "noncluster_TrTransaction_idUser")]
public partial class TrTransaction
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idUser")]
    public int? IdUser { get; set; }

    [Column("idItem")]
    [StringLength(5)]
    [Unicode(false)]
    public string? IdItem { get; set; }

    [Column("qty")]
    public int? Qty { get; set; }

    [Column("createby")]
    [Unicode(false)]
    public string? Createby { get; set; }

    [Column("createat", TypeName = "datetime")]
    public DateTime? Createat { get; set; }

    [ForeignKey("IdItem")]
    [InverseProperty("TrTransactions")]
    public virtual Msitem? IdItemNavigation { get; set; }

    [ForeignKey("IdUser")]
    [InverseProperty("TrTransactions")]
    public virtual Msuser? IdUserNavigation { get; set; }
}
