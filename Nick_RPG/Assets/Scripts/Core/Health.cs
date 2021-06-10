using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float healthPoints = 100f;

        private Animator anim;
        private bool isDead = false;
        
        public bool IsDead()
        {
            return isDead;
        }

        void Start()
        {
            anim = GetComponent<Animator>();
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            print(healthPoints);
            if(healthPoints <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            if (isDead) return;
            isDead = true;
            print("woah is me, i have dieded");
            anim.SetTrigger("die");           
            GetComponent<ActionScheduler>().CancelCurrentAction();
            //add nav mesh disabled here at some point nav.enabled = !health.IsDead();
        }
    }
}
