using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataPribadiNetCoreMVC.Models;

[Table("Department")]
public partial class Department
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string DepartmentName { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string? Abbreviation { get; set; }

    [InverseProperty("Department")]
    public virtual ICollection<JobTitle> JobTitles { get; } = new List<JobTitle>();
}
