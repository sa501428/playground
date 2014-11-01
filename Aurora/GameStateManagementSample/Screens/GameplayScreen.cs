#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using AuroraManagement;
#endregion


namespace AuroraGame
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class GameplayScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        float pauseAlpha;
        InputAction pauseAction;

        //GraphicsDeviceManager graphics;
        //SpriteBatch spriteBatch;
        //Ship ship;
        World world;
        List<Texture2D> picslist, shiplist, powrlist;
        List<string> scorelist;
        List<int> highs;
        //ButtonState prev;

        //Emitter emitter;
        GamePadState[] states;
        KeyboardState[] statesK;
        //SpriteFont Font1;
        Vector2 FontPos;
        Vector2 Pos2;
        Vector2 Pos3;
        Texture2D lifepic;
        //string output;
        Texture2D terr;
        Rectangle rect;
        int count;
        Rectangle sourcerect;
        SpriteFont mfont;
        string line, myname;
        int lives2, scorewrite;
        


        public AudioEngine engine;
        public SoundBank soundBank;
        public WaveBank waveBank;
        Song song1;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            pauseAction = new InputAction(
                new Buttons[] { Buttons.Start, Buttons.Back },
                new Keys[] { Keys.Escape },
                true);
            states = new GamePadState[2];
            statesK = new KeyboardState[2];
            


        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {
                if (content == null)
                    content = new ContentManager(ScreenManager.Game.Services, "Content");
                Viewport viewport = ScreenManager.GraphicsDevice.Viewport;

                song1 = content.Load<Song>("bks");
                MediaPlayer.Play(song1);
                MediaPlayer.IsRepeating = true;
                // A real game would probably have more content than this sample, so
                // it would take longer to load. We simulate that by delaying for a
                // while, giving you a chance to admire the beautiful loading screen.

                count = 1500;//1948;
                terr = content.Load<Texture2D>("Terrain1");
                lifepic = content.Load<Texture2D>("Aurora");
                rect = new Rectangle(0, 0, 800, 480);//GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
                //lrect = new Rectangle(0, 0, (int)1.5 * 14 * 800 / 15, (int)1.5 * 14 * 480/15);//GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
                sourcerect = new Rectangle(0, 900, 200, 100);
                mfont = content.Load<SpriteFont>("menufont");
                FontPos = new Vector2(14 * 800 / 15, 480 / 15);
                Pos2 = new Vector2(800 / 30, 2* 480 / 15);
                Pos3 = new Vector2((float)1.5*800 / 15, (float)1.1*480 / 15);

                engine = new AudioEngine(@"Content\Audio\SoundTextGame.xgs");
                soundBank = new SoundBank(engine, @"Content\Audio\Sound Bank.xsb");
                waveBank = new WaveBank(engine, @"Content\Audio\Wave Bank.xwb");

                //FileStream fs = new FileStream("U:\\test.txt", FileMode.Create);
                scorelist = new List<string>();
                highs = new List<int>();
                //int iter = 0;
                scorewrite = 0;
                string path = Directory.GetCurrentDirectory();
                using (StreamReader sr = new StreamReader(path+"\\test.txt"))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        scorelist.Add(line);
                    }
                    for (int j = 0; j < 3; j++)
                        highs.Add(Convert.ToInt32(scorelist[2 * j], 10));
                }

                
                //fs.Close(); 



                //soundBank.PlayCue("laser_fire");
                
                //lives = OptionsMenuScreen. .Lives;
                lives2 = OptionsMenuScreen.lives;
                myname = OptionsMenuScreen.l1 + "" + OptionsMenuScreen.l2 + OptionsMenuScreen.l3;
                picslist = new List<Texture2D>();
                picslist.Add(content.Load<Texture2D>("1"));
                picslist.Add(content.Load<Texture2D>("2"));
                picslist.Add(content.Load<Texture2D>("3"));
                picslist.Add(content.Load<Texture2D>("4"));
                picslist.Add(content.Load<Texture2D>("5"));
                picslist.Add(content.Load<Texture2D>("6"));
                picslist.Add(content.Load<Texture2D>("7")); 
                picslist.Add(content.Load<Texture2D>("e1"));
                picslist.Add(content.Load<Texture2D>("e2"));
                picslist.Add(content.Load<Texture2D>("e3"));
                picslist.Add(content.Load<Texture2D>("e4"));


                shiplist = new List<Texture2D>();
                shiplist.Add(content.Load<Texture2D>("Aurora"));
                shiplist.Add(content.Load<Texture2D>("TurnR"));
                shiplist.Add(content.Load<Texture2D>("SteepR"));
                shiplist.Add(content.Load<Texture2D>("TurnL"));
                shiplist.Add(content.Load<Texture2D>("SteepL"));
                shiplist.Add(content.Load<Texture2D>("Mist"));


                powrlist = new List<Texture2D>();
                powrlist.Add(content.Load<Texture2D>("PowerBox"));
                powrlist.Add(content.Load<Texture2D>("pweapon"));
                powrlist.Add(content.Load<Texture2D>("pspeed"));
                powrlist.Add(content.Load<Texture2D>("pshield"));
                powrlist.Add(content.Load<Texture2D>("pwave"));

                world = new World(new Vector2(0.5f * 800,0.5f * 480),
                    new Vector2(0.94f * 800,0.9f * 480),
                    picslist, powrlist,
                    new Vector2(800,480), soundBank);

                world.Ship = new Ship(shiplist, new Vector2(800,480));
                
                Thread.Sleep(1500);

                // once the load has finished, we use ResetElapsedTime to tell the game's
                // timing mechanism that we have just finished a very long frame, and that
                // it should not try to catch up.
                ScreenManager.Game.ResetElapsedTime();
                //SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
                //prev = ButtonState.Released;
                //prevState = GamePad.GetState(PlayerIndex.One);
                //base.Initialize();

            }

