namespace CaseBridge.Domain.Entities;

public class Process
{
    public int Id { get; set; }
    public string Title { get; set; }
    public StatusEnum Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public Process(string title)
    {
        Id = 0;
        Title = title;
        Status = StatusEnum.Aberto;
        CreatedAt = DateTime.Now;
    }
}
