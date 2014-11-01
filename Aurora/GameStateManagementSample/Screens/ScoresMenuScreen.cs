#region File Description
//-----------------------------------------------------------------------------
// OptionsMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.IO;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace AuroraGame
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    class ScoresMenuScreen : MenuScreen
    {
        #region Fields

        MenuEntry s1Entry, s2Entry, s3Entry;
        List<string> scorelist;
        string line;
        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public ScoresMenuScreen()
            : base("High Scores")
        {

            scorelist = new List<string>();
            string path = Directory.GetCurrentDirectory();
            using (StreamReader sr = new StreamReader(path+"\\test.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    scorelist.Add(line);
                }
            }

            // Create our menu entries.
            s1Entry = new MenuEntry(string.Empty);
            s2Entry = new MenuEntry(string.Empty);
            s3Entry = new MenuEntry(string.Empty);
            
            SetMenuEntryText();

            MenuEntry back = new MenuEntry("Back");

            // Hook up menu event handlers.
            back.Selected += OnCancel;
            
            // Add entries to the menu.
            
            MenuEntries.Add(s1Entry);
            MenuEntries.Add(s2Entry);
            MenuEntries.Add(s3Entry);
            MenuEntries.Add(back);
        }

        //public float Lives
        //{
        //    get { return (float)lives; }
        //    set { ;}
        //}

        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        
        {
            s1Entry.Text = scorelist[1] + " " + scorelist[0];
            s2Entry.Text = scorelist[3] + " " + scorelist[2];
            s3Entry.Text = scorelist[5] + " " + scorelist[4];
        }

        
        #endregion

        #region Handle Input



        #endregion
    }
}
