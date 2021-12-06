using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Project1
{
    class ObjectHandler
    {
        public List<Object> Load(GraphicsDevice graphicsDevice)
        {
            string data = "";
            string path = @"..\..\..\objs\";

            DirectoryInfo folder = new DirectoryInfo(path);
            List<Object> objs = new List<Object>();
            FileInfo[] files = folder.GetFiles("*.obj");

            for (int i = 0; i < files.Length; i++)
            {
                List<Vector3> v = new List<Vector3>();
                List<Vector2> vt = new List<Vector2>();
                List<Vector3> vn = new List<Vector3>();
                List<string> textureNames = new List<string>();
                List<string> cubesOrder = new List<string>();
                bool loaded = false;
                string imageFile = "gradient";

                using (StreamReader sr = new StreamReader(files[i].FullName))
                {
                    

                    while ((data = sr.ReadLine()) != null)
                    {
                        string[] newdata = data.Split(" ");
                        if (data.Contains("v ") || data.Contains("vt ") || data.Contains("vn "))
                        {
                            float[] coords = new float[3];
                            for (int j = 1; j < newdata.Length; j++)
                            {
                                newdata[j] = newdata[j].Replace(".", ","); //replace with , instead of . for parse formatting
                                coords[j - 1] = float.Parse(newdata[j]);
                            }
                            if (data.Contains("v "))
                            {
                                v.Add(new Vector3(coords[0], coords[1], coords[2]));
                            }
                            else if (data.Contains("vt "))
                            {
                                vt.Add(new Vector2(coords[0], coords[1]));
                            }
                            else if (data.Contains("vn "))
                            {
                                vn.Add(new Vector3(coords[0], coords[1], coords[2]));
                            }
                        }
                        else if (data.Contains("f "))
                        {
                            if(!loaded) {
                                loaded = true;
                                objs.Add(new Object(new List<Points>(), newdata[1], imageFile, new BasicEffect(graphicsDevice) { World = Matrix.CreateTranslation(i * 5, 0, 0), TextureEnabled = true }));
                            }
                            for (int j = 1; j < newdata.Length; j++)
                            {
                                if (newdata[j] != "")
                                {
                                    cubesOrder.Add(newdata[j]); //adding float values to cube order
                                }
                            }
                        }
                        else if (data.Contains("mtllib "))
                        {
                            Debug.WriteLine(newdata[1]);
                            using (StreamReader sr_mt = new StreamReader(folder + newdata[1]))
                            {
                                string data2 = "";
                                while ((data2 = sr_mt.ReadLine()) != null)
                                {
                                    if (data2.Contains("map_Kd"))
                                    {
                                        imageFile = data2.Split(' ')[1].Replace(".png", "");
                                        textureNames.Add(imageFile);
                                    }
                                }
                            }
                        }

                    }
                }
                for (int j = 0; j < cubesOrder.Count; j++)
                {
                    string[] d = cubesOrder[j].Split("/");
                    int[] order = new int[d.Length];
                    for (int x = 0; x < d.Length; x++)
                    {
                        order[x] = int.Parse(d[x]);
                    }
                    Vector3 checkVn = vn.Count > 0 ? vn[order[2] - 1] : Vector3.Zero; //Not all obj files contains vn
                    objs[objs.Count - 1].points.Add(new Points(v[order[0] - 1], vt[order[1] - 1], checkVn));
                }
            }


            return objs;
        }

    }
}
