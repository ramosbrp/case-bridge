namespace CaseBridge.Domain.Entities;

public class Process
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Number { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public Process(string title)
    {
        Id = 0;
        Title = title;
        Number =  DateTime.Now.ToString("yyyyMMddHHmmss");
        Status = StatusEnum.Aberto.ToString();
        CreatedAt = DateTime.Now;
    }
}
