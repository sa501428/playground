using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace AuroraGame
{
	public class World
	{
		Ship ship;
		Vector2 origin, bounds, boxorigin, BoxPos;
        //Powers powers;
        List<Bullet> blasts, enembullets, xplod;
        List<Enemy> enemies;
        List<Powers> powers;
        List<int> vault;
        // powervault needed : shield, snic blasts
        List<Texture2D> epics, powrs;
        //Texture2D xplodpic;
        Random rand;
        bool fire;
        float reload, spawn, spawn2, score, spawnlim, spawn3, spawn4;
        public SoundBank soundb;
        int crazystream, curr, shieldstream;
        //Random phi;

		public Ship Ship
		{
			get { return ship; }
			set
			{
				ship = value;
				ship.Position = origin;
			}
		}

        //public Powers Powers
        //{
        //    get { return powers; }
        //    set
        //    {
        //        powers = value;
        //        powers.Position = boxorigin;
        //    }
        //}

        

		public Vector2 Origin { get { return origin; } }

        public float Score { get { return score; } }


        public void EmitFire(bool fireOn)
        {
            fire = fireOn;
        }

        public World(Vector2 o, Vector2 p, List<Texture2D> enems, List<Texture2D> pows, Vector2 org2, SoundBank sb)
		{
			origin = o;
            bounds = org2;
            boxorigin = p;
			ship = null;
            //powers = null;
            blasts = new List<Bullet>();
            enembullets = new List<Bullet>();
            enemies = new List<Enemy>();
            xplod = new List<Bullet>();
            vault = new List<int>();
            epics = enems;
            powrs = pows;
            powers = new List<Powers>();
            BoxPos = new Vector2(14 * 800 / 15, 13.5f*480 / 15);
            curr = 0;

            spawnlim = 200;
            reload = 0;
            spawn = 0;
            spawn2 = 0;
            spawn3 = 0;
            spawn4 = 0;
            rand = new Random();
            score = 0;
            soundb = sb;
            crazystream = 0;
            shieldstream = 0;
		}

        public void Crazy()
        {
            crazystream = 200;
        }

        public void Shielder()
        {
            shieldstream = 200;
        }

        public void UsePower()
        {
            if (vault.Count > 0)
            {
                if (vault[curr] == 3)
                {
                    Shielder();
                    vault.Remove(vault[curr]);
                    curr = 0;
                }
                else
                {
                    Crazy();
                    vault.Remove(vault[curr]);
                    curr = 0;
                }
            }
        }

        public void Cycle(int ii)
        {
            if (vault.Count > 0)
            {
            curr = curr + ii;
            curr = curr % vault.Count;
            if (curr < 0) curr = vault.Count + curr;
            if (curr < 0 || curr > vault.Count) curr = 0;
            }
        }

		public void Update(float elapsed)
		{
            reload++;
            spawn++;
            if (crazystream > 0) crazystream--;
            if (shieldstream > 0) shieldstream--;
			if (ship != null)
				ship.Update(elapsed);
            //if (powers != null)
            //    powers.Update(elapsed);

            
            // ship bullets
            for (int f = 0; f < blasts.Count; f++)
            {
                // Remove dead particles, update living particles
                blasts[f].Update(elapsed);

                if (blasts[f].LifeSpan < blasts[f].Lifetime)
                {
                    blasts.Remove(blasts[f]);
                    
                }
            }

            // explosion particles
            for (int ff = 0; ff < xplod.Count; ff++)
            {
                // update living particles
                xplod[ff].Update(elapsed);

                // Remove dead explosion particles
                if (xplod[ff].LifeSpan < xplod[ff].Lifetime)
                {
                    xplod.Remove(xplod[ff]);
                }
            }

            // power ups
            for (int ps = 0; ps < powers.Count; ps++)
            {
                // update living particles
                powers[ps].Update(elapsed);

                // crashing into ship
                if ((powers[ps].Position - Ship.Position).Length() < 30)
                {
                    if (powers[ps].level == 1)
                    {
                        Ship.PowerUp();
                    }
                    else if(powers[ps].level == 2)
                    {
                        Ship.SpeedUp();
                    }
                    else if (powers[ps].level == 3)
                    {
                        int num = 3;
                        vault.Add(num);
                    }
                    else
                    {
                        int num = 4;
                        vault.Add(num);
                    }
                    powers.Remove(powers[ps]);
                    break; 
                }

                // Remove dead explosion particles
                if (powers[ps].Position.Y > bounds.Y || powers[ps].LifeSpan < powers[ps].Lifetime)
                {
                    powers.Remove(powers[ps]);
                }
            }

            // enemy bullets
            for (int g = 0; g < enembullets.Count; g++)
            {
                // Remove dead particles, update living particles
                enembullets[g].Update(elapsed);

                if (enembullets[g].LifeSpan < enembullets[g].Lifetime || (enembullets[g].Position - Ship.Position).Length() < 30)
                {
                    if (enembullets[g].LifeSpan > enembullets[g].Lifetime)
                    { if(shieldstream < 1) Ship.gotHit(); }
                    enembullets.Remove(enembullets[g]);

                }

            }

            // spawning random bullets
            if (fire && reload > 5)
            {
                // Add new particles
                reload = 0;
                if (crazystream > 1)
                {
                    for (int pw = 0; pw < 1000; pw++)
                    {
                        float temptheta = rand.Next();
                        blasts.Add(new Bullet(ship.Position, -40 * new Vector2((float)Math.Cos(temptheta),
                            (float)Math.Sin(temptheta)), epics[0], -MathHelper.PiOver2, (float).05, 0, 70));
                    }
                }
                else
                {

                    for (int pw = -1 * ship.PowerLevel; pw < ship.PowerLevel + 1; pw++)
                    {
                        int tempcount = ship.PowerLevel * 2 + 1;
                        blasts.Add(new Bullet(ship.Position, -40 * new Vector2((float)Math.Cos(pw * Math.PI / tempcount + MathHelper.PiOver2),
                            (float)Math.Sin(pw * Math.PI / tempcount + MathHelper.PiOver2)), epics[0], -MathHelper.PiOver2, (float).05, 0, 70));
                    }
                    
                }
            }

            // updating and moving enemy ship
            for (int r = 0; r < enemies.Count; r++)
            {
                // shooting
                enemies[r].Update(elapsed);
                if (rand.Next(1,5) < 3 && spawn % 14 == 1)
                {
                    for (int epw = -1 * enemies[r].Level; epw < enemies[r].Level + 1; epw++)
                    {
                        int tempcount = enemies[r].Level * 2 + 1;
                        enembullets.Add(new Bullet(enemies[r].Position, 40 * new Vector2((float)Math.Cos(epw * Math.PI / tempcount + MathHelper.PiOver2),
                            (float)Math.Sin(epw * Math.PI / tempcount + MathHelper.PiOver2)), epics[5], -MathHelper.PiOver2, (float).05, 0, 70));
                    }
                    
                }

                // crashing into ship
                if ((enemies[r].Position - Ship.Position).Length() < 50)
                {
                    if(shieldstream < 1) Ship.gotHit();
                    enemies[r].hit(10);
                    
                }

                if (enemies[r].Position.Y > bounds.Y)
                {
                    enemies.Remove(enemies[r]);
        
                }
                
                // getting hit by bullets
                for (int r2 = 0; r2 < blasts.Count; r2++)
                {
                    // 
                    
                    if (r < enemies.Count && r2 < blasts.Count && (blasts[r2].Position - enemies[r].Position).Length() < 30 + 10*enemies[r].Level)
                    {
                        blasts.Remove(blasts[r2]);
                        enemies[r].hit(1);
                        
                        // death
                        if(enemies[r].Life < 1)
                        {
                            for (int xx = 0; xx < 200; xx++)
                            {
                                float theta = (float).01 * rand.Next(-300, 300); //new Vector2(rand.Next(-10, 10), rand.Next(-10, 10))
                                xplod.Add(new Bullet(enemies[r].Position, rand.Next(2, 15) * new Vector2((float)Math.Cos(theta),
                                    (float)Math.Sin(theta)), epics[6], rand.Next(3, 3), (float).005 * rand.Next(1, 5), 0, 20));
                            }
                            score += 10*(enemies[r].Level + 1);

                            int temp = (enemies[r].Level+1)* rand.Next(0, 1001);
                            int pwr = 0;
                            if (temp > 700)
                            {

                                if (temp < 850) { pwr = 2; }
                                else if (temp < 930) { pwr = 1; }
                                else if (temp < 970) { pwr = 3; }
                                else { pwr = 4; }
                                powers.Add(new Powers(enemies[r].Position, enemies[r].Velocity, powrs[pwr], -MathHelper.PiOver2, .5f, 90, pwr));
                            }
                            //int pwr = rand.Next(1, 5);
                            //powers.Add(new Powers(enemies[r].Position, enemies[r].Velocity, powrs[pwr], -MathHelper.PiOver2, .5f, 90, pwr));
                            enemies.Remove(enemies[r]);
                            soundb.PlayCue("explosion");
                            break;
                        }

                    }
                }
                
            }

            // spawning enemies
            if (spawn % (spawnlim +1) == spawnlim)
            {
                // Add new particles
                //spawn = 0;
                if (spawnlim > 100) spawnlim = spawnlim - 5;

                spawn2++;
                spawn3++;
                spawn4++;
                if (spawn2 > 5)
                {
                    enemies.Add(new Enemy(epics[8], MathHelper.PiOver2, rand.Next(1, 3),
                        new Vector2(rand.Next((int)(0.1 * bounds.X), (int)(0.9 * bounds.X)), 0), bounds,1, 5));// Color.Transparent));
                    spawn2 = 0;
                }

                if (spawn3 > 9)
                {
                    enemies.Add(new Enemy(epics[9], MathHelper.PiOver2, rand.Next(1, 3),
                        new Vector2(rand.Next((int)(0.1 * bounds.X), (int)(0.9 * bounds.X)), 0), bounds, 3, 15));// Color.Transparent));
                    spawn3 = 0;
                }

                if (spawn4 > 23)
                {
                    enemies.Add(new Enemy(epics[10], MathHelper.PiOver2, rand.Next(1, 3),
                        new Vector2(rand.Next((int)(0.1 * bounds.X), (int)(0.9 * bounds.X)), 0), bounds, 4, 20));// Color.Transparent));
                    spawn4 = 0;
                }

                enemies.Add(new Enemy(epics[7], MathHelper.PiOver2, rand.Next(1,3),
                    new Vector2(rand.Next((int)(0.1*bounds.X), (int)(0.9*bounds.X)), 0), bounds,0,2));
            }
		}

		public void Draw(SpriteBatch batch)
		{
			
            foreach (Bullet p in blasts)
            {
                p.Draw(batch);
            }

            foreach (Bullet pp in xplod)
            {
                pp.Draw(batch);
            }

            foreach (Bullet r in enembullets)
            {
                r.Draw(batch);
            }

            foreach (Enemy q in enemies)
            {
                q.Draw(batch);
            }

            foreach (Powers pw in powers)
            {
                pw.Draw(batch);
            }

            batch.Draw(powrs[0], BoxPos, null, Color.White, 0, new Vector2(powrs[0].Width, powrs[0].Height) / 2,.4f,SpriteEffects.None,0);
            if (vault.Count > 0)
            {
                batch.Draw(powrs[vault[curr]], BoxPos, null, Color.White, -MathHelper.PiOver2, new Vector2(powrs[vault[curr]].Width,
                    powrs[vault[curr]].Height) / 2, .25f, SpriteEffects.None, 0);
            }

            if (shieldstream > 0)
            {
                batch.Draw(powrs[3], Ship.Position, null, Color.White, -MathHelper.PiOver2, new Vector2(powrs[3].Width,
                    powrs[3].Height) / 2, .5f, SpriteEffects.None, 0);
            }
            if (ship != null)
                ship.Draw(batch);
            
		}
	}
}
