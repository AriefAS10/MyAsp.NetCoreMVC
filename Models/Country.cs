namespace DataPribadiNetCoreMVC.Models;

[Index("Iso", Name = "uc_Countries_Iso", IsUnique = true)]
public partial class Country
{
    [Key]
    public int CountryId { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string Iso { get; set; } = null!;

    [StringLength(80)]
    [Unicode(false)]
    public string Negara { get; set; } = null!;

    [StringLength(3)]
    [Unicode(false)]
    public string? Iso3 { get; set; }

    public int? NumCode { get; set; }

    public int PhoneCode { get; set; }

    [InverseProperty("Country")]
    public virtual ICollection<DataDb> DataDbs { get; } = new List<DataDb>();
}
