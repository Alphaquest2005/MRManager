namespace SystemInterfaces
{
    public interface IMachineInfo
    {
        string MachineName { get; }
        string MachineLocation { get; }
        int Processors { get; }
    }
}
