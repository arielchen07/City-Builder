/// <summary>
/// The IProvicer interface is implemented by all utility provider classes <br/>
/// </summary>

interface IProvider {
    /// <summary>
    /// Allocate finds all housing objects within the provider's range and attempts to provide the respective utility. <br/>
    /// Each provider implements the Allocate function slightly differently. <br/>
    /// </summary>
    void Allocate();
}
