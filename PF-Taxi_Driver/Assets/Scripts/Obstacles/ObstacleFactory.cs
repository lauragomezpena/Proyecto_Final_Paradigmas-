using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class ObstacleFactory : MonoBehaviour
{
    [SerializeField] private List<GameObject> roadPieces; // Lista de piezas del camino
    [SerializeField] private GameObject obstaclePrefab; // Prefab del obstáculo
    //[SerializeField] private float spawnInterval = 2f; // Intervalo entre spawns

    private void Start()
    {
        FindRoad();
        SpawnObstacle();

        // Comienza el proceso de spawn
        //InvokeRepeating(nameof(SpawnObstacle), 1f, spawnInterval);
    }

    private void SpawnObstacle()
    {
        if (roadPieces.Count == 0) return;

        // Selecciona una pieza del camino al azar
        int randomIndex = Random.Range(0, roadPieces.Count);
        GameObject selectedPiece = roadPieces[randomIndex];

        // Calcula una posición aleatoria dentro de los Bounds del MeshCollider
        Vector3 randomPosition = GetRandomPointInMesh(selectedPiece);
        Debug.Log(randomPosition);
        // Genera el obstáculo en la posición aleatoria
        GameObject newObstacle = Instantiate(obstaclePrefab, randomPosition, Quaternion.identity);
        newObstacle.transform.parent = this.transform;

    }

    private Vector3 GetRandomPointInMesh(GameObject roadPiece)
    {
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
        return new Vector3(randomX, bounds.center.y, randomZ);
    }

    void FindRoad()
    {


        GameObject roadPiecesParent = GameObject.FindGameObjectWithTag("RoadPieces");


        foreach (Transform child in roadPiecesParent.transform)
        {
            roadPieces.Add(child.gameObject);
        }
    }
}