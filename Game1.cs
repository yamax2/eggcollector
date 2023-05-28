using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace EggCollector
{
    public class EggCollectorGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Texture2D rabbitTexture;
        private Vector2 rabbitPosition;
        private int rabbitWidth;
        private int rabbitHeight;

        private Texture2D eggTexture;
        private List<Vector2> eggPositions;
        private int eggWidth;
        private int eggHeight;

        private Texture2D obstacleTexture;
        private List<Vector2> obstaclePositions;
        private int obstacleWidth;
        private int obstacleHeight;

        private int score;
        private bool isGameOver;

        private SpriteFont spriteFont;

        public EggCollectorGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            rabbitPosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            rabbitWidth = 64;  // Задайте желаемую ширину кролика
            rabbitHeight = 64; // Задайте желаемую высоту кролика

            eggPositions = new List<Vector2>();
            obstaclePositions = new List<Vector2>();

            score = 0;
            isGameOver = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            rabbitTexture = Content.Load<Texture2D>("rabbit");
            eggTexture = Content.Load<Texture2D>("egg");
            obstacleTexture = Content.Load<Texture2D>("obstacle");

            eggWidth = 32;      // Задайте желаемую ширину яйца
            eggHeight = 32;     // Задайте желаемую высоту яйца

            obstacleWidth = 48; // Задайте желаемую ширину препятствия
            obstacleHeight = 48; // Задайте желаемую высоту препятствия

            // Загрузка шрифта
            spriteFont = Content.Load<SpriteFont>("silverfont");

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!isGameOver)
            {
                KeyboardState keyboardState = Keyboard.GetState();
                if (keyboardState.IsKeyDown(Keys.Left))
                    rabbitPosition.X -= 5;
                if (keyboardState.IsKeyDown(Keys.Right))
                    rabbitPosition.X += 5;
                if (keyboardState.IsKeyDown(Keys.Up))
                    rabbitPosition.Y -= 5;
                if (keyboardState.IsKeyDown(Keys.Down))
                    rabbitPosition.Y += 5;

                if (gameTime.TotalGameTime.Seconds % 2 == 0)
                {
                    Random random = new Random();
                    int x = random.Next(0, graphics.PreferredBackBufferWidth - eggWidth + 1);
                    int y = random.Next(0, graphics.PreferredBackBufferHeight - eggHeight + 1);
                    eggPositions.Add(new Vector2(x, y));
                }

                if (gameTime.TotalGameTime.Seconds % 3 == 0)
                {
                    Random random = new Random();
                    int x = random.Next(0, graphics.PreferredBackBufferWidth - obstacleWidth + 1);
                    int y = random.Next(0, graphics.PreferredBackBufferHeight - obstacleHeight + 1);
                    obstaclePositions.Add(new Vector2(x, y));
                }

                Rectangle rabbitRectangle = new Rectangle((int)rabbitPosition.X, (int)rabbitPosition.Y, rabbitWidth, rabbitHeight);
                for (int i = 0; i < eggPositions.Count; i++)
                {
                    Rectangle eggRectangle = new Rectangle((int)eggPositions[i].X, (int)eggPositions[i].Y, eggWidth, eggHeight);
                    if (rabbitRectangle.Intersects(eggRectangle))
                    {
                        eggPositions.RemoveAt(i);
                        score++;
                    }
                }

                for (int i = 0; i < obstaclePositions.Count; i++)
                {
                    Rectangle obstacleRectangle = new Rectangle((int)obstaclePositions[i].X, (int)obstaclePositions[i].Y, obstacleWidth, obstacleHeight);
                    if (rabbitRectangle.Intersects(obstacleRectangle))
                    {
                        isGameOver = true;
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(rabbitTexture, rabbitPosition, Color.White);

            foreach (Vector2 eggPosition in eggPositions)
            {
                spriteBatch.Draw(eggTexture, eggPosition, Color.White);
            }

            foreach (Vector2 obstaclePosition in obstaclePositions)
            {
                spriteBatch.Draw(obstacleTexture, obstaclePosition, Color.White);
            }

            spriteBatch.DrawString(spriteFont, "Score: " + score, new Vector2(10, 10), Color.Black);

            if (isGameOver)
            {
                spriteBatch.DrawString(spriteFont, "Game Over", new Vector2(graphics.PreferredBackBufferWidth / 2 - 50, graphics.PreferredBackBufferHeight / 2 - 20), Color.Red);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
