using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units;


public class unit_handler : MonoBehaviour
{   
    public Transform pos;
    private GameObject spawnedGroup;
    private GameObject spawnedUnits;
    private GameObject spawnedDes;
    private int workerGroupCount;
    private int workerCount;

    public int workerGroupPopulation;
    private List<Vector3> spawnPos; 

    private void Start()
    {
        workerGroupPopulation = 16;
        workerGroupCount = 1;
        spawnPos = new List<Vector3>
        {
            new Vector3(0,0,0),
            new Vector3(1,0,0),
            new Vector3(1,0,1),
            new Vector3(0,0,1),
            new Vector3(-1,0,1),
            new Vector3(-1,0,0),
            new Vector3(-1,0,-1),
            new Vector3(0,0,-1),
            new Vector3(1,0,-1),
            new Vector3(2,0,-1),
            new Vector3(2,0,0),
            new Vector3(2,0,1),
            new Vector3(2,0,2),
            new Vector3(1,0,2),
            new Vector3(0,0,2),
            new Vector3(-1,0,2),
        };
    }
    void Update()//refactor
    { 
        if (Input.GetKeyDown(KeyCode.N))
        {
            spawnedGroup = ObjectPooler.groupEmptyPool.Get();
            spawnedGroup.transform.position = pos.position;
            spawnedGroup.transform.SetParent(transform);
            spawnedGroup.name = "worker group " + workerGroupCount++;
            
            workerCount = 0;
            for (int i = 0; i < workerGroupPopulation; i++)
            {
                spawnedUnits = ObjectPooler.ownWorkerPool.Get();
                spawnedUnits.transform.SetParent(spawnedGroup.transform);
                spawnedUnits.transform.localPosition = spawnPos[i];
                spawnedUnits.transform.rotation = Quaternion.identity;
                spawnedUnits.name = "worker" + workerCount ++; 
            }
            
            workerCount = 0;
            for (int i = 0; i < workerGroupPopulation; i++)
            {
                spawnedDes = ObjectPooler.infdesPlcHolderPool.Get();
                spawnedDes.transform.SetParent(spawnedGroup.transform);
                spawnedDes.name = "des" + workerCount ++;
            }
            spawnedGroup.AddComponent<group_info>();
        }
    }
}