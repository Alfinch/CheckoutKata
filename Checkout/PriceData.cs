namespace Checkout;

public class PriceData : IPriceData
{
    public IReadOnlyDictionary<string, decimal> UnitPrices { get; }
    public IReadOnlyCollection<ISpecialPrice> SpecialPrices { get; }

    public PriceData(IReadOnlyDictionary<string, decimal> unitPrices, IReadOnlyCollection<ISpecialPrice> specialPrices)
    {
        UnitPrices = unitPrices ?? throw new ArgumentNullException(nameof(unitPrices));
        SpecialPrices = specialPrices ?? throw new ArgumentNullException(nameof(specialPrices));
    }

    /// <summary>
    /// A shorthand constructor for creating a PriceData object with a dictionary of unit prices and a dictionary of condensed special prices.
    /// </summary>
    /// <param name="unitPrices"></param>
    /// <param name="condensedSpecialPrices"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public PriceData(Dictionary<string, decimal> unitPrices, Dictionary<string, decimal> condensedSpecialPrices)
    {
        UnitPrices = unitPrices ?? throw new ArgumentNullException(nameof(unitPrices));
        SpecialPrices = condensedSpecialPrices != null ? ExpandSpecialPrices(condensedSpecialPrices) : throw new ArgumentNullException(nameof(condensedSpecialPrices));
    }

    /// <summary>
    /// Expands a dictionary of SKU combinations and prices into a collection of special prices.
    /// </summary>
    /// <param name="condensedSpecialPrices"></param>
    /// <returns></returns>
    private IReadOnlyCollection<ISpecialPrice> ExpandSpecialPrices(Dictionary<string, decimal> condensedSpecialPrices)
    {
        return condensedSpecialPrices
            .Select(csp => new SpecialPrice(
                csp.Key.Split(',').Select(sku => sku.Trim()).ToArray(),
                csp.Value
            ))
            .ToList();
    }
}
