namespace CaseBridge.Core;

public class Process
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreateAt { get; set; }
}
