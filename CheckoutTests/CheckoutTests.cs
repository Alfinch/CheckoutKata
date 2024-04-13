namespace CheckoutTests;

using Checkout;

[TestFixture]
public class CheckoutTests
{
    private Checkout checkout;

    [SetUp]
    public void Setup()
    {
        // Todo - what if we have no special price? Specifying a special quantity of 1 is clunky.
        var priceData = new Dictionary<string, PriceData>
        {
            { "A", new PriceData(50, 130, 3) },
            { "B", new PriceData(30, 45, 2) },
            { "C", new PriceData(20, 20, 1) },
            { "D", new PriceData(15, 15, 1) }
        };

        checkout = new Checkout(priceData);
    }

    [Test]
    public void Scan_WillThrowAgumentNullExceptionIfSkuIsNull()
    {
        Assert.That(() => checkout.Scan(null), Throws.ArgumentNullException);
    }

    [Test]
    public void Scan_WillThrowArgumentExceptionIfSkuIsNotInPriceData()
    {
        Assert.That(() => checkout.Scan("E"), Throws.ArgumentException);
    }

    [Test]
    public void Remove_WillThrowAgumentNullExceptionIfSkuIsNull()
    {
        Assert.That(() => checkout.Remove(null), Throws.ArgumentNullException);
    }

    [Test]
    public void Remove_WillReturnFalseIfSkuIsNotInItems()
    {
        Assert.That(checkout.Remove("A"), Is.False);
    }

    [Test]
    public void GetTotalPrice_WillReturnZeroIfNoItemsScanned()
    {
        Assert.That(checkout.GetTotalPrice(), Is.EqualTo(0));
    }
}