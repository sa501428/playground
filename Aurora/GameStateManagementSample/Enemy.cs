using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AuroraGame
{
	public class Enemy
	{
        
		// Radians per second the ship rotates
		const float turnSpeed = MathHelper.Pi;

		Texture2D img;
        //Texture2D img2;

   //     Texture2D blastpic;
 //       Texture2D img3;
		Vector2 worldBounds;
		Vector2 pos;
		Vector2 orig;
        Vector2 speed;
		float scale, angle;
        int level;
        bool alive;
        int life;
        //List<Bullet> explosion;

		public Vector2 Position
		{
			get { return pos; }
			set
			{
				Vector2 npos = value;
//				if (npos.X >= worldBounds.X)
 //                   npos.X = worldBounds.X;
  //              if(npos.X <= 0)
   //                 npos.X = 0;
    //            if (npos.Y >= worldBounds.Y)
     //               npos.Y = worldBounds.Y;
      //          if (npos.Y <= 0)
       //             npos.Y = 0;
				pos = npos;
			}
		}

        public Vector2 Velocity
        {
            get { return speed; }
            set
            {
                ;
            }
        }

        public int Level
        {
            get { return level; }
            set
            {
                ;
            }
        }

        public int Life
        {
            get { return life; }
            set
            {
                ;
            }
        }


        public Enemy(Texture2D img, float ratio, float sped, Vector2 posh, Vector2 b, int lv, int lf)
        {
            this.img = img;
     //       this.img2 = img;
       //     this.img3 = img;
         //   this.blastpic = img;
            pos = posh;
            worldBounds = b;
            orig = new Vector2(img.Width, img.Height) / 2;
            scale = 80f / (float)img.Width;
            speed = new Vector2((float)Math.Cos((double)ratio), (float)Math.Sin((double)ratio)) * sped;
            angle = ratio;
            alive = true;
            level = lv;
            life = lf;
		}

        public void hit(int d)
        {
            life = life - d;
        }
        
		public void Update(float elapsed)
		{
			// Update the ship's position
            pos += speed;

            if (pos.X > 1.1*worldBounds.X)
            {
                pos.X = (float) 1.1 * worldBounds.X;
            }
            if (pos.X < -0.1 * worldBounds.X)
            {
                pos.X = (float) -0.1 * worldBounds.X;
            }
            if (pos.Y > 1.1*worldBounds.Y)
            {
                pos.Y = (float)1.1*worldBounds.Y;
            }
            if (pos.Y < -0.1 * worldBounds.Y)
            {
                pos.Y = (float) -0.1 * worldBounds.Y;
            }

            if (!alive)
            {
                scale = (float).99 * scale;
            }

		}

		public void Draw(SpriteBatch batch)
		{
            // You must draw your shield!
            batch.Draw(img, pos, null, Color.White, angle, orig, scale, SpriteEffects.None, 0f);
 		}
	}
}
