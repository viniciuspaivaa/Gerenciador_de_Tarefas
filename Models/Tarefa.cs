public class Tarefa
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public DateTime DataCriada { get; set; } = DateTime.Now;
    public int Progresso { get; set; } // 0 = pendente, 1 = concluída
}