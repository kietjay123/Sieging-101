using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class group_info : MonoBehaviour
{
    private List<unit_start> unit_starts;
    public GameObject[] members;
    public int groupSize;
    GameObject leader;
    GameObject desHolder;
    Vector3 direction;
    Vector3 middle;
    List<Vector3> movePos;
    public enum morale 
    {
        good,
        normal,
        bad
    }
    public int groupHealth;

    public event Action<List<Vector3>> unitMove;

    void Start()
    {
        getLeader();
        CountMember();
        getMiddle();
        groupSize = members.Length;
        direction = leader.transform.forward;
    }

    void getMiddle()
    {
        middle = leader.transform.position;
    }
    void getLeader()
    {
        leader = transform.GetChild(0).gameObject;//change later
    }

    void Update()
    {

    }

    private void OnEnable()
    {
        GameEvents.current.midPos += calPos;
    }

    private void OnDisable()
    {
        GameEvents.current.midPos -= calPos;
    }

    void CountMember()
    {
        unit_starts = new List<unit_start>{};
        unit_starts.AddRange(GetComponentsInChildren<unit_start>());
        members = new GameObject[unit_starts.Count];
        for (int i = 0; i < unit_starts.Count; i++)
        {
            members[i] = unit_starts[i].gameObject;
        }
    }


// assign formation pos to each unit/////
    public void calPos(Vector3 _des, Vector3 _turnVector)
    {
        if (unitMove != null)
        {
            if ( _turnVector != Vector3.zero )
            {
                direction = _turnVector;
            }
            else
            {
                direction = (_des - middle).normalized;
            }
            movePos = new List<Vector3>{};
            int i = 0;
            int l = 0;
            int n = 1;
            int mod = 1;
            Vector3 pos = Vector3.zero;
            if(clear(_des))
            {
                movePos.Add(_des);
                desHolder = transform.GetChild(l + 16).gameObject;
                desHolder.transform.position = Cast(FinalDes(pos, direction, _des));
                Debug.DrawLine(_des, new Vector3( _des.x,  _des.y + 5, _des.z), Color.red, 15f);
                i++;
                l++;
            }
            else
            {
                i++;
            }
            while( l < groupSize )
            {
                if ( i > n * (n + 1))
                {
                    n += 1;
                    mod *= -1;
                }
                else
                {
                    //do nothing
                }

                if ( n >= i - n * (n - 1))
                {
                    pos.x += mod;
                    //Debug.Log("0 " + mod);
                }

                else
                {
                    pos.z += mod;
                    //Debug.Log(mod + " 0");
                }

                if (clear(Cast(FinalDes(pos, direction, _des))))
                {
                    movePos.Add(Cast(FinalDes(pos, direction, _des)));
                    desHolder = transform.GetChild(l + 15).gameObject;
                    desHolder.transform.position = Cast(FinalDes(pos, direction, _des));
                    Debug.DrawLine(FinalDes(pos, direction, _des), new Vector3( FinalDes(pos, direction, _des).x, FinalDes(pos, direction, _des).y + 5, FinalDes(pos, direction, _des).z), Color.red, 15f);
                    i++;
                    l++;
                }

                else
                {
                    i++;
                    l++;
                    Debug.Log("sdfs");
                }
            }
            unitMove(movePos);
        }
    }

    private  bool clear(Vector3 _pos)
    {
        UnityEngine.AI.NavMeshHit hit;
        if (_pos == Vector3.zero)
        {
            Debug.Log("1");
            return false;
        }
        else if(UnityEngine.AI.NavMesh.SamplePosition(_pos, out hit, 0.05f, UnityEngine.AI.NavMesh.GetAreaFromName("Walkable")))//fix when have precaculated nav mesh
        {
            if ( placeCheck(_pos))
            {
                Debug.Log("2");

                return true;
            }
            else
            {
                Debug.Log("3");
                return false;
            }
        }
        else
        {
            Debug.Log("4");
            return false;
        }
    }
    
    public bool placeCheck(Vector3 _pos)
    {
        Collider[] check = Physics.OverlapCapsule(_pos, new Vector3(_pos.x, _pos.y + 2, _pos.z), 0.5f, (1 << 9), QueryTriggerInteraction.Ignore);
        if (check.Length != 0)
        {
            Debug.Log("5");
            for(int i = 0; i < check.Length; i++)
            {
                unit_start cache = check[i].gameObject.GetComponent<unit_start>();
                if(cache)
                {
                    if ((cache.stage == Units.unit_template.stage.running) || (cache.gameObject == gameObject))
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            Debug.Log("6");
            return true;
        }
        else
        {
            Debug.Log("7");
            return true;
        }
    }


// returning vector 0 to check clearance may cause problem

    private Vector3 FinalDes(Vector3 _vec, Vector3 _direction, Vector3 _des)//rotate and add _des vecor to result to give final des
    {
        float amount = Vector3.SignedAngle(new Vector3(0, 0, 1), _direction, new Vector3(0, 1, 0));
        float x1 = _vec.x;
        float y1 = _vec.z;
        Vector3 result = new Vector3(x1 * Mathf.Cos(-amount * Mathf.Deg2Rad) - y1 * Mathf.Sin(-amount * Mathf.Deg2Rad), 0, x1 * Mathf.Sin(-amount * Mathf.Deg2Rad) + y1 * Mathf.Cos(-amount * Mathf.Deg2Rad));
        result += _des;// change to call method get middle
        //Debug.DrawLine(result, new Vector3(result.x, result.y + 5, result.z), Color.red, 1f);
        return result;
    }

    private Vector3 Cast(Vector3 _vec)
    {
        RaycastHit hit;
        Ray ray = new Ray(new Vector3(_vec.x, 50, _vec.z), new Vector3(0,-1, 0));
        if(Physics.Raycast(ray,out hit, 50000.0f, (1 << 8)))
        { 
            return hit.point; 
        }
        else
        {   
            Debug.Log("no terrain at pos cast");
            return Vector3.zero;
        }
    }
}
