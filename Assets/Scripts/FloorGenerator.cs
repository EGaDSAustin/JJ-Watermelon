using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    public GameObject floorPrefab;
    public Transform player;
    public Transform bottomFloor;
    public float generationDistance = 50f;
    public Vector3 generationSpot = new Vector3(0, -33.4f, 101.71f);
    public bool ALLNEWFLOORSHAVEBEENGENERATEDWOOHOOOOOOOO;

    private int generationCount;
    private int lastGeneratedRotation;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bottomFloor = transform.GetChild(0).GetChild(0);
        ALLNEWFLOORSHAVEBEENGENERATEDWOOHOOOOOOOO = false;
        generationCount = 0;
        lastGeneratedRotation = -1;
    }

    private void Update()
    {
        CheckIfPlayerIsNearGenerationSpotIfSoGenerateANewFloor();
    }

    private void GenerateFloorAndSetNEWFLOORHASBEENGENERATEDWOOHOOOOOOOOToTrueIfNEWFLOORHASBEENGENERATEDWOOHOOOOOOOOIsntTrueAlready()
    {
        if (ALLNEWFLOORSHAVEBEENGENERATEDWOOHOOOOOOOO) return;

        GameObject newFloor = Instantiate(floorPrefab, bottomFloor.position, Quaternion.identity);

        int randomRotation = GetUniqueRandomRotation();
        float rotationAngle = randomRotation * 90f;
        newFloor.transform.Rotate(Vector3.up, rotationAngle);

        generationCount++;
        if (generationCount >= 2)
        {
            ALLNEWFLOORSHAVEBEENGENERATEDWOOHOOOOOOOO = true;
        }
        lastGeneratedRotation = randomRotation;
    }

    private void CheckIfPlayerIsNearGenerationSpotIfSoGenerateANewFloor()
    {
        Debug.Log(Vector3.Distance(player.position, bottomFloor.position));

        if (!ALLNEWFLOORSHAVEBEENGENERATEDWOOHOOOOOOOO && Vector3.Distance(player.position, bottomFloor.position) < generationDistance)
        {
            GenerateFloorAndSetNEWFLOORHASBEENGENERATEDWOOHOOOOOOOOToTrueIfNEWFLOORHASBEENGENERATEDWOOHOOOOOOOOIsntTrueAlready();
        }
    }

    private int GetUniqueRandomRotation()
    {
        int randomRotation = Random.Range(0, 4);
        while (randomRotation == lastGeneratedRotation)
        {
            randomRotation = Random.Range(0, 4);
        }

        return randomRotation;
    }
}
