using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting.Messaging;
using LaMuLib.MSD;
using LaMuLib.Util;

namespace LaMuLib
{
    public struct MapMSD
    {
        public AnimatedTile[] AnimatedTiles;
        public byte GraphicsFilesID;
        public short NumberOfZones;
        public Zone[] Zones;
        
        public static MapMSD ParseFile(string path)
        {
            var msd = new MapMSD();
            
            using (var file = File.OpenRead(path))
            {
                var reader = new BigEndianBinaryReader(file);

                msd.AnimatedTiles = AnimatedTile.FromReader(reader);
                msd.GraphicsFilesID = reader.ReadByte();
                msd.NumberOfZones = reader.ReadInt16();
                msd.Zones = Zone.FromReader(reader, msd);
            }
            
            return msd;
        }

        public void ToFile(string path)
        {
            using (var file = File.OpenWrite(path))
            {
                var writer = new BigEndianBinaryWriter(file);

                // Write animated tiles
                foreach (var animatedTile in AnimatedTiles)
                {
                    animatedTile.Write(writer);
                }

                // Write end of antimated tile section
                writer.Write((short) 0);

                writer.Write(GraphicsFilesID);
                writer.Write((short)Zones.Length);

                
                foreach (var zone in Zones)
                {
                    zone.Write(writer);
                }

                var paddingLength = (writer.BaseStream.Length % 32);
                if (paddingLength == 0) paddingLength = 32;
                
                Debug.WriteLine("MSD padding bytes: " + paddingLength);
                
                // For some reason the game data is padded with an X amount of 00 bytes
                // The game works without it, but it's included nonetheless to be safe
                for (int i = 0; i < paddingLength; i++)
                {
                    writer.Write((byte) 0);
                }
                
            }
        }
    }
}