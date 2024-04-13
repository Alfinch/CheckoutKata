namespace Checkout;

public class SpecialPrice : ISpecialPrice
{
    public IReadOnlyCollection<string> Combination { get; }
    public decimal Price { get; }

    public SpecialPrice(IReadOnlyCollection<string> combination, decimal price)
    {
        Combination = combination ?? throw new ArgumentNullException(nameof(combination));
        Price = price;
    }
}