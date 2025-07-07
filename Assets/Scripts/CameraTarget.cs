using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 defaultCameraPosition;

    void Start()
    {
        defaultCameraPosition = transform.position;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, defaultCameraPosition.z);
        transform.LookAt(target);
    }
}
