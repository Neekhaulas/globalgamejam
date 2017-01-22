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
    private PolygonCollider2D _polygonCollider2D;
    public float TimeElapsed;
    private bool _useCollider;

    private ShipWreckedGenerator _shipWreckedGenerator;

    public Vector2 SpawnPosition
    {
        get
        {
            return new Vector2(Offset * HorizonWavePoints.Count,
                HorizonWavePoints.Last().Height + minDist);
        }
    }

    public WavePoint Last
    {
        get
        {
            return HorizonWavePoints.Last();
        }
    }

    private float minDist
    {
        get { return 50; }
    }

    // Use this for initialization
    void Start()
    {
        _waveGenerator = GameObject.FindGameObjectWithTag("WaveGenerator").GetComponent<WaveGenerator>();

        _shipWreckedGenerator =
            GameObject.FindGameObjectWithTag("ShipWreckedGenerator").GetComponent<ShipWreckedGenerator>();

        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        if (_polygonCollider2D != null)
        {
            _useCollider = true;
        }
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
    void FixedUpdate()
    {
        HorizonGenerator();

        float oldDelta = Delta;
        Delta += Time.fixedDeltaTime * SpeedWave;

        int nbPointToGenerate = 0;
        WavePoint pointOnHold = null;

        while (Delta >= Offset)
        {
            Delta = Delta - Offset;
            nbPointToGenerate++;
            //pointOnHold = _waveGenerator.GetWavePoint(TimeElapsed);
        }

        float deltaTime = Time.fixedDeltaTime / nbPointToGenerate;

        if (nbPointToGenerate > 0)
        {
            for (int i = 0; i < nbPointToGenerate; i++)
            {
                HorizonWavePoints[0].IndexInTheList = -1;
                HorizonWavePoints.RemoveAt(0);
                TimeElapsed += deltaTime;
                HorizonWavePoints.Add(_waveGenerator.GetWavePoint(TimeElapsed + transform.position.z));
            }
        }
        else
        {
            TimeElapsed += Time.fixedDeltaTime;
        }

        // because i'm lazy, actualize the index at each frame
        for (int i = 0; i < HorizonWavePoints.Count; i++)
        {
            HorizonWavePoints[i].IndexInTheList = i;
        }

        _shipWreckedGenerator.TryToActualizeShipWrecked();
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

        if (_useCollider)
        {
            Vector2[] edgePoints = new Vector2[NumberPoints + 2];
            for (int i = 0; i < NumberPoints; i++)
            {
                edgePoints[i] = new Vector2(i * Offset - Delta, HorizonWavePoints[i].Height + MinHeight);
            }
            edgePoints[NumberPoints] = new Vector2(NumberPoints * Offset, 0);
            edgePoints[NumberPoints + 1] = new Vector2(0, 0);

            _polygonCollider2D.points = edgePoints;
        }
        

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

    public Vector2 GetPosition(int indexWavePoint)
    {
        return new Vector2(Offset * indexWavePoint - Delta,
            HorizonWavePoints[indexWavePoint].Height + MinHeight);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "TorsoCollider")
        {
            other.gameObject.GetComponent<TorsoCollider>().FallInWater();
        }
    }
}