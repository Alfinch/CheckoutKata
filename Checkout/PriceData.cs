namespace Checkout;

public struct PriceData
{
    public PriceData(decimal unitPrice, decimal specialPrice, int specialQuantity)
    {
        UnitPrice = unitPrice;
        SpecialPrice = specialPrice;
        SpecialQuantity = specialQuantity;
    }

    public decimal UnitPrice { get; }

    public decimal SpecialPrice { get; }

    public int SpecialQuantity { get; }
}