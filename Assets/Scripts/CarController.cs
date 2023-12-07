using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Transform[] targetPos;
    [SerializeField] private int currentTargetIndex = 1;
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private int initTargetIndex = 0;
    [SerializeField] private GameObject carBody;
    float axisY = 0.0f;
    float tempAngle = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        SetInitPos();
       
    }

    private void SetInitPos()
    {
        transform.position = targetPos[initTargetIndex].position + Vector3.up * 0.5f;
        if (initTargetIndex < 3)
        {
            currentTargetIndex = initTargetIndex + 1;
        }
        else
        {
            currentTargetIndex = 0;
        }
    }
    void Update()
    {
        MoveForward();
        CheckTarget();
        Animate();
    }

    public void CheckTarget()
    {
        if (Vector3.Distance(transform.position, targetPos[currentTargetIndex].position) < 13.0f)
        {
            StartCoroutine(TurnRight());
            if (currentTargetIndex < 3)
            {
                currentTargetIndex++;
            }
            else
            {
                currentTargetIndex = 0;
            }
            
        }
    }
    
    private IEnumerator TurnRight()
    {
        float rotateY = 0.0f;
        float timeToRotate = 1.0f;
        float timeElapsed = 0.0f;
        Vector3 currentRotation = transform.rotation.eulerAngles;
        while (timeElapsed<timeToRotate)
        {
            timeElapsed += Time.deltaTime;
            rotateY = Mathf.Lerp(0.0f, 90.0f, timeElapsed / timeToRotate);
            transform.rotation = Quaternion.Euler(currentRotation + Vector3.up * rotateY);
            yield return null;
        }

        // transform.rotation = Quaternion.Euler(currentRotation + Vector3.up * rotateY);
        
    }

    public void MoveForward()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    public void Animate()
    {
        
        tempAngle += moveSpeed * Time.deltaTime * 100;
        
        axisY = Mathf.Sin(tempAngle * Mathf.Deg2Rad);
        carBody.transform.position = new Vector3(carBody.transform.position.x, carBody.transform.position.y + axisY * 0.02f, carBody.transform.position.z);
    }
}
