using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float offset;
    [SerializeField] private float height;
    [SerializeField] private float rotationSpeed;

    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateCameraBehavior();
    }

    void Init()
    {
        offset = 5.0f;
    }

    public void UpdateCameraBehavior()
    {
        if (!target) return;
        UpdateCamPosition();
        UpdateCamRotation();

    }
    
    

    void UpdateCamPosition()
    {
        Vector3 desiredPosition = target.transform.position - (target.transform.forward * offset);
        desiredPosition.y += height;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * rotationSpeed);

        Vector3 desiredDirection = (target.transform.position - transform.position) + Vector3.up * 3;
        transform.forward = desiredDirection;
    }

    void UpdateCamRotation()
    {
        // transform.rotation = Quaternion.Mo
    }
}
