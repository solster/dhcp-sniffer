
namespace System.Networking.Protocols.Bootstrap
{
    /// <summary>
    /// 
    /// </summary>
    public enum BootstrapQueryType : byte
    {
        Acknowledgement = 5,
        Decline = 4,
        Discover = 1,
        Exception = 6,
        Inform = 8,
        None = 0,
        Offer = 2,
        Release = 7,
        Request = 3
    };
};