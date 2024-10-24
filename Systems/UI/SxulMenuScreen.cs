using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using ReLogic.Content;
using System.Collections.Generic;
using Sxul.Config;

namespace Sxul.Systems.UI
{
    internal class SxulMenuScreen : ModMenu
    {
        public override string DisplayName => "Sxul Style";
        public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>("Sxul/Assets/UI/SxulLogo");
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/Solace");
        public override ModSurfaceBackgroundStyle MenuBackgroundStyle => ModContent.GetInstance<NullSurfaceBackground>();

        private Texture2D normalBackgroundTexture;
        private Texture2D decemberBackgroundTexture;
        private Texture2D halloweenBackgroundTexture;
        private Texture2D currentBackgroundTexture;

        // Light texture
        private Texture2D lightTexture;

        // List of twinkling lights
        private List<LightParticle> lights = new List<LightParticle>();

        public override void SetStaticDefaults()
        {
            if (!BackgroundStyle.Instance.Halloweener && !BackgroundStyle.Instance.Christmas)
            {
                currentBackgroundTexture = normalBackgroundTexture;
            }
        }

        #region Drawing
        public override void OnSelected()
        {
            base.OnSelected();
            LoadBackgrounds(); // Load the background textures
            UpdateBackgroundTexture(); // Set the initial background texture

            lightTexture = ModContent.Request<Texture2D>("Sxul/Assets/UI/LightParticle").Value;

            // Initialize twinkling lights
            for (int i = 0; i < 40; i++)
            {
                lights.Add(new LightParticle(new Vector2(Main.rand.Next(0, Main.screenWidth), Main.rand.Next(0, Main.screenHeight)), Main.rand.NextFloat(0.5f, 1.5f)));
            }
        }

        private void LoadBackgrounds()
        {
            normalBackgroundTexture = ModContent.Request<Texture2D>("Sxul/Assets/UI/SxulBackgroundTexture").Value;
            decemberBackgroundTexture = ModContent.Request<Texture2D>("Sxul/Assets/UI/SxulSnow").Value;
            halloweenBackgroundTexture = ModContent.Request<Texture2D>("Sxul/Assets/UI/FireHalloween").Value;
        }

        private void UpdateBackgroundTexture()
        {
            bool useChristmasBackground = ModContent.GetInstance<BackgroundStyle>().Christmas;
            bool useHalloweenBackground = ModContent.GetInstance<BackgroundStyle>().Halloweener;
            if (useChristmasBackground && !useHalloweenBackground)
            {
                currentBackgroundTexture = decemberBackgroundTexture;
            }
            else if (useHalloweenBackground && !useChristmasBackground)
            {
                currentBackgroundTexture = halloweenBackgroundTexture;
            }
            else
            {
                currentBackgroundTexture = normalBackgroundTexture;
            }
        }

        public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
        {
            UpdateBackgroundTexture();

            // Scale and draw background based on screen size
            Vector2 drawOffset = Vector2.Zero;
            float xScale = (float)Main.screenWidth / currentBackgroundTexture.Width;
            float yScale = (float)Main.screenHeight / currentBackgroundTexture.Height;
            float scale = xScale;

            if (xScale != yScale)
            {
                if (yScale > xScale)
                {
                    scale = yScale;
                    drawOffset.X -= (currentBackgroundTexture.Width * scale - Main.screenWidth) * 0.5f;
                }
                else
                {
                    drawOffset.Y -= (currentBackgroundTexture.Height * scale - Main.screenHeight) * 0.5f;
                }
            }

            spriteBatch.Draw(currentBackgroundTexture, drawOffset, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            // Now draw the logo
            drawColor = Color.White;
            Vector2 drawPos = new Vector2(Main.screenWidth / 2f, 100f);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
            spriteBatch.Draw(Logo.Value, drawPos, null, drawColor, 0f, Logo.Value.Size() * 0.5f, logoScale, SpriteEffects.None, 0f);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);

            // Update and draw the twinkling lights
            foreach (var light in lights)
            {
                light.Update();
                light.Draw(spriteBatch, lightTexture);
            }

            return false;
        }
        #endregion
        #region LightParticle

        public class LightParticle
        {
            public Vector2 Position;
            public float Scale;
            public float Opacity;
            private float twinkleSpeed;
            private bool isTwinklingUp;

            public LightParticle(Vector2 position, float scale)
            {
                Position = position;
                Scale = scale / 3.5f;
                Opacity = Main.rand.NextFloat(0.3f, 1f); // Start with random opacity
                twinkleSpeed = Main.rand.NextFloat(0.01f, 0.03f);
                isTwinklingUp = Main.rand.NextBool();
            }

            public void Update()
            {
                // Twinkle logic (opacity goes up and down)
                if (isTwinklingUp)
                {
                    Opacity += twinkleSpeed;
                    if (Opacity >= 1f)
                    {
                        isTwinklingUp = false;
                    }
                }
                else
                {
                    Opacity -= twinkleSpeed;
                    if (Opacity <= 0.3f)
                    {
                        isTwinklingUp = true;
                        // Move to a new position when the full flicker cycle completes (opacity fully decreases)
                        MoveToRandomPosition();
                    }
                }
            }

            private void MoveToRandomPosition()
            {
                Position = new Vector2(Main.rand.Next(0, Main.screenWidth), Main.rand.Next(0, Main.screenHeight));
            }

            public void Draw(SpriteBatch spriteBatch, Texture2D lightTexture)
            {
                Color color = Color.White * Opacity;
                spriteBatch.Draw(lightTexture, Position, null, color, 0f, lightTexture.Size() * 0.5f, Scale, SpriteEffects.None, 0f);
            }
        }

        #endregion
    }
}
