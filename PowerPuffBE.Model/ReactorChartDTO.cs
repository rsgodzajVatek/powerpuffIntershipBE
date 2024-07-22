namespace PowerPuffBE.Model;

using Enums;

public class ReactorChartDTO
{
    public long Time {get; set;}
    public int Value {get; set;}
    
    public ReactorStatusEnum Status {get; set;}
}