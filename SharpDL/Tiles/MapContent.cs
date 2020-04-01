using SharpDL.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;

namespace SharpDL.Tiles
{
    public enum Orientation : byte
    {
        Orthogonal,
        Isometric
    }

    public class MapContent
    {
        private PropertyCollection properties = new PropertyCollection();
        private List<TileSetContent> tileSets = new List<TileSetContent>();
        private List<LayerContent> layers = new List<LayerContent>();

        public string FileName { get; private set; }

        public string FileDirectory { get; private set; }

        public string Version { get; private set; }

        public Orientation Orientation { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public int TileWidth { get; private set; }

        public int TileHeight { get; private set; }

        public PropertyCollection Properties { get { return properties; } }

        public IReadOnlyCollection<TileSetContent> TileSets { get { return tileSets.AsReadOnly(); } }

        public IReadOnlyCollection<LayerContent> Layers { get { return layers.AsReadOnly(); } }

        public MapContent(string filePath, Renderer renderer, string contentRoot)
        {
            Utilities.ThrowExceptionIfIsNullOrEmpty(filePath, "filePath");
            Debug.Assert(renderer != null, "Renderer cannot be null when loading a tiled map.");

            XmlDocument document = new XmlDocument();
            document.Load(filePath);

            XmlNode mapNode = document[AttributeNames.MapAttributes.Map];

            Utilities.ThrowExceptionIfIsNull(mapNode, "mapNode");
            Utilities.ThrowExceptionIfAttributeIsNull(mapNode, "mapNode", AttributeNames.MapAttributes.Version);
            Utilities.ThrowExceptionIfAttributeIsNull(mapNode, "mapNode", AttributeNames.MapAttributes.Orientation);
            Utilities.ThrowExceptionIfAttributeIsNull(mapNode, "mapNode", AttributeNames.MapAttributes.Width);
            Utilities.ThrowExceptionIfAttributeIsNull(mapNode, "mapNode", AttributeNames.MapAttributes.Height);
            Utilities.ThrowExceptionIfAttributeIsNull(mapNode, "mapNode", AttributeNames.MapAttributes.TileWidth);
            Utilities.ThrowExceptionIfAttributeIsNull(mapNode, "mapNode", AttributeNames.MapAttributes.TileHeight);

            Version = mapNode.Attributes[AttributeNames.MapAttributes.Version].Value;
            Orientation = (Orientation)Enum.Parse(typeof(Orientation), mapNode.Attributes[AttributeNames.MapAttributes.Orientation].Value, true);
            Width = Utilities.TryToParseInt(mapNode.Attributes[AttributeNames.MapAttributes.Width].Value);
            Height = Utilities.TryToParseInt(mapNode.Attributes[AttributeNames.MapAttributes.Height].Value);
            TileWidth = Utilities.TryToParseInt(mapNode.Attributes[AttributeNames.MapAttributes.TileWidth].Value);
            TileHeight = Utilities.TryToParseInt(mapNode.Attributes[AttributeNames.MapAttributes.TileHeight].Value);

            // properties are optional
            XmlNode propertiesNode = document.SelectSingleNode(AttributeNames.MapAttributes.MapProperties);
            if (propertiesNode != null)
            {
                properties = new PropertyCollection(propertiesNode);
            }

            BuildTileSets(document);

            BuildLayers(document);

            BuildTileSetTextures(renderer, contentRoot);

            GenerateTileSourceRectangles();
        }

        private void BuildTileSets(XmlDocument document)
        {
            foreach (XmlNode tileSetNode in document.SelectNodes(AttributeNames.MapAttributes.MapTileSet))
            {
                if (!Utilities.IsNull(tileSetNode.Attributes[AttributeNames.MapAttributes.Source]))
                {
                    tileSets.Add(new ExternalTileSetContent(tileSetNode));
                }
                else
                {
                    tileSets.Add(new TileSetContent(tileSetNode));
                }
            }
        }

        private void BuildLayers(XmlDocument document)
        {
            foreach (XmlNode layerNode in document.SelectNodes(AttributeNames.MapAttributes.MapTileLayer + "|" + AttributeNames.MapAttributes.MapObjectLayer))
            {
                LayerContent layer;
                if (layerNode.Name == AttributeNames.MapAttributes.TileLayer)
                {
                    layer = new TileLayerContent(layerNode);
                }
                else if (layerNode.Name == AttributeNames.MapAttributes.ObjectLayer)
                {
                    layer = new ObjectLayerContent(layerNode);
                }
                else
                {
                    throw new Exception(String.Format("Unknown layer: {0}. Must be tile or object layer.", layerNode.Name));
                }

                Utilities.ThrowExceptionIfIsNullOrEmpty(layer.Name, "layerName");

                string layerName = layer.Name;
                int duplicateCount = 2;

                // this handles renaming duplicates, how can we make this faster without O(n) searching through a list?
                // store these in hash tables and perform existence check prior to adding?
                while (layers.Any(l => l.Name == layerName))
                {
                    layerName = String.Format("{0}{1}", layer.Name, duplicateCount);
                    duplicateCount++;
                }

                layer.Name = layerName;

                layers.Add(layer);
            }
        }

        private void BuildTileSetTextures(Renderer renderer, string contentRoot)
        {
            // build textures
            foreach (TileSetContent tileSet in tileSets)
            {
                string path = Path.Combine(contentRoot, tileSet.ImageSource);

                // need to use colorkey
                // need to support more than PNG
                Surface surface = new Surface(path, SurfaceType.PNG);
                tileSet.Texture = new Texture(renderer, surface);
            }
        }

        private void GenerateTileSourceRectangles()
        {
            // process the tilesets, calculate tiles to fit in each set, calculate source rectangles
            foreach (TileSetContent tileSet in tileSets)
            {
                int imageWidth = tileSet.Texture.Width;
                int imageHeight = tileSet.Texture.Height;

                imageWidth -= tileSet.Margin * 2;
                imageHeight -= tileSet.Margin * 2;

                int tileCountX = 0;
                while ((tileCountX + 1) * tileSet.TileWidth <= imageWidth)
                {
                    tileCountX++;
                    imageWidth -= tileSet.Spacing;
                }

                int tileCountY = 0;
                while ((tileCountY + 1) * tileSet.TileHeight <= imageHeight)
                {
                    tileCountY++;
                    imageHeight -= tileSet.Spacing;
                }

                for (int y = 0; y < tileCountY; y++)
                {
                    for (int x = 0; x < tileCountX; x++)
                    {
                        int rx = tileSet.Margin + x * (tileSet.TileWidth + tileSet.Spacing);
                        int ry = tileSet.Margin + y * (tileSet.TileHeight + tileSet.Spacing);
                        Rectangle source = new Rectangle(rx, ry, tileSet.TileWidth, tileSet.TileHeight);

                        int index = tileSet.FirstGID + (y * tileCountX + x);
                        PropertyCollection tileProperties = new PropertyCollection();
                        if (tileSet.TileProperties.ContainsKey(index))
                        {
                            tileProperties = tileSet.TileProperties[index];
                        }

                        TileContent tile = new TileContent(source, tileProperties);
                        tileSet.Tiles.Add(tile);
                    }
                }
            }
        }
    }
}