using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace VoxelEngine
{
    public class Chunk : MonoBehaviour
    {
        internal Vector3 posChunk;

        private Dictionary<Vector3, Voxel> voxels = new Dictionary<Vector3, Voxel>();
        private int size;

        // crea un chunk nella posizione indicata
        internal static Chunk CreateChunkAt(Vector3 posChunk, GameObject chunckContainer, int sizeChunk)
        {
            GameObject newChunk = new GameObject("chunck " + posChunk.x + " " + posChunk.y + " " + posChunk.z);
            newChunk.transform.parent = chunckContainer.transform;
            
            return Chunk.CreateComponent(newChunk, posChunk, sizeChunk);
        }

        internal static Chunk CreateComponent(GameObject gameObject, Vector3 position, int size)
        {
            Chunk newVChunk = gameObject.AddComponent<Chunk>();
            gameObject.AddComponent<MeshRenderer>();
            gameObject.AddComponent<MeshFilter>();

            newVChunk.posChunk = position;
            newVChunk.size = size;
            return newVChunk;
        }

        void Start()
        {

        }

        void Update()
        {

        }

        internal void Refresh()
        {
            Debug.Log("Refresh chunk: " + posChunk);

            List<Vector3> newVertices = new List<Vector3>();
            List<int> newTriangles = new List<int>();
            List<Vector2> newUV = new List<Vector2>();
            Mesh mesh = GetComponent<MeshFilter>().mesh;
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            int contVertex = 0;

            foreach (Voxel voxel in voxels.Values)
            {
                contVertex += voxel.DrawFaces(newVertices, newTriangles, newUV, contVertex);
            }

            meshRenderer.material = VEngine.voxelMaterials.materials[0].material;
            mesh.Clear();
            mesh.vertices = newVertices.ToArray();
            mesh.triangles = newTriangles.ToArray();
            mesh.uv = newUV.ToArray();
            mesh.Optimize();
            mesh.RecalculateNormals();
        }

        internal void AddVoxelAt(Vector3 posVoxel, VoxelType voxelType)
        {
            if (voxels.ContainsKey(posVoxel))
                voxels[posVoxel] = new Voxel(this, voxelType);
            else
                voxels.Add(posVoxel, new Voxel(this, voxelType));
        }

        private Color ?color = null;
        void OnDrawGizmos()
        {
            if( color == null)
                color = new Color( UnityEngine.Random.Range(0.0f, 1f), 
                                   UnityEngine.Random.Range(0.0f, 1f), 
                                   UnityEngine.Random.Range(0.0f, 1f));

            Gizmos.color = (Color)color;

            ////disegna i bordi a terra
            //Vector3 tmpPos = startPosition;
            //Gizmos.DrawLine(tmpPos, tmpPos + Vector3.forward * size);
            //Gizmos.DrawLine(tmpPos, tmpPos + Vector3.right * size);
            //tmpPos = startPosition + Vector3.right * size + Vector3.forward * size;
            //Gizmos.DrawLine(tmpPos, tmpPos + Vector3.left * size);
            //Gizmos.DrawLine(tmpPos, tmpPos + Vector3.back * size);

            //disegna una linea verticale sullo start
            Gizmos.DrawLine(posChunk, posChunk + Vector3.up * size);

            Gizmos.DrawSphere(posChunk + (Vector3.right + Vector3.forward + Vector3.up) * size / 2, 0.2f);

            //disegna un cubo
            Gizmos.DrawWireCube(posChunk + (Vector3.right + Vector3.forward + Vector3.up) * size / 2,
                                (Vector3.right + Vector3.forward + Vector3.up) * size);

            foreach (Vector3 key in voxels.Keys)
            {
                Gizmos.DrawWireCube(key + posChunk + (Vector3.right + Vector3.forward + Vector3.up) * 1 / 2,
                                   (Vector3.right + Vector3.forward + Vector3.up) * 1);
            }
        }

        
    }
}


