using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selection_component : MonoBehaviour
{
    void Start()//refactor to read unit type 
    {
        if (gameObject.GetComponent<unit_start>().ownership == Units.unit_template.unitOwnership.enemy)
        {
            if (gameObject.GetComponent<enemySelect>() != null)
            {
                gameObject.GetComponent<enemySelect>().enabled = true;
            }
            else
            {
                gameObject.AddComponent<enemySelect>();
            }
        }
        else if (gameObject.GetComponent<unit_start>().ownership == Units.unit_template.unitOwnership.ally)
        {
            if (gameObject.GetComponent<allySelect>() != null)
            {
                gameObject.GetComponent<allySelect>().enabled = true;
            }
            else
            {
                gameObject.AddComponent<allySelect>();
            }
        }
        else
        {
            if (gameObject.GetComponent<ownSelect>() != null)
            {
                gameObject.GetComponent<ownSelect>().enabled = true;
            }
            else
            {
                gameObject.AddComponent<ownSelect>();
            }
            gameObject.GetComponent<Units.unit_movement>().enabled = true;
            gameObject.GetComponent<Units.unit_movement>().AddMoveSignal();
        }
    }

    private void OnDestroy()
    {
        if (gameObject.GetComponent<unit_start>().ownership == Units.unit_template.unitOwnership.enemy)
        {
            gameObject.GetComponent<enemySelect>().enabled = false;
        }
        else if (gameObject.GetComponent<unit_start>().ownership == Units.unit_template.unitOwnership.ally)
        {
            gameObject.GetComponent<allySelect>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<Units.unit_movement>().RemoveMoveSignal();
            gameObject.GetComponent<ownSelect>().enabled = false;
            //remove after reaching destination 
            if (GetComponent<Units.unit_movement>().IsMoving() == false)
            {
                gameObject.GetComponent<Units.unit_movement>().enabled = false;
            }
            else 
            {
                gameObject.GetComponent<Units.unit_movement>().StrCo();
            }
        }
    }
}
