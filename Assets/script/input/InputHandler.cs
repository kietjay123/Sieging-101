using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    RaycastHit hit1;
    RaycastHit hit2;
    Vector3 p1;
    Vector3 p2;
    Vector3 t1;
    Vector3 t2;
    Vector3 turnVector;
    bool turnSelect;

    void Start()
    {
         
    }
    void Update()
    {
        mouseInput();
    }

    private void mouseInput()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            p1 = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(p1);
            if(Physics.Raycast(ray,out hit1, 50000.0f, (1 << 8)))
            {
                t1 = hit1.point;
            }
        }

        if (Input.GetMouseButton(1))
        {
            if((p1 - Input.mousePosition).magnitude > 20)
            {
                turnSelect = true;
            }
            else 
            {
                turnSelect = false;
            }
        }

        
        if (Input.GetMouseButtonUp(1))
        {
            p2 = Input.mousePosition;
            if (turnSelect == true)
            {
                Ray ray = Camera.main.ScreenPointToRay(p2);
                if(Physics.Raycast(ray,out hit2, 50000.0f, (1 << 8)))
                {
                    t2 = hit2.point;
                    turnVector = new Vector3(t2.x - t1.x, 0, t2.z - t1.z).normalized;
                    Debug.DrawLine(t1, t2, Color.blue, 5);
                    GameEvents.current.RightMousePressed(t1, turnVector);
                }
                else
                {
                    turnVector = Vector3.zero; 
                    Debug.Log("p2 out of bound");
                    GameEvents.current.RightMousePressed(t1, turnVector);
                }
            }
            else
            {
                turnVector = Vector3.zero; 
                GameEvents.current.RightMousePressed(t1, turnVector);
            }
        }
    }
}
