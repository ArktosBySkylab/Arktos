using System;
using System.Collections;
using Playground.Characters.Heros;
using Playground.Weapons;
using UnityEditor;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace Playground.Characters.Monsters
{
    public abstract class Monster : Character
    {
        //pour modifier la vitesse des monstres veuillez modifier direct via l'IA
        public float speed;
        
        private Transform target;
        
        // Animator variables
        protected Vector3 OldPosition;
        
        public float minimumDistance;
        
        public float initial_time;
        private float timeBtwAttack ;
        public int damage;
        public Transform attackPos;
        public float attackRange;
        
        protected new MonstersNames name;
        public GameObject DropOnDeath;
        protected Monster(MonstersNames name, WeaponsNames primaryWeapon, int maxPv, int level) : base(maxPv, level)
        {
            this.name = name;
            timeBtwAttack = initial_time;
            
        }

        public virtual void Start() =>
             OldPosition = gameObject.transform.position;
        
        public override void Update()
        {
            base.Update();
            target = GameObject.FindGameObjectWithTag("Heros").transform;

            if (timeBtwAttack <= 0)
            {
                if (Vector2.Distance(transform.position,target.position ) > minimumDistance)
                    Attack();
                timeBtwAttack = initial_time;
            }
            else
                timeBtwAttack -= Time.deltaTime;
        }

        public virtual void FixedUpdate()
        {
             horizontalMove = Math.Abs(OldPosition.x - transform.position.x);
             OldPosition = transform.position;
             
             if (horizontalMove > 0.01)
                animator.SetBool("IsRunning", true);
            
            else
                animator.SetBool("IsRunning", false);
             
            
        }

        public void Attack()
        {
            Collider2D[] heroToDammage = Physics2D.OverlapCircleAll(attackPos.position, attackRange);
            
            for (int i = 0; i < heroToDammage.Length; i++)
            {
                if (heroToDammage[i].GetComponent<Hero>() != null)
                {
                    /*
                    if (heroToDammage[i].GetComponent<Hero>().transform.position.x > transform.position.x )
                    {
                        transform.position = new Vector3( transform.position.x+1f,transform.position.y,transform.position.z);
                        heroToDammage[i].GetComponent<Hero>().Pv -= damage;
                        // Debug.Log(heroToDammage[i].GetComponent<Hero>().Pv);
                    }

                    else
                    { 
                        transform.position = new Vector3( transform.position.x-1f,transform.position.y,transform.position.z);
                        heroToDammage[i].GetComponent<Hero>().Pv -= damage;
                        // Debug.Log(heroToDammage[i].GetComponent<Hero>().Pv);
                    }
                    */
                    animator.SetInteger("IsFighting", 1);
                    heroToDammage[i].GetComponent<Hero>().Pv -= damage;
                }
            }
        }

        public void TakeDamage(int damage)
        {
            pv -= damage;
            // Debug.Log(pv);
        }
        
        protected override IEnumerator TheDeathIsComing()
        {
            if (this.name == MonstersNames.BossMonster)
            {
                Instantiate( DropOnDeath, transform.position, Quaternion.identity);
            }
            yield return base.TheDeathIsComing();
            Destroy(gameObject);
        }
    }

}