namespace Ilia.ControleDePonto.Domain
{
    public class Registro
    {
        public string Dia { get; set; }
        public List<string> Horarios { get; set; }

        public Registro() { }

        public Registro(List<MomentoData> momentos)
        {
            if (momentos == null || momentos.Count == 0 || momentos.Count > 4 || momentos.Select(m => m.Data).Distinct().Count() != 1)
                throw new ArgumentException("Lista de Momentos inválida para ser convertida para Registro");

            Horarios = new List<string>();
            Dia = momentos.FirstOrDefault().Data.ToString("yyyy-MM-dd");

            foreach (MomentoData momento in momentos.OrderBy(m => m.Data).ThenBy(m => m.Hora))
                Horarios.Add(momento.Hora.ToString("HH:mm:ss"));
        }
    }
}