#if WINDOWS_PHONE
            if (Microsoft.Phone.Shell.PhoneApplicationService.Current.State.ContainsKey("PlayerPosition"))
            {
                playerPosition = (Vector2)Microsoft.Phone.Shell.PhoneApplicationService.Current.State["PlayerPosition"];
                enemyPosition = (Vector2)Microsoft.Phone.Shell.PhoneApplicationService.Current.State["EnemyPosition"];
            }
#endif
        }


        public override void Deactivate()
        {
#if WINDOWS_PHONE
            Microsoft.Phone.Shell.PhoneApplicationService.Current.State["PlayerPosition"] = playerPosition;
            Microsoft.Phone.Shell.PhoneApplicationService.Current.State["EnemyPosition"] = enemyPosition;
#endif

            base.Deactivate();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>

        
        
        public override void Unload()
        {
            content.Unload();

#if WINDOWS_PHONE
            Microsoft.Phone.Shell.PhoneApplicationService.Current.State.Remove("PlayerPosition");
            Microsoft.Phone.Shell.PhoneApplicationService.Current.State.Remove("EnemyPosition");
#endif
        }


        #endregion

        #region Update and Draw

        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
            
            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive && (lives2 - (int)world.Ship.Death) > 0)
            {

                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;


                // updating background map atlasing
                count = (1395 + count - 1) % 1395;
                sourcerect = new Rectangle(0, count, 300, 200);

#if XBOX
                // We want to know the difference between a button initially being pressed, and a button being held
                states[1] = states[0];                         // Previous state of the game pad
                states[0] = GamePad.GetState(PlayerIndex.One); // Current state of the game pad

                GamePadState currState = states[0];
                GamePadState prevState = states[1];

                world.Ship.Accelerate(currState.ThumbSticks.Left.X, currState.ThumbSticks.Left.Y, elapsed);

                    // When A is pressed, start emitting fire
                    if ((currState.Buttons.A == ButtonState.Pressed && prevState.Buttons.A == ButtonState.Released)) // B pressed
                    {
                        world.EmitFire(true);
                    }
            
                    // When A is released, stop emitting fire
                    if (currState.Buttons.A == ButtonState.Released && prevState.Buttons.A == ButtonState.Pressed) // B released
                    {
                        world.EmitFire(false);
                    }
            
                    if ((currState.Triggers.Left > .5 && prevState.Triggers.Left < .5)) // B pressed
                    {
                        world.Cycle(-1);
                    }
            

                    if ((currState.Triggers.Right > .5 && prevState.Triggers.Right < .5)) // B pressed
                    {
                        world.Cycle(1);
                    }
                
                    if ((currState.Buttons.X == ButtonState.Pressed && prevState.Buttons.X == ButtonState.Released)) // B pressed
                    {
                        world.UsePower();
                    }
            
                    if ((currState.Buttons.LeftShoulder == ButtonState.Pressed && prevState.Buttons.LeftShoulder == ButtonState.Released)) // B pressed
                    {
                        world.Shielder();
                    }

#endif


#if WINDOWS
                statesK[1] = statesK[0];                         // Previous state of the game pad
                statesK[0] = Keyboard.GetState(PlayerIndex.One); // Current state of the game pad
                
                KeyboardState currState = statesK[0];
                KeyboardState prevState = statesK[1];

                int dy = 0, dx = 0;

                if((currState.IsKeyDown(Keys.Up))){
                    dy = 1;
                }
                else if((currState.IsKeyDown(Keys.Down))){
                    dy = -1;
                }

                if((currState.IsKeyDown(Keys.Right))){
                    dx = 1;
                }
                else if((currState.IsKeyDown(Keys.Left))){
                    dx = -1;
                }

                world.Ship.Accelerate(dx, dy, elapsed);

                // When A is pressed, start emitting fire
                if ((currState.IsKeyDown(Keys.A) && prevState.IsKeyUp(Keys.A)))
                {
                    world.EmitFire(true);
                }

                // When A is released, stop emitting fire
                if ((currState.IsKeyUp(Keys.A) && prevState.IsKeyDown(Keys.A)))
                {
                    world.EmitFire(false);
                }

                if ((currState.IsKeyUp(Keys.W) && prevState.IsKeyDown(Keys.E))) 
                {
                    world.Cycle(-1);
                }


                if ((currState.IsKeyUp(Keys.E) && prevState.IsKeyDown(Keys.W))) 
                {
                    world.Cycle(1);
                }

                if ((currState.IsKeyDown(Keys.S) && prevState.IsKeyUp(Keys.S)))
                {
                    world.UsePower();
                }

                if ((currState.IsKeyDown(Keys.D) && prevState.IsKeyUp(Keys.D)))
                {
                    world.Shielder();
                }
#endif
                    
            }
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            PlayerIndex player;
            if (pauseAction.Evaluate(input, ControllingPlayer, out player) || gamePadDisconnected)
            {
#if WINDOWS_PHONE
                ScreenManager.AddScreen(new PhonePauseScreen(), ControllingPlayer);
#else
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
#endif
            }
            else if ((lives2 - (int)world.Ship.Death) < 1)
            {
                if (scorewrite == 0)
                {
                    string path = Directory.GetCurrentDirectory();
                    using (StreamWriter sw = new StreamWriter(path+"\\test.txt"))
                    {
                        int currscore = (int)world.Score;
                        if (currscore > highs[0])
                        {
                            sw.WriteLine(currscore.ToString());
                            sw.WriteLine(myname);
                            sw.WriteLine(scorelist[0]);
                            sw.WriteLine(scorelist[1]);
                            sw.WriteLine(scorelist[2]);
                            sw.WriteLine(scorelist[3]);
                        }
                        else if (currscore > highs[1])
                        {
                            
                            sw.WriteLine(scorelist[0]);
                            sw.WriteLine(scorelist[1]);
                            sw.WriteLine(currscore.ToString());
                            sw.WriteLine(myname);
                            sw.WriteLine(scorelist[2]);
                            sw.WriteLine(scorelist[3]);
                        }
                        else if (currscore > highs[2])
                        {
                            
                            sw.WriteLine(scorelist[0]);
                            sw.WriteLine(scorelist[1]);
                            sw.WriteLine(scorelist[2]);
                            sw.WriteLine(scorelist[3]);
                            sw.WriteLine(currscore.ToString());
                            sw.WriteLine(myname);
                        }
                        else
                        {
                            sw.WriteLine(scorelist[0]);
                            sw.WriteLine(scorelist[1]);
                            sw.WriteLine(scorelist[2]);
                            sw.WriteLine(scorelist[3]);
                            sw.WriteLine(scorelist[4]);
                            sw.WriteLine(scorelist[5]);
                        }

                    }
                    scorewrite = 1;
                }
                ScreenManager.AddScreen(new EndGameScreen(), ControllingPlayer);

            }
            else
            {
                // Otherwise move the player position.
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                //if (myship != null)
                //    myship.Update(elapsed);

                if ((lives2 - (int)world.Ship.Death) > 0)
                {
                    world.Update(elapsed);
                }
                
            }
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.Transparent);
            
            
            // TODO: Add your drawing code here
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();
            spriteBatch.Draw(terr, rect, sourcerect, Color.White);

            world.Draw(spriteBatch);

            string output = world.Score.ToString();
            // Find the center of the string
            Vector2 FontOrigin = mfont.MeasureString(output) / 2;
                // Draw the string
            spriteBatch.DrawString(mfont, output, FontPos, Color.LimeGreen,
                0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);

            int temp = lives2-(int)world.Ship.Death;
            string ls = "FAIL";

            if (temp < 1)
            {
                temp = 0;
                FontOrigin = mfont.MeasureString(ls) / 2;
                spriteBatch.DrawString(mfont, ls, new Vector2(400, 240), Color.Red, 0, FontOrigin, 4.0f, SpriteEffects.None, 0.5f); ;
            }

            ls = temp.ToString();
            FontOrigin = mfont.MeasureString(ls) / 2;
            spriteBatch.DrawString(mfont, ls, Pos3, Color.LimeGreen,
                0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);

            spriteBatch.Draw(lifepic, Pos2, null, Color.LimeGreen, MathHelper.PiOver2, new Vector2(400, 240),.15f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(lifepic, Pos3, null, Color.LimeGreen, MathHelper.PiOver2, new Vector2(400,240), .8, SpriteEffects.None, 0f);

            

            spriteBatch.End();
        }


        #endregion
    }
}
