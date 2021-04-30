using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float offSet;
    [SerializeField] private NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        NavMeshAgent nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {        
        nav.destination = target.position;
    }


}
