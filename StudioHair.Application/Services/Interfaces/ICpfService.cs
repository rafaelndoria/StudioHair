namespace StudioHair.Application.Services.Interfaces
{
    public interface ICpfService
    {
        bool ValidarCPF(string cpf);
        string PegarPrimeiroDigitoVerificador(string nineDigits);
        string PegarSegundoDigitoVerificador(string nineDigits, string firstVerifyingDigit);
        bool VerificarDigitosIguais(string cpf);
        string GerarCPFAleatorio();
    }
}
