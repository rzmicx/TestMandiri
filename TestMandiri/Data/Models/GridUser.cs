using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TestMandiri.Data.Models;

[Keyless]
public partial class GridUser
{
    [Column("id")]
    public int Id { get; set; }

    [Column("username")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Username { get; set; }

    [Column("nama")]
    [Unicode(false)]
    public string? Nama { get; set; }

    [Column("item")]
    [StringLength(8000)]
    [Unicode(false)]
    public string? Item { get; set; }

    [Column("total")]
    public int? Total { get; set; }
}
