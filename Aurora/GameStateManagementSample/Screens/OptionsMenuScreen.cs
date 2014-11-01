#region File Description
//-----------------------------------------------------------------------------
// OptionsMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
#endregion

namespace AuroraGame
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    class OptionsMenuScreen : MenuScreen
    {
        #region Fields

        MenuEntry livesEntry, nameEntry, name1Entry, name2Entry, name3Entry;

        public enum stuff
        {
            A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,
        }

       // static string[] languages = { "CIA", "NSA", "FBI", "" };

        public static stuff l1 = stuff.A;
        public static stuff l2 = stuff.A;
        public static stuff l3 = stuff.A;

        public static int lives = 3;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsMenuScreen()
            : base("Options")
        {
            // Create our menu entries.
            nameEntry = new MenuEntry(string.Empty);
            name1Entry = new MenuEntry(string.Empty);
            name2Entry = new MenuEntry(string.Empty);
            name3Entry = new MenuEntry(string.Empty);
            livesEntry = new MenuEntry(string.Empty);
            

            SetMenuEntryText();

            MenuEntry back = new MenuEntry("Back");

            // Hook up menu event handlers.
            livesEntry.Selected += livesEntrySelected;
            back.Selected += OnCancel;
            
            name1Entry.Selected += name1EntrySelected;
            name2Entry.Selected += name2EntrySelected;
            name3Entry.Selected += name3EntrySelected;
            
            // Add entries to the menu.
            
            MenuEntries.Add(nameEntry);
            MenuEntries.Add(name1Entry);
            MenuEntries.Add(name2Entry);
            MenuEntries.Add(name3Entry);
            MenuEntries.Add(livesEntry);
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
            nameEntry.Text = "Agent Name: " + l1 + l2 + l3;
            name1Entry.Text = "First Letter";
            name2Entry.Text = "Second Letter";
            name3Entry.Text = "Third Letter";
            livesEntry.Text = "Lives: " + lives;
        }

        void name1EntrySelected(object sender, PlayerIndexEventArgs e)
        {
            l1++;

            if (l1 > stuff.Z)
                l1 = 0;

            SetMenuEntryText();
        }

        void name2EntrySelected(object sender, PlayerIndexEventArgs e)
        {
            l2++;

            if (l2 > stuff.Z)
                l2 = 0;

            SetMenuEntryText();
        }

        void name3EntrySelected(object sender, PlayerIndexEventArgs e)
        {
            l3++;

            if (l3 > stuff.Z)
                l3 = 0;

            SetMenuEntryText();
        }

        #endregion

        #region Handle Input


        
        /// <summary>
        /// Event handler for when the Elf menu entry is selected.
        /// </summary>
        void livesEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            lives++;

            if (lives > 8) lives = 3;
            
            SetMenuEntryText();
        }


        #endregion
    }
}
