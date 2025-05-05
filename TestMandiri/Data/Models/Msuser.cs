using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TestMandiri.Data.Models;

[Table("msuser")]
[Index("Username", Name = "noncluster_msuser_username")]
public partial class Msuser
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("username")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Username { get; set; }

    [Column("passcode")]
    public byte[]? Passcode { get; set; }

    [Column ("active")]
    public bool? Active { get; set; }

    [InverseProperty("IdUserNavigation")]
    public virtual ICollection<MsdetailUser> MsdetailUsers { get; set; } = new List<MsdetailUser>();

    [InverseProperty("IdUserNavigation")]
    public virtual ICollection<TrTransaction> TrTransactions { get; set; } = new List<TrTransaction>();
}
