namespace Aula2projeto.Models
{
    public class Atletas
    {
        public int codAtleta { get; set; }
        public string nomeAtleta { get; set; }
        public string dataNascimento { get; set; }
        public char Sexo { get; set; }
        public decimal? Altura { get; set; }
        public decimal? Peso { get; set; }
        public int codCidade { get; set; }
        public int codModalidade { get; set; }
        public string Modalidade { get; set; }
        public string CidadeNascimento { get; set; }
        public string EstadoNascimento { get; set; }
    }
}
