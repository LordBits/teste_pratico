namespace TestePratico.Models
{
    public class Cadeira
    {
        public int Id { get; init; }
        public int Numero { get; set; }
        public string? Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}