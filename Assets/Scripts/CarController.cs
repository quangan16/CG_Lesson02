using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Transform[] targetPos;
    [SerializeField] private int currentTargetIndex = 1;
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private int initTargetIndex = 0;
    [SerializeField] private GameObject carBody;
    [SerializeField] private ControlMode controlMode;
    [SerializeField] private float steerRate;
    float axisY = 0.0f;
    float tempAngle = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        controlMode = ControlMode.Automatic;
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
        
        if (controlMode == ControlMode.Automatic)
        {
            MoveForward();
            CheckTarget();
        }else if (controlMode == ControlMode.Manual)
        {
            MoveWithInput();
        }
        
        Animate();
    }

    public Vector3 GetInput()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    [SerializeField] private  Vector3 desiredSteeing;
    public void MoveWithInput()
    {
        transform.position += GetInput().z * transform.forward * Time.deltaTime * moveSpeed;
        desiredSteeing += Vector3.up * GetInput().x * steerRate  * Time.deltaTime;
        desiredSteeing = Vector3.ClampMagnitude(desiredSteeing, 100.0f);
        if (GetInput().x == 0)
        {
            desiredSteeing.y = Mathf.MoveTowards(desiredSteeing.y, 0, Time.deltaTime * steerRate * 2);
        }
        if (Mathf.Abs(GetInput().z) > 0.05f)
        {
            transform.eulerAngles += desiredSteeing * Mathf.Sign(GetInput().z) * Time.deltaTime;
        }
       
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

public enum ControlMode
{
    Manual,
    Automatic
}
