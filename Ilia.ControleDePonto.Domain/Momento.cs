using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ilia.ControleDePonto.Domain
{
    [Table("Momento")]
    public class Momento
    {
        [Key]
        [Column("DataHora")]
        public string DataHora { get; set; }
    }
}