using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] private Transform target;
        [SerializeField] private float offSet;
        [SerializeField] private NavMeshAgent nav;
        private Health health;

        private Ray lastRay;

        // Start is called before the first frame update
        void Start()
        {
            NavMeshAgent nav = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        void LateUpdate()
        {
            // Debug.DrawRay(lastRay.origin, lastRay.direction * 100);
            nav.enabled = !health.IsDead();
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        public void MoveTo(Vector3 destination)
        {
            nav.destination = destination;
            nav.isStopped = false;
        }

        public void StartMoveAction(Vector3 destination)
        {           
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }
        public void Cancel()
        {
            nav.isStopped = true;
        }

    }

}