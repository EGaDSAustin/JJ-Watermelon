using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenDisabler : MonoBehaviour
{
    private void Start()
    {
        RandomlyDisableChildren();
    }

    private void RandomlyDisableChildren()
    {
        foreach (Transform child in transform)
        {
            bool disablechild = Random.Range(0, 4) < 3;
            child.gameObject.SetActive(disablechild);
        }
    }
}
