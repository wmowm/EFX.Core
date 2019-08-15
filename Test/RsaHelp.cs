using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;




namespace Test
{
    public class RsaHelp
    {

     
        public void GenRSAKeyPair(string name)
        {
            var generator = new RsaKeyPairGenerator();
            var seed = Encoding.UTF8.GetBytes(name);
            var secureRandom = new SecureRandom();
            secureRandom.SetSeed(seed);
            generator.Init(new KeyGenerationParameters(secureRandom, 2048));
            var pair = generator.GenerateKeyPair();


            var twPrivate = new StringWriter();
            PemWriter pwPrivate = new PemWriter(twPrivate);
            pwPrivate.WriteObject(pair.Private);
            pwPrivate.Writer.Flush();
            var privateKey = twPrivate.ToString().Replace("-----BEGIN RSA PRIVATE KEY-----","").Replace("-----END RSA PRIVATE KEY-----","").Trim();
            Console.WriteLine(privateKey);

            var twPublic = new StringWriter();
            PemWriter pwPublic = new PemWriter(twPublic);
            pwPublic.WriteObject(pair.Public);
            pwPublic.Writer.Flush();
            var publicKey = twPublic.ToString().Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "").Trim();
            Console.WriteLine(publicKey);
        }
    }
}
