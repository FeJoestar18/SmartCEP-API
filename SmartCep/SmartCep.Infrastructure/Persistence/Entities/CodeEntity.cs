using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCep.Infrastructure.Persistence.Entities;

[Table("postal_codes")]
public class CodeEntity
{
    [Key]
    [Column("postal_code")]
    [MaxLength(8)]
    public string PostalCode { get; set; } = null!;

    [Column("street")]
    [MaxLength(200)]
    public string Street { get; set; } = null!;
    
    [Column("neighborhood")]
    [MaxLength(100)]
    public string Neighborhood { get; set; } = null!;
    
    [Column("city")]
    [MaxLength(100)]
    public string City { get; set; } = null!;
    
    [Column("state")]
    [MaxLength(2)]
    public string State { get; set; } = null!;
}

