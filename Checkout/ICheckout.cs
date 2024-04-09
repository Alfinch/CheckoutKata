namespace Checkout;

public interface ICheckout
{
    void Scan(string item);

    bool Remove(string item);

    decimal GetTotalPrice();
}