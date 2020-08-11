using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GoForFish
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Character class
        CharacterEntity character;

        // Font
        private SpriteFont title;
        private SpriteFont paragraph;

        // Text
        private string game_title = "GoForFish";
        private string enter_key_text = "Press a key to start";
        
        // Textures 
        Texture2D characterSheetTexture;
        
        // Scene
        private string current_scene;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            /// <summary>method <c>Initialize</c> Initialize the game.</summary>
            // Initialization logic here
            
            character = new CharacterEntity (this.GraphicsDevice, _graphics.PreferredBackBufferHeight, _graphics.PreferredBackBufferWidth);

            // The first scene is the splashscreen
            current_scene =  "splashscreen";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            /// <summary>method <c>LoadContent</c> load the game content.</summary>

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            using (var stream = TitleContainer.OpenStream ("Content/charactersheet.png"))
            {
                characterSheetTexture = Texture2D.FromStream (this.GraphicsDevice, stream);

            }

            // (With this.Content) Load your game content here
            title = Content.Load<SpriteFont>("Title");            
            paragraph = Content.Load<SpriteFont>("Paragraph");     
        }

        protected override void Update(GameTime gameTime)
        {

            /// <summary>method <c>Update</c> update the game.</summary>

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // KEY EVENT HANDLER 
            var kstate = Keyboard.GetState();
            if (Keyboard.GetState().GetPressedKeys().Length > 0)
                if(current_scene == "splashscreen")
                        current_scene = "scene1";
                            
            character.Update (gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            /// <summary>method <c>Draw</c> renders the game.</summary>
            
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Drawing code here
            _spriteBatch.Begin();

            
            if(current_scene == "splashscreen"){
                _spriteBatch.DrawString(title, $"{game_title}", new Vector2(_graphics.PreferredBackBufferWidth / 3, _graphics.PreferredBackBufferHeight / 4), Color.White);  
                _spriteBatch.DrawString(paragraph, $"{enter_key_text}", new Vector2((_graphics.PreferredBackBufferWidth / 3) + 20, _graphics.PreferredBackBufferHeight - 130), Color.White);        
            } else {
                character.Draw(_spriteBatch);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
