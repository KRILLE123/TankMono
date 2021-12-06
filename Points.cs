using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1
{
    class Points
    {
        public Vector3 v = new Vector3();
        public Vector2 vt = new Vector2();
        public Vector3 vn = new Vector3();
        public Points(Vector3 v, Vector2 vt, Vector3 vn)
        {
            if (vn == null) {
                vn = Vector3.Zero;
            }
            this.v = v;
            this.vt = vt;
            this.vn = vn;
        }
    }
}
