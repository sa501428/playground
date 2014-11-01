using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AuroraGame
{
    public class Bullet
    {
        /// <summary>
        /// Position of the particle
        /// </summary>
        public Vector2 Position;
        /// <summary>
        /// Velocity of the particle
        /// </summary>
        public Vector2 Velocity;
        /// <summary>
        /// Color of the particle
        /// </summary>
        //public Color Color;
        /// <summary>
        /// Texture of the particle
        /// </summary>
        public Texture2D Texture;
        /// <summary>
        /// Image scale of the particle
        /// </summary>
        public float Scale;
        /// <summary>
        /// Image rotation of the particle. Do *NOT* use this as an angle theta for motion!
        /// </summary>
        public float Rotation;
        /// <summary>
        /// Maximum time (in seconds) this particle may remain alive
        /// </summary>
        public float LifeSpan;
        /// <summary>
        /// Time (in seconds) this particle has been alive
        /// </summary>
        public float Lifetime;
        public float FriendOrFoe;

        public Bullet(Vector2 pos, Vector2 vel, Texture2D tex, float rot, float scale, float status,float life)//, Color col)
        {
            //Random numGen = new Random();
            this.Position = pos;
            this.Velocity = vel;
            //this.Color = col;
            this.Texture = tex;
            this.Scale = (float) (scale * 0.5);
            this.Rotation = rot;
            this.LifeSpan = life;
            this.Lifetime = 0;
            this.FriendOrFoe = status;
        }

        public void Update(float elapsed)
        {
            Lifetime += 1;
            Position += 10 * Velocity * elapsed;
        }



        public void Draw(SpriteBatch sbatch)
        {
            sbatch.Draw(Texture, Position, null, Color.White, Rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects.None, 0);
        }
    }
}
