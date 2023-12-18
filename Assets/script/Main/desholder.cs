using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desholder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        int i = other.transform.GetSiblingIndex();
        if ((other.gameObject.layer == 9) && (i == transform.GetSiblingIndex() - 16))
        {
            Debug.Log("fsdsdf");
            transform.parent.GetChild(i).GetComponent<unit_start>().stage = Units.unit_template.stage.idle;
            enabled = false;
        }
    }
}
