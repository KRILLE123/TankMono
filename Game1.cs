using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace Project1
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 20), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
        Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.01f, 10000000f);
        Camera camera;
        Physics cube;
        Terrain terrain;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";


        }

        protected override void Initialize()
        {
            base.Initialize();
        }
        List<Object> objs;

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ObjectHandler objHandler = new ObjectHandler();
            objs = objHandler.Load(GraphicsDevice);
            terrain = new Terrain(GraphicsDevice);

            //for (int i = 0; i < textureNames.Count; i++)
            //{
            //    textureList.Add(Content.Load<Texture2D>(textureNames[i]));
            //}

            graphics.PreferredBackBufferWidth = screenWidth;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = screenHeight;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            camera = new Camera(screenWidth, screenHeight, new Vector3(0,0,0), objs[1]);
            cube = new Physics(objs[1], screenWidth, screenHeight);
           
            for (int i = 0; i < objs.Count; i++)
            {
                objs[i].basicEffect.Texture = Content.Load<Texture2D>(objs[i].textureName);
                objs[i].basicEffect.View = view;
                objs[i].basicEffect.Projection = projection;
            }

            for (int i = 0; i < objs.Count; i++)
            {
                VertexPositionNormalTexture[] vertices = new VertexPositionNormalTexture[objs[i].points.Count];

                for (int j = 0; j < objs[i].points.Count; j++)
                {
                    vertices[j] = new VertexPositionNormalTexture(objs[i].points[j].v, objs[i].points[j].vn, objs[i].points[j].vt);
                }

                objs[i].vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionNormalTexture), vertices.Length, BufferUsage.WriteOnly);
                objs[i].vertexBuffer.SetData<VertexPositionNormalTexture>(vertices);
         
            }
        }
        

        protected override void UnloadContent()
        {
        }
        static int screenWidth = 1920;
        static int screenHeight = 1200;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            camera.Update(gameTime);
            cube.Move(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            camera.Update(gameTime);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;

            BlendState blendState = new BlendState();
            blendState.ColorDestinationBlend = Blend.InverseSourceAlpha;

            GraphicsDevice.BlendState = blendState;
            GraphicsDevice.RasterizerState = rasterizerState;

            for (int i = 0; i < objs.Count; i++)
            {
                objs[i].basicEffect.Projection = camera.Projection;
                objs[i].basicEffect.View = camera.View;
                GraphicsDevice.SetVertexBuffer(objs[i].vertexBuffer);

                foreach (EffectPass pass in objs[i].basicEffect.CurrentTechnique.Passes)
                {
                    pass.Apply();

                    GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, objs[i].points.Count);
                }
            }
            terrain.Update(GraphicsDevice, objs[0].basicEffect);

            base.Draw(gameTime);
        }
    }
}
