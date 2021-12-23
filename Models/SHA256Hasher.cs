using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using NewApp.Models;

namespace NewApp.Models
{
    public class SHA256Hasher
    {
        public static string Hash(string PlainText)
        {

            //ASSUMPTION password will be in english only (i.e ascii)
            //if password can be multilingual then use Encoding.UTF8.GetBytes() instead.

            //obtain byte array of plain text
            byte[] plainTextBytes = Encoding.ASCII.GetBytes(PlainText);

            //create managed instance of Sha256 algo
            SHA256Managed sha = new SHA256Managed();

            //obtain hash in byte array
            byte[] hashBytes = sha.ComputeHash(plainTextBytes);

            //obtain hexadecimal string of the hash.
            StringBuilder sb = new StringBuilder();

            foreach (byte hashByte in hashBytes)
            {
                sb.Append(hashByte.ToString("x2"));
            }

            //return the hash string now
            return sb.ToString();
        }
    }
}