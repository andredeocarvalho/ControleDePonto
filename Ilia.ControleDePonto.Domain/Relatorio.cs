namespace Ilia.ControleDePonto.Domain
{
    public class Relatorio
    {
        public string Mes { get; set; }
        public string HorasTrabalhadas { get; set; }
        public string HorasExcedentes { get; set; }
        public string HorasDevidas { get; set; }
        public List<Registro> Registros { get; set; }
    }
}
