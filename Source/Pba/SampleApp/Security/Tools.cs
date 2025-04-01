//Copyright (C) 2006, 2007 Alexander Loesel. All rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Data;
using System.Collections.ObjectModel;
using System.Collections;
using System.Diagnostics;

namespace SIT.Components.Security {
    public static class Tools {

        private static byte[] KIV = new byte[] { 120, 34, 76, 64, 37, 51, 89, 18 };
        private static byte[] Key = new byte[] { 214, 210, 170, 249, 132, 220, 55, 156, 227, 231, 192, 178, 237, 108, 254, 196, 247, 40, 235, 145, 68, 19, 9, 83 };



        public static string GetHash( string text ) {

            MD5 hashalg;
            byte[] hashvalue;
            
            hashalg = MD5.Create();
            hashvalue = hashalg.ComputeHash( Encoding.Default.GetBytes( text ) );

            return SIT.Components.Tools.Convert.ByteToHex( hashvalue );

        }

        public static bool VerifyHash( string text, string hashvalue ) {

            string newhash = GetHash( text );
            return newhash.CompareTo( hashvalue ) == 0;

        }


        public static string Encrypt( string plaintext ) {

            return EncryptData( EncryptData( plaintext ) );

        }

        private static byte[] AssemblyInfo(int len) {
            string s = System.Reflection.Assembly.GetExecutingAssembly().FullName;
            s = s.Substring( 0, len );
            byte[] ai = Encoding.Default.GetBytes( s );
            return ai;
        }

        private static string EncryptData( string plaintext ) {

            byte[] plaintextbytes, kIV, key;
            TripleDES TDES;

            plaintextbytes = Encoding.Default.GetBytes( plaintext );
            TDES = TripleDES.Create();

            //Create the file streams to handle the input and output files.
            MemoryStream min = new MemoryStream( plaintextbytes );
            MemoryStream mout = new MemoryStream();
            mout.SetLength( 0 );

            //Create variables to help with read and write.
            byte[] bin = new byte[ 100 ]; //This is intermediate storage for the encryption.
            long rdlen = 0;              //This is the total number of bytes written.
            long totlen = min.Length;    //This is the total length of the input file.
            int len;                     //This is the number of bytes to be written at a time.

            kIV = AssemblyInfo( 8 );
            key = AssemblyInfo(24);
            CryptoStream encStream = new CryptoStream( mout, TDES.CreateEncryptor( key, kIV ), CryptoStreamMode.Write );
            clearBytes( kIV );
            clearBytes( key );

            //Read from the input file, then encrypt and write to the output file.
            while( rdlen < totlen ) {
                len = min.Read( bin, 0, 100 );
                encStream.Write( bin, 0, len );
                rdlen = rdlen + len;
            }

            encStream.Close();

            return SIT.Components.Tools.Convert.ByteToHex( mout.ToArray() );
        }

        public static string Decrypt( string encryptedtext ) {
            return DecryptData( DecryptData( encryptedtext ) );
        }

        private static string DecryptData( string encryptedtext ) {

            byte[] encryptedtextbytes, kIV, key;
            TripleDES TDES;


            encryptedtextbytes = SIT.Components.Tools.Convert.HexToByte( encryptedtext );
            TDES = TripleDES.Create();

            //Create the file streams to handle the input and output files.
            MemoryStream min = new MemoryStream( encryptedtextbytes );
            MemoryStream mout = new MemoryStream();
            mout.SetLength( 0 );

            //Create variables to help with read and write.
            byte[] bin = new byte[ 100 ]; //This is intermediate storage for the encryption.
            long rdlen = 0;              //This is the total number of bytes written.
            long totlen = min.Length;    //This is the total length of the input file.
            int len;                     //This is the number of bytes to be written at a time.

            kIV = AssemblyInfo( 8 );
            key = AssemblyInfo( 24 );
            CryptoStream decStream = new CryptoStream( mout, TDES.CreateDecryptor( AssemblyInfo( 24 ), AssemblyInfo( 8 ) ), CryptoStreamMode.Write );
            clearBytes( kIV );
            clearBytes( key );


            //Read from the input file, then encrypt and write to the output file.
            while( rdlen < totlen ) {
                len = min.Read( bin, 0, 100 );
                decStream.Write( bin, 0, len );
                rdlen = rdlen + len;
            }

            decStream.Close();

            return Encoding.Default.GetString( mout.ToArray() );
        }




        public static TripleDESCryptoServiceProvider test( string password ) {

            // Get a password from the user.
            Console.WriteLine( "Enter a password to produce a key:" );

            //********************************************************
            //* Security Note: Never hard-code a password within your
            //* source code.  Hard-coded passwords can be retrieved 
            //* from a compiled assembly.
            //********************************************************

            byte[] pwd = Encoding.Unicode.GetBytes( password );

            byte[] salt = createRandomSalt( 7 );

            // Create a TripleDESCryptoServiceProvider object.
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.IV = new byte[] { 120, 34, 76, 64, 37, 51, 89, 18 };
            try {
                Console.WriteLine( "Creating a key with PasswordDeriveBytes..." );

                // Create a PasswordDeriveBytes object and then create 
                // a TripleDES key from the password and salt.
                PasswordDeriveBytes pdb = new PasswordDeriveBytes( pwd, salt );

                // Create the key and add it to the Key property.
                tdes.Key = pdb.CryptDeriveKey( "TripleDES", "SHA1", 192, tdes.IV );
                tdes.IV = new byte[] { 120, 34, 76, 64, 37, 51, 89, 18 };
                Console.WriteLine( "Operation complete." );
                return tdes;
            } catch( Exception e ) {
                Console.WriteLine( e.Message );
                return null;
            } finally {
                // Clear the buffers
                clearBytes( pwd );
                clearBytes( salt );

                // Clear the key.
                tdes.Clear();
            }

        }

        //********************************************************
        //* Helper methods:
        //* createRandomSalt: Generates a random salt value of the 
        //*                   specified length.  
        //*
        //* clearBytes: Clear the bytes in a buffer so they can't 
        //*             later be read from memory.
        //********************************************************

        public static byte[] createRandomSalt( int Length ) {
            // Create a buffer
            byte[] randBytes;

            if( Length >= 1 ) {
                randBytes = new byte[ Length ];
            } else {
                randBytes = new byte[ 1 ];
            }

            // Create a new RNGCryptoServiceProvider.
            RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();

            // Fill the buffer with random bytes.
            rand.GetBytes( randBytes );

            // return the bytes.
            return randBytes;
        }

        public static void clearBytes( byte[] Buffer ) {
            // Check arguments.
            if( Buffer == null ) {
                throw new ArgumentException( "Buffer" );
            }

            // Set each byte in the buffer to 0.
            for( int x = 0; x <= Buffer.Length - 1; x++ ) {
                Buffer[ x ] = 0;
            }
        }
    }




}
