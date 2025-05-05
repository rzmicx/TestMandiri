using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TestMandiri.Data.Models;

[Table("msdetail_user")]
[Index("IdUser", Name = "noncluster_msdetail_user_idUser")]
public partial class MsdetailUser
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idUser")]
    public int? IdUser { get; set; }

    [Column("nama")]
    [Unicode(false)]
    public string? Nama { get; set; }

    [Column("umur")]
    public int? Umur { get; set; }

    [Column("tanggallahir", TypeName = "datetime")]
    public DateTime? Tanggallahir { get; set; }

    [ForeignKey("IdUser")]
    [InverseProperty("MsdetailUsers")]
    public virtual Msuser? IdUserNavigation { get; set; }
}
