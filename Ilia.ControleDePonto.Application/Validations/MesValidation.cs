using Ilia.ControleDePonto.Domain;

namespace Ilia.ControleDePonto.Application.Validations
{
    public class MesValidation : IMesValidation
    {
        public (bool, MensagemRetorno) ValidateMes(string mes)
        {
            bool isBadRequest = new();
            MensagemRetorno mensagem = new();

            if (string.IsNullOrEmpty(mes))
            {
                mensagem.Mensagem = "Campo obrigatório não informado";
                isBadRequest = true;
            }
            else if (mes.Length != 7 || !DateOnly.TryParse(mes, out _))
            {
                mensagem.Mensagem = "Ano e mês em formato inválido";
                isBadRequest = true;
            }

            return (isBadRequest, mensagem);
        }
    }
}
