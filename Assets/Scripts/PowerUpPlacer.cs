using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PowerUpPlacer : MonoBehaviour
{
    public GameObject[] objectsToPlace;
    public GameObject biggerBummPU;
    public GameObject moreBombsPU;
    public GameObject detonatorPU;
    public GameObject rollerSkatePU;
    public GameObject invulnerablePU;
    public GameObject ghostPU;
    public GameObject placerPU;
    private Vector3[] positions;

    void Start()
    {
        GameObject[] destructibles = GameObject.FindGameObjectsWithTag("Destructible");
        List<Vector3> positionList = new List<Vector3>();

        foreach (GameObject obj in destructibles)
        {
            Vector3 modifiedPosition = obj.transform.position;
            modifiedPosition.y += 0.5f;
            positionList.Add(modifiedPosition);
        }


        positions = positionList.ToArray();

        objectsToPlace = new GameObject[] {
            biggerBummPU, biggerBummPU, biggerBummPU,
            biggerBummPU, biggerBummPU, biggerBummPU,
            biggerBummPU, biggerBummPU, biggerBummPU,
            moreBombsPU, moreBombsPU, moreBombsPU,
            moreBombsPU, moreBombsPU, moreBombsPU,
            moreBombsPU, moreBombsPU, moreBombsPU
        };

        PlaceObjectsRandomly();
    }

    void PlaceObjectsRandomly()
    {
        List<Vector3> availablePositions = new List<Vector3>(positions);

        for (int i = 0; i < objectsToPlace.Length; i++)
        {
            if (availablePositions.Count > 0)
            {
                int randomIndex = Random.Range(0, availablePositions.Count);
                Instantiate(objectsToPlace[i], availablePositions[randomIndex], Quaternion.identity);
                availablePositions.RemoveAt(randomIndex);
            }
            else
            {
                Debug.LogWarning("Nincs elég pozíció minden GameObject elhelyezéséhez!");
                break;
            }
        }
    }

}
