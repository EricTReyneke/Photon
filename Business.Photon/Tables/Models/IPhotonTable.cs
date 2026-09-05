namespace Business.Photon.Tables.Models
{
    /// <summary>
    /// Defines the common contract for a Photon database table.
    /// </summary>
    internal interface IPhotonTable
    {
        Type ModelType { get; }
    }
}