using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace LeandroCurioso.ActionRpg
{
    internal class Controller
    {
        public static double timer = 2D;
        public static double maxTime = 2D;
        public static Random rand = new Random();

        public static void Update(GameTime gameTime, Texture2D enemySpriteSheet)
        {
            timer -= gameTime.ElapsedGameTime.TotalSeconds;
            if (timer <= 0)
            {
                int side = rand.Next(4);
                switch(side)
                {
                    case 0: // Left
                        Enemy.enemies.Add(new Enemy(new Vector2(-500, rand.Next(-500, 2000)), enemySpriteSheet));
                        break;
                    case 1: // Right
                        Enemy.enemies.Add(new Enemy(new Vector2(-2000, rand.Next(-500, 2000)), enemySpriteSheet));
                        break;
                    case 2: // Top
                        Enemy.enemies.Add(new Enemy(new Vector2(rand.Next(-500, 2000), -500), enemySpriteSheet));
                        break;
                    case 3: // Bottom
                        Enemy.enemies.Add(new Enemy(new Vector2(rand.Next(-500, 2000), 200), enemySpriteSheet));
                        break;
                }

                timer = maxTime;
                if (maxTime > 0.5)
                {
                    maxTime -= 0.05D;
                }
            }
        }


    }
}
