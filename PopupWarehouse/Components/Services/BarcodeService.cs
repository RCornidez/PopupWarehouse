using System;
using System.Diagnostics;
using System.IO;

namespace Services
{
    public class BarcodeGenerator
    {
        public static void GenerateBarcode(string barcodeType, string data, string filename, float moduleWidth, float moduleHeight)
        {
            // Example usage
            //BarcodeGenerator.GenerateBarcode("code128", "123456789012", "my_barcode", 0.2f, 15.0f);

            // Path to the executable
            string executablePath = @"C:\Users\rodri\Documents\code\C#\PopupWarehouse_\PopupWarehouse\Components\Executables\BarcodeGenerator\GenerateBarcode.exe";

            // Constructing the arguments string
            string args = $"{barcodeType} {data} {filename} {moduleWidth} {moduleHeight}";

            ProcessStartInfo startInfo = new ProcessStartInfo(executablePath, args)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.WriteLine(result);
                }
            }
        }
    }
}