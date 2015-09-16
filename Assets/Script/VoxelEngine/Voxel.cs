using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VoxelEngine
{
    public class Voxel
    {
        private VoxelType voxelType = null;
        private Chunk chunk;

        public Voxel(Chunk chunk, VoxelType voxelType)
        {
            this.chunk = chunk;
            this.voxelType = voxelType;
        }
        
        public int DrawFaces(List<Vector3> vertices, List<int> triangles, List<Vector2> UV, int contVertex)
        {
            contVertex = DisegnaDown(vertices, triangles,  UV, contVertex);
            contVertex = DisegnaTop(vertices, triangles, UV, contVertex);
            contVertex = DisegnaSx(vertices, triangles, UV, contVertex);
            contVertex = DisegnaDx(vertices, triangles, UV, contVertex);
            contVertex = DisegnaFront(vertices, triangles, UV, contVertex);
            contVertex = DisegnaBack(vertices, triangles, UV, contVertex);

            return contVertex;
        }

        private int addPoint(List<Vector3> vertices, List<int> triangles, List<Vector2> UV, int contVertex, 
                              float x1, float y1, float z1, float tx, float ty, bool rotate = false)
        {
            float tUnit = 1;
            Vector2 tPos = new Vector2(0, 0);

            if( voxelType != null )
            {
                tUnit = voxelType.Material.offset;
                tPos = voxelType.Material.position;
            }

            float x = -0.5f + chunk.posChunk.x;
            float y = -0.5f + chunk.posChunk.y;
            float z = -0.5f + chunk.posChunk.z;

            vertices.Add(new Vector3(x + x1, y + y1, z + z1));
            triangles.Add(contVertex);

            if (!rotate)
                UV.Add(new Vector2(tUnit * tPos.x + tUnit * tx,
                                      tUnit * tPos.y + tUnit * ty));
            else
                UV.Add(new Vector2(tUnit * tPos.x + tUnit * ty,
                                   tUnit * tPos.y + tUnit * tx));
            return 1;
        }

        private int DisegnaDown(List<Vector3> vertices, List<int> triangles, List<Vector2> UV, int contVertex)
        {
            contVertex += addPoint(vertices, triangles, UV, contVertex, 0, 0, 1, 0, 1);  // 0,1
            contVertex += addPoint(vertices, triangles, UV, contVertex, 0, 0, 0, 0, 0);  // 0,0
            contVertex += addPoint(vertices, triangles, UV, contVertex, 1, 0, 0, 1, 0);  // 1,0

            contVertex += addPoint(vertices, triangles, UV, contVertex, 0, 0, 1, 0, 1);  // 0,1
            contVertex += addPoint(vertices, triangles, UV, contVertex, 1, 0, 0, 1, 0);  // 1,0
            contVertex += addPoint(vertices, triangles, UV, contVertex, 1, 0, 1, 1, 1);  // 1,1

            return contVertex;
        }

        private int DisegnaTop(List<Vector3> vertices, List<int> triangles, List<Vector2> UV, int contVertex)
        {
            contVertex += addPoint(vertices, triangles, UV, contVertex, 0, 1, 0, 0, 0);  // 0,0
            contVertex += addPoint(vertices, triangles, UV, contVertex, 0, 1, 1, 0, 1);  // 0,1
            contVertex += addPoint(vertices, triangles, UV, contVertex, 1, 1, 1, 1, 1);  // 1,1
            contVertex += addPoint(vertices, triangles, UV, contVertex, 0, 1, 0, 0, 0);  // 0,0
            contVertex += addPoint(vertices, triangles, UV, contVertex, 1, 1, 1, 1, 1);  // 1,1
            contVertex += addPoint(vertices, triangles, UV, contVertex, 1, 1, 0, 1, 0);  // 1,0   
            return contVertex;    
        }

        private int DisegnaBack(List<Vector3> vertices, List<int> triangles, List<Vector2> UV, int contVertex)
        {
            contVertex += addPoint(vertices, triangles, UV, contVertex, 1, 0, 1, 1, 0);  // 1,0  
            contVertex += addPoint(vertices, triangles, UV, contVertex, 1, 1, 1, 1, 1);  // 1,1
            contVertex += addPoint(vertices, triangles, UV, contVertex, 0, 1, 1, 0, 1);  // 0,1
            contVertex += addPoint(vertices, triangles, UV, contVertex, 1, 0, 1, 1, 0);  // 1,0   
            contVertex += addPoint(vertices, triangles, UV, contVertex, 0, 1, 1, 0, 1);  // 0,1
            contVertex += addPoint(vertices, triangles, UV, contVertex, 0, 0, 1, 0, 0);  // 0,0
            return contVertex;
        }

        private int DisegnaFront(List<Vector3> vertices, List<int> triangles, List<Vector2> UV, int contVertex)
        {
            // x, y, 0
            contVertex += addPoint(vertices, triangles, UV, contVertex, 0, 0, 0, 0, 0);  // 0,0
            contVertex += addPoint(vertices, triangles, UV, contVertex, 0, 1, 0, 0, 1);  // 0,1
            contVertex += addPoint(vertices, triangles, UV, contVertex, 1, 1, 0, 1, 1);  // 1,1
            contVertex += addPoint(vertices, triangles, UV, contVertex, 0, 0, 0, 0, 0);  // 0,0
            contVertex += addPoint(vertices, triangles, UV, contVertex, 1, 1, 0, 1, 1);  // 1,1
            contVertex += addPoint(vertices, triangles, UV, contVertex, 1, 0, 0, 1, 0);  // 1,0
            return contVertex;
        }

        private int DisegnaDx(List<Vector3> vertices, List<int> triangles, List<Vector2> UV, int contVertex)
        {
            contVertex += addPoint(vertices, triangles, UV, contVertex, 1, 0, 0, 0, 0, false);  // 0,0
            contVertex += addPoint(vertices, triangles, UV, contVertex, 1, 1, 0, 1, 0, false);  // 1,0
            contVertex += addPoint(vertices, triangles, UV, contVertex, 1, 1, 1, 1, 1, false);  // 1,1
            contVertex += addPoint(vertices, triangles, UV, contVertex, 1, 0, 0, 0, 0, false);  // 0,0
            contVertex += addPoint(vertices, triangles, UV, contVertex, 1, 1, 1, 1, 1, false);  // 1,1
            contVertex += addPoint(vertices, triangles, UV, contVertex, 1, 0, 1, 0, 1, false);  // 0,1
            return contVertex;
        }

        private int DisegnaSx(List<Vector3> vertices, List<int> triangles, List<Vector2> UV, int contVertex)
        {
            contVertex += addPoint(vertices, triangles, UV, contVertex, 0, 0, 0, 0, 0, true);  // 0,0
            contVertex += addPoint(vertices, triangles, UV, contVertex, 0, 0, 1, 0, 1, true);  // 0,1
            contVertex += addPoint(vertices, triangles, UV, contVertex, 0, 1, 0, 1, 0, true);  // 1,0
            contVertex += addPoint(vertices, triangles, UV, contVertex, 0, 0, 1, 0, 1, true);  // 0,1
            contVertex += addPoint(vertices, triangles, UV, contVertex, 0, 1, 1, 1, 1, true);  // 1,1
            contVertex += addPoint(vertices, triangles, UV, contVertex, 0, 1, 0, 1, 0, true);  // 1,0
            return contVertex;
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawSphere(new Vector3(0, 0, 0), 0.1f);
        }
    }
}


