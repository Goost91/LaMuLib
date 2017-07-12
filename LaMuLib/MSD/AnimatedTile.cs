using System.Collections.Generic;
using LaMuLib.Util;

namespace LaMuLib.MSD
{
    public struct AnimatedTile
    {
        private readonly short _internalValue;

        public bool unk00 => (_internalValue & 0b1) == 1;
        public short NumberOfFrames => (short) ((_internalValue >> 1) & 0x7FFF);
        public TileID[] Frames;

        public AnimatedTile(short val)
        {
            _internalValue = val;
            Frames = new TileID[val >> 1 & 0x7FFF];
        }
        
        public static AnimatedTile[] FromReader(BigEndianBinaryReader reader)
        {
            var tiles = new List<AnimatedTile>();
            bool foundEnd = false;
            
            while (!foundEnd)
            {
                var chunk = reader.ReadInt16();

                if (chunk == 0 && reader.ReadInt16() == 0)
                {
                    foundEnd = true;
                    continue;
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