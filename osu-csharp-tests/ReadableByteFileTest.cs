using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OsuAPI.Common.Tests
{

    [TestClass]
    public class ReadableByteFileTest
    {

        [TestMethod]
        public void ReadByte()
        {
            byte[] Bytes = { 0x1F, 0x2D, 0x00, 0x05 };

            // Create the ByteFile.
            ReadableByteFile File = new ReadableByteFile(Bytes);

            // Test the first byte.
            Assert.AreEqual(0x1F, File.ReadByte());

            // Test the second byte.
            Assert.AreEqual(0x2D, File.ReadByte());

            // Test the third byte.
            Assert.AreEqual(0x00, File.ReadByte());

            // Test the fourth byte.
            Assert.AreEqual(0x05, File.ReadByte());
        }

        [TestMethod]
        public void ReadBytes()
        {
            byte[] Bytes = { 0x1F, 0x2D, 0x00, 0x05 };

            // Create the ByteFile.
            ReadableByteFile File = new ReadableByteFile(Bytes);

            // Test the bytes.
            CollectionAssert.AreEqual(Bytes, File.ReadBytes(4));
        }

        [TestMethod]
        public void ReadShort()
        {
            short Expected = 150;
            byte[] Bytes = { (byte)(Expected >> 0), (byte)(Expected >> 8) };

            // Create the ByteFile.
            ReadableByteFile File = new ReadableByteFile(Bytes);

            // Test the bytes.
            Assert.AreEqual(Expected, File.ReadShort());
        }

        [TestMethod]
        public void ReadShortMinimum()
        {
            short Expected = short.MinValue;
            byte[] Bytes = { (byte)(Expected >> 0), (byte)(Expected >> 8) };

            // Create the ByteFile.
            ReadableByteFile File = new ReadableByteFile(Bytes);

            // Test the bytes.
            Assert.AreEqual(Expected, File.ReadShort());
        }

        [TestMethod]
        public void ReadShortMaximum()
        {
            short Expected = short.MaxValue;
            byte[] Bytes = { (byte)(Expected >> 0), (byte)(Expected >> 8) };

            // Create the ByteFile.
            ReadableByteFile File = new ReadableByteFile(Bytes);

            // Test the bytes.
            Assert.AreEqual(Expected, File.ReadShort());
        }

        [TestMethod]
        public void ReadInt()
        {
            int Expected = 150;
            byte[] Bytes = { (byte)(Expected >> 0), (byte)(Expected >> 8), (byte)(Expected >> 16), (byte)(Expected >> 24) };

            // Create the ByteFile.
            ReadableByteFile File = new ReadableByteFile(Bytes);

            // Test the bytes.
            Assert.AreEqual(Expected, File.ReadInt());
        }

        [TestMethod]
        public void ReadIntMinimum()
        {
            int Expected = int.MinValue;
            byte[] Bytes = { (byte)(Expected >> 0), (byte)(Expected >> 8), (byte)(Expected >> 16), (byte)(Expected >> 24) };

            // Create the ByteFile.
            ReadableByteFile File = new ReadableByteFile(Bytes);

            // Test the bytes.
            Assert.AreEqual(Expected, File.ReadInt());
        }

        [TestMethod]
        public void ReadIntMaximum()
        {
            int Expected = int.MaxValue;
            byte[] Bytes = { (byte)(Expected >> 0), (byte)(Expected >> 8), (byte)(Expected >> 16), (byte)(Expected >> 24) };

            // Create the ByteFile.
            ReadableByteFile File = new ReadableByteFile(Bytes);

            // Test the bytes.
            Assert.AreEqual(Expected, File.ReadInt());
        }

        [TestMethod]
        public void ReadULEB128()
        {
            int Expected = 150;
            byte[] Bytes = { 0x96, 0x01 };

            // Create the ByteFile.
            ReadableByteFile File = new ReadableByteFile(Bytes);

            // Test the bytes.
            Assert.AreEqual(Expected, File.ReadULEB128());
        }

        [TestMethod]
        public void ReadULEB128Minimum()
        {
            int Expected = int.MinValue;
            byte[] Bytes = { 0x80, 0x80, 0x80, 0x80, 0x78 };

            // Create the ByteFile.
            ReadableByteFile File = new ReadableByteFile(Bytes);

            // Test the bytes.
            Assert.AreEqual(Expected, File.ReadULEB128());
        }

        [TestMethod]
        public void ReadULEB128Maximum()
        {
            int Expected = int.MaxValue;
            byte[] Bytes = { 0xFF, 0xFF, 0xFF, 0xFF, 0x07 };

            // Create the ByteFile.
            ReadableByteFile File = new ReadableByteFile(Bytes);

            // Test the bytes.
            Assert.AreEqual(Expected, File.ReadULEB128());
        }

        [TestMethod]
        public void ReadLong()
        {
            long Expected = 150;
            byte[] Bytes = {
                (byte)(Expected >> 0),
                (byte)(Expected >> 8),
                (byte)(Expected >> 16),
                (byte)(Expected >> 24),
                (byte)(Expected >> 32),
                (byte)(Expected >> 40),
                (byte)(Expected >> 48),
                (byte)(Expected >> 56)
            };

            // Create the ByteFile.
            ReadableByteFile File = new ReadableByteFile(Bytes);

            // Test the bytes.
            Assert.AreEqual(Expected, File.ReadLong());
        }

        [TestMethod]
        public void ReadLongMinimum()
        {
            long Expected = long.MinValue;
            byte[] Bytes = {
                (byte)(Expected >> 0),
                (byte)(Expected >> 8),
                (byte)(Expected >> 16),
                (byte)(Expected >> 24),
                (byte)(Expected >> 32),
                (byte)(Expected >> 40),
                (byte)(Expected >> 48),
                (byte)(Expected >> 56)
            };

            // Create the ByteFile.
            ReadableByteFile File = new ReadableByteFile(Bytes);

            // Test the bytes.
            Assert.AreEqual(Expected, File.ReadLong());
        }

        [TestMethod]
        public void ReadLongMaximum()
        {
            long Expected = long.MaxValue;
            byte[] Bytes = {
                (byte)(Expected >> 0),
                (byte)(Expected >> 8),
                (byte)(Expected >> 16),
                (byte)(Expected >> 24),
                (byte)(Expected >> 32),
                (byte)(Expected >> 40),
                (byte)(Expected >> 48),
                (byte)(Expected >> 56)
            };

            // Create the ByteFile.
            ReadableByteFile File = new ReadableByteFile(Bytes);

            // Test the bytes.
            Assert.AreEqual(Expected, File.ReadLong());
        }

        [TestMethod]
        public void ReadString()
        {
            string Expected = "😀😁😂😃 Example UTF-8 String";
            byte[] Bytes =
            {
                0x0B, // String identifier

                0x25, // 25 as ULEB128

                0xF0, 0x9F, 0x98, 0x80,
                0xF0, 0x9F, 0x98, 0x81,
                0xF0, 0x9F, 0x98, 0x82,
                0xF0, 0x9F, 0x98, 0x83,

                0x20, // Space

                0x45, 0x78, 0x61, 0x6D, 0x70, 0x6C, 0x65, // Example

                0x20, // Space

                0x55, 0x54, 0x46, 0x2D, 0x38, // UTF-8

                0x20, // Space

                0x53, 0x74, 0x72, 0x69, 0x6E, 0x67 // String
            };

            // Create the ByteFile.
            ReadableByteFile File = new ReadableByteFile(Bytes);

            // Test the bytes.
            Assert.AreEqual(Expected, File.ReadString());
        }

        [TestMethod]
        public void ReadASCIIString()
        {
            string Expected = "Example ASCII String";
            byte[] Bytes =
            {
                0x0B, // String identifier

                0x14, // 20 as ULEB128

                0x45, 0x78, 0x61, 0x6D, 0x70, 0x6C, 0x65, // Example

                0x20, // Space

                0x41, 0x53, 0x43, 0x49, 0x49, // ASCII

                0x20, // Space

                0x53, 0x74, 0x72, 0x69, 0x6E, 0x67 // String
            };

            // Create the ByteFile.
            ReadableByteFile File = new ReadableByteFile(Bytes);

            // Test the bytes.
            Assert.AreEqual(Expected, File.ReadASCIIString());
        }

    }

}
