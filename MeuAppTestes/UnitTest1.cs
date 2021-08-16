using System;
using Xunit;

namespace MeuAppTestes
{
    public class UnitTest1
    {
        //Teste Unitário -> Testa apenas um método/unidade
        //Teste de Integração -> Testa um método que chama outro
        //Teste de Sistema -> Testa Tudo
        [Fact]
        public void OResultadoDaSomaDeveSerQuatro()
        {
            Assert.Equal(4, Add(2, 2)); //Primeiro parâmetro -> Esperado, Segundo parâmetro -> valor atual
        }

        [Fact]
        public void OResultadoDaSomaDeveSerSeis()
        {
            Assert.Equal(4, Add(3, 3)); //Primeiro parâmetro -> Esperado, Segundo parâmetro -> valor atual
        }

        [Fact]
        public void CriaUmDiretor()
        {
            var diretor = new Diretor("Nome Teste", "EmailTeste@teste.com");
            Assert.Equal("Nome Teste", diretor.Nome);
        }


        [Fact]
        public void Test()
        {
            Assert.NotEqual(4, Sum(2, 2));
        }

        int Sum(int x, int y)
        {
            return x + y + 1;
        }
        int Add(int x, int y)
        {
            return x + y;
        }

        [Theory]
        [InlineData(3)] //Teste com o numero 3, 2, 1
        [InlineData(2)]
        [InlineData(1)]
        public void MyFirstTheory(int value)
        {
            Assert.True(MenorQueQuatro(value));
        }

        bool MenorQueQuatro(int value)
        {
            return value < 4;
        }



    }
}
