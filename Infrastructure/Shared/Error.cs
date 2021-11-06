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
    }
}
