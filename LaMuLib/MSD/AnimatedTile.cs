using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using LaMuLib.Util;

namespace LaMuLib.MSD
{
    public struct AnimatedTile
    {
        private readonly ushort _internalValue;

        public bool unk00 => (_internalValue >> 15) == 1;
        public short NumberOfFrames => (short) ((_internalValue) & 0x7FFF);
        public TileID[] Frames;

        public AnimatedTile(ushort val)
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
                var chunk = reader.ReadUInt16();
                
                Debug.WriteLine("Found chunk val " + chunk);
                
                if (chunk == 0)
                {
                    //reader.ReadInt16();
                    return tiles.ToArray();
                    if (reader.ReadInt16() == 0)
                    {
                    }
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
    }
}