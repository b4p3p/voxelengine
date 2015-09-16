using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace VoxelEngine
{
    [RequireComponent(typeof(VoxelMaterials))]
    public class VEngine : MonoBehaviour
    {
        public static VoxelMaterials voxelMaterials;

        public int ChunkSize = 4;

        public bool AutoRefresh { get; set; }

        //keep chunks in the world
        private Dictionary<Vector3, Chunk> chunks = new Dictionary<Vector3, Chunk>();
        private GameObject chunckContainer;

        void Start()
        {
            AutoRefresh = false;
            voxelMaterials = GetComponent<VoxelMaterials>();

            //create chunks container
            chunckContainer = new GameObject("Chunks");
            chunckContainer.transform.parent = this.gameObject.transform;
        }

        void Update()
        {

        }

        public void SetAutoRefresh(bool v)
        {
            AutoRefresh = true;
            Refresh();
        }

        public void Refresh()
        {
            foreach (Chunk chunk in chunks.Values)
            {
                chunk.Refresh();
            }
        }
        
        public void AddVoxelAt(Vector3 posVoxel, VoxelType type)
        {
            //converto la posizione del voxel nella posizione iniziale del chunk
            Vector3 posChunk = GetStartPosChunk(posVoxel);
            
            //verifico che il chunk esista
            //se non esiste lo creo
            Chunk chunk = null;
            if (chunks.ContainsKey(posChunk) == false)
            {
                chunk = Chunk.CreateChunkAt(posChunk, chunckContainer, ChunkSize);
                chunks.Add( posChunk, chunk );
            }

            chunk = chunks[posChunk];
            chunk.AddVoxelAt(posVoxel - posChunk, type);
        }

        private Vector3 GetStartPosChunk(Vector3 worldPos)
        {
            /*
            chunkSize = 4
            worldPos = 0,0,0 -> return 0,0,0
            worldPos = 3,3,3 -> return 0,0,0
            worldPos = 4,0,0 -> return 4,0,0
            worldPos = -1,0,0 -> return -4,0,0
            */

            Vector3 tmpPos = worldPos;  

            tmpPos.x = Mathf.FloorToInt(tmpPos.x / ChunkSize);
            tmpPos.y = Mathf.FloorToInt(tmpPos.y / ChunkSize);
            tmpPos.z = Mathf.FloorToInt(tmpPos.z / ChunkSize);

            return tmpPos * ChunkSize;
        }

    }
}


