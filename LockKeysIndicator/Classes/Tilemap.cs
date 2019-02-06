using System.Drawing;
using System.Collections.Generic;

namespace LockKeysIndicator
{
    public class Tilemap
    {
        /// <summary>
        /// Loads a tilemap, seperates parts by Width and returns a List of images.
        /// </summary>
        /// <param name="tileMapPngPath"></param>
        /// <returns></returns>
        public static List<Image> Load(Image tilemap)
        {
            try
            {
                Bitmap tilemap_bmp = (Bitmap)tilemap;
                List<Image> seperated = new List<Image>();
                // Gets the image width (also defines the Height of a single part)
                int iconSize = tilemap.Width;
                // Determines how many parts the image has from Top to Bottom
                int partCount = (tilemap.Height / iconSize);
                // Loop through every part and add it to a List of Images (contains image parts)
                for (int part = 0; part <= partCount - 1; part++)
                {
                    Rectangle srcRect = new Rectangle(0, part * iconSize, iconSize, iconSize);
                    Bitmap croppedPart = tilemap_bmp.Clone(srcRect, tilemap_bmp.PixelFormat);
                    seperated.Add(croppedPart);
                }
                return seperated;
            }
            catch { return new List<Image>(); }
        }
    }
}
