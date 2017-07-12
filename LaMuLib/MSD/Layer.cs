using System.Collections.Generic;
using LaMuLib.Util;

namespace LaMuLib.MSD
{
    public struct Layer
    {
        public short LayerWidth;
        public short LayerHeight;
        public byte NumberOfSubLayers;
        public SubLayer[] SubLayers;
        
        public static Layer[] FromReader(BigEndianBinaryReader reader, byte numLayers)
        {
            var layers = new List<Layer>();

            for (int numLayer = 0; numLayer < numLayers; numLayer++)
            {
                var layer = new Layer();

                layer.LayerWidth = reader.ReadInt16();
                layer.LayerHeight = reader.ReadInt16();
                layer.NumberOfSubLayers = reader.ReadByte();
                layer.SubLayers = SubLayer.FromReader(reader, layer);
                layers.Add(layer);
            }
            
            return layers.ToArray();
        }
    }
}