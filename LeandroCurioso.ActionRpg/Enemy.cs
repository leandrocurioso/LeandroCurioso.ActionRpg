using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LeandroCurioso.ActionRpg
{
    class Enemy
    {
        private Vector2 position = new Vector2(0, 0);
        private int speed = 150;
        public SpriteAnimation anim;

        public static List<Enemy> enemies = new List<Enemy>();

        public Vector2 Position {
            get {
                return position;
            }
        }
        public Enemy(Vector2 position, Texture2D spriteSheet) {
            this.position = position;
            anim = new SpriteAnimation(spriteSheet, 10, 6);
        }

        public void Update(GameTime gameTime, Vector2 playerPos) {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            anim.Position = new Vector2(position.X - 48, position.Y - 66);
            anim.Update(gameTime);

            // move to player position
            Vector2 moveDir = playerPos - position;
            moveDir.Normalize();
            position += moveDir * (speed * dt);
        }


    }
}