namespace SharpDL.Tiles
{
    internal static class AttributeNames
    {
        public static class LayerAttributes
        {
            public static string Name = "name";
            public static string Width = "width";
            public static string Height = "height";
            public static string Opacity = "opacity";
            public static string Visible = "visible";
            public static string Properties = "properties";
        }

        public static class PropertyAttributes
        {
            public static string Name = "name";
            public static string Value = "value";
        }

        public static class MapObjectLayerAttributes
        {
            public static string Color = "color";
            public static string Object = "object";
        }

        public static class MapObjectAttributes
        {
            public static string Name = "name";
            public static string Type = "type";
            public static string Properties = "properties";
            public static string X = "x";
            public static string Y = "y";
            public static string Width = "width";
            public static string Height = "height";
            public static string GID = "gid";
            public static string Polygon = "polygon";
            public static string Polyline = "polyline";
            public static string Points = "points";
        }

        public static class TileLayerAttributes
        {
            public static string Data = "data";
            public static string Encoding = "encoding";
            public static string Compression = "compression";
        }

        public static class TileSetAttributes
        {
            public static string FirstGID = "firstgid";
            public static string Name = "name";
            public static string TileWidth = "tilewidth";
            public static string TileHeight = "tileheight";
            public static string Spacing = "spacing";
            public static string Margin = "margin";
            public static string Image = "image";
            public static string Source = "source";
            public static string Transparency = "trans";
            public static string Tile = "tile";
            public static string ID = "id";
            public static string Properties = "properties";
            public static string TileSet = "tileset";
        }

        public static class MapAttributes
        {
            public static string Map = "map";
            public static string Version = "version";
            public static string Orientation = "orientation";
            public static string Width = "width";
            public static string Height = "height";
            public static string TileWidth = "tilewidth";
            public static string TileHeight = "tileheight";
            public static string Properties = "properties";
            public static string TileSet = "tileset";
            public static string TileLayer = "layer";
            public static string ObjectLayer = "objectgroup";
            public static string Source = "source";
            public static string MapProperties = Map + "/" + Properties;
            public static string MapTileSet = Map + "/" + TileSet;
            public static string MapTileLayer = Map + "/" + TileLayer;
            public static string MapObjectLayer = Map + "/" + ObjectLayer;
        }
    }
}