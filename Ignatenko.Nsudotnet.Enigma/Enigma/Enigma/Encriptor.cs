using System.Security.Cryptography;
using System.IO;
using System;

namespace Ignatenko.Nsudotnet.Enigma
{
    class Encriptor
    {
        private const string KeyFileName = "file.key.txt";

        public static void EncryptFile(string inputFileName, string outputFileName, string algorithmName)
        {
            using (SymmetricAlgorithm algorithm = AlgorithmProvider.GetAlgorithm(algorithmName))
            using (var inputFileStream = new FileStream(inputFileName, FileMode.Open, FileAccess.Read))
            using (var outputFileStream = new FileStream(outputFileName, FileMode.OpenOrCreate, FileAccess.Write))
            using (var cryptoStream = new CryptoStream(outputFileStream, algorithm.CreateEncryptor(), CryptoStreamMode.Write))
            using (var streamWriter = new StreamWriter(KeyFileName))
            {
                inputFileStream.CopyTo(cryptoStream);
                streamWriter.WriteLine(Convert.ToBase64String(algorithm.Key));
                streamWriter.WriteLine(Convert.ToBase64String(algorithm.IV));
            }
        }
    }
}