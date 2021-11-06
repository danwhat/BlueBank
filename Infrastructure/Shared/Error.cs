using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared
{
    static class Error
    {
        internal static string InsufficientFunds = "Saldo insuficiente.";
        internal static string AccountNotFound = "Conta não encontrada.";
        internal static string AccountFromNotFound = "Conta origem de transferência não encontrada.";
        internal static string AccountToNotFound = "Conta destino da transferência não encontrada.";
        internal static string AccountInvalidId = "Identificador de conta inválido.";
        internal static string PersonInvalidDoc = "Documento do cliente inválido.";
        internal static string PersonNotFound = "Cliente não encontrado.";
        internal static string InitialDateInvalid = "Data inicial maior que data final.";
        internal static string DateInvalid = "Uma das datas é inválida.";
    }
}
