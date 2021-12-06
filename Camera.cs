using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;



namespace Project1
{
    class Camera
    {
        public Matrix View { get; private set; }
        public Matrix Projection { get; private set; }

        //Vector3 forward = new Vector3(0.0f, 0.0f, 1.0f);
        //Vector3 right = new Vector3(1.0f, 0.0f, 0.0f);
        Object player;

        //Vector3 target = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 up = new Vector3(0f, 1f, 0f);
        Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);

        //float moveLeftRight = 0.0f;
        //float moveBackForward = 0.0f;
        //float moveSpeed = 1f;

        //float yaw = 0.0f;
        float pitch = 0.0f;

        float screenWidth_, screenHeight_;
        KeyboardState currentKeyboardState;
        MouseState currentMouseState;
        Vector2 screenCenter;
        float YrotationSpeed;
        Vector3 target = new Vector3(0.0f, 0.0f, 0.0f);
        float XrotationSpeed;

        //float jumpAmount;
        //float gravity = 0.1f;
        //bool inAir = false;
        private void HandleMouse(GameTime gameTime)
        {
            currentMouseState = Mouse.GetState();
            float amount = (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f;
            if (currentMouseState.Y != screenCenter.Y)
            {
                pitch -= XrotationSpeed * (currentMouseState.Y - screenCenter.Y) * amount;
                if (pitch > MathHelper.ToRadians(50)) //Inte titta helt neråt
                    pitch = MathHelper.ToRadians(50);
                if (pitch < MathHelper.ToRadians(-85)) //Inte titta helt uppåt
                    pitch = MathHelper.ToRadians(-85);
            }
            Mouse.SetPosition((int)screenCenter.X, (int)screenCenter.Y);
        }
        public Camera(float screenWidth, float screenHeight, Vector3 cameraPosition, Object _player)
        {
            this.player = _player;
            screenWidth_ = screenWidth;
            screenHeight_ = screenHeight;
            screenCenter = new Vector2(screenWidth_, screenHeight_) / 2;

            position = cameraPosition;
            YrotationSpeed = screenWidth_ * 0.0001f;
            XrotationSpeed = screenHeight_ * 0.0001f;

            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), screenWidth_ / screenHeight_, 0.1f, 10000);
        }


        public void Update(GameTime gameTime)
        {
            Matrix camRotationMatrix = Matrix.CreateRotationX(pitch);
            target = Vector3.Transform(player.basicEffect.World.Forward * 10, camRotationMatrix);
            target.Normalize();

            position = target + player.basicEffect.World.Translation;

            View = Matrix.CreateLookAt(position, player.basicEffect.World.Translation, up);


        }

    }
}