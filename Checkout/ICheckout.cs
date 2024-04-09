namespace Checkout;

public interface ICheckout
{
    void Scan(string item);

    void Remove(string item);

    int GetTotalPrice();
}