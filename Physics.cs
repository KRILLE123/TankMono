using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Project1
{
    class Physics
    {
        Object targetObject;
        KeyboardState currentKeyboardState;
        public float rotation = 0.0f;
        float YrotationSpeed;
        float XrotationSpeed;
        Vector2 screenCenter;
        Vector3 forward;
        public Vector3 pos;

        public Physics(Object @object, int screenWidth_, int screenHeight_)
        {
            targetObject = @object;
            screenCenter = new Vector2(screenWidth_, screenHeight_) / 2;
            YrotationSpeed = screenWidth_ * 0.0001f;
            XrotationSpeed = screenHeight_ * 0.0001f;
        }


        public void Rotate(float value)
        {
            if(rotation + value < 0.0)
            {
                rotation = 360.0f;
            } else if(rotation + value > 360)
            {
                rotation = 0.0f;
            } else
            {
                rotation += value;
            }
            targetObject.basicEffect.World = Matrix.CreateRotationY(MathHelper.ToRadians(rotation)) * Matrix.CreateTranslation(targetObject.basicEffect.World.Translation);
            
        }

        public void Move(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.W))
            {                
                forward = Vector3.Add(Vector3.Forward * (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.01f, targetObject.basicEffect.View.Forward);
                forward.Normalize();

                pos = targetObject.basicEffect.World.Translation;
                targetObject.basicEffect.World = Matrix.CreateTranslation(Vector3.Add(pos, forward));

            } 
            else if (currentKeyboardState.IsKeyDown(Keys.A))
            {
                Rotate(-1f);
            }
            else if (currentKeyboardState.IsKeyDown(Keys.D))
            {
                Rotate(1f);
            }
            else if (currentKeyboardState.IsKeyDown(Keys.S))
            {

            }
        }
    }
}
