namespace Checkout;

public abstract class CheckoutBase : ICheckout
{
    protected readonly IPriceData priceData;
    protected readonly IList<string> items = new List<string>();

    public CheckoutBase(IPriceData priceData)
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

    public abstract decimal GetTotalPrice();
}
