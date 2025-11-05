using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace Canada_Hunts
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D scopeTexture;
        private Vector2 mousePosition;
        Vector2 goosespeed= new Vector2 (-4,0);

        Vector2 moosespeed= new Vector2 (2,0);

        SoundEffect gunsound;
        SpriteFont font;

        Double timer = 25;

        int score = 0; 

        bool end = false;
        bool win = false;





        Random generator;
        List<Rectangle> gooserectangle;

        List<Rectangle> mooserectangle;
        List<Int32> moosehealth;

        List<Texture2D> moosetextures;



        List<Texture2D> goosetextures;
        Random random = new Random();
        Rectangle window;
        Texture2D canadabg, gooseTexture, mooseTexture, gameover, winscreen;


        float seconds;
        float respawntime;
        MouseState mousestate, previousMouseState;



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;

            

        }



      

        protected override void Initialize()
        {
            window = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            generator = new Random();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            scopeTexture = Content.Load<Texture2D>("images/scope");

            gooserectangle = new List<Rectangle>();
            goosetextures = new List<Texture2D>();
            mooserectangle = new List<Rectangle>();
            moosetextures = new List<Texture2D>();
            moosehealth = new List<Int32>();
            
            for (int i = 0; i < 5; i++)
            {
                gooserectangle.Add(new Rectangle(
                    generator.Next(0, window.Width - 50),
                    generator.Next(0, 130 ),
                    50,
                    50
                    ));


            }
            for (int i = 0; i < 5; i++)
            {
                mooserectangle.Add(new Rectangle(
                    generator.Next(0, window.Width - 50),
                    325,   
                    80,
                    80
                    ));

                moosehealth.Add(2);


            }


            seconds = 0f;
            respawntime = 2f;

            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            canadabg = Content.Load<Texture2D>("Images/canadabg");
            gooseTexture = Content.Load<Texture2D>("Images/goose");
            mooseTexture = Content.Load<Texture2D>("Images/moose");
            gunsound = Content.Load<SoundEffect>("Sounds/gunshot");
             font = Content.Load<SpriteFont>("fonts/font");
            gameover = Content.Load<Texture2D>("Images/gameover");
            winscreen = Content.Load<Texture2D>("Images/winscreen");







            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            var mouseState = Mouse.GetState();
            mousePosition = new Vector2(mouseState.X, mouseState.Y);

            this.Window.Title = "MooseCount " + mooserectangle.Count + " MooseHealthcount " + moosehealth.Count;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mousestate = Mouse.GetState();


            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (seconds >= respawntime)
            {
                for (int i = 1; i <= 5; i++)
                gooserectangle.Add(new Rectangle(
                     generator.Next(window.Width, window.Width +150),
                     generator.Next(0, 130),
                     50,
                     50
                     ));

                for (int i = 1; i <= 1; i++)
                {
                    mooserectangle.Add(new Rectangle(
                         generator.Next(-2000, -50),
                         325,

                         80,
                         80
                         ));


                    moosehealth.Add(2);
                }
                    

                seconds = 0f;

            }
            if (mousestate.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton != ButtonState.Pressed)
            {
                gunsound.Play();
                Point mousepoint = new Point(mousestate.X, mousestate.Y);

               
                for (int i = gooserectangle.Count - 1; i >= 0; i--)
                {
                    if (gooserectangle[i].Contains(mousepoint))
                    {
                        gooserectangle.RemoveAt(i);
                        score += 1;
                    }
                }



               
                for (int i = 0; i < mooserectangle.Count ; i++)
                {
                    if (mooserectangle[i].Contains(mousepoint))
                    {
                        moosehealth[i] -= 1;

                        if (moosehealth[i] == 0)
                        {
                            mooserectangle.RemoveAt(i);
                            moosehealth.RemoveAt(i);
                            i--;
                            score += 2;
                        }
                        
                    }
                    

                }
                
                if (score>= 60)
                {
                    win = true;
                    seconds = 0f;
                }




            }

            Rectangle temp;
            for (int i = 0; i < gooserectangle.Count; i++)
            {
                temp = gooserectangle[i];
                temp.X += (int)goosespeed.X;
                gooserectangle[i] = temp;
            }
            for (int i = 0; i < mooserectangle.Count; i++)
            {
                temp = mooserectangle[i];
                temp.X += (int)moosespeed.X;
                mooserectangle[i] = temp;
            }

            previousMouseState = mousestate;

            
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                if (timer<0)

            {
                end = true;

            }






            // TODO: Add your update logic here

            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            _spriteBatch.Draw(canadabg, window, Color.White);

            _spriteBatch.Draw(
                scopeTexture,
                mousePosition - new Vector2(scopeTexture.Width / 2, scopeTexture.Height / 2),
                Color.White
            );

            for (int i = 0; i < gooserectangle.Count; i++)
            {
                _spriteBatch.Draw(gooseTexture, gooserectangle[i], Color.White);
            }
            for (int i = 0; i < mooserectangle.Count; i++)
            {
                _spriteBatch.Draw(mooseTexture, mooserectangle[i], Color.White);
            }

            if (end)
            {
                _spriteBatch.Draw(gameover, window, Color.White);
            }

            if (win)
            {
               
                _spriteBatch.Draw(winscreen, window, Color.White);

               
            }
            else
            {
                
                string timertext = "Time Left: " + Math.Round(timer).ToString();
                string scoretext = $"score: {score}";
                _spriteBatch.DrawString(font, timertext, new Vector2(10, 10), Color.White);
                _spriteBatch.DrawString(font, scoretext, new Vector2(10, 40), Color.White);
                _spriteBatch.DrawString(font, "Shoot 60 animals to win!", new Vector2(300, 10), Color.White);
            }

            // TODO: Add your drawing code here

            base.Draw(gameTime);
            _spriteBatch.End();
        }
    }
}
