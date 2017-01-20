using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horizon : MonoBehaviour
{
    public List<WavePoint> HorizonWavePoints;
    public int NumberPoints;
    public float Offset;
    public Mesh Mesh;
    public float MinHeight;
    private WaveGenerator _waveGenerator;
    // Use this for initialization
	void Start ()
	{
	    _waveGenerator = GameObject.FindGameObjectWithTag("WaveGenerator").GetComponent<WaveGenerator>();
        HorizonWavePoints = new List<WavePoint>();
	}
	
	// Update is called once per frame
	void Update () {
		HorizonGenerator();
	}

    public void HorizonGenerator()
    {
        if (HorizonWavePoints == null)
        {
            HorizonWavePoints = new List<WavePoint>();
        }
        HorizonWavePoints.Clear();
        for (int i = 0; i < NumberPoints; i++)
        {
            HorizonWavePoints.Add(_waveGenerator.GetWavePoint(i * Offset));
        }
        Vector3[] vertices = new Vector3[(NumberPoints-1)*4];
        int[] triangles = new int[(NumberPoints - 1) * 6];
        Vector2[] uvs = new Vector2[(NumberPoints - 1) * 4];

        for (int i = 0; i < NumberPoints - 1; i++)
        {
            vertices[i * 4] = new Vector3(i * Offset, HorizonWavePoints[i].Height + MinHeight);
            vertices[i * 4 + 1] = new Vector3((i + 1) * Offset, HorizonWavePoints[i + 1].Height + MinHeight);
            vertices[i * 4 + 2] = new Vector3((i + 1) * Offset, 0);
            vertices[i * 4 + 3] = new Vector3(i * Offset, 0);
        }
        
        for (int i = 0; i < NumberPoints - 1; i++)
        {
            triangles[i * 6] = i;
            triangles[i * 6 +1] = i+1;
            triangles[i * 6 + 2] = i+2;
            triangles[i * 6 + 3] = i;
            triangles[i * 6 + 4] = i+2;
            triangles[i * 6 + 5] = i+3;
        }

        for (int i = 0; i < NumberPoints - 1; i++)
        {
            uvs[i * 4] = new Vector2(0, 0);
            uvs[i * 4 + 1] = new Vector2(1, 0);
            uvs[i * 4 + 2] = new Vector2(1, 1);
            uvs[i * 4 + 3] = new Vector2(0, 1);
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
