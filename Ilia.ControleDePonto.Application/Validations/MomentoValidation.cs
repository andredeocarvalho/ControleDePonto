using Ilia.ControleDePonto.Domain;

namespace Ilia.ControleDePonto.Application.Validations
{
    public class MomentoValidation : IMomentoValidation
    {
        public (bool, MensagemRetorno) ValidateBadRequest(string momento, out DateTime dataHora)
        {
            bool isBadRequest = new();
            MensagemRetorno mensagem = new();
            dataHora = new();

            if (string.IsNullOrEmpty(momento))
            {
                mensagem.Mensagem = "Campo obrigatório não informado";
                isBadRequest = true;
            }
            else if (!DateTime.TryParse(momento, out dataHora))
            {
                mensagem.Mensagem = "Data e hora em formato inválido";
                isBadRequest = true;
            }

            return (isBadRequest, mensagem);
        }

        public (bool, MensagemRetorno) ValidateForbidden(Registro registro, DateTime dataHora)
        {
            bool isForbidden = new();
            MensagemRetorno mensagem = new();

            if (registro.Horarios.Count >= 4)
            {
                mensagem.Mensagem = "Apenas 4 horários podem ser registrados por dia";
                isForbidden = true;
            }
            else if (new[] { 0, 6 }.Contains((int)dataHora.DayOfWeek))
            {
                mensagem.Mensagem = "Sábado e domingo não são permitidos como dia de trabalho";
                isForbidden = true;
            }
            else if (registro.Horarios.Count == 2)
            {
                _ = TimeOnly.TryParse(registro.Horarios[1], out TimeOnly horaDeSaidaParaAlmoço);
                var horaDeChegadaDoAlmoco = new TimeOnly(dataHora.Hour, dataHora.Minute, dataHora.Second);
                if ((horaDeChegadaDoAlmoco - horaDeSaidaParaAlmoço).TotalHours < 1)
                {
                    mensagem.Mensagem = "Deve haver no mínimo 1 hora de almoço";
                    isForbidden = true;
                }
            }

            return (isForbidden, mensagem);
        }

        public (bool, MensagemRetorno) ValidateConflict(Registro registro, DateTime dataHora)
        {
            bool isConflict = new();
            MensagemRetorno mensagem = new();

            if (registro.Horarios.Contains(dataHora.ToString("HH:mm:ss")))
            {
                mensagem.Mensagem = "Horário já registrado";
                isConflict = true;
            }

            return (isConflict, mensagem);
        }
    }
}
