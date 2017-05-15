using System.Security.Cryptography;
using System.IO;
using System;

namespace Ignatenko.Nsudotnet.Enigma
{
    class Decryptor
    {
        public static void DecryptFile(string inputFileName, string outputFileName, string keyFileName, string algorithmName)
        {
            using (SymmetricAlgorithm algorithm = AlgorithmProvider.GetAlgorithm(algorithmName))
            {
                using (var streamReader = new StreamReader(keyFileName))
                {
                    algorithm.Key = Convert.FromBase64String(streamReader.ReadLine());
                    algorithm.IV = Convert.FromBase64String(streamReader.ReadLine());
                }
                using (var inputStreamFile = new FileStream(inputFileName, FileMode.Open, FileAccess.Read))
                using (var cryptostream = new CryptoStream(inputStreamFile, algorithm.CreateDecryptor(), CryptoStreamMode.Read))
                using (var outputStreamFile = new FileStream(outputFileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    cryptostream.CopyTo(outputStreamFile);
                }
            }
        }
    }
}
