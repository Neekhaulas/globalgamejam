using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private EdgeCollider2D _edgeCollider2D;
    public float TimeElapsed;

    // Use this for initialization
	void Start ()
	{
	    _waveGenerator = GameObject.FindGameObjectWithTag("WaveGenerator").GetComponent<WaveGenerator>();
	    _edgeCollider2D = GetComponent<EdgeCollider2D>();
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

	    int nbPointToGenerate = 0;
	    WavePoint pointOnHold = null;

        while (Delta >= Offset)
	    {
	        Delta = Delta - Offset;
	        nbPointToGenerate++;
	        pointOnHold = _waveGenerator.GetWavePoint(TimeElapsed);

	    }

	    if (nbPointToGenerate > 0)
	    {
            WavePoint lastPoint = HorizonWavePoints.Last();

	        for (int i = 0; i < nbPointToGenerate; i++)
	        {
                HorizonWavePoints.RemoveAt(0);
            }

	        float height = lastPoint.Height;

	        for (int i = 0; i < nbPointToGenerate; i++)
	        {
	            height += (pointOnHold.Height - lastPoint.Height)/nbPointToGenerate;    
	            HorizonWavePoints.Add(new WavePoint(height));
	        }
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
        
        Vector2[] edgePoints = new Vector2[NumberPoints];
        for (int i = 0; i < NumberPoints; i++)
        {
            edgePoints[i] = new Vector2(i * Offset - Delta, HorizonWavePoints[i].Height + MinHeight);
        }
        _edgeCollider2D.points = edgePoints;

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
