namespace Domain;

public class Image
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public string Name { get; set; }
    public string Extension { get; set; }

    public Guid? EntityId { get; set; } 
    public string EntityType { get; set; } 
    public object Entity { get; set; }

}