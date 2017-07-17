using System;
using LaMuLib.Util;

namespace LaMuLib.MSD
{
    public struct TileID
    {
        private readonly short _internalValue;

        public short RawTileId => (short) (_internalValue & 0x7FF);
        public TileType TileType => (TileType) (_internalValue >> 11 & 0x3);
        public bool FlippedHorizontally => (_internalValue >> 13 & 0x1) == 1;
        public bool Rotated90Degrees => (_internalValue >> 14 & 0x1) == 1;
        public bool Rotated180Degrees => (_internalValue >> 15 & 0x1) == 1;

        public TileID(short val)
        {
            _internalValue = val;
        }

        public static TileID FromReader(BigEndianBinaryReader reader)
        {
            return new TileID(reader.ReadInt16());
        }

        public void Write(BigEndianBinaryWriter writer)
        {
            writer.Write(_internalValue);
        }
    }
}