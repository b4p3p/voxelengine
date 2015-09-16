using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace VoxelEngine
{
    /// <summary>
    ///     Materiali usati in tutto il gioco con le relative impostazioni
    /// </summary>
    [System.Serializable]
    public class VoxelMaterial
    {
        public float offset = 0.0625f;
        public Material material;
        public EnumVoxelType key;
        public Vector2 position;
    }
}
