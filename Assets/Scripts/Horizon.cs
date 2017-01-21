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
    public float Delta;
    public float SpeedWave;
    private WaveGenerator _waveGenerator;
    private PolygonCollider2D _polygonCollider2D;
    public float TimeElapsed;

    // Use this for initialization
	void Start ()
	{
	    _waveGenerator = GameObject.FindGameObjectWithTag("WaveGenerator").GetComponent<WaveGenerator>();
	    _polygonCollider2D = GetComponent<PolygonCollider2D>();
        HorizonWavePoints = new List<WavePoint>();

        if (HorizonWavePoints == null)
        {
            HorizonWavePoints = new List<WavePoint>();
        }
        HorizonWavePoints.Clear();
        for (int i = 0; i < NumberPoints; i++)
        {
            HorizonWavePoints.Add(_waveGenerator.GetWavePoint(0));
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		HorizonGenerator();
	    Delta += Time.fixedDeltaTime * SpeedWave;
	    TimeElapsed += Time.fixedDeltaTime;
	    while (Delta >= Offset)
	    {
	        Delta = Delta - Offset;
            HorizonWavePoints.RemoveAt(0);
            HorizonWavePoints.Add(_waveGenerator.GetWavePoint(TimeElapsed));
	    }
	}

    public void HorizonGenerator()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        for (int i = 0; i < NumberPoints; i++)
        {
            vertices.Add(new Vector3(i * Offset - Delta, 0));
            vertices.Add(new Vector3(i * Offset - Delta, HorizonWavePoints[i].Height + MinHeight));

            if (vertices.Count >= 4)
            {
                // We have completed a new quad, create 2 triangles
                int start = vertices.Count - 4;
                triangles.Add(start + 0);
                triangles.Add(start + 1);
                triangles.Add(start + 2);
                triangles.Add(start + 1);
                triangles.Add(start + 3);
                triangles.Add(start + 2);
            }
        }

        Vector2[] edgePoints = new Vector2[NumberPoints+2];
        for (int i = 0; i < NumberPoints; i++)
        {
            edgePoints[i] = new Vector2(i * Offset - Delta, HorizonWavePoints[i].Height + MinHeight);
        }
        edgePoints[NumberPoints] = new Vector2(NumberPoints * Offset, 0);
        edgePoints[NumberPoints+1] = new Vector2(0, 0);
        
        _polygonCollider2D.points = edgePoints;

        DestroyImmediate(Mesh);
        Mesh = new Mesh();
        if (!transform.GetComponent<MeshFilter>() || !transform.GetComponent<MeshRenderer>()) 
        {
            transform.gameObject.AddComponent<MeshFilter>();
            transform.gameObject.AddComponent<MeshRenderer>();
        }

        transform.GetComponent<MeshFilter>().mesh = Mesh;

        Mesh.name = "Tidal";

        Mesh.vertices = vertices.ToArray();
        Mesh.triangles = triangles.ToArray();
        //Mesh.uv = uvs;

        Mesh.RecalculateNormals();
    }
}
