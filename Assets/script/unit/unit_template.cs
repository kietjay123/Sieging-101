using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Units
{
    [CreateAssetMenu(fileName = "New unit", menuName = "New unit")]
    public class unit_template : ScriptableObject
    {
        //add more later to control animation
        public enum stage
        {
            idle,
            running
        }
        public enum unitType
        {
            worker,
            rifleMan
        }

        public enum unitOwnership
        {
            player,
            ally,
            enemy
        }
        public enum damageType
        {
            AOE,
            physic
        }
        public stage unitstage;

        public unitType type;

        public unitOwnership ownership;

        public damageType damageT;

        public string unitName;

        public GameObject unitPrefab;
        
        public int cost;
        public int attack;
        public int health;
        public int physicArmor;
        public int aoeArmor;
    }
}
