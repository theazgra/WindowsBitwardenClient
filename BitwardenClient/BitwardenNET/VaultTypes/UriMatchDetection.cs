
namespace BitwardenNET.VaultTypes
{
    public enum UriMatchDetection
    {
        Default = -1,
        BaseDomain = 0,
        Host = 1,
        StartsWith = 2,
        Exact = 3,
        RegularExpression = 4,
        Never = 5
    }
}
