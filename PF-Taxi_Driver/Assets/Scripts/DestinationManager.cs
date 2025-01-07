using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class DestinationManager : MonoBehaviour
{
    public List<GameObject> destinations;
    private GameObject currentDestination;
    [SerializeField] GameObject destinationMark;

    private void Start()
    {

        GenerateDestinations();
        NewDestination();   
    }
    void NewDestination()

    {

        
        int randomIndex = Random.Range(0, destinations.Count);
        currentDestination = destinations[randomIndex];
        currentDestination.gameObject.SetActive(true);
        Instantiate(destinationMark, currentDestination.transform.position, Quaternion.identity);
        Debug.Log("Destination determined");
    }

    void GenerateDestinations()
    {

        GameObject pathParent = GameObject.FindGameObjectWithTag("Destinations");

        foreach (Transform child in pathParent.transform)
        {

            destinations.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }

    }




}
