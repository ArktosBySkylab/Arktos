using System;
using System.Collections;
using System.Collections.Generic;
using Playground.Characters.Heros;
using Playground.Weapons;
using TreeEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Playground.Characters.Monsters
{
    public abstract class Monster : Character
    {
        public float speed;
        
        public Transform target;
        
        public float minimumDistance;
        
        public float initial_time;
        private float timeBtwAttack ;
        public int damage;
        public Transform attackPos;
        public float attackRange;
        
        protected new MonstersNames name;
        protected Monster(MonstersNames name, WeaponsNames primaryWeapon, int maxPv, int level) : base(maxPv, level)
        {
            this.name = name;
            timeBtwAttack = initial_time;
        }
        
        public override void Update()
        {
            //UsePrimaryWeapon = true;
            
            target = GameObject.FindGameObjectWithTag("Heros").transform;

            if (timeBtwAttack <= 0)
            {
                if (Vector2.Distance(transform.position,target.position ) > minimumDistance)
                    Attack();
                timeBtwAttack = initial_time;
            }
            if (pv <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }

        public void Attack()
        {
            Collider2D[] heroToDammage = Physics2D.OverlapCircleAll(attackPos.position, attackRange);
            Debug.Log(heroToDammage.Length);
            for (int i = 0; i < heroToDammage.Length; i++)
            {
                if (heroToDammage[i].GetComponent<Hero>() != null)
                {
                    if (heroToDammage[i].GetComponent<Hero>().transform.position.x - transform.position.x > 0f &&
                        heroToDammage[i].GetComponent<Hero>().transform.position.x - transform.position.x <= 1.5f)
                    {
                        transform.position = new Vector3( transform.position.x+0.5f,transform.position.y,transform.position.z);
                        Debug.Log(heroToDammage[i].GetComponent<Hero>().Name);
                        heroToDammage[i].GetComponent<Hero>().Pv -= damage;
                        Debug.Log(heroToDammage[i].GetComponent<Hero>().Pv);
                        transform.position = new Vector3( transform.position.x-0.5f,transform.position.y,transform.position.z);
                    }

                    if (heroToDammage[i].GetComponent<Hero>().transform.position.x - transform.position.x < 0f &&
                        heroToDammage[i].GetComponent<Hero>().transform.position.x - transform.position.x >= -1.5f)
                    {
                        transform.position = new Vector3( transform.position.x-0.5f,transform.position.y,transform.position.z);
                        Debug.Log(heroToDammage[i].GetComponent<Hero>().Name);
                        heroToDammage[i].GetComponent<Hero>().Pv -= damage;
                        Debug.Log(heroToDammage[i].GetComponent<Hero>().Pv);
                        transform.position = new Vector3( transform.position.x+0.5f,transform.position.y,transform.position.z);
                    }

                    else
                    {
                        Debug.Log(heroToDammage[i].GetComponent<Hero>().Name);
                        heroToDammage[i].GetComponent<Hero>().Pv -= damage;
                        Debug.Log(heroToDammage[i].GetComponent<Hero>().Pv);
                    }
                }
            }
        }

        public void TakeDamage(int damage)
        {
            pv -= damage;
            Debug.Log(pv);
        }
        
        protected override IEnumerator TheDeathIsComing()
        {
            yield return base.TheDeathIsComing();
        }
    }

}