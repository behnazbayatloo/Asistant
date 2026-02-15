public class SerilogConfiguration
{
    public List<string> Using { get; set; }
    public MinimumLevel MinimumLevel { get; set; }
    public List<WriteTo> WriteTo { get; set; }
}
