using StudioHair.Application.InputModels;
using StudioHair.Application.Services.Interfaces;
using StudioHair.Application.ViewModels;
using StudioHair.Core.Entities;
using StudioHair.Core.Interfaces;

namespace StudioHair.Application.Services.Implementations
{
    public class ServicoService : IServicoService
    {
        private readonly IServicoRepository _servicoRepository;

        public ServicoService(IServicoRepository servicoRepository)
        {
            _servicoRepository = servicoRepository;
        }

        public async Task AtualziarServico(AtualizarServicoInputModel model)
        {
            var servico = await _servicoRepository.GetServicoPorIdAsync(model.Id);
            if (servico == null)
                throw new Exception("Serviço não encontrado");

            servico.Atualizar(model.Nome, model.DuracaoMinutos, model.ValorServico);
            await _servicoRepository.AtualizarServicoAsync(servico);
        }

        public async Task CriarServico(CadastroServicoInputModel inputModel)
        {
            var servico = new Servico(inputModel.Nome, inputModel.DuracaoMinutos, inputModel.ValorServico);
            await _servicoRepository.CriarServicoAsync(servico);
        }

        public async Task<ServicoViewModel> GetServicoConfig(int id)
        {
            var servico = await _servicoRepository.GetServicoPorIdAsync(id);
            if (servico == null)
                throw new Exception("Serviço não encontrado");

            var servicoViewModel = new ServicoViewModel(servico.Id, servico.Nome, servico.DuracaoEmMinutos, servico.ValorServico);
            return servicoViewModel;
        }

        public async Task<AtualizarServicoInputModel> GetServicoParaUpdate(int id)
        {
            var servico = await _servicoRepository.GetServicoPorIdAsync(id);
            if (servico == null)
                throw new Exception("Serviço não encontrado");

            var servicoInputModel = new AtualizarServicoInputModel() 
            { 
                Id = servico.Id,
                Nome = servico.Nome,
                DuracaoMinutos = servico.DuracaoEmMinutos,
                ValorServico = servico.ValorServico
            };

            return servicoInputModel;
        }

        public async Task<IEnumerable<ServicosViewModel>> GetServicos()
        {
            IEnumerable<ServicosViewModel> list;
            var servicos = await _servicoRepository.GetServicosAsync();
            if (servicos == null)
                list = new List<ServicosViewModel>();
            var servicosViewModel = servicos.Select(x =>
                                                        new ServicosViewModel(x.Id, x.Nome, x.DuracaoEmMinutos, x.ValorServico));
            list = servicosViewModel;
            return list;
        }
    }
}
