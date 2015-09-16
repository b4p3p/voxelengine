using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace VoxelEngine 
{
    [System.Serializable]
    public class VoxelType
    {
        private VoxelMaterial material;

        public VoxelType(VoxelMaterial material)
        {
            this.material = material;
        }
        
        public VoxelMaterial Material { get { return material; } }

        public float Offset
        {
            get
            {
                return material.offset;
            }
        }

        public Vector2 Position
        {
            get
            {
                return material.position;
            }
        }

        public TexturePosition TexturePosition
        {
            get
            {
                return new TexturePosition(Offset, Position);
            }
        }
    }
}
