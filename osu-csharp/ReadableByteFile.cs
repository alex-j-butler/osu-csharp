using System;
using System.IO;

namespace OsuAPI.Common
{

    public class ReadableByteFile : IDisposable
    {

        private BinaryReader Reader;

        public ReadableByteFile(string filepath)
        {
            // Open the file stream and binary reader.
            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            Reader = new BinaryReader(fs);
        }

        public ReadableByteFile(byte[] file)
        {
            // Open the memory stream and binary reader.
            MemoryStream ms = new MemoryStream(file);
            Reader = new BinaryReader(ms);
        }

        public void Dispose()
        {
            Reader.Dispose();
        }

        public Byte ReadByte()
        {
            return Reader.ReadByte();
        }

        public Byte[] ReadBytes(int count)
        {
            return Reader.ReadBytes(count);
        }

        public short ReadShort()
        {
            return Reader.ReadInt16();
        }

        public int ReadInt()
        {
            return Reader.ReadInt32();
        }

        public int ReadULEB128()
        {
            int result = 0;
            int shift = 0;

            while (true)
            {
                byte b = Reader.ReadByte();
                result = result | ((b & Convert.ToInt32("01111111", 2)) << shift);
                if ((b & Convert.ToInt32("10000000", 2)) == 0x00)
                {
                    break;
                }

                shift += 7;
            }

            return result;
        }

        public long ReadLong()
        {
            return Reader.ReadInt64();
        }

        public byte[] ReadStringBytes()
        {
            byte type = Reader.ReadByte();

            if (type == 0x00)
            {
                // The string is empty.
                return new byte[0];
            }
            else if (type == 0x0B)
            {
                int length = ReadULEB128();
                byte[] byteArray = Reader.ReadBytes(length);

                return byteArray;
            }
            else
            {
                throw new Exception("Invalid string");
            }
        }

        public string ReadString()
        {
            return System.Text.Encoding.UTF8.GetString(ReadStringBytes());
        }

        public string ReadASCIIString()
        {
            return System.Text.Encoding.ASCII.GetString(ReadStringBytes());
        }

    }

}
