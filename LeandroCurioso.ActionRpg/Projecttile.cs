using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LeandroCurioso.ActionRpg
{
    class Projectile
    {
        private Vector2 position = new Vector2(500, 300);
        private int speed = 1000;
        public int radius = 18;
        private Dir direction;

        private bool collided = false;

        public Vector2 Position {
            get {
                return position;
            }
        }

        public bool Collided
        {
            get { return collided; }
            set { collided = value;}
        }

        public static List<Projectile> projectiles = new List<Projectile>();

        public Projectile(Vector2 position, Dir dir) {
            this.position = position;
            direction = dir;

        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            switch (direction)
                {
                    case Dir.Down:
                        position.Y += speed * dt;
                        break;
                    case Dir.Up:
                        position.Y -= speed * dt;
                        break;
                    case Dir.Left:
                        position.X -= speed * dt;
                        break;
                    case Dir.Right:
                        position.X += speed * dt;
                        break;
                }
        }
    }
}
