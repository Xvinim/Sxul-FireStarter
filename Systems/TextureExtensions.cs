using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;

namespace Sxul.Systems
{
    public static class TextureExtensions
    {
        // This extension method extracts the color data from a Texture2D.
        public static Color[] GetColorsFromTexture(this Texture2D texture)
        {
            Color[] colorData = new Color[texture.Width * texture.Height]; // Create an array to store color data
            texture.GetData(colorData); // Get the color data from the texture
            return colorData;
        }
    }
}
