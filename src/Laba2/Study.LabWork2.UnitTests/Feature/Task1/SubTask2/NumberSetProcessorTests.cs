using Study.LabWork2.Feature.Task1.SubTask2;

namespace Study.LabWork2.UnitTests.Feature.Task1.SubTask2;

[TestFixture]
public sealed class NumberSetProcessorTests
{
    [Test]
    public void Process_ShouldReturn15Results()
    {
        var processor = new NumberSetProcessor(maxThreads: 3);
        processor.Process();
        var result = processor.GetResult();

        Assert.That(result.ProcessedSetsCount, Is.EqualTo(15));
        Assert.That(result.Results, Has.Count.EqualTo(15));
    }

    [Test]
    public void Process_ShouldHaveCorrectTotalSum()
    {
        var processor = new NumberSetProcessor(maxThreads: 4);
        processor.Process();
        var result = processor.GetResult();

        int manualSum = result.Results.Sum(r => r.Sum);

        Assert.That(result.TotalSum, Is.EqualTo(manualSum));
    }

    [Test]
    public void Process_ShouldHaveExecutionTime()
    {
        var processor = new NumberSetProcessor(maxThreads: 2);
        processor.Process();
        var result = processor.GetResult();

        Assert.That(result.ExecutionTime.TotalMilliseconds, Is.GreaterThan(0));
    }

    [Test]
    public void Process_EachResultShouldHaveValidData()
    {
        var processor = new NumberSetProcessor(maxThreads: 3);
        processor.Process();
        var result = processor.GetResult();

        foreach (var entry in result.Results)
        {
            Assert.That(entry.SetNumber, Is.InRange(1, 15));
            Assert.That(entry.Sum, Is.GreaterThan(0));
            Assert.That(entry.ThreadId, Is.GreaterThan(0));
        }
    }

    [Test]
    public void GetResult_BeforeProcess_ShouldThrow()
    {
        var processor = new NumberSetProcessor(maxThreads: 3);

        Assert.Throws<InvalidOperationException>(() => processor.GetResult());
    }
}
