using UnityEngine;
using System.Collections;

public class Create_MyLineMesh : MonoBehaviour {

	private Mesh mesh;
	public int SplitMesh = 1;

	// Use this for initialization
	void Start () {
		mesh = new Mesh(); 
		Vector3[] Vertices = new Vector3[SplitMesh*4];
		Vector2[] UV = new Vector2[SplitMesh*4];
		int[] Tri = new int[3*2*SplitMesh];

		for(int i=0;i<SplitMesh*4;i+=2)
		{
			Vertices[i] = new Vector3(-0.5f,0,i/2);
			Vertices[i+1] = new Vector3(0.5f,0,i/2);
			UV[i] = new Vector2((float)(i/2)/(float)SplitMesh,0);
			UV[i+1] = new Vector2((float)(i/2)/(float)SplitMesh,1);
		}
		/*
		Tri[0] = 0;
		Tri[1] = 2;
		Tri[2] = 1;
		Tri[3] = 2;
		Tri[4] = 3;
		Tri[5] = 1;
		*/

		for(int i=0,j=0;i<SplitMesh*2*3;i+=6,j+=2)
		{
				Tri[i] = 0+j;	Tri[i+1] = 2+j; Tri[i+2] = 1+j;
				Tri[i+3] = 2+j;	Tri[i+4] = 3+j; Tri[i+5] = 1+j;
		}
		mesh.vertices  = Vertices;
		mesh.uv        = UV;
		mesh.triangles = Tri;

		GetComponent<MeshFilter>().sharedMesh = mesh;
		GetComponent<MeshFilter>().sharedMesh.name = "MyLineMesh";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
