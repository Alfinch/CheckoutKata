namespace Checkout;

public class Checkout : ICheckout
{
    private readonly IDictionary<string, PriceData> priceData;
    private readonly IList<string> items = new List<string>();

    public Checkout(IDictionary<string, PriceData> priceData)
    {
        this.priceData = priceData ?? throw new ArgumentNullException(nameof(priceData));
    }

    public void Scan(string item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        if (!priceData.ContainsKey(item))
        {
            throw new ArgumentException($"No price data for product SKU {item}", nameof(item));
        }

        items.Add(item);
    }

    public bool Remove(string item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        return items.Remove(item);
    }

    public decimal GetTotalPrice()
    {
        return items
            .GroupBy(i => i)
            .Join(
                priceData, g => g.Key, p => p.Key,
                (g, p) => GetItemTotal(g.Count(), p.Value))
            .Sum();
    }

    private decimal GetItemTotal(int quantity, PriceData priceData)
    {
        var specialTotal = Math.Floor((decimal)quantity / priceData.SpecialQuantity) * priceData.UnitPrice;
        var remainingUnitTotal = quantity % priceData.SpecialQuantity * priceData.UnitPrice;
        return specialTotal + remainingUnitTotal;
    }
}
