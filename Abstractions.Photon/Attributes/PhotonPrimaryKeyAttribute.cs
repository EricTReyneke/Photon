namespace Abstractions.Photon.Attributes
{
    /// <summary>
    /// Identifies a model property as the primary key for a Photon table.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class PhotonPrimaryKeyAttribute : Attribute
    {
    }
}