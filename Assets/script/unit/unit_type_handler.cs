using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units;

public class unit_type_handler : MonoBehaviour
{
    private static unit_type_handler _i;
    public static unit_type_handler i
    {
        get
        {
            if (_i == null) 
            {
                _i = (Instantiate(Resources.Load("prefab and template/unit_type_handler")) as GameObject).GetComponent<unit_type_handler>();
            }
            return _i;
        }
    }
    
    
    public unit_template Own_worker_Template;
    public unit_template Own_rifle_man_Template;
    public unit_template Ally_worker_Template;
    public unit_template Ally_rifle_man_Template;
    public unit_template Enemy_worker_Template;
    public unit_template Enemy_rifle_man_Template;
    public GameObject groupEmptyObject;
    public GameObject checkPointHolder;

}
