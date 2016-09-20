using System;
using System.IO;

namespace OsuAPI.Common
{

    public class WritableByteFile : IDisposable
    {

        private BinaryWriter Writer;

        public WritableByteFile(string filepath)
        {
            // Open the file stream and binary reader.
            using (System.IO.File.Create(filepath));
            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Write);
            Writer = new BinaryWriter(fs);
        }

        public WritableByteFile(byte[] file)
        {
            // Open the memory stream and binary reader.
            MemoryStream ms = new MemoryStream(file);
            Writer = new BinaryWriter(ms);
        }

        public void Dispose()
        {
            Writer.Dispose();
        }

        public void WriteByte(Byte b)
        {
            Writer.Write(b);
        }

        public void WriteBytes(Byte[] b)
        {
            Writer.Write(b);
        }

        public void WriteBool(bool b)
        {
            Writer.Write(b);
        }

        public void WriteShort(short s)
        {
            Writer.Write(s);
        }

        public void WriteInt(int i)
        {
            Writer.Write(i);
        }

        public void WriteULEB128(int i)
        {
            int padding = 0;

            do
            {
                Byte b = (byte)(i & 0x7F);
                i >>= 7;

                if (i != 0 || padding != 0)
                {
                    b |= 0x80;
                }

                Writer.Write(b);
            }
            while (i != 0);

            if (padding != 0)
            {
                for (; padding != 1; --padding)
                {
                    Writer.Write(0x80);
                }

                Writer.Write(0x00);
            }
        }

        public void WriteLong(long l)
        {
            Writer.Write(l);
        }

        private void WriteStringBytes(Byte[] bytes)
        {
            int length = bytes.Length;

            if (length == 0)
            {
                WriteByte(0x00);
            }
            else
            {
                WriteByte(0x0B);
                WriteULEB128(length);
                WriteBytes(bytes);
            }
        }

        public void WriteString(string s)
        {
            Byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(s);

            WriteStringBytes(byteArray);
        }

        public void WriteASCIIString(string s)
        {
            Byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(s);

            WriteStringBytes(byteArray);
        }

    }

}
