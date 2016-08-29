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
        byte[] inPDF; 
        byte[] outPDF;
        int inPtr = 0;
        int outPtr = 0;

        // properties

        // pdf Info
        public string Version { set; get; }
        public string FileType { set; get; }

        public byte[] serObj = { 0x20, 0x6F, 0x62, 0x6A };
        public byte[] serXref = { 0x78, 0x72, 0x65, 0x66 };
        public byte[] serStreamStart = { 0x73, 0x74, 0x72, 0x65, 0x61, 0x6D };
        public byte[] serStreamEnd = { 0x65, 0x6E, 0x64, 0x73, 0x74, 0x72, 0x65, 0x61, 0x6D };

        //==< Method Library >====================================================================================
        //--------------------------------------------------------------------------------------------------------
        public int LoadPdf(string fileName)
        {
            inPDF = null;
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                inPDF = new byte[fs.Length];
                fs.Read(inPDF, 0, (int)fs.Length);
            }
            outPDF = new byte[inPDF.Length];
            return 0;
        }

        //--------------------------------------------------------------------------------------------------------
        public byte[] ReadPdfNextline()
        {
            MemoryStream mstream = new MemoryStream();
            byte lastbyte = 0x00;
            byte newbyte = 0x00;
            
            while (inPtr <= inPDF.Length)
            {
                lastbyte = newbyte;
                newbyte = inPDF[inPtr];
                mstream.Write(new[] { newbyte }, 0, 1);

                inPtr++;

                if ((newbyte == 0x0a))
                    break;
            }
            return mstream.ToArray();
        }

        //--------------------------------------------------------------------------------------------------------
        public int WritePdfline(byte[] PDFLine)
        {
            for (int i = 0; i <= PDFLine.Length-1; i++, outPtr++ )
                outPDF[outPtr] = PDFLine[i];
            
            return 0;
        }

        //--------------------------------------------------------------------------------------------------------
        public int SaveNewPdf(string fileName)
        {
            inPDF = null;
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                inPDF = new byte[fs.Length];
                fs.Read(inPDF, 0, (int)fs.Length);
            }
            return 0;
        }




        //--------------------------------------------------------------------------------------------------------
        public int IndexOf(byte[] input, byte[] pattern)
        {
            byte firstByte = pattern[0];
            int index = -1;

            if ((index = Array.IndexOf(input, firstByte)) >= 0)
            {
                for (int i = 0; i < pattern.Length; i++)
                {
                    if (index + i >= input.Length ||
                     pattern[i] != input[index + i]) return -1;
                }
            }

            return index;
        }


        public byte[] AppendTwoByteArrays(byte[] arrayA, byte[] arrayB)
        {
            byte[] outputBytes = new byte[arrayA.Length + arrayB.Length];
            Buffer.BlockCopy(arrayA, 0, outputBytes, 0, arrayA.Length);
            Buffer.BlockCopy(arrayB, 0, outputBytes, arrayA.Length, arrayB.Length);
            return outputBytes;
        }



        //--< Unsed Method Library >-------------------------------------------------------------------------------------------
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

        //--------------------------------------------------------------------------------
        public byte[] readbinaryline(BinaryReader br)
        {
            MemoryStream mstream = new MemoryStream();
            byte lastbyte = 0x00;
            byte newbyte = 0x00;

            if (br.BaseStream.Position == br.BaseStream.Length)
                return null;

            while (!(br.BaseStream.Position == br.BaseStream.Length))
            {
                lastbyte = newbyte;
                newbyte = br.ReadByte();
                mstream.Write(new[] { newbyte }, 0, 1);

                if ((newbyte == 0x0a))
                    break;

            }
            return mstream.ToArray();

        }

    }


}


