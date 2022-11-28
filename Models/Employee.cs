using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataPribadiNetCoreMVC.Models;

[Table("Employee")]
public partial class Employee
{
    [Key]
    [Column("EmpID")]
    [StringLength(50)]
    [Unicode(false)]
    public string EmpId { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string EmployeeName { get; set; } = null!;

    public int JobTitles { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Phone { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime HiredDate { get; set; }

    public int JobId { get; set; }

    public int Country { get; set; }


    [ForeignKey("EmployeeName")]
    public virtual DataDb DataDb { get; set; } = null!;

    [ForeignKey("JobTitles")]
    public virtual JobTitle JobTitle { get; set; } = null!;
}
