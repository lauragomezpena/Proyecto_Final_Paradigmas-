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

    // Start is called before the first frame update
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

    // This method is now optional if no additional enabling logic is needed.
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

        // Selecciona una pieza del camino al azar
        int randomIndex = Random.Range(0, roadPieces.Count);
        GameObject roadPiece = roadPieces[randomIndex];


        MeshCollider meshCollider = roadPiece.GetComponent<MeshCollider>();
        if (meshCollider == null)
        {
            Debug.LogError($"El GameObject {roadPiece.name} no tiene un MeshCollider.");
            return Vector3.zero;
        }

        // Obtén los límites del MeshCollider
        Bounds bounds = meshCollider.bounds;

        // Genera coordenadas aleatorias dentro de los límites
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);


        // Asegúrate de que la altura (Y) sea correcta
        //return new Vector3(randomX, bounds.center.y, randomZ);
        return new Vector3(randomX, 0, randomZ);
    }

    //private void SpawnObstacle()
    //{
    //    if (roadPieces.Count == 0) return;

    //    // Selecciona una pieza del camino al azar
    //    int randomIndex = Random.Range(0, roadPieces.Count);
    //    GameObject selectedPiece = roadPieces[randomIndex];

    //    // Calcula una posición aleatoria dentro de los Bounds del MeshCollider
    //    Vector3 randomPosition = GetRandomPointInMesh(selectedPiece);
    //    Debug.Log(randomPosition);
    //    // Genera el obstáculo en la posición aleatoria
    //    GameObject newObstacle = Instantiate(obstaclePrefab, randomPosition, Quaternion.identity);
    //    newObstacle.transform.parent = this.transform;

    //}
}


