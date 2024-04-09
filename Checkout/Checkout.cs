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
        throw new NotImplementedException();
    }

    public void Remove(string item)
    {
        throw new NotImplementedException();
    }

    public int GetTotalPrice()
    {
        throw new NotImplementedException();
    }
}
