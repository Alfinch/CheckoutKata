namespace Checkout;

/// <summary>
/// Represents special pricing for a combination of products.
/// </summary>
public interface ISpecialPrice
{
    /// <summary>
    /// The combination of SKUs which should receive the special price.
    /// </summary>
    public IReadOnlyCollection<string> Combination { get; }

    /// <summary>
    /// The special price.
    /// </summary>
    public decimal Price { get; }
}