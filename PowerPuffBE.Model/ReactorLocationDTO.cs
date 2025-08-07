namespace PowerPuffBE.Model;

public class ReactorLocationDTO
{
    public Guid Id { get; set; }
    public Guid reactorId { get; set; }
    public double latitude { get; set; }
    public double longitude { get; set; }
}