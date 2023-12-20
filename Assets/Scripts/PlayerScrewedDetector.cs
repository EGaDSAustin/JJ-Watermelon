using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScrewedDetector : MonoBehaviour
{
    public float screwedMaxTime = 4f;
    public GameObject screwedPrompt;
    public LayerMask groundLayer;

    private float screwedTimer = 0f;

    private void Update()
    {
        bool isPlayerInContact = Physics.Raycast(transform.position, Vector3.down, 1f, groundLayer);

        if (isPlayerInContact)
        {
            screwedTimer = 0f;
        }
        else
        {
            screwedTimer += Time.deltaTime;

            if (screwedTimer >= screwedMaxTime)
            {
                if (screwedPrompt != null && !screwedPrompt.activeSelf)
                {
                    screwedPrompt.SetActive(true);
                    Time.timeScale = 0f;
                }
            }
        }
    }
}
