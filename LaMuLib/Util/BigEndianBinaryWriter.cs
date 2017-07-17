using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace LaMuLib.Util
{
    public class BigEndianBinaryWriter : BinaryWriter
    {
        public BigEndianBinaryWriter() : base() { }
        public BigEndianBinaryWriter(Stream output) : base(output) {}
        public BigEndianBinaryWriter(Stream output, Encoding encoding): base(output,encoding) { }
        
        public override void Write(short value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Debug.Assert(bytes.Length == 2);

            Array.Reverse(bytes, 0, 2);
            Write(bytes);
        }

        public override void Write(ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Debug.Assert(bytes.Length == 2);

            Array.Reverse(bytes, 0, 2);
            Write(bytes);
        }
        
        public override void Write(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Debug.Assert(bytes.Length == 4);

            Array.Reverse(bytes, 0, 4);
            Write(bytes);
        }
        
        public override void Write(uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Debug.Assert(bytes.Length == 4);

            Array.Reverse(bytes, 0, 4);
            Write(bytes);
        }
    }
}