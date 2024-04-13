namespace CheckoutTests;

using Checkout;

[TestFixture]
public class CheckoutTests
{
    private Checkout checkout;

    [SetUp]
    public void Setup()
    {
        var priceData = new PriceData(
            new Dictionary<string, decimal>
            {
                { "A", 50 },
                { "B", 30 },
                { "C", 20 },
                { "D", 15 }
            },
            new Dictionary<string, decimal>
            {
                { "A,A,A", 130 },
                { "B,B", 45 }
            }
        );

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
    public void Scan_WillNotThrowIfSkuIsInPriceData()
    {
        Assert.That(() => checkout.Scan("A"), Throws.Nothing);
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
    public void Remove_WillReturnTrueIfSkuIsInItems()
    {
        checkout.Scan("A");
        Assert.That(checkout.Remove("A"), Is.True);
    }

    [Test]
    public void GetTotalPrice_WillReturnZeroIfNoItemsScanned()
    {
        Assert.That(checkout.GetTotalPrice(), Is.EqualTo(0));
    }
    
    [TestCase("A", ExpectedResult = 50)]
    [TestCase("A,A", ExpectedResult = 100)]
    [TestCase("A,A,A", ExpectedResult = 130)]
    [TestCase("A,A,A,A", ExpectedResult = 180)]
    [TestCase("A,A,A,A,A", ExpectedResult = 230)]
    [TestCase("A,A,A,A,A,A", ExpectedResult = 260)]
    [TestCase("B", ExpectedResult = 30)]
    [TestCase("B,B", ExpectedResult = 45)]
    [TestCase("B,B,B", ExpectedResult = 75)]
    [TestCase("B,B,B,B", ExpectedResult = 90)]
    [TestCase("C", ExpectedResult = 20)]
    [TestCase("C,C", ExpectedResult = 40)]
    [TestCase("C,C,C", ExpectedResult = 60)]
    [TestCase("D", ExpectedResult = 15)]
    [TestCase("D,D", ExpectedResult = 30)]
    [TestCase("D,D,D", ExpectedResult = 45)]
    [TestCase("A,B", ExpectedResult = 80)]
    [TestCase("A,A,A,B,B", ExpectedResult = 175)]
    [TestCase("A,B,A,B,A", ExpectedResult = 175)]
    [TestCase("B,B,A,A,A", ExpectedResult = 175)]
    [TestCase("A,B,C,D", ExpectedResult = 115)]
    [TestCase("A,A,A,A,B,B,B,C,D", ExpectedResult = 290)]
    public decimal GetTotalPrice_WillReturnCorrectPriceForScannedItems(string skus)
    {
        foreach (var sku in skus.Split(','))
        {
            checkout.Scan(sku);
        }

        return checkout.GetTotalPrice();
    }
}