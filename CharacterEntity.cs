using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Input;

namespace GoForFish
{
    public class CharacterEntity
    {
        static Texture2D characterSheetTexture;
        
        
        // WALK ANIMATIONS 
        Animation walkDown;     // Down
        Animation walkUp;       // Up
        Animation walkLeft;     // Left
        Animation walkRight;    // Right



        // STAND ANIMATIONS 
        Animation standDown;    // Down
        Animation standUp;      // Up
        Animation standLeft;    // Left
        Animation standRight;   // Right


        // Current animation
        Animation currentAnimation;

        // Speed
        private float playerSpeed = 200;
        
        // Screen size
        private int screenHeight;
        private int screenWidth;

        public float X
        {
            get;
            set;
        }

        public float Y
        {
            get;
            set;
        }

        public CharacterEntity (GraphicsDevice graphicsDevice, int preferredBackBufferHeight, int preferredBackBufferWidth)
        {
            if (characterSheetTexture == null)
            {
                using (var stream = TitleContainer.OpenStream ("Content/charactersheet.png"))
                {
                    characterSheetTexture = Texture2D.FromStream (graphicsDevice, stream);
                }
            }

            this.screenHeight = preferredBackBufferHeight;
            this.screenWidth = preferredBackBufferWidth;
            
            // Walk DOWN
            walkDown = new Animation ();
            walkDown.AddFrame (new Rectangle (0, 0, 16, 16), TimeSpan.FromSeconds (.25));
            walkDown.AddFrame (new Rectangle (16, 0, 16, 16), TimeSpan.FromSeconds (.25));
            walkDown.AddFrame (new Rectangle (0, 0, 16, 16), TimeSpan.FromSeconds (.25));
            walkDown.AddFrame (new Rectangle (32, 0, 16, 16), TimeSpan.FromSeconds (.25));

            // Walk UP
            walkUp = new Animation ();
            walkUp.AddFrame (new Rectangle (144, 0, 16, 16), TimeSpan.FromSeconds (.25));
            walkUp.AddFrame (new Rectangle (160, 0, 16, 16), TimeSpan.FromSeconds (.25));
            walkUp.AddFrame (new Rectangle (144, 0, 16, 16), TimeSpan.FromSeconds (.25));
            walkUp.AddFrame (new Rectangle (176, 0, 16, 16), TimeSpan.FromSeconds (.25));

            // Walk LEFT
            walkLeft = new Animation ();
            walkLeft.AddFrame (new Rectangle (80, 0, 16, 16), TimeSpan.FromSeconds (.25));
            walkLeft.AddFrame (new Rectangle (64, 0, 16, 16), TimeSpan.FromSeconds (.25));
            walkLeft.AddFrame (new Rectangle (80, 0, 16, 16), TimeSpan.FromSeconds (.25));
            walkLeft.AddFrame (new Rectangle (48, 0, 16, 16), TimeSpan.FromSeconds (.25));

            // Walk RIGHT
            walkRight = new Animation ();
            walkRight.AddFrame (new Rectangle (96, 0, 16, 16), TimeSpan.FromSeconds (.25));
            walkRight.AddFrame (new Rectangle (112, 0, 16, 16), TimeSpan.FromSeconds (.25));
            walkRight.AddFrame (new Rectangle (96, 0, 16, 16), TimeSpan.FromSeconds (.25));
            walkRight.AddFrame (new Rectangle (128, 0, 16, 16), TimeSpan.FromSeconds (.25));
            

            // Standing animations only have a single frame of animation:
            
            // Stand DOWN
            standDown = new Animation ();
            standDown.AddFrame (new Rectangle (0, 0, 16, 16), TimeSpan.FromSeconds (.25));

            // Stand UP
            standUp = new Animation ();
            standUp.AddFrame (new Rectangle (144, 0, 16, 16), TimeSpan.FromSeconds (.25));

            // Stand LEFT
            standLeft = new Animation ();
            standLeft.AddFrame (new Rectangle (48, 0, 16, 16), TimeSpan.FromSeconds (.25));

            // Stand RIGHT
            standRight = new Animation ();
            standRight.AddFrame (new Rectangle (96, 0, 16, 16), TimeSpan.FromSeconds (.25));
          
        }

        public void Update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Up)){                   // Up event
                if(this.Y  > 0){
                    currentAnimation = walkUp;
                    this.Y -= playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
            else if (kstate.IsKeyDown(Keys.Down)){                   // Down event
                if((this.Y  + 16) < screenHeight){
                    currentAnimation = walkDown;
                    this.Y += playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
            else if (kstate.IsKeyDown(Keys.Left)){                   // Up event
                if(this.X > 0){
                    currentAnimation = walkLeft;
                    this.X -= playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
            else if (kstate.IsKeyDown(Keys.Right)){                   // Down event
                if((this.X  + 16) < screenWidth){
                    currentAnimation = walkRight;
                    this.X += playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            } else {
                
                // If the character was walking, we can set the standing animation
                // according to the walking animation that is playing:
                if (currentAnimation == walkRight)
                {
                    currentAnimation = standRight;
                }
                else if (currentAnimation == walkLeft)
                {
                    currentAnimation = standLeft;
                }
                else if (currentAnimation == walkUp)
                {
                    currentAnimation = standUp;
                }
                else if (currentAnimation == walkDown)
                {
                    currentAnimation = standDown;
                }
                else if (currentAnimation == null)
                {
                    currentAnimation = standDown;
                }

                // if none of the above code hit then the character
                // is already standing, so no need to change the animation.
            }


            currentAnimation.Update (gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 topLeftOfSprite = new Vector2 (this.X, this.Y);
            Color tintColor = Color.White;
            var sourceRectangle = currentAnimation.CurrentRectangle;

            spriteBatch.Draw(characterSheetTexture, topLeftOfSprite, sourceRectangle, Color.White);
        }
    }
}