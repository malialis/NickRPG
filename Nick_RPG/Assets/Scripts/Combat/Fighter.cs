using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [Header("References")]
        private Health target;
        private Mover mover;
        private Animator anim;

        [Header("Attributes")]
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 0.5f;
        [SerializeField] float weaponDamage = 5f;

        private float timeSinceLastAttack = Mathf.Infinity;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            anim = GetComponent<Animator>();
        }
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;

            if (target.IsDead()) return;

            if (!GetIsInRange())
            {
                mover.MoveTo(target.transform.position);               
            }
            else
            {
                mover.Cancel();  
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform.position);
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                //this will trigger the Hit animation event
                TriggerAttackMethod();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttackMethod()
        {
            anim.ResetTrigger("stopAttacking");
            anim.SetTrigger("attack");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null)
            {
                return false;
            }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            print("On your Knees vodka swiller");
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();            
        }

        public void Cancel()
        {
            target = null;
            StopAttackMethod();
        }

        private void StopAttackMethod()
        {
            anim.ResetTrigger("attack");
            anim.SetTrigger("stopAttacking");
            anim.ResetTrigger("stopAttacking");
        }

        private void Hit()
        {
            //Animation Event
            if(target == null)
            {
                return;
            }
            target.TakeDamage(weaponDamage);
        }


    }
}
