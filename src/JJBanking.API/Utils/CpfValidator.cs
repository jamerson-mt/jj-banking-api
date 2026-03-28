namespace JJBanking.API.Utils;

public static class CpfValidator
{
    public static bool IsValidCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        // Remove caracteres não numéricos (pontos e traços)
        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11)
            return false;

        // Elimina CPFs com todos os números iguais (ex: 111.111.111-11)
        if (new string(cpf[0], 11) == cpf)
            return false;

        // Cálculo dos dígitos verificadores
        int[] multiplicador1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplicador2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

        string tempCpf = cpf[..9]; //
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        string digito = resto.ToString();
        tempCpf += digito;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        digito += resto.ToString();
        // O CPF é válido se os dígitos calculados forem iguais aos dígitos do CPF fornecido
        return cpf.EndsWith(digito); // retorna true se os dígitos verificadores calculados forem iguais aos do CPF fornecido
    }
}
