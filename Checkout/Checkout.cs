namespace Checkout;

public class Checkout : ICheckout
{
    private readonly IDictionary<string, PriceData> priceData;
    private readonly IList<string> items = new List<string>();

    public Checkout(IDictionary<string, PriceData> priceData)
    {
        this.priceData = priceData ?? throw new ArgumentNullException(nameof(priceData));
    }

    public void Scan(string sku)
    {
        if (sku == null)
        {
            throw new ArgumentNullException(nameof(sku));
        }

        if (!priceData.ContainsKey(sku))
        {
            throw new ArgumentException($"No price data for product SKU {sku}", nameof(sku));
        }

        items.Add(sku);
    }

    public bool Remove(string sku)
    {
        if (sku == null)
        {
            throw new ArgumentNullException(nameof(sku));
        }

        return items.Remove(sku);
    }

    public decimal GetTotalPrice()
    {
        return items
            .GroupBy(sku => sku)
            .Join(
                priceData, g => g.Key, p => p.Key,
                (g, p) => GetItemTotal(g.Count(), p.Value))
            .Sum();
    }

    /// <summary>
    /// Gets the total price for a given quantity of items with the given price data.
    /// </summary>
    /// <param name="quantity"></param>
    /// <param name="priceData"></param>
    /// <returns>The total price for the given quantity of items.</returns>
    private decimal GetItemTotal(int quantity, PriceData priceData)
    {
        var specialTotal = Math.Floor((decimal)quantity / priceData.SpecialQuantity) * priceData.UnitPrice;
        var remainingUnitTotal = quantity % priceData.SpecialQuantity * priceData.UnitPrice;
        return specialTotal + remainingUnitTotal;
    }
}
