using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Canada_Hunts
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        Random generator;
        List<Rectangle> gooserectangle;

        List<Texture2D> goosetextures;
        Random random = new Random();
        Rectangle window;
        Texture2D canadabg,gooseTexture;


        float seconds;
        float respawntime;
        MouseState mousestate, previousMouseState;



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            window = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            generator = new Random();
           
            gooserectangle = new List<Rectangle>();
            goosetextures = new List<Texture2D>();
            for (int i = 0; i < 5; i++)
            {
                gooserectangle.Add(new Rectangle(
                    generator.Next(0, window.Width - 50),
                    generator.Next(0, 130 ),
                    50,
                    50
                    ));


            }

            seconds = 0f;
            respawntime = 3f;

            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            canadabg = Content.Load<Texture2D>("Images/canadabg");
            gooseTexture = Content.Load<Texture2D>("Images/goose");


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mousestate = Mouse.GetState();


            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (seconds >= respawntime)
            {
                gooserectangle.Add(new Rectangle(
                     generator.Next(0, window.Width - 50),
                     generator.Next(0, 130),
                     50,
                     50
                     ));

                seconds = 0f;

            }
            if (mousestate.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton != ButtonState.Pressed)
            {
                Point mousepoint = new Point(mousestate.X, mousestate.Y);
                for (int i = gooserectangle.Count - 1; i >= 0; i--)
                {
                    if (gooserectangle[i].Contains(mousepoint))
                    {
                        gooserectangle.RemoveAt(i);
                    }
                }


            }

            previousMouseState = mousestate;


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();


            _spriteBatch.Draw(canadabg, window, Color.White);
            for (int i = 0; i < gooserectangle.Count; i++)
            {
                _spriteBatch.Draw(gooseTexture, gooserectangle[i], Color.White);

            }


           

            // TODO: Add your drawing code here

            base.Draw(gameTime);
            _spriteBatch.End();
        }
    }
}
