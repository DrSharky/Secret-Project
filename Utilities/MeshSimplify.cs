using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMeshSimplifier;

public class MeshSimplify : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("vert count: " + GetComponent<MeshFilter>().sharedMesh.vertices.Length);
        MeshSimplifier simp = new MeshSimplifier();
        simp.Initialize(GetComponent<MeshFilter>().sharedMesh);
        simp.Agressiveness = 100f;
        //simp.PreserveBorderEdges = true;
        simp.PreserveUVFoldoverEdges = true;
        simp.PreserveUVSeamEdges = true;
        simp.PreserveSurfaceCurvature = true;
        simp.SimplifyMeshLossless();
        Mesh destMesh = simp.ToMesh();
        GetComponent<MeshFilter>().sharedMesh = destMesh;
        Debug.Log("vert count2: " + GetComponent<MeshFilter>().sharedMesh.vertices.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
