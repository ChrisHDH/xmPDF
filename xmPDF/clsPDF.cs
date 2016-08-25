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
        string[] sObjects = new string[10000];

        // pdf properties
        public string path { set; get; }
        public string PdfVersion { set; get; }
        
        // methods --------------------------------------------------------------------------------
        public string merge()
        {
            string sRtn = "Valid";

            // Open the stream and read it back. 
            using (StreamReader sr = File.OpenText(path))
            {
                //pdfVersion - first line 
                PdfVersion = sr.ReadLine();
                PdfVersion = PdfVersion + " --- " + sr.ReadLine();
                PdfVersion = PdfVersion.Replace("\r\n", "");
                PdfVersion = PdfVersion.Replace("%", "");
                PdfVersion = PdfVersion.Trim();

                Console.WriteLine(PdfVersion);

                // read objects
                string s = "";
                int iCtn = 0;

                bool sInObjFlag = false;
                
                while ((s = sr.ReadLine()) != null) 
                {
                    // Get Object ID ==============================================================================================================
                    if (s.Contains(" obj") && sInObjFlag == false) sInObjFlag = true;

                    if (sInObjFlag == false) Console.WriteLine("-------------------------------------------------------------------------------------");
                
                    if (sInObjFlag == true)
                    {
                        Console.WriteLine(s);
                        if (s == "<<")
                        {




                            if (s == "Stream")
                        {
                            // Parse and merge








                            iCtn++;
                        }



                    }

                    if (s == "endobj" && sInObjFlag == true) sInObjFlag = false;
                    // write to new file
                }


                // Write out to new file ================================================================

                Console.ReadKey();
                //string s = "";
                //while ((s = sr.ReadLine()) != null)
                //{
                //Console.WriteLine(s);
                //}

            }

            return sRtn;
        }


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


        // Read line binary ---------------------------------------
        public class LineReader : IDisposable
        {
            private Stream stream;
            private BinaryReader reader;

            public LineReader(Stream stream) { reader = new BinaryReader(stream); }

            public string ReadLine()
            {
                StringBuilder result = new StringBuilder();
                char lastChar = reader.ReadChar();
                // an EndOfStreamException here would propogate to the caller

                try
                {
                    char newChar = reader.ReadChar();
                    if (lastChar == '\r' && newChar == '\n')
                        return result.ToString();

                    result.Append(lastChar);
                    lastChar = newChar;
                }
                catch (EndOfStreamException)
                {
                    result.Append(lastChar);
                    return result.ToString();
                }
            }

            public void Dispose()
            {
                reader.Close();
            }
        }
        //-----------------------------------------------------------


    }
}
