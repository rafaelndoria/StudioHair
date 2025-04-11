using StudioHair.Application.Services.Implementations;

namespace StudioHair.Test.Services
{
    public class CpfServiceTests
    {
        [Fact]
        public void InputCpfDigitosIguais_Executa_RetornaFalse()
        {
            // Arrange
            var cpf = "11111111111";
            var cpfService = new CpfService();

            // Act
            var result = cpfService.VerificarDigitosIguais(cpf);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void InputCpfSemDigitosIguais_Executa_RetornaTrue()
        {
            // Arrange
            var cpf = "11111111112";
            var cpfService = new CpfService();

            // Act
            var result = cpfService.VerificarDigitosIguais(cpf);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void InputCpfFormatoInvalido_Executa_RetornaFalse()
        {
            // Arrange
            var cpf = "11111a11111";
            var cpfService = new CpfService();

            // Act
            var result = cpfService.ValidarCPF(cpf);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void InputCpfMenorQue11Digitos_Executa_RetornaFalse()
        {
            // Arrange
            var cpf = "111";
            var cpfService = new CpfService();

            // Act
            var result = cpfService.ValidarCPF(cpf);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void InputCpfInvalido_Executa_RetornaFalse()
        {
            // Arrange
            var cpf = "12334556789";
            var cpfService = new CpfService();

            // Act
            var result = cpfService.ValidarCPF(cpf);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void InputCpfValido_Executa_RetornaTrue()
        {
            // Arrange
            var cpf = "47220886837";
            var cpfService = new CpfService();

            // Act
            var result = cpfService.ValidarCPF(cpf);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyCpfGeradoValido_Executa_RetornaTrue()
        {
            // Arrange
            var cpfService = new CpfService();
            var cpf = cpfService.GerarCPFAleatorio();

            // Act
            var result = cpfService.ValidarCPF(cpf);

            // Assert
            Assert.True(result);
        }
    }
}
