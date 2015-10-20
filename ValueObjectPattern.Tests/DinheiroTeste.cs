using NUnit.Framework;
using System;
using ValueObjectPattern.Implementation;

namespace ValueObjectPattern.Tests
{
    [TestFixture]
    public class DinheiroTeste
    {
        private Dinheiro _dinheiro, _mesmoDinheiro, _dinheiroDiferente;

        [SetUp]
        public void SetUp()
        {
            _dinheiro = new Dinheiro("R$", 10.00m);
            _mesmoDinheiro = new Dinheiro("R$", 10.00m);
            _dinheiroDiferente = new Dinheiro("US$", 10.01m);
        }

        [Test]
        public void Deve_comparar_valores()
        {
            Assert.IsTrue(_dinheiro.Equals(_mesmoDinheiro));
            Assert.IsFalse(_dinheiro.Equals(_dinheiroDiferente));
        }

        [Test]
        public void Deve_comparar_hashcodes()
        {
            Assert.IsTrue(_dinheiro.GetHashCode() == _mesmoDinheiro.GetHashCode());
            Assert.IsFalse(_dinheiro.GetHashCode() == _dinheiroDiferente.GetHashCode());
        }

        [Test]
        public void Deve_comparar_usando_operadores_igual_e_diferente()
        {
            Assert.IsTrue(_dinheiro == _mesmoDinheiro);
            Assert.IsTrue(_dinheiro != _dinheiroDiferente);
        }

        [Test]
        public void Deve_somar_dois_valores_de_mesma_moeda()
        {
            var novoDinheiro = _dinheiro.SomarCom(_mesmoDinheiro);

            Assert.AreEqual(new Dinheiro("R$", 20.00m), novoDinheiro);
        }

        [Test]
        public void Nao_deve_criar_sem_informar_moeda()
        {
            Assert.Throws<ArgumentNullException>(() => new Dinheiro("", 10m));
        }

        [Test]
        public void Nao_deve_criar_com_valor_menor_igual_zero()
        {
            Assert.Throws<ArgumentException>(() => new Dinheiro("R$", 0m));
        }

        [Test]
        public void Nao_deve_permitir_somar_dois_valores_de_moedas_diferentes()
        {
            Assert.Throws<InvalidOperationException>(() => _dinheiro.SomarCom(_dinheiroDiferente));
        }

        [Test]
        public void Deve_formatar_como_string()
        {
            var dinheiro = new Dinheiro("R$", 10.50m);

            var dinheiroComoString = dinheiro.ToString();

            var esperado = string.Format("{0} {1:n2}", dinheiro.Moeda, dinheiro.Valor);
            Assert.AreEqual(esperado, dinheiroComoString);
        }

        [TestCase(10.50, 11.50, true)]
        [TestCase(10.50, 10.50, true)]
        [TestCase(10.51, 10.50, false)]
        public void Deve_comparar_valores_com_sinal_de_menor_ou_igual(decimal valorEsquerda, decimal valorDireita, bool esperado)
        {
            Assert.AreEqual(esperado, new Dinheiro("R$", valorEsquerda) <= new Dinheiro("R$", valorDireita));
        }

        [TestCase(10.51, 10.50, true)]
        [TestCase(10.50, 10.50, true)]
        [TestCase(10.50, 11.50, false)]
        public void Deve_comparar_valores_com_sinal_de_maior_ou_igual(decimal valorEsquerda, decimal valorDireita, bool esperado)
        {
            Assert.AreEqual(esperado, new Dinheiro("R$", valorEsquerda) >= new Dinheiro("R$", valorDireita));
        }
    }
}
