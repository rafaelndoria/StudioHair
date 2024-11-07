using Microsoft.AspNetCore.Http;
using StudioHair.Application.InputModels;
using StudioHair.Application.Services.Interfaces;
using StudioHair.Application.ViewModels;
using StudioHair.Core.Entities;
using StudioHair.Core.Enums;
using StudioHair.Core.Interfaces;
using System;
using System.Globalization;

namespace StudioHair.Application.Services.Implementations
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task AdicionarImagemProduto(IFormFile arquivo, int produtoId)
        {
            // Definir o diretório de upload dentro de wwwroot
            var caminhoPasta = Path.Combine("wwwroot", "uploads");
            if (!Directory.Exists(caminhoPasta))
            {
                Directory.CreateDirectory(caminhoPasta);
            }

            // Gerar o nome do arquivo
            string extensao = Path.GetExtension(arquivo.FileName);
            var nomeArquivoGerado = Guid.NewGuid().ToString() + "_" + produtoId + extensao;
            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivoGerado);

            // Salvar o arquivo no caminho completo
            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            // Salvar o caminho relativo no banco de dados (começando com "/uploads")
            var caminhoRelativo = $"/uploads/{nomeArquivoGerado}";
            var arquivoBanco = new Arquivo(nomeArquivoGerado, caminhoRelativo, produtoId);
            await _produtoRepository.CriarArquivoAsync(arquivoBanco);
        }

        public async Task AtualizarProduto(AtualizarProdutoInputModel inputModel)
        {
            var produto = await _produtoRepository.GetProdutoPorIdAsync(inputModel.ProdutoId);
            if (produto == null)
                throw new Exception("Produto não encontrado");

            produto.Atualizar(inputModel.Nome,
                              inputModel.Marca,
                              inputModel.CodigoBarras,
                              decimal.Parse(inputModel.ValorVendas),
                              inputModel.ProdutoParaVenda,
                              inputModel.ControlaEstoque);
            await _produtoRepository.AtualizarProdutoAsync(produto);
        }

        public async Task AtualizarProdutoInclusao(CadastroProdutoInputModel inputModel)
        {
            var produto = new Produto((int)inputModel.Id, inputModel.Nome, inputModel.Marca, inputModel.CodigoBarras, decimal.Parse(inputModel.ValorVendas), inputModel.ProdutoParaVenda, inputModel.ControlaEstoque, inputModel.Descricao);
            await _produtoRepository.AtualizarProdutoAsync(produto);
        }

        public async Task AtualizarProdutoUnidade(ConfiguracaoUnidadeInputModel inputModel)
        {
            var unidade = await _produtoRepository.GetProdutoUnidadePorIdAsync(inputModel.Id);
            if (unidade == null)
                throw new Exception("Unidade não encontrada");

            var tipoUnidade = (EUnidade)Enum.Parse(typeof(EUnidade), inputModel.TipoUnidade);
            unidade.Atualizar(tipoUnidade, inputModel.Quantidade);
            await _produtoRepository.AtualizarProdutoUnidadeAsync(unidade);
        }

        public async Task<int> CriarProduto(CadastroProdutoInputModel inputModel)
        {
            var valorVendas = decimal.Parse(inputModel.ValorVendas, new CultureInfo("pt-BR"));
            var produto = new Produto(inputModel.Nome,
                                      inputModel.Marca,
                                      inputModel.CodigoBarras,
                                      valorVendas,
                                      inputModel.ProdutoParaVenda,
                                      inputModel.ControlaEstoque,
                                      inputModel.Descricao);

            var id = await _produtoRepository.CriarProdutoAsync(produto);

            if (inputModel.Arquivo != null && inputModel.Arquivo.Length > 0)
            {
                await AdicionarImagemProduto(inputModel.Arquivo, id);
            }

            return id;
        }

        public async Task CriarProdutoUnidade(CadastroProdutoUnidadeInputModel inputModel)
        {
            var produtoUnidade = new ProdutoUnidade((EUnidade)Enum.Parse(typeof(EUnidade), inputModel.TipoUnidade.ToString()), inputModel.Quantidade, inputModel.ProdutoId);
            await _produtoRepository.CriarProdutoUnidadeAsync(produtoUnidade);
       }

        public async Task DeletarImagemProduto(int imagemId)
        {
            var imagem = await _produtoRepository.GetImagemProduto(imagemId);
            if (imagem == null)
                throw new Exception("Imagem não encontrada");

            var caminhoPasta = Path.Combine("wwwroot", "uploads");
            var caminhoCompleto = Path.Combine(caminhoPasta, imagem.NomeArquivo);
            if (File.Exists(caminhoCompleto))
            {
                File.Delete(caminhoCompleto);
            }

            await _produtoRepository.DeletarImagem(imagem);
        }

        public async Task DeletarUnidade(int id)
        {
            var produtoUnidade = await _produtoRepository.GetProdutoUnidadePorIdAsync(id);
            if (produtoUnidade == null)
                throw new Exception("Unidade não encontrada");
            await _produtoRepository.DeletarProdutoUnidadeAsync(produtoUnidade);
        }

        public async Task<ProdutoDetalheCatalogoViewModel> GetDetalhesProduto(int produtoId)
        {
            var produto = await _produtoRepository.GetProdutoPorIdAsync(produtoId);
            if (produto == null)
                throw new Exception("Produto não encontrado");

            var detalhesProduto = new ProdutoDetalheCatalogoViewModel(produto.Nome, produto.Descricao, produto.Marca, produto.ValorPraticado, produtoId, produto.Arquivos[0].Caminho);

            return detalhesProduto;
        }

        public async Task<ImagemProdutoViewModel> GetImagemProduto(int produtoId)
        {
            var imagemProduto = await _produtoRepository.GetArquivoProduto(produtoId);
            if (imagemProduto == null)
            {
                var imagemViewModel = new ImagemProdutoViewModel(0, null, null, produtoId);
                return imagemViewModel;
            }
            else
            {
                var imagemViewModel = new ImagemProdutoViewModel(imagemProduto.Id, imagemProduto.Caminho, imagemProduto.Produto.Nome, produtoId);
                return imagemViewModel;
            }
        }

        public async Task<ProdutoConfigViewModel> GetProdutoConfigPorId(int id)
        {
            var produto = await _produtoRepository.GetProdutoPorIdAsync(id);
            if (produto == null)
                throw new Exception("Produto não encontrado no sistema");
            var produtoViewModel = new ProdutoConfigViewModel(produto.Id, 
                                                              produto.Nome,
                                                              produto.Marca,
                                                              produto.CodigoBarras,
                                                              produto.ValorPraticado,
                                                              produto.ProdutoParaVenda,
                                                              produto.ControlaEstoque);

            return produtoViewModel;
        }

        public async Task<CadastroProdutoInputModel> GetProdutoInputPorId(int id)
        {
            var produto = await _produtoRepository.GetProdutoPorIdAsync(id);

            var produtoInputModel = new CadastroProdutoInputModel()
            {
                Nome = produto.Nome,
                Marca = produto.Marca,
                CodigoBarras = produto.CodigoBarras,
                ValorVendas = produto.ValorPraticado.ToString(),
                ControlaEstoque = produto.ControlaEstoque,
                ProdutoParaVenda = produto.ProdutoParaVenda,
                RedirecionarUnidade = false,
                Id = produto.Id
            };

            return produtoInputModel;
        }

        public async Task<AtualizarProdutoInputModel> GetProdutoParaAtualizar(int id)
        {
            var produto = await _produtoRepository.GetProdutoPorIdAsync(id);
            if (produto == null)
                throw new Exception("Produto não encontrado");

            var produtoInputModel = new AtualizarProdutoInputModel()
            {
                ProdutoId = produto.Id,
                Nome = produto.Nome,
                Marca = produto.Marca,
                CodigoBarras = produto.CodigoBarras,
                ValorVendas = produto.ValorPraticado.ToString(),
                ProdutoParaVenda = produto.ProdutoParaVenda,
                ControlaEstoque = produto.ControlaEstoque
            };

            return produtoInputModel;
        }

        public async Task<IEnumerable<ProdutoViewModel>> GetProdutos()
        {
            var produtos = await _produtoRepository.GetProdutosAsync();

            var viewModel = produtos.Select(x =>
                                new ProdutoViewModel(x.Id, x.Nome, x.Marca, x.ValorPraticado));

            return viewModel; 
        }

        public async Task<CatalogoViewModel> GetProdutosCatalogo(int tamanhoPagina, int paginaAtual, string query)
        {
            // Obtenha o total de produtos para calcular o número total de páginas
            int totalProdutos = await _produtoRepository.CountProdutosAsync();
            int totalPaginas = (int)Math.Ceiling((double)totalProdutos / tamanhoPagina);
            var catalogoViewModel = new CatalogoViewModel(paginaAtual, totalPaginas, tamanhoPagina);

            // Busque os produtos da página atual
            var produtos = await _produtoRepository.GetProdutosCatalogoAsync(tamanhoPagina, paginaAtual, query);
            var list = produtos.Select(x => new CatalogoProdutoVendaViewModel(
                x.Id,
                x.Arquivos?.FirstOrDefault()?.Caminho,
                x.Nome,
                x.ValorPraticado)).ToList();
            catalogoViewModel.Produtos = list;

            return catalogoViewModel;
        }

        public async Task<List<ProdutoUnidadeViewModel>> GetUnidadesPorProdutoId(int id)
        {
            var produto = await _produtoRepository.GetProdutoPorIdAsync(id);
            var unidades = produto.ProdutoUnidades;
            List<ProdutoUnidadeViewModel> list= new List<ProdutoUnidadeViewModel>();    
            if (unidades.Count == 0)
                return list;
            foreach (var unidade in unidades)
            {
                var produtoUnidadViewModel = new ProdutoUnidadeViewModel(unidade.ProdutoId, unidade.Id, unidade.Quantidade, Enum.GetName(typeof(EUnidade), unidade.Unidade));
                list.Add(produtoUnidadViewModel);
            }
            return list;
        }
    }
}
