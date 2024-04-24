using StudioHair.Application.InputModels;
using StudioHair.Application.Services.Interfaces;
using StudioHair.Application.ViewModels;
using StudioHair.Core.Entities;
using StudioHair.Core.Interfaces;

namespace StudioHair.Application.Services.Implementations
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task AtivarCliente(int id)
        {
            var cliente = await _clienteRepository.GetClientePorId(id);
            if (cliente == null)
                throw new Exception("Cliente não encontrado");

            cliente.Ativar();
            await _clienteRepository.AtualizarClienteAsync(cliente);
        }

        public async Task AtualizarCliente(UpdateClienteInputModel inputModel)
        {
            var cliente = await _clienteRepository.GetClientePorId(inputModel.Id);
            if (cliente == null)
                throw new Exception("Cliente não encontrado");

            cliente.Atualizar(inputModel.ClienteEmail,
                           inputModel.ClienteTelefoneCelular,
                           inputModel.ClienteWhatsapp,
                           inputModel.ClienteFacebook,
                           1,
                           inputModel.ClienteObservacao);
            cliente.Pessoa.Atualizar(inputModel.PessoaRua,
                                     inputModel.PessoaCep,
                                     inputModel.PessoaCidade,
                                     inputModel.PessoaBairro,
                                     inputModel.PessoaNumero);
            await _clienteRepository.AtualizarClienteAsync(cliente);
        }

        public async Task CriarCliente(CadastroClienteInputModel inputModel)
        {
            var cliente = new Cliente(inputModel.Email,
                                      inputModel.TelefoneCelular,
                                      inputModel.Whatsapp,
                                      inputModel.Facebook,
                                      1,
                                      inputModel.Observacao != null ? inputModel.Observacao : "Adicionado",
                                      inputModel.PessoaId);

            await _clienteRepository.CriarClienteAsync(cliente);
        }

        public async Task<int> CriarPessoa(CadastroPessoaInputModel inputModel)
        {
            var pessoa = new Pessoa(inputModel.Nome,
                                    inputModel.DataNascimento,
                                    inputModel.Rua,
                                    inputModel.Cep,
                                    inputModel.Cidade,
                                    inputModel.Bairro,
                                    inputModel.Numero,
                                    inputModel.Cpf);

            var id = await _clienteRepository.CriarPessoaAsync(pessoa);

            return id;
        }

        public async Task DeletarPessoa(int pessoaId)
        {
            await _clienteRepository.DeletarPessoaAsync(pessoaId);
        }

        public async Task<ClienteViewModel> GetClienteConfig(int id)
        {
            var cliente = await _clienteRepository.GetClientePorId(id);
            if (cliente == null)
                return null;
            var clienteViewModel = new ClienteViewModel(cliente.Id,
                                                        cliente.Pessoa.Nome,
                                                        cliente.Pessoa.DataDeNascimento,
                                                        cliente.Pessoa.Rua,
                                                        cliente.Pessoa.Cep,
                                                        cliente.Pessoa.Cidade,
                                                        cliente.Pessoa.Bairro,
                                                        cliente.Pessoa.Numero,
                                                        cliente.Pessoa.Cpf,
                                                        cliente.Email,
                                                        cliente.TelefoneCelular,
                                                        cliente.Whatsapp,
                                                        cliente.Facebook,
                                                        cliente.FrequenciaSalaoPorMes,
                                                        cliente.Observacao,
                                                        cliente.DataCadastro,
                                                        cliente.Ativo);

            return clienteViewModel;
        }

        public async Task<IEnumerable<ClientesViewModel>> GetClientes(int page = 1, int pageSize = 5)
        {
            var clientes = await _clienteRepository.GetClientesAsync(page, pageSize);
            if (clientes.Count() == 0)
                return null;

            var clientesViewModel = clientes.Select(x =>
                                                    new ClientesViewModel(x.Id,
                                                                          x.Pessoa.Nome,
                                                                          x.Email,
                                                                          x.TelefoneCelular,
                                                                          x.Whatsapp,
                                                                          x.Ativo));

            return clientesViewModel;
        }

        public async Task<UpdateClienteInputModel> GetClienteUpdate(int id)
        {
            var cliente = await _clienteRepository.GetClientePorId(id);
            if (cliente == null)
                return null;
            var updateCliente = new UpdateClienteInputModel()
            {
                Id = cliente.Id,
                PessoaRua = cliente.Pessoa.Rua,
                PessoaCep = cliente.Pessoa.Cep,
                PessoaCidade = cliente.Pessoa.Cidade,
                PessoaBairro = cliente.Pessoa.Bairro,
                PessoaNumero = cliente.Pessoa.Numero,
                ClienteEmail = cliente.Email,
                ClienteTelefoneCelular = cliente.TelefoneCelular,
                ClienteWhatsapp = cliente.Whatsapp,
                ClienteFacebook = cliente.Facebook,
                ClienteObservacao = cliente.Observacao
            };

            return updateCliente;
        }

        public async Task InativarCliente(int id)
        {
            var cliente = await _clienteRepository.GetClientePorId(id);
            if (cliente == null)
                throw new Exception("Cliente não encontrado");

            cliente.Inativar();
            await _clienteRepository.AtualizarClienteAsync(cliente);
        }
    }
}
