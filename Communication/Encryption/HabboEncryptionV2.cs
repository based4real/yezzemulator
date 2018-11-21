﻿using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.Utilities;
using Yezz.Communication.Encryption.Keys;
using Yezz.Communication.Encryption.Crypto.RSA;
using Yezz.Communication.Encryption.KeyExchange;

namespace Yezz.Communication.Encryption
{
    public static class HabboEncryptionV2
    {
        private static RSAKey Rsa;
        private static DiffieHellman DiffieHellman;

        public static void Initialize(RSAKeys keys)
        {
            Rsa = RSAKey.ParsePrivateKey(keys.N, keys.E, keys.D);
            DiffieHellman = new DiffieHellman();
        }

        private static string GetRsaStringEncrypted(string message)
        {
            try
            {
                byte[] m = Encoding.Default.GetBytes(message);
                byte[] c = Rsa.Sign(m);

                return Converter.BytesToHexString(c);
            }
            catch
            {
                return "0";
            }
        }

        public static string GetRsaDiffieHellmanPrimeKey()
        {
            string key = DiffieHellman.Prime.ToString(10);
            return GetRsaStringEncrypted(key);
        }

        public static string GetRsaDiffieHellmanGeneratorKey()
        {
            string key = DiffieHellman.Generator.ToString(10);
            return GetRsaStringEncrypted(key);
        }

        public static string GetRsaDiffieHellmanPublicKey()
        {
            string key = DiffieHellman.PublicKey.ToString(10);
            return GetRsaStringEncrypted(key);
        }

        public static BigInteger CalculateDiffieHellmanSharedKey(string publicKey)
        {
            try
            {
                byte[] cbytes = Converter.HexStringToBytes(publicKey);
                byte[] publicKeyBytes = Rsa.Verify(cbytes);
                string publicKeyString = Encoding.Default.GetString(publicKeyBytes);
                return DiffieHellman.CalculateSharedKey(new BigInteger(publicKeyString, 10));
            }
            catch
            {
                return 0;
            }
        }
    }
}
