using System;

namespace Ignatenko.Nsudotnet.Enigma
{
    class Program
    {
        static void ShowInputFormat()
        {
            Console.WriteLine("Invalid input.");
            Console.WriteLine("Usage:\nEnigma.exe encrypt <file_name> <algorithm_name> <output_file_name.bin>\n" +
                              "Enigma.exe decrypt <file_name.bin> <algoritm_name> <file'_name.key.txt> <file_name>");
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            if (args.Length < 5)
            {
                ShowInputFormat();
                return;
            }
            if (args[1] == "encrypt")
            {
                if (args.Length != 5)
                {
                    ShowInputFormat();
                    return;
                }
                Encriptor.EncryptFile(args[2], args[4], args[3]);
            }
            else if (args[1] == "decrypt")
            {
                if (args.Length != 6)
                {
                    ShowInputFormat();
                    return;
                }
                Decryptor.DecryptFile(args[2], args[5], args[4], args[3]);
            }
            else
            {
                ShowInputFormat();
            }
        }
    }
}