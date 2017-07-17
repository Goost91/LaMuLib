using System.Collections.Generic;
using LaMuLib.Util;

namespace LaMuLib.MSD
{
    public struct SubLayer
    {
        public TileID[] tiles;

        public SubLayer(short width, short height)
        {
            tiles = new TileID[width * height];
        }

        public static SubLayer[] FromReader(BigEndianBinaryReader reader, Layer layer)
        {
            var sublayers = new List<SubLayer>();
            
            for (var numSubLayer = 0; numSubLayer < layer.NumberOfSubLayers; numSubLayer++)
            {
                var sublayer = new SubLayer(layer.LayerWidth, layer.LayerHeight);
                for (var numSubLayerTile = 0; numSubLayerTile < sublayer.tiles.Length; numSubLayerTile++)
                {
                    sublayer.tiles[numSubLayerTile] = TileID.FromReader(reader);
                }
                sublayers.Add(sublayer);
            }

            return sublayers.ToArray();
        }

        public void Write(BigEndianBinaryWriter writer)
        {
            foreach (var tile in tiles)
            {
                tile.Write(writer);
            }
        }
    }
}