using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ReLogic.Content;
using Sxul.Config;
using System.Collections.Generic;

namespace Sxul.Systems.UI
{
    internal class MenuScreen : ModMenu
    {
        public override void SetStaticDefaults()
        {
            if (!BackgroundStyle.Instance.Halloweener && !BackgroundStyle.Instance.Christmas)
            {
                currentBackgroundTexture = normalBackgroundTexture;
            }
        }
        public class Shower
        {
            public int Time;
            public int Lifetime;
            public int IdentityIndex;
            public float Scale;
            public float Depth;
            public Color DrawColor;
            public Vector2 Velocity;
            public Vector2 Center;

            public Shower(int lifetime, int identity, float depth, Color color, Vector2 startingPosition, Vector2 startingVelocity)
            {
                Lifetime = lifetime;
                IdentityIndex = identity;
                Depth = depth;
                DrawColor = color;
                Center = startingPosition;
                Velocity = startingVelocity;
            }
        }

        public static List<Shower> Showers
        {
            get;
            internal set;
        } = new();

        // Define the Firefly class for floating particles
        public class Firefly
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public Vector2 newVel;
            public float Lifespan;
            public float TimeAlive;
            public float Scale;
            public float Depth;
            public static Asset<Texture2D> FireflyTexture;
            private float hue;
            private List<Vector2> TrailPositions = new List<Vector2>(); // For the trail effect

            public Firefly(Vector2 position, float depth)
            {
                Position = position;
                Velocity = new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-0.5f, 0.5f));
                Lifespan = Main.rand.NextFloat(5f, 10f); // Fireflies will live between 5 and 10 seconds
                TimeAlive = 0f;
                Depth = depth; // Depth affects movement and scaling
                Scale = MathHelper.Lerp(0.5f, 2f, Depth); // Scaling based on depth
                hue = Main.rand.NextFloat(0f, 1f); // Starting hue for rainbow effect
            }

            public void Update()
            {
                // Slight random adjustments to the velocity
                Velocity += new Vector2(Main.rand.NextFloat(-0.1f, 0.1f), Main.rand.NextFloat(-0.1f, 0.1f));
                Velocity = Vector2.Clamp(Velocity, new Vector2(-0.5f, -1.5f), new Vector2(0.5f, 1.5f)); // Limit speed
                Position += Velocity;

                // Update time alive
                TimeAlive = 5f; // Time progresses per frame (assuming 60fps)

                // Rainbow color shifting
                hue += 0.001f; // Slow color transition
                if (hue > 1f) hue -= 1f; // Loop the hue
                Color rainbowColor = Main.hslToRgb(hue, 1f, 0.75f);

                // Add trail positions for the trail effect
                if (TrailPositions.Count > 15)
                    TrailPositions.RemoveAt(0); // Limit trail length
                TrailPositions.Add(Position);

                // Wrap the fireflies around screen edges
                if (Position.X < 0) Position.X = Main.screenWidth;
                if (Position.X > Main.screenWidth) Position.X = 0;
                if (Position.Y < 0) Position.Y = Main.screenHeight;
                if (Position.Y > Main.screenHeight) Position.Y = 0;
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                // Draw the trail first
                DrawTrail(spriteBatch);

                // Draw the firefly itself
                spriteBatch.Draw(FireflyTexture.Value, Position, null, Main.hslToRgb(hue, 1f, 0.75f), 0f, FireflyTexture.Size() * 0.5f, Scale, SpriteEffects.None, 0f);
            }

            private void DrawTrail(SpriteBatch spriteBatch)
            {
                // Draw the trail fading out
                for (int i = 0; i < TrailPositions.Count; i++)
                {
                    float alpha = (float)i / TrailPositions.Count; // Fade out as the trail goes farther
                    spriteBatch.Draw(FireflyTexture.Value, TrailPositions[i], null, Color.White * alpha, 0f, FireflyTexture.Size() * 0.5f, Scale * 0.5f, SpriteEffects.None, 0f);
                }
            }

            public bool ShouldRemove()
            {
                return TimeAlive >= Lifespan; // Remove when time alive exceeds lifespan
            }
        }

        // List of firefly particles
        public static List<Firefly> Fireflies = new();
        public override string DisplayName => "Fire Starter Style";
        public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>("Sxul/Assets/UI/FireLogo");
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/StartingFire");
        public override ModSurfaceBackgroundStyle MenuBackgroundStyle => ModContent.GetInstance<NullSurfaceBackground>();

        private Texture2D normalBackgroundTexture;
        private Texture2D decemberBackgroundTexture;
        private Texture2D halloweenBackgroundTexture;
        private Texture2D currentBackgroundTexture;

        public override void OnSelected()
        {
            base.OnSelected();
            LoadBackgrounds(); // Load the background textures
            UpdateBackgroundTexture(); // Set the initial background texture
        }

        private void LoadBackgrounds()
        {
            normalBackgroundTexture = ModContent.Request<Texture2D>("Sxul/Assets/UI/MenuBackground").Value;
            decemberBackgroundTexture = ModContent.Request<Texture2D>("Sxul/Assets/UI/FireSnow").Value;
            halloweenBackgroundTexture = ModContent.Request<Texture2D>("Sxul/Assets/UI/FireHalloween(NotSxul)").Value;
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

            // Draw the background texture
            spriteBatch.Draw(currentBackgroundTexture, drawOffset, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            // Firefly management with a max number of fireflies
            const int maxFireflies = 40;

            if (Showers.Count == 0 && Fireflies.Count < maxFireflies)
            {
                Firefly.FireflyTexture = ModContent.Request<Texture2D>("Sxul/Assets/UI/FireFlyTexture");

                for (int i = Fireflies.Count; i < maxFireflies; i++)
                {
                    float randomDepth = Main.rand.NextFloat(0f, 1f); // Random depth between 0 (closest) and 1 (farthest)
                    Fireflies.Add(new Firefly(new Vector2(Main.rand.Next(0, Main.screenWidth), Main.rand.Next(0, Main.screenHeight)), randomDepth));
                }
            }

            // Update and remove fireflies that should expire
            for (int i = Fireflies.Count - 1; i >= 0; i--)
            {
                Fireflies[i].Update();
                Fireflies[i].Draw(spriteBatch);

                if (Fireflies[i].ShouldRemove())
                {
                    Fireflies.RemoveAt(i); // Remove expired fireflies
                }
            }

            // Now proceed to draw the logo
            drawColor = Color.White;
            Vector2 drawPos = new Vector2(Main.screenWidth / 2f, 100f);

            // End and restart spriteBatch for transparency effects
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
            spriteBatch.Draw(Logo.Value, drawPos, null, drawColor, 0f, Logo.Value.Size() * 0.5f, logoScale, SpriteEffects.None, 0f);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);

            return false;
        }
    }
}
