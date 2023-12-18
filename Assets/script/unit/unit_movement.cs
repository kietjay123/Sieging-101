using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Units
{
    public class unit_movement : MonoBehaviour
    {
        private NavMeshAgent nav ;
        private NavMeshPath path;
        private float elapsed = 0.0f;

        
        private void OnEnable()
        {
            StopCoroutine("QueueDes");
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
        }


        private void OnDisable()
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
        }


        void Start()//debugging purpose
        {
            //Debug.Log(gameObject.name + transform.GetSiblingIndex());
            nav = gameObject.GetComponent<NavMeshAgent>();
            path = new NavMeshPath();
            elapsed = 0.0f;
        }

        public void moveUnit(List<Vector3> _des)
        {
            Debug.Log(_des.Count);
            foreach(Vector3 v in _des)
            {
                Debug.Log(v);
            }
            nav.SetDestination(_des[transform.GetSiblingIndex()]); 
            gameObject.GetComponent<unit_start>().stage = unit_template.stage.running;
        }

        public void RemoveMoveSignal()
        {
            GetComponentInParent<group_info>().unitMove -= moveUnit;
        }

        public void AddMoveSignal()
        {
            GetComponentInParent<group_info>().unitMove += moveUnit;
        }

        void Update()
        {
            // visualize the way to the goal every 1/5 second.
            elapsed += Time.deltaTime;
            if (elapsed > 0.2f)
            {
                elapsed -= 0.2f;
                NavMesh.CalculatePath(transform.position, nav.destination, NavMesh.AllAreas, path);
            }
            for (int i = 0; i < path.corners.Length - 1; i++)
                Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
        }



//////////////////////////////////////////check if unit is moving////////////////////////////////////
        public bool IsMoving()
        {
            if (!nav.pathPending)
            {
                if (nav.remainingDistance <= nav.stoppingDistance)
                {
                    if (!nav.hasPath && nav.velocity.sqrMagnitude == 0f)
                    {
                        return false;
                    }
                    return true;
                }
                
                return true;
            }
            
            return true;
        }

        public void StrCo()
        {
            StopCoroutine("QueueDes");
            StartCoroutine("QueueDes");
        }

        public IEnumerator QueueDes()
        {
            while(IsMoving())
            {
                yield return null;
            }
            GetComponent<Units.unit_movement>().enabled = false;
        }
    }
}
///////////////////////////////////////////////////////////////////////////////////////////////////////