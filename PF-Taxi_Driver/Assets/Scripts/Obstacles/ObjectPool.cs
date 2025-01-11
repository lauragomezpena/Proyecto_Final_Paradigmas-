using System.Collections.Generic;
using UnityEngine;



public class ObjectPool : MonoBehaviour
{

    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] private List<GameObject> roadPieces;
    [SerializeField] int poolSize = 20;
    GameObject[] pool;

    void Awake()
    {
        FindRoad();
        PopulatePool();
    }


    void Start()
    {
        ActivateAllObjects(); // Activate all objects in the pool
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            Vector3 position = GetRandomPointInMesh();
            pool[i] = Instantiate(obstaclePrefab, position, Quaternion.identity);
            pool[i].transform.parent = transform;
            pool[i].SetActive(false);
            
        }
    }

    void ActivateAllObjects()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i].SetActive(true);
        }
    }

    void EnableObjectInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }
    void FindRoad()
    {


        GameObject roadPiecesParent = GameObject.FindGameObjectWithTag("RoadPieces");


        foreach (Transform child in roadPiecesParent.transform)
        {
            roadPieces.Add(child.gameObject);
        }
    }

    private Vector3 GetRandomPointInMesh()

    {


        int randomIndex = Random.Range(0, roadPieces.Count);
        GameObject roadPiece = roadPieces[randomIndex];


        MeshCollider meshCollider = roadPiece.GetComponent<MeshCollider>();
        if (meshCollider == null)
        {
            Debug.LogError($"El GameObject {roadPiece.name} no tiene un MeshCollider.");
            return Vector3.zero;
        }

        //  los límites del MeshCollider
        Bounds bounds = meshCollider.bounds;

        // Genera coordenadas aleatorias dentro de los límites
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);


        return new Vector3(randomX, 0, randomZ);
    }

}


