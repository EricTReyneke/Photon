namespace Abstractions.Photon.Attributes
{
    /// <summary>
    /// Identifies a model property that should not be included as a column
    /// within a Photon table.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class PhotonIgnoreAttribute : Attribute
    {
    }
}