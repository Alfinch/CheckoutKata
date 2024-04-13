namespace Checkout;

/// <summary>
/// Represents the price data for a product.
/// </summary>
public struct PriceData
{
    public PriceData(decimal unitPrice, decimal specialPrice, int specialQuantity)
    {
        UnitPrice = unitPrice;
        SpecialPrice = specialPrice;
        SpecialQuantity = specialQuantity;
    }

    /// <summary>
    /// The unit price for the product.
    /// </summary>
    public decimal UnitPrice { get; }

    /// <summary>
    /// A price applied per special quantity of items.
    /// </summary>
    public decimal SpecialPrice { get; }

    /// <summary>
    /// The quantity of items which must be purchased for the special price to apply.
    /// </summary>
    public int SpecialQuantity { get; }
}