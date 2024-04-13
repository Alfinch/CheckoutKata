namespace Checkout;

/// <summary>
/// Represents a checkout system for scanning in and calculating the total price of items.
/// </summary>
public interface ICheckout
{
    /// <summary>
    /// Scans in an item with the given SKU.
    /// </summary>
    /// <param name="sku"></param>
    void Scan(string sku);

    /// <summary>
    /// Removes an item with the given SKU from the checkout.
    /// </summary>
    /// <param name="sku"></param>
    /// <returns>True if the item was removed, otherwise false.</returns>
    bool Remove(string sku);

    /// <summary>
    /// Gets the total price of all items scanned in.
    /// </summary>
    /// <returns>The total price of all items scanned in.</returns>
    decimal GetTotalPrice();
}