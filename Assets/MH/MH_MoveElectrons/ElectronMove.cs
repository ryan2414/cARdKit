using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronMove : MonoBehaviour
{
    List<Transform> departments = new List<Transform>();
    int index_Department;

    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        index_Department = 0;
    }

    void Update()
    {
        Move();
    }

    public void GetDepart(List<Transform> movePoints)
    {
        departments = movePoints;
        index_Department++;
    }

    void Move()
    {
        if (departments != null)
        {
            if (!CircuitManager_MH.instance.isCircuitActivated)
            {
                Invoke("DestroySelf", 1);
            }

            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, departments[index_Department].position, moveSpeed * Time.deltaTime);
            gameObject.transform.LookAt(departments[index_Department].position);

            if (Vector3.Distance(gameObject.transform.position, departments[index_Department].position) < 0.001f)
            {
                index_Department++;
                if (index_Department > (departments.Count - 1))
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
