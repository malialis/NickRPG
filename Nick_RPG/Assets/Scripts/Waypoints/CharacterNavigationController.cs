using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(WaypointNavigator))]
public class CharacterNavigationController : MonoBehaviour
{

    public float movementSpeed = 5f;
    public float rotationSpeed = 120f;
    public float stopDistance = 2f;
    public Vector3 destination;
    private Vector3 lastPosition;
    private Vector3 velocity;
    public Animator anim;
    public bool reachedDestination;


    public enum variant
    {
        Navmesh,
        Transform
    }
    public variant State;

    //bool isMove;


  private void Awake()
    {
        movementSpeed = Random.Range(1f, 5f);
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (transform.position != destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;
            float destinationDistance = destinationDirection.magnitude;
            if(destinationDistance >= stopDistance)
            {
                reachedDestination = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
            else
            {
                reachedDestination = true;
            }
            velocity = (transform.position - lastPosition) / Time.deltaTime;
            velocity.y = 0;
            var velocityMagnitude = velocity.magnitude;
            velocity = velocity.normalized;
            var fwdDotProduct = Vector3.Dot(transform.forward, velocity);
            var rightDotProduct = Vector3.Dot(transform.right, velocity);

            anim.SetFloat("forwardMovement", fwdDotProduct);
        }
        lastPosition = transform.position;
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false;
    }

    
    void Animation()
    {
        if (State == variant.Navmesh)
        {

            if (agent.velocity.magnitude > 0)
            {
                anim.SetFloat("forwardSpeed", 1);
                isMove = true;
            }
            else
            {
                anim.SetFloat("forwardSpeed", 0);
                isMove = false;
            }
        }

        if (State == variant.Transform)
        {

            if (rb.velocity.magnitude > 0)
            {
                anim.SetFloat("forwardSpeed", 1);
                isMove = true;
            }
            else
            {
                anim.SetFloat("forwardSpeed", 0);
                isMove = false;
            }
        }
    }

   
}







/*
 * using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(WaypointNavigator))]
public class CharacterNavigationController : MonoBehaviour
{

    private Animator anim;
    private Rigidbody rb;
    private NavMeshAgent agent;
    [SerializeField] WaypointNavigator WayNavigator;

    public bool canMove;
    public Vector3 TargetPos;

    public float TrahsHold;
    public float stopDistance;
    public float movementSpeed;
    private Vector3 startPosition;
    private Vector3 startRotation;


    public enum variant
    {
        Navmesh,
        Transform
    }
    public variant State;

    bool isMove;


    public void SetTarget(Vector3 target)
    {
        TargetPos = target;

        if (State == variant.Navmesh)
        {
            if (agent == null)
                agent = GetComponent<NavMeshAgent>();

            agent.SetDestination(TargetPos);
        }

        if (State == variant.Transform)
        {
            Vector3 objPos = Vector3.MoveTowards(gameObject.transform.position, TargetPos, movementSpeed * Time.deltaTime);
        }

    }

    void Update()
    {

        if (canMove)
        {
            Animation();

            if (State == variant.Navmesh)
            {
                agent.SetDestination(TargetPos);
            }

            if (State == variant.Transform)
            {
                Vector3 objPos = Vector3.MoveTowards(
                   gameObject.transform.position, TargetPos, movementSpeed * Time.deltaTime);

                if ((Mathf.Abs(TargetPos.x - objPos.x) < TrahsHold) &&
                (Mathf.Abs(TargetPos.y - objPos.y) < TrahsHold) &&
                (Mathf.Abs(TargetPos.z - objPos.z) < TrahsHold))
                {
                    transform.position = startPosition;
                }
                else
                {
                    transform.position = objPos;
                }
            }
        }
    }

    public bool IsReached()
    {
        if (TargetPos != Vector3.zero)
        {
            stopDistance = Vector3.Distance(transform.position, TargetPos);
            if (stopDistance < TrahsHold)
            {
                Debug.Log("Reached");      
                return true;
            }
            else
                return false;
        }
        return false;
    }


    void Animation()
    {
        if (State == variant.Navmesh)
        {

            if (agent.velocity.magnitude > 0)
            {
                anim.SetFloat("forwardSpeed", 1);
                isMove = true;
            }
            else
            {
                anim.SetFloat("forwardSpeed", 0);
                isMove = false;
            }
        }

        if (State == variant.Transform)
        {

            if (rb.velocity.magnitude > 0)
            {
                anim.SetFloat("forwardSpeed", 1);
                isMove = true;
            }
            else
            {
                anim.SetFloat("forwardSpeed", 0);
                isMove = false;
            }
        }
    }


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();


        if (State == variant.Transform)
        {
            startPosition = gameObject.transform.position;
            startRotation = gameObject.transform.localEulerAngles;

        }
    }

}
*/


