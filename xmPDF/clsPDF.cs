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


