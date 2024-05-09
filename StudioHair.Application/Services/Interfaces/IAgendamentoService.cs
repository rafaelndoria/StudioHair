﻿using StudioHair.Application.InputModels;
using StudioHair.Application.ViewModels;

namespace StudioHair.Application.Services.Interfaces
{
    public interface IAgendamentoService
    {
        Task<CadastroAgendamentoInputModel> PrepararAgendamento();
        Task<int> CriarAgendamentoProv(CadastroAgendamentoInputModel inputModel);
        Task AdicionarServicoAgendamento(CadastroAgendamentoInputModel inputModel);
        Task<IEnumerable<ServicosAgendamentoViewModel>> ListProdutosAgendamento(int id);
        Task<bool> VerificarDisponibilizadaAgendamento(CadastroAgendamentoInputModel inputModel);
        Task SalvarAgendamento(CadastroAgendamentoInputModel inputModel);
        Task<IEnumerable<AgendamentosViewModel>> FiltrarAgendamentos(FiltroAgendamentosInputModel inputModel);
        Task<IEnumerable<AgendamentosViewModel>> GetAgendamentos();
        Task<DetalhesAgendamentoViewModel> GetDetalheAgendamento(int id);
    }
}
