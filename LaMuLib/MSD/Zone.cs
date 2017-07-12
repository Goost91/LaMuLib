using System.Collections.Generic;
using LaMuLib.Util;

namespace LaMuLib.MSD
{
    public struct Zone
    {
        public byte UseBossGraphics;
        public byte NumberOfLayers;
        public byte NumberOfPrimeLayer;
        public short HitMaskWidth;
        public short HitMaskHeight;
        public byte[,] CollisionMask;
        public Layer[] Layers;
        
        public static Zone[] FromReader(BigEndianBinaryReader reader, MapMSD targetMsd)
        {
            var zones = new List<Zone>();

            for (var numZone = 0; numZone < targetMsd.NumberOfZones; numZone++)
            {
                var zone = new Zone();
                zone.UseBossGraphics = reader.ReadByte();
                zone.NumberOfLayers = reader.ReadByte();
                zone.NumberOfPrimeLayer = reader.ReadByte();
                zone.HitMaskWidth = reader.ReadInt16();
                zone.HitMaskHeight = reader.ReadInt16();
                zone.CollisionMask = new byte[zone.HitMaskWidth, zone.HitMaskHeight];

                for (var x = 0; x < zone.HitMaskWidth; x++)
                {
                    for (var y = 0; y < zone.HitMaskHeight; y++)
                    {
                        zone.CollisionMask[x, y] = reader.ReadByte();
                    }
                }

                zone.Layers = Layer.FromReader(reader, zone.NumberOfLayers);

                zones.Add(zone);
            }
            
            return zones.ToArray();
        }
    }
}