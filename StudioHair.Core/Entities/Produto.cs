using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioHair.Core.Entities
{
    public class Produto : Entidade 
    {
        public Produto(string nome, string marca, string codigoBarras, decimal valorPraticado, bool produtoParaVenda, bool controlaEstoque)
        {
            Nome = nome;
            Marca = marca;
            CodigoBarras = codigoBarras;
            ValorPraticado = valorPraticado;
            ProdutoParaVenda = produtoParaVenda;
            ControlaEstoque = controlaEstoque;
        }

        public string Nome { get; private set; }
        public string Marca { get; private set; }
        public string CodigoBarras { get; private set; }
        public decimal ValorPraticado { get; private set; }
        public bool ProdutoParaVenda { get; private set; }
        public bool ControlaEstoque { get; private set; }
    }
}
