using System.Security.Cryptography;
using System;

namespace Ignatenko.Nsudotnet.Enigma
{
    class AlgorithmProvider
    {
        public static SymmetricAlgorithm GetAlgorithm(string algorithmName)
        {
            switch (algorithmName)
            {
                case "aes":
                    return new AesCryptoServiceProvider();

                case "des":
                    return new DESCryptoServiceProvider();

                case "rc2":
                    return new RC2CryptoServiceProvider();

                case "rijndael":
                    return new RijndaelManaged();

                default:
                    throw new Exception("Invalid name of algorithm.");
            }
        }
    }
}