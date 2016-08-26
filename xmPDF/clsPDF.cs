using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reflection;
using Ionic.Zlib;


namespace xmPDF
{
    public class PDF
    {

        // local variables
        private FileStream streamIn;
        BinaryReader br;
        
        // properties
        public string path { set; get; }
        
        // pdf Info
        public string Version { set; get; }
        public string FileType { set; get; }



        // constructor
        public PDF(string filepath)
        {
            path = filepath;

            // Open the stream to read
            streamIn = new FileStream(path, FileMode.Open);
            br = new BinaryReader(streamIn);

             // read 
            Version = readbinaryline(br).ToString();


        }

        
       

        //--------------------------------------------------------------------------------
        public byte[] readbinaryline(BinaryReader br)
        {
            MemoryStream mstream = new MemoryStream();
            byte lastbyte = br.ReadByte();
            try
            {
                byte newbyte = br.ReadByte();
                if (lastbyte == '\r' && newbyte == '\n')
                {
                    mstream.Write(new [] { lastbyte }, 0, 1);
                    return mstream.ToArray();
                }
                  
                mstream.Append(lastbyte);
                lastbyte = newbyte;
            }
            catch (EndOfStreamException)
            {
                mstream.Append(lastbyte);
            }
            return mstream.ToArray();
        }

        






        //--< Method Library >-------------------------------------------------------------------------------------------
        // Converts a string to a MemoryStream.
        static System.IO.MemoryStream StringToMemoryStream(string s)
        {
            byte[] a = System.Text.Encoding.ASCII.GetBytes(s);
            return new System.IO.MemoryStream(a);
        }

        // Converts a MemoryStream to a string. Makes some assumptions about the content of the stream.
        static String MemoryStreamToString(System.IO.MemoryStream ms)
        {
            byte[] ByteArray = ms.ToArray();
            return System.Text.Encoding.ASCII.GetString(ByteArray);
        }

        // Copy stream
        static void CopyStream(System.IO.Stream src, System.IO.Stream dest)
        {
            byte[] buffer = new byte[1024];
            int len;
            while ((len = src.Read(buffer, 0, buffer.Length)) > 0)
                dest.Write(buffer, 0, len);
            dest.Flush();
        }

        
    }

    public static class MemoryStreamExtensions
    {
        public static void Append(this MemoryStream stream, byte value)
        {
            stream.Append(new[] { value });
        }

        public static void Append(this MemoryStream stream, byte[] values)
        {
            stream.Write(values, 0, values.Length);
        }
    }



}


