using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AuroraGame
{
    public class Powers
    {
        // Position of the particle
        public Vector2 Position;
        // Velocity of the particle
        public Vector2 Velocity;
        // Texture of the particle
        public Texture2D Texture;
        // Image scale of the particle
        public float Scale;
        // Image rotation of the particle. Do *NOT* use this as an angle theta for motion!
        public float Rotation;
        // Maximum time (in seconds) this particle may remain alive
        public float LifeSpan;
        // Time (in seconds) this particle has been alive
        public float Lifetime;

        public int level;

        public Powers(Vector2 pos, Vector2 vel, Texture2D tex, float rot, float scale,float life, int lv)//, Color col)
        {
            //Random numGen = new Random();
            Position = pos;
            Velocity = vel;
            //this.Color = col;
            Texture = tex;
            Scale = (float) (scale * 0.5);
            Rotation = rot;
            LifeSpan = life;
            Lifetime = 0;
            //this.FriendOrFoe = status;
            level = lv;
        }

        public void Update(float elapsed)
        {
            // Update the ship's position
            Lifetime += 1;
            Position += 40 * Velocity * elapsed;
        }



        public void Draw(SpriteBatch sbatch)
        {
            sbatch.Draw(Texture, Position, null, Color.White, Rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects.None, 0);
        }
    }
}
