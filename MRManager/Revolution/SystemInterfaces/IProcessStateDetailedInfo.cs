using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IProcessStateDetailedInfo
    {
        string Status { get; }
        string Notes { get; }
    }
}