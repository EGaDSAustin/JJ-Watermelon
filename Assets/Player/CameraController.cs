using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera { get; private set; }
    public Vector3 Offset
    {
        get { return orbitalTransposer.EffectiveOffset; }
        set { orbitalTransposer.m_FollowOffset = value; }
    }
    public Vector3 InitialOffset;

    private CinemachineOrbitalTransposer orbitalTransposer;

    // Start is called before the first frame update
    void Start()
    {
        VirtualCamera = GetComponent<CinemachineVirtualCamera>();
        orbitalTransposer = VirtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        Offset = InitialOffset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
