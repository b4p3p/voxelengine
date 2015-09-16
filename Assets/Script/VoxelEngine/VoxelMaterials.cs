using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace VoxelEngine
{
    public class VoxelMaterials : MonoBehaviour
    {

        public List<VoxelMaterial> materials;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        internal VoxelMaterial GetMaterial(EnumVoxelType type)
        {
            return (from VoxelMaterial m in materials
                    where m.key == type
                    select m).FirstOrDefault<VoxelMaterial>();
        }
    }
}
