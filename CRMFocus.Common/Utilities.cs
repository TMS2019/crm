using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace CRMFocus.Common
{
    public class Utilities
    {
        // User Role
        public const string AdminDealer = "Admin - Dealer";
        public const string Dealer = "Dealer";
        public const string DealePICDealer = "Dealer - PIC Dealer";
        public const string HO = "HO";
        public const string MainDealer = "Main Dealer";
        public const string System = "System";

        // Messages
        public const string SaveComplete = "Your data has been saved successfully.";
        public const string SaveInComplete = "Your data cannot be saved.";
        public const string DocumentIsNotValid = "Document is not valid!";

        // Status
        public const string ErrorStatus = "Error";
        public const string SucceedStatus = "Succeed";
    }

    public static class StaticType
    {
        public static Dictionary<byte, string> GetScenarioType()
        {
            var dict = new Dictionary<byte, string>();
            dict.Add(1, "H1");
            dict.Add(2, "H2");
            dict.Add(3, "H3");

            return dict;
        }

        public static Dictionary<byte, string> GetScenarioResourceTypes()
        {
            var dict = new Dictionary<byte, string>();
            dict.Add(1, "H1");
            dict.Add(2, "H2");
            dict.Add(3, "H3");
            dict.Add(0, "None");

            return dict;
        }

        public static Dictionary<byte, string> GetScenarioDestinationTypes()
        {
            var dict = new Dictionary<byte, string>();
            dict.Add(1, "H1");
            dict.Add(2, "H2");
            dict.Add(3, "H3");

            return dict;
        }

        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
