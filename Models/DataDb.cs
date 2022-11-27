namespace DataPribadiNetCoreMVC.Models;

[Table("DataDB")]
public partial class DataDb
{
    private const string V = "^[0-9]*$";

    [Key]
    [Column("NIK")]
    [Required(ErrorMessage = "Mohon Isi NIK Anda")]
    [Display(Name = "NIK")]
    [StringLength(16, MinimumLength = 16, ErrorMessage = "NIK harus terdiri dari 16 angka")]
    [RegularExpression(V, ErrorMessage = "NIK hanya berisi angka")]
    public string Nik { get; set; } = null!;

    [Column("Nama Lengkap")]
    [Display(Name = "Nama Lengkap")]
    public string NamaLengkap { get; set; } = null!;

    [Column("Jenis Kelamin")]
    [Display(Name = "Jenis Kelamin")]
    [StringLength(10)]
    public string JenisKelamin { get; set; } = null!;

    [Column("Tanggal Lahir", TypeName = "date")]
    [Display(Name = "Tanggal Lahir")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime TanggalLahir { get; set; }

    [Column(TypeName = "text")]
    [Display(Name = "Alamat")]
    public string Alamat { get; set; } = null!;

    [Display(Name = "Negara")]
    public int CountryId { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("DataDbs")]
    public virtual Country Country { get; set; } = null!;
}
