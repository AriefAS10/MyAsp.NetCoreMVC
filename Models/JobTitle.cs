using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataPribadiNetCoreMVC.Models;

[Table("JobTitle")]
public partial class JobTitle
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(10)]
    [Display(Name = "Job Code")]
    public string JobCode { get; set; } = null!;

    [Column("JobTitle")]
    [StringLength(20, ErrorMessage = "Job Title Field must be less than equal to 20 characters long.")]
    [Display(Name = "Job Title")]
    [Required(ErrorMessage = "Job Title Field is required")]
    public string JobTitle1 { get; set; } = null!;

    [Column("DepartmentID")]
    [Display(Name = "Department")]
    [Required(ErrorMessage = "Department must be selected")]
    public int DepartmentId { get; set; }

    [ForeignKey("DepartmentId")]
    [InverseProperty("JobTitles")]
    [Display(Name = "Department")]
    public virtual Department Department { get; set; } = null!;
}
