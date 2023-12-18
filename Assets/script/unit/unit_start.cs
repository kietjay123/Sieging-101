using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units;

public class unit_start : MonoBehaviour//refactor
{
    [SerializeField]
    private unit_template template;
    [SerializeField]
    private unit_template.unitType type;
    [SerializeField]
    public unit_template.unitOwnership ownership;
    [SerializeField]
    private unit_template.damageType damageT;
    [SerializeField]
    public unit_template.stage stage;
    [SerializeField]
    public string unitName;

    [SerializeField]
    private int cost;
    [SerializeField]
    private int attack;
    [SerializeField]
    private int health;
    [SerializeField]
    private int physicArmor;
    [SerializeField]
    private int aoeArmor;

    private void Start() 
    {
        //change when add more type
        template = unit_type_handler.i.Own_worker_Template;
        //read from template
        type = template.type;
        ownership = template.ownership;
        damageT = template.damageT;
        unitName = template.unitName;
        cost = template.cost;
        attack = template.attack;
        health = template.health;
        physicArmor = template.physicArmor;
        aoeArmor = template.aoeArmor;
        stage = template.unitstage; 
    }
}
