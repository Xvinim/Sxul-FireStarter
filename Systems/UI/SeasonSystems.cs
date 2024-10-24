using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using ReLogic.Content;
using System;

namespace Sxul.Systems.UI
{
    internal class SeasonSystems : ModSystem
    {
        /*
        private List<FallingObject> fallingObjects = new List<FallingObject>();
        private int seasonTimer = 0;
        private const int seasonDuration = 60 * 60 * 10; // 10 minutes

        private enum Season
        {
            Summer,
            Autumn,
            Winter,
            Spring
        }

        private Season currentSeason;

        public override void UpdateUI(GameTime gameTime)
        {
            SxulMenuScreen sx = ModContent.GetInstance<SxulMenuScreen>();
            if (!sx.IsSelected)
            {
                return;
            }

            seasonTimer++;

            // Change season every 10 minutes
            if (seasonTimer >= seasonDuration)
            {
                ChangeSeason();
                seasonTimer = 0;
            }

            // Update falling objects
            foreach (var obj in fallingObjects)
            {
                obj.Update(gameTime);
            }

            // Clean up inactive objects
            fallingObjects.RemoveAll(obj => !obj.IsActive);
        }

        private void ChangeSeason()
        {
            switch (currentSeason)
            {
                case Season.Summer:
                    currentSeason = Season.Autumn;
                    SpawnFallingObjects(Season.Autumn);
                    break;
                case Season.Autumn:
                    currentSeason = Season.Winter;
                    SpawnFallingObjects(Season.Winter);
                    break;
                case Season.Winter:
                    currentSeason = Season.Spring;
                    SpawnFallingObjects(Season.Spring);
                    break;
                case Season.Spring:
                    currentSeason = Season.Summer;
                    SpawnFallingObjects(Season.Summer);
                    break;
            }
        }

        private void SpawnFallingObjects(Season season)
        {
            fallingObjects.Clear(); // Reset previous objects

            int objectCount = 50; // Set the number of objects

            switch (season)
            {
                case Season.Summer:
                    for (int i = 0; i < objectCount; i++)
                    {
                        fallingObjects.Add(new FallingObject(ModContent.Request<Texture2D>("Sxul/Assets/UI/PetalSprite"), LeafBehavior));
                    }
                    break;
                case Season.Autumn:
                    for (int i = 0; i < objectCount; i++)
                    {
                        fallingObjects.Add(new FallingObject(ModContent.Request<Texture2D>("Sxul/Assets/UI/LeafSprite"), LeafBehavior));
                    }
                    break;
                case Season.Winter:
                    for (int i = 0; i < objectCount; i++)
                    {
                        fallingObjects.Add(new FallingObject(ModContent.Request<Texture2D>("Sxul/Assets/UI/SnowSprite"), SnowBehavior));
                    }
                    break;
                case Season.Spring:
                    for (int i = 0; i < objectCount; i++)
                    {
                        fallingObjects.Add(new FallingObject(ModContent.Request<Texture2D>("Sxul/Assets/UI/RainSprite"), RainBehavior));
                    }
                    break;
            }
        }

        // Behavior for leaves and petals (slow and random)
        private void LeafBehavior(FallingObject obj, GameTime gameTime)
        {
            obj.Position.Y += 1f; // Slow falling speed
            obj.Position.X += Main.rand.NextFloat(-0.5f, 0.5f); // Random horizontal sway
        }

        // Behavior for snow (falls to the right)
        private void SnowBehavior(FallingObject obj, GameTime gameTime)
        {
            obj.Position.Y += 1.5f; // Moderate falling speed
            obj.Position.X += 0.5f; // Drift right
        }

        // Behavior for rain (falls faster, slightly to the right)
        private void RainBehavior(FallingObject obj, GameTime gameTime)
        {
            obj.Position.Y += 3f; // Fast falling speed
            obj.Position.X += 0.2f; // Slight drift right
        }
    }

    internal class FallingObject
    {
        public Texture2D Texture;
        public Vector2 Position;
        public bool IsActive;
        private Action<FallingObject, GameTime> UpdateBehavior;

        public FallingObject(Asset<Texture2D> texture, Action<FallingObject, GameTime> behavior)
        {
            Texture = texture.Value;
            Position = new Vector2(Main.rand.Next(0, Main.screenWidth), 0);
            IsActive = true;
            UpdateBehavior = behavior;
        }

        public void Update(GameTime gameTime)
        {
            UpdateBehavior(this, gameTime);

            if (Position.Y > Main.screenHeight)
            {
                IsActive = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
        */
    }
}
