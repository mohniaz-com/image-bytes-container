using System;

namespace ImageBytesContainer.CLI;

class Program
{
    private static void Encode(string text)
    {
        IBCEncoder.EncodeToImage(text, "output.png");
        Console.WriteLine("Encoding complete.");
    }

    private static void Decode(string filePath)
    {
        var decodedString = IBCEncoder.DecodeFromImage(filePath);
        Console.WriteLine();
        Console.WriteLine("Decoded text:");
        Console.Write(decodedString);
        Console.WriteLine();
        Console.WriteLine("Decoding complete.");
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("ImageBytesContainer by Sameer Niaz");

        if (args.Length < 1)
        {
            Console.WriteLine("Running in prompt mode.");
            Console.WriteLine("Press E to encode. Press D to decode. Press any other key to exit.");
            var inputKey = Console.ReadKey();
            Console.WriteLine();
            
            if (inputKey.Key == ConsoleKey.E)
            {
                Console.WriteLine("The encoded image will be outputted to a file named 'output.png' next to the executable.");
                Console.WriteLine("Enter text to be encoded:");
                var inputString = Console.ReadLine();
                Encode(inputString);
            }
            else if (inputKey.Key == ConsoleKey.D)
            {
                Console.WriteLine("The decoded text will be outputted to the console.");
                Console.WriteLine("Enter file path of the image to be decoded:");
                var filePath = Console.ReadLine();
                Decode(filePath);
            }
            else
            {
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Program execution complete. Press any key to exit.");
            Console.ReadKey();

            return;
        }
        
        Console.WriteLine("Support for program arguments has not been implemented. Press any key to exit.");
        Console.ReadKey();
    }
}
