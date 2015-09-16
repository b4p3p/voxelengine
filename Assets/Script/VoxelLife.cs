using UnityEngine;
using System.Collections;
using VoxelEngine;

public class VoxelLife : MonoBehaviour {

    private VEngine voxelEngine;
    private VoxelMaterials materials;

	// Use this for initialization
	void Start () {

        voxelEngine = GetComponent<VEngine>();
        materials = GetComponent<VoxelMaterials>();

        voxelEngine.AddVoxelAt(new Vector3(0, 0, 0), new VoxelType(materials.materials[0]));
        voxelEngine.AddVoxelAt(new Vector3(1, 1, 1), new VoxelType(materials.materials[0]));
        voxelEngine.SetAutoRefresh(true);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
