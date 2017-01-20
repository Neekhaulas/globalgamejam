using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horizon : MonoBehaviour
{
    public List<WavePoint> HorziontWavePoints;
    public int NumberPoints;
    public float Offset;
    public Mesh Mesh;
    // Use this for initialization
	void Start () {
        HorziontWavePoints = new List<WavePoint>();
        for (int i = 0; i < 10; i++)
	    {
	        HorziontWavePoints.Add(new WavePoint(NumberPoints));
	    }
	}
	
	// Update is called once per frame
	void Update () {
		HorizonGenerator();
	}

    public void HorizonGenerator()
    {
        Vector3[] vertices = new Vector3[(NumberPoints-1)*4];
        int[] triangles = new int[(NumberPoints - 1) * 6];
        Vector2[] uvs = new Vector2[(NumberPoints - 1) * 4];

        for (int i = 0; i < NumberPoints - 1; i++)
        {
            vertices[i * 4] = new Vector3(i * Offset, HorziontWavePoints[i].Height);
            vertices[i * 4 + 1] = new Vector3((i + 1) * Offset, HorziontWavePoints[i + 1].Height);
            vertices[i * 4 + 2] = new Vector3((i + 1) * Offset, 0);
            vertices[i * 4 + 3] = new Vector3(i * Offset, 0);
        }
        
        for (int i = 0; i < NumberPoints - 1; i++)
        {
            triangles[i] = i;
            triangles[i+1] = i+1;
            triangles[i+2] = i+2;
            triangles[i+3] = i;
            triangles[i+4] = i+2;
            triangles[i+5] = i+3;
        }

        for (int i = 0; i < NumberPoints - 1; i++)
        {
            uvs[i] = new Vector2(0, 0);
            uvs[i+1] = new Vector2(1, 0);
            uvs[i+2] = new Vector2(1, 1);
            uvs[i+3] = new Vector2(0, 1);
        }

        Mesh = new Mesh();
        if (!transform.GetComponent<MeshFilter>() || !transform.GetComponent<MeshRenderer>()) //If you will havent got any meshrenderer or filter
        {
            transform.gameObject.AddComponent<MeshFilter>();
            transform.gameObject.AddComponent<MeshRenderer>();
        }

        transform.GetComponent<MeshFilter>().mesh = Mesh;

        Mesh.name = "MyOwnObject";

        Mesh.vertices = vertices; //Just do this... Use Logic...
        Mesh.triangles = triangles;
        Mesh.uv = uvs;

        Mesh.RecalculateNormals();
    }
}
