using HealthBar;
using NUnit.Framework;

public class HeartTest
{
    /*[TestCase(100, 6)]
    // Less than 100% should give between 1 and 5 index
    [TestCase(99, 5)]
    [TestCase(81, 5)]
    [TestCase(80, 4)]
    [TestCase(61, 4)]
    [TestCase(60, 3)]
    [TestCase(41, 3)]
    [TestCase(40, 2)]
    [TestCase(21, 2)]
    [TestCase(20, 1)]
    [TestCase(1, 1)]
    // 0% should give 0 index
    [TestCase(0, 0)]
    public void CalculateIndexFromPercentageWith7States(int percentage, int expectedIndex)
    {
        // Use the Assert class to test conditions
        var index = Heart.CalculateIndexFromPercentage(percentage, 7);
        Assert.AreEqual(expectedIndex, index);
    }*/
    
    // A Test behaves as an ordinary method
    [TestCase(100, 2)]
    // Less than 100% should 1
    [TestCase(99, 1)]
    [TestCase(1, 1)]
    // 0% should give 0 hearts
    [TestCase(0, 0)]
    public void CalculateIndexFromPercentageWith3States(int percentage, int expectedIndex)
    {
        // Use the Assert class to test conditions
        var index = Heart.CalculateIndexFromPercentage(percentage, 3);
        Assert.AreEqual(expectedIndex, index);
    }
}