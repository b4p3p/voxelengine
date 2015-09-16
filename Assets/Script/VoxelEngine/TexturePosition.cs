using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace VoxelEngine
{
    public class TexturePosition
    {
        public float tUnit;
        public Vector2 tPos;

        public TexturePosition(float tUnit, Vector2 tPos)
        {
            this.tPos = tPos;
            this.tUnit = tUnit;
        }
    }
}

