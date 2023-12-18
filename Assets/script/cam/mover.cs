using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour
{
    public float normalSpeed;
    public float fastSpeed;
    public float moveSpeed;
    public float mouseSpeed;
    public float moveTime;
    public float rotationA;
    public float maxZoom;
    public float minZoom;
    


    public Vector3 newPos;
    public Quaternion newRotation;
    public Transform camtrans;
    public Vector3 newZoom;
    public Vector3 zoomAmount;
    private Vector3 p1;
    private float speedScale = 1;

    [SerializeField]
    private Vector3 rotatedVector;



    void Start()
    { 
        newPos = transform.position; 
        newRotation = transform.rotation;
        newZoom = camtrans.localPosition;
    }
    void Update()
    {
        mouseInput();
        keyInput();
        move();
    }

    void keyInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = fastSpeed * speedScale;
        }
        else
        {
            moveSpeed = normalSpeed * speedScale;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPos += (transform.forward * moveSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPos += (transform.forward * -moveSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPos += (transform.right * -moveSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPos += (transform.right * moveSpeed);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationA);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationA);
        }
    }
    void mouseInput()
    {
        if (Input.GetMouseButtonDown(2))
        {
            p1 = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            rotatedVector = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * new Vector3((p1 - Input.mousePosition).x,0,(p1 - Input.mousePosition).y);
            newPos += rotatedVector * mouseSpeed * Mathf.Log(camtrans.localPosition.y,305);
            p1 = Input.mousePosition;
        }
        if(Input.mouseScrollDelta.y != 0)
        {
            if (Input.mouseScrollDelta.y > 0 && newZoom.y > minZoom || Input.mouseScrollDelta.y < 0 && newZoom.y < maxZoom)
            {
                newZoom += Input.mouseScrollDelta.y * zoomAmount;
                speedScale = 0.5f + (0.5f * (newZoom.y - minZoom)/ (maxZoom - minZoom));
            }
        }
    }
    
    void move()
    {   
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * moveTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * moveTime);
        camtrans.localPosition = Vector3.Lerp(camtrans.localPosition, newZoom, Time.deltaTime * moveTime);
    }
}
