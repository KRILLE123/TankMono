using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Project1
{
    class Projectile
    {
        Object tank;
        int screenWidth;
        int screenHeight;
        Vector3 p1;
        Vector3 p2;
        float near = 0.1f, far = 1000f;

        public Projectile(Object tank)
        {
            this.tank = tank;
        }

        public void calculateRay()
        {
            float fov = MathHelper.ToRadians(45);
            float dx = tank.basicEffect.World.Translation.X;
            float dy = tank.basicEffect.World.Translation.Y;
            float aspectRatio = screenWidth / screenHeight;

            Matrix invView = Matrix.Invert(tank.basicEffect.View);

            dx = (float)(Math.Tan(fov * 0.5f) * (dx / (screenWidth * 0.5) - 1.0f) / aspectRatio);
            dy = (float)(Math.Tan(fov * 0.5f) * (1.0f - dy / (screenHeight * 0.5)));

            p1 = new Vector3(dx * near * -1, dy * near, near * -1);
            p2 = new Vector3(dx * far * -1, dy * far, far * -1);

            p1 = Vector3.Transform(p1, invView);
            p2 = Vector3.Transform(p2, invView);
        }

        public void CheckCollision(List<Object> targetObj)
        {
            Vector3 dir = Vector3.Normalize(p1 - p2);
            Ray r = new Ray(p1, dir);

            for (int i = 0; i < targetObj.Count; i++)
            {
                BoundingBox bb = new BoundingBox(targetObj[i].basicEffect.World.Translation, targetObj[i].basicEffect.World.Translation + new Vector3(10, 10, 10));
                float? dist = r.Intersects(bb);

                if(dist != null)
                {
                    Debug.WriteLine("tjena");
                }
            }
        }
    }
}
