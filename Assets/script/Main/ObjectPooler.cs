using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class ObjectPooler : MonoBehaviour
{
    public static ObjectPool<GameObject> ownWorkerPool;
    public static ObjectPool<GameObject> infdesPlcHolderPool;
    public static ObjectPool<GameObject> groupEmptyPool;
    public int poolSizeOWorker = 150;
    public int poolSizePlcHolder = 300;
    public int poolSizeGroupEmpty = 300;


    //debug purpose
    public int inactPlcHolder;
    public int actPlcHolder;
    public int actOWorker;
    public int inactOWorker;
    public int actGroup;
    public int inactGroup;




    void Awake()
    {
        ownWorkerPool = new ObjectPool<GameObject>(CreateOwnWorker, OnSpawnOWorker, OnDeSpwnOworker, null, true, poolSizeOWorker);
        infdesPlcHolderPool = new ObjectPool<GameObject>(CreatePlcHolder, OnSpawnPlcHolder, OnDeSpwnPlcHolder, null, true, poolSizePlcHolder);
        groupEmptyPool = new ObjectPool<GameObject>(CreateGroup, OnSpawnGroup, OnDeSpwnGroup, null, true, poolSizeGroupEmpty);

    }

    GameObject CreateOwnWorker()
    {
        GameObject ownWorker = Instantiate(unit_type_handler.i.Own_worker_Template.unitPrefab);
        ownWorker.AddComponent<unit_start>();
        return ownWorker;
    }

    void OnSpawnOWorker(GameObject _OWorker)
    {
        _OWorker.SetActive(true);
    }
    
    void OnDeSpwnOworker(GameObject _OWorker)
    {
        _OWorker.SetActive(false);
    }



    GameObject CreatePlcHolder()
    {
        GameObject plcHolder = Instantiate(unit_type_handler.i.checkPointHolder);
        return plcHolder;
    }

    void OnSpawnPlcHolder(GameObject _PlcHolder)
    {
        //_PlcHolder.SetActive(true);
    }

    void OnDeSpwnPlcHolder(GameObject _PlcHolder)
    {
        _PlcHolder.SetActive(false);
    }



    GameObject CreateGroup()
    {
        GameObject group = Instantiate(unit_type_handler.i.groupEmptyObject);
        return group;
    }

    void OnSpawnGroup(GameObject _Group)
    {
        _Group.SetActive(true);
    }

    void OnDeSpwnGroup(GameObject _Group)
    {
        _Group.SetActive(false);
    }

    void Update()//debug purpose
    {
        inactOWorker = ownWorkerPool.CountInactive;
        actOWorker = ownWorkerPool.CountActive;
        inactPlcHolder = infdesPlcHolderPool.CountInactive;
        actPlcHolder = infdesPlcHolderPool.CountActive;
        inactGroup = groupEmptyPool.CountInactive;
        actGroup = groupEmptyPool.CountActive;
    }
}
