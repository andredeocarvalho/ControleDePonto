using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ilia.ControleDePonto.Domain
{
    [Table("Momento")]
    public class MomentoData
    {
        [Key]
        [Column("Data", Order = 1)]
        public DateOnly Data { get; set; }

        [Key]
        [Column("Hora", Order = 2)]
        public TimeOnly Hora { get; set; }
    }
}