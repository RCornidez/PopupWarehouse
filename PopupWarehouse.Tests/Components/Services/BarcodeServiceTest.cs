using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using Services;

[TestFixture]
public class BarcodeServiceTest
{
    private readonly string outputDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "barcode", "temp");

    [SetUp]
    public void SetUp()
    {
        // Ensure the output directory exists and is empty before each test
        if (Directory.Exists(outputDir))
        {
            Directory.Delete(outputDir, true);
        }
        Directory.CreateDirectory(outputDir);
    }

    [Test]
    public void BarcodeService_CreateBarcodeSVG()
    {
        // Arrange
        string barcodeType = "code128";
        string data = "123456789012";
        string filename = "test_barcode";
        float moduleWidth = 0.3f;
        float moduleHeight = 5.0f;

        // Act
        BarcodeGenerator.GenerateBarcode(barcodeType, data, filename, moduleWidth, moduleHeight);

        // Assert
        string expectedFilePath = Path.Combine(outputDir, filename + ".svg");
        Assert.That(File.Exists(expectedFilePath), Is.True);
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up the output directory after each test
        if (Directory.Exists(outputDir))
        {
            Directory.Delete(outputDir, true);
        }
    }

}
