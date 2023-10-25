using UnityEngine;
using UnityEngine.AI;

public class testnavmesh : MonoBehaviour
{
    private NavMeshAgent navmesh;
    void Start()
    {
        navmesh = GetComponent<NavMeshAgent>();
        navmesh.SetDestination(Vector3.zero);
    }

    private void Update()
    {
        Debug.Log(navmesh.destination);
    }
}
