namespace Checkout;

public class Checkout : ICheckout
{
    private readonly IPriceData priceData;
    private readonly IList<string> items = new List<string>();

    public Checkout(IPriceData priceData)
    {
        this.priceData = priceData ?? throw new ArgumentNullException(nameof(priceData));
    }

    public void Scan(string sku)
    {
        if (sku == null)
        {
            throw new ArgumentNullException(nameof(sku));
        }

        if (!priceData.UnitPrices.ContainsKey(sku))
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
        var items = this.items.ToList();
        var total = 0m;

        // Apply special prices.
        foreach (var specialPrice in priceData.SpecialPrices)
        {
            // Count the number of times this combination appears in the items list.
            var occurrences = specialPrice.Combination
                .GroupBy(sku => sku)
                .Select(group => {

                    // Count the number of times this SKU appears individually in the items list.
                    var individualOccurrences = items.Count(sku => sku == group.Key);
                    
                    // Divide this by the quantity in the special price combination
                    // to figure out the number of times this part of the combination appears.
                    return (int)Math.Floor((double)individualOccurrences / group.Count());
                })
                .Min();

            // Remove the items from the list.
            foreach (var sku in specialPrice.Combination)
            {
                for (var i = 0; i < occurrences; i++)
                {
                    items.Remove(sku);
                }
            }

            // Apply the special price.
            total += occurrences * specialPrice.Price;
        }

        // Apply unit prices.
        foreach (var item in items)
        {
            total += priceData.UnitPrices[item];
        }

        return total;
    }
}
