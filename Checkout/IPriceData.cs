namespace Checkout;

/// <summary>
/// Represents pricing data for a set of products.
/// </summary>
public interface IPriceData
{
    /// <summary>
    /// Contains each product SKU and its unit price.
    /// </summary>
    public IReadOnlyDictionary<string, decimal> UnitPrices { get; }

    /// <summary>
    /// Contains special pricing for combinations of products.
    /// These should be applied before the unit price, and in the order they are defined.
    /// </summary>
    public IReadOnlyCollection<ISpecialPrice> SpecialPrices { get; }
}
