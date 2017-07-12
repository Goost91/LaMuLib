using System.IO;
using LaMuLib.MSD;
using LaMuLib.Util;

namespace LaMuLib
{
    public struct MapMSD
    {
        public AnimatedTile[] AntimatedTiles;
        public byte GraphicsFilesID;
        public short NumberOfZones;
        public Zone[] Zones;
        
        public static MapMSD ParseFile(string path)
        {
            var msd = new MapMSD();
            var reader = new BigEndianBinaryReader(File.OpenRead(path));

            msd.AntimatedTiles = AnimatedTile.FromReader(reader);
            msd.GraphicsFilesID = reader.ReadByte();
            msd.NumberOfZones = reader.ReadInt16();
            msd.Zones = Zone.FromReader(reader,msd);
            
            return msd;
        }
    }
}