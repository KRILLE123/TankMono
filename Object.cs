using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1
{
    class Object
    {
        public List<Points> points;
        public string name;
        public string textureName;
        public BasicEffect basicEffect;
        public VertexBuffer vertexBuffer;
        public Object(List<Points> points, string name, string textureName, BasicEffect basicEffect)
        {
            this.points = points;
            this.name = name;
            this.textureName = textureName;
            this.basicEffect = basicEffect;
        }
    }
}
