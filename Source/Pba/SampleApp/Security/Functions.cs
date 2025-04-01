//Copyright (C) 2006, 2007 Alexander Loesel. All rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using SIT.Components.Data;
using System.Data;
using System.Security.Cryptography;

namespace SIT.Components.Security {
    internal static class Functions {


        public static string CreateHash( string value ) {
            SHA384 sha;
            byte[] bytes;
            byte[] hash;
            try {

                sha = SHA384.Create();
                bytes = Convert.FromBase64String( value );
                hash = sha.ComputeHash( bytes );
                sha = null;
                return SIT.Components.Tools.Convert.ByteToHex( hash );
            } finally {
                sha = null;
                bytes = null;
                hash = null;
            }
        }

    }
}


namespace SIT.Components.Tools {
    public class Convert {


        public static byte[] HexToByte(string hex) {

            byte[] ret;

            ret = new byte[hex.Length / 2];

            for (int i = 0; i < hex.Length / 2; i++)
                ret[i] = byte.Parse(hex.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);

            return ret;

        }

        public static string ByteToHex(byte[] b) {

            string ret = "";

            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("X2");

            return ret;

        }

    }
}