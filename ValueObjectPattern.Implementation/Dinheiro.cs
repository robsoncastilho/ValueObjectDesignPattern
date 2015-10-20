using System;

namespace ValueObjectPattern.Implementation
{
    /// <summary>
    /// Classe pode ser incrementada como por exemplo implementar outros operators, utilizar exceções customizadas e 
    /// implementar outras operações com Dinheiro além do 'SomarCom' mas o código atual já demonstra o que é um VO
    /// </summary>
    public class Dinheiro
    {
        public string Moeda { get; private set; }
        public decimal Valor { get; private set; }

        public Dinheiro(string moeda, decimal valor)
        {
            if (string.IsNullOrWhiteSpace(moeda)) throw new ArgumentNullException("moeda");
            if (valor <= 0) throw new ArgumentException("Valor deve ser maior que zero", "valor");
            Moeda = moeda;
            Valor = valor;
        }

        public Dinheiro SomarCom(Dinheiro outroDinheiro)
        {
            if (Moeda != outroDinheiro.Moeda) throw new InvalidOperationException("Não é possível somar valores de moedas diferentes");
            return new Dinheiro(outroDinheiro.Moeda, Valor + outroDinheiro.Valor);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            var dinheiro = obj as Dinheiro;
            if (ReferenceEquals(null, dinheiro)) return false;
            return (Moeda == dinheiro.Moeda && Valor == dinheiro.Valor);
        }

        public override int GetHashCode()
        {
            return Moeda.GetHashCode() ^ Valor.GetHashCode();
        }

        public static bool operator ==(Dinheiro dinheiro, Dinheiro outroDinheiro)
        {
            return Equals(dinheiro, outroDinheiro);
        }

        public static bool operator !=(Dinheiro dinheiro, Dinheiro outroDinheiro)
        {
            return !Equals(dinheiro, outroDinheiro);
        }

        public static bool operator >=(Dinheiro dinheiro, Dinheiro outroDinheiro)
        {
            return dinheiro.Valor >= outroDinheiro.Valor;
        }

        public static bool operator <=(Dinheiro dinheiro, Dinheiro outroDinheiro)
        {
            return dinheiro.Valor <= outroDinheiro.Valor;
        }

        public static implicit operator decimal (Dinheiro dinheiro)
        {
            return dinheiro.Valor;
        }

        public override string ToString()
        {
            return string.Format("{0} {1:n2}", Moeda, Valor);
        }
    }
}
