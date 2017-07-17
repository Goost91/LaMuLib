using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using LaMuLib.Util;

namespace LaMuLib.MSD
{
    public struct AnimatedTile
    {
        private readonly short _internalValue;

        public bool unk00 => (_internalValue >> 15) == 1;
        public short NumberOfFrames => (short) ((_internalValue) & 0x7FFF);
        public TileID[] Frames;

        public AnimatedTile(short val)
        {
            _internalValue = val;
            Frames = new TileID[val & 0x7FFF];
        }
        
        public static AnimatedTile[] FromReader(BigEndianBinaryReader reader)
        {
            var tiles = new List<AnimatedTile>();
            bool foundEnd = false;
            
            while (!foundEnd)
            {
                var chunk = reader.ReadInt16();
                
                if (chunk == 0)
                {
                    return tiles.ToArray();
                }

                var tile = new AnimatedTile(chunk);
                
                for (int numFrame = 0; numFrame < tile.NumberOfFrames; numFrame++)
                {
                    tile.Frames[numFrame] = TileID.FromReader(reader);
                }

                tiles.Add(tile);
            }

            return tiles.ToArray();
        }

        public void Write(BigEndianBinaryWriter writer)
        {
            writer.Write(_internalValue);
            foreach (var frame in Frames)
            {
                frame.Write(writer);
            }
        }
    }
}