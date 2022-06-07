using System.Collections;
using Playground.Characters.Heros;
using Playground.Weapons;
using UnityEditor;
using UnityEngine;

namespace Playground.Characters.Monsters
{
    public abstract class Monster : Character
    {
        //pour modifier la vitesse des monstres veuillez modifier direct via l'IA
        public float speed;
        
        private Transform target;
        
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
            
            for (int i = 0; i < heroToDammage.Length; i++)
            {
                if (heroToDammage[i].GetComponent<Hero>() != null)
                {
                    if (heroToDammage[i].GetComponent<Hero>().transform.position.x > transform.position.x )
                    {
                        heroToDammage[i].GetComponent<Hero>().Pv -= damage;
                    }
                    else
                    { 
                        heroToDammage[i].GetComponent<Hero>().Pv -= damage;
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
            if (this.name == MonstersNames.BossMonster)
            {
                Instantiate( DropOnDeath, transform.position, Quaternion.identity);
            }
            yield return base.TheDeathIsComing();
        }
    }

}