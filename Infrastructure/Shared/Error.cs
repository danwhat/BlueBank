namespace Infrastructure.Shared
{
    internal static class Error
    {
        internal static string InsufficientFunds = "Saldo insuficiente.";
        internal static string AccountNotFound = "Conta não encontrada.";
        internal static string AccountFromNotFound = "Conta origem de transferência não encontrada.";
        internal static string AccountToNotFound = "Conta destino da transferência não encontrada.";
        internal static string AccountInvalidId = "Identificador de conta inválido.";
        internal static string AccountAlreadyExists = "Cliente já possui conta ativa.";
        internal static string AccountGetFail = "Falha ao buscar conta.";
        internal static string AccountCreateFail = "Falha ao tentar criar conta. Conta não criada.";
        internal static string PersonInvalidDoc = "Documento do cliente inválido.";
        internal static string PersonNotFound = "Cliente não encontrado.";
        internal static string PersonInvalidType = "Tipo de cliente inválido.";
        internal static string PersonGetFail = "Falha ao tentar buscar cliente.";
        internal static string PersonUpdateFail = "Falha ao tentar atualizar dados de cliente. Dados não alterados.";
        internal static string InitialDateInvalid = "Data inicial maior que data final.";
        internal static string DateInvalid = "Uma das datas é inválida.";
        internal static string ContactNotFound = "Contato não encontrado.";
    }
}