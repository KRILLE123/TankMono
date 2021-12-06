using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project1
{
    class Terrain
    {
        VertexBuffer vertexBufferTerrain;
        IndexBuffer indexBufferTerrain;
        int cols = 6;
        int rows = 6;
        int numFaces = 0;
        public Terrain(GraphicsDevice graphicsDevice)
        {
            int numVertices = cols * rows;
            int[,] grid = new int[cols, rows];
            numFaces = (rows - 1) * (cols - 1) * 2;
            List<VertexPositionNormalTexture> ground = new List<VertexPositionNormalTexture>(numVertices);

            List<int> indices = new List<int>(numFaces * 3) { };
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    ground.Add(new VertexPositionNormalTexture(new Vector3(i*10, 1, j*10), new Vector3(1, 1, 1), new Vector2(0, 0)));
                }
            }

            int y = 0;
            int x = 0;
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    indices.Add(x);
                    indices.Add(x + 1);
                    indices.Add(i * rows + j);
                    x++;

                    indices.Add(y);
                    indices.Add(y + 1);
                    indices.Add(j * rows + i);
                    y++;
                }
            }

            for (int i = 0; i < indices.Count; i++)
            {
                Debug.WriteLine(indices[i]);
            }

            indexBufferTerrain = new IndexBuffer(graphicsDevice, IndexElementSize.ThirtyTwoBits, indices.Count, BufferUsage.WriteOnly); 
            indexBufferTerrain.SetData<int>(indices.ToArray()); 
            vertexBufferTerrain = new VertexBuffer(graphicsDevice, typeof(VertexPositionNormalTexture), ground.Count, BufferUsage.WriteOnly);
            vertexBufferTerrain.SetData<VertexPositionNormalTexture>(ground.ToArray());
        }
        public void Update(GraphicsDevice GraphicsDevice, BasicEffect be)
        {
            foreach (EffectPass pass in be.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.SetVertexBuffer(vertexBufferTerrain);
                GraphicsDevice.Indices = indexBufferTerrain;
                GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, numFaces, 1);
            }

        }
    }
}
