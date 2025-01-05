using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObstacle : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;


    // Si necesitas sobreescribir el Start de la clase base
     void Start()
    {
        Debug.Log("Instanciando Obstacle");
        GameObject newObstacle = Instantiate(obstaclePrefab, new Vector3(15, 0, 60), Quaternion.identity);
        newObstacle.transform.parent = this.transform;
    }
}

