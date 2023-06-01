using Ilia.ControleDePonto.Domain;
using Ilia.ControleDePonto.Repository.Repositories;

namespace Ilia.ControleDePonto.Application.Services
{
    public class RegistroService : IRegistroService
    {
        private readonly IControleDePontoRepository _controleDePontoRepository;

        public RegistroService(IControleDePontoRepository controleDePontoRepository)
        {
            _controleDePontoRepository = controleDePontoRepository;
        }

        public Relatorio? GetRelatorio(string mes)
        {
            Relatorio relatorio = new()
            {
                Mes = mes,
                Registros = _controleDePontoRepository.GetRegistrosPorMes(mes)
            };

            CalcularHoras(relatorio);

            return relatorio;
        }

        public Registro? GetRegistro(DateOnly data) =>
            _controleDePontoRepository.GetRegistro(data);

        public Registro AddMomento(DateTime dataHora)
        {
            var Data = DateOnly.FromDateTime(dataHora);
            var Hora = TimeOnly.FromDateTime(dataHora);

            _controleDePontoRepository.AddMomento(new MomentoData
            {
                Data = Data,
                Hora = Hora
            });

            return _controleDePontoRepository.GetRegistro(Data);
        }

        private static void CalcularHoras(Relatorio relatorio)
        {
            // Não está explícito como devem ser calculados os valores de horas trabalhadas, horas excedentes e horas devidas.
            // Estou colocando uma opção do que poderia ser.

            TimeSpan horasTrabalhadas = new();
            TimeSpan horasExcedentes = new();
            TimeSpan horasDevidas = new();

            var oitoHoras = new TimeSpan(8, 0, 0);

            foreach (var registro in relatorio.Registros)
            {
                var totalNoDia = (TimeOnly.Parse(registro.Horarios[3]) - TimeOnly.Parse(registro.Horarios[2]))
                               + (TimeOnly.Parse(registro.Horarios[1]) - TimeOnly.Parse(registro.Horarios[0]));
                if (totalNoDia > oitoHoras)
                    horasExcedentes += totalNoDia - oitoHoras;
                else if (totalNoDia < oitoHoras)
                    horasDevidas += oitoHoras - totalNoDia;
                horasTrabalhadas += totalNoDia;
            }

            if (horasExcedentes > horasDevidas)
            {
                horasExcedentes -= horasDevidas;
                horasDevidas = new();
            }
            else
            {
                horasDevidas -= horasExcedentes;
                horasExcedentes = new();
            }

            relatorio.HorasTrabalhadas = GetTimeString(horasTrabalhadas);
            relatorio.HorasExcedentes = GetTimeString(horasExcedentes);
            relatorio.HorasDevidas = GetTimeString(horasDevidas);
        }

        private static string GetTimeString(TimeSpan horas)
        {
            var timeString = "PT";
            if (horas.TotalHours != 0)
                timeString += horas.TotalHours + "H";
            if (horas.TotalHours != 0 || horas.Minutes != 0)
                timeString += horas.Minutes + "M";
            timeString += horas.Seconds + "S";
            return timeString;
        }
    }
}
