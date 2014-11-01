using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AuroraGame
{
	public class Ship
	{
		// Radians per second the ship rotates
		const float turnSpeed = MathHelper.Pi;

		List<Texture2D> img;
        Vector2 worldBounds;
		Vector2 pos;
		Vector2 orig;
        Vector2 speed;
		float scale;
        float angle;
        float death;
        //bool shielding;
        //bool lazer;
        Vector2 lpos;
        float boost;        // speed powerup
        float hitnum;       // color change, bullet hit
        float xval;         // for plane tilting
        int powerlevel, altern;


		public Vector2 Position
		{
			get { return pos; }
			set
			{
				Vector2 npos = value;
				if (npos.X >= worldBounds.X)
                    npos.X = worldBounds.X;
                if(npos.X <= 0)
                    npos.X = 0;
                if (npos.Y >= worldBounds.Y)
                    npos.Y = worldBounds.Y;
                if (npos.Y <= 0)
                    npos.Y = 0;
				pos = npos;
			}
		}

        public float Death
        {
            get { return death; }
            set { }
        }

        public int PowerLevel
        {
            get { return powerlevel; }
            set { }
        }

        public Vector2 DPosition
        {
            get { return 800 * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)); }
            set
            {
                Vector2 npos = value;
                if (npos.X >= worldBounds.X || npos.X <= 0)
                    npos.X = pos.X % worldBounds.X;
                if (npos.Y >= worldBounds.Y || npos.Y <= 0)
                    npos.Y = pos.Y % worldBounds.Y;
                lpos = npos;
            }
        }

        public Vector2 D2Position
        {
            get { return pos + 800 * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)); }
            set
            {
                Vector2 npos = value;
                if (npos.X >= worldBounds.X || npos.X <= 0)
                    npos.X = pos.X % worldBounds.X;
                if (npos.Y >= worldBounds.Y || npos.Y <= 0)
                    npos.Y = pos.Y % worldBounds.Y;
                lpos = npos;
            }
        }

        public Ship(List<Texture2D> imgs, Vector2 b)
        {
            img = imgs;
            worldBounds = b;
			orig = new Vector2(img[0].Width, img[0].Height) / 2;
			scale = 80f / (float)img[0].Width;
            speed = new Vector2(0,0);
            boost = 1;
            angle = -MathHelper.PiOver2;
            hitnum = 1;
            death = 0;
            powerlevel = 0;
            altern = 0;
		}

        public void Accelerate(float amountX, float amountY, float elapsed)
		{
			// Alter the ship's speed
            speed = new Vector2(amountX, -amountY)*elapsed*200*boost;
            xval = amountX;
		}

        public void PowerUp()
        {
            // Alter the ship's speed
            powerlevel++;
        }

        public void SpeedUp()
        {
            // Alter the ship's speed
            boost = boost*1.02f;
        }

        public void gotHit()
        {
            // Alter the ship
            if (hitnum == 1)
            {
                hitnum = 60;
                death += 1;
            }
        }

        //public void Shield(bool shieldOn)
        //{
            // Toggle your shield on or off based on the shieldOn parameter
         //   this.shielding = shieldOn;

        //}

        //public void Firing(bool lazer)
        //{
            // Toggle your shield on or off based on the shieldOn parameter
        //}
        
		//public void Turn(Vector2 thumbstick, float elapsed)
		//{
		//	angle += turnSpeed * thumbstick.X * elapsed;
		//	// Feel free to change how the turning works. For example, using Math.Atan2
		//}

        public void Update(float elapsed)
        {
            // Update the ship's position


            pos += speed;

            if (pos.X > worldBounds.X)
            {
                pos.X = worldBounds.X;
            }
            if (pos.X < 0)
            {
                pos.X = 0;
            }
            if (pos.Y > worldBounds.Y)
            {
                pos.Y = worldBounds.Y;
            }
            if (pos.Y < 0)
            {
                pos.Y = 0;
            }
        }

		public void Draw(SpriteBatch batch)
		{
            Color temp = Color.White;
            // You must draw your shield!
            if (altern == 0)
            {
                altern = 1;
            }
            else
            {
                altern = 0;
            }

            if (hitnum > 1 && altern == 0)
            {
                temp = Color.Crimson;
                //batch.Draw(img, pos, null, Color.Navy, angle, orig, (float)1.5*scale, SpriteEffects.None, 0f);
                hitnum -= 1;
            }
                if (xval > .6)
                {
                    batch.Draw(img[2], pos, null, temp, angle, orig, scale, SpriteEffects.None, 0f);
                }
                else if (xval > .1)
                {
                    batch.Draw(img[1], pos, null, temp, angle, orig, scale, SpriteEffects.None, 0f);
                }
                else if (xval < -.6)
                {
                    batch.Draw(img[4], pos, null, temp, angle, orig, scale, SpriteEffects.None, 0f);
                }
                else if (xval < -.1)
                {
                    batch.Draw(img[3], pos, null, temp, angle, orig, scale, SpriteEffects.None, 0f);
                }
                else
                {
                    batch.Draw(img[0], pos, null, temp, angle, orig, scale, SpriteEffects.None, 0f);
                }
            
          //  if (shielding)
          //      batch.Draw(img2, pos, null, Color.White, angle, orig, scale, SpriteEffects.None, 0f);
          //  if (lazer)
          //      batch.Draw(img3, pos, null, Color.White, angle, orig, scale, SpriteEffects.None, 0f);

 		}
	}
}
