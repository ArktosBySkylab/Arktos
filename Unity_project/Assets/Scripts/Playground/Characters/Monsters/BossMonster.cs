
using System;
using System.Numerics;
using Pathfinding;
using UnityEngine;
using Playground.Weapons;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Playground.Characters.Monsters
{
    public class BossMonster: Monster

    {
        public BossMonster() : base(MonstersNames.BossMonster, WeaponsNames.Stick, 1000, 1)
        {
        }

        [Header("Pathfinding")]
        private Transform target;
        public float activateDistance = 50f;

        [Header("Physics")]
        public float nextWaypointDistance = 3f;
        private bool FacingRight = true;
        private RaycastHit2D isGrounded;
        private int getGroundPos = 0;

        private float initialPos;
        private int currentWaypoint = 0;
        Rigidbody2D rb;

         public override void Start()
         {
             // j'ai pas reussi a reutiliser le CC2D ducoup je recupere le rigidbody pour faire 
             // faire bouger le monstre 
             rb = GetComponent<Rigidbody2D>();
             target = GetTarget();
             base.Start();
         }
           
         
         //function used to select the closest player to chase 
         // we asume there is a player in the room 
         public Transform GetTarget()
         {
             GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Heros");
             Transform targ  = gameObjects[0].transform;
             
             foreach (var tar in gameObjects)
             {
                 if (Vector2.Distance(tar.transform.position, rb.position) <
                     Vector2.Distance(targ.position, rb.position))
                 {
                     targ = tar.transform;
                 }
             }
             return targ;
         }
         
         
         // bool pour activer/desactiver le monstre en fonction de la distance avec le joueur 
         private bool TargetInDistance()
         {
             return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
         }
    
    
         //verification du path 
         public override void FixedUpdate()
         {
             base.FixedUpdate();
             if (TargetInDistance())
             {
                 PathFollow();
             }
         }
         
         private void PathFollow()
         {
             Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y );
             isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);
             if (getGroundPos == 0 && isGrounded)
             {
                 //5.5 est la distance entre le bidou et les pieds du monstres a l'echelle
                 initialPos = (float)Math.Round(transform.position.y)+5.5f;
             }
             
     
             // condition pour l'arret du monstre qd il est a proximite du joueur 
             if (Vector2.Distance(target.position,this.transform.position) <= minimumDistance)
             {
                 return;
             }
             
             // Do not move if the boss is attacking
             if (AlreadyFighting)
                 return;
             
             // calcul de la direction 
             Vector2 direction = (target.position.x > transform.position.x ? new Vector2(1,0):new Vector2(-1,0));
             //Debug.Log("x : "+direction.x);
             //Debug.Log("tr : "+transform.position.x);
             
             Vector2 force = direction * speed * Time.deltaTime*1.3f;
             
             // Movement
             rb.AddForce(force*1.5f);
             // Debug.Log(initialPos);
             if (initialPos != null)
             {
                 if (Math.Abs(rb.position.y - (initialPos)) > 0.1f)
                 {
                     rb.AddForce(new Vector2(0,initialPos - (-1)*rb.position.y)*20f);
                 }
             }

             // flip le monstre pour qu'il regarde du bon cote 
             if (target.transform.position.x - transform.position.x >0&& !FacingRight) 
                 Flip();
             else if (target.transform.position.x - transform.position.x <0 && FacingRight) 
                 Flip();

             // on augmente le currentwaypoint qui l'arret du monstre lorsque superieur ou egal au max
             // -> a modifier avec le calcule de la distance directement prcq sert a rien 
             float distance = Vector2.Distance(rb.position, target.position); 
             if (distance < nextWaypointDistance) 
             { 
                 currentWaypoint++;
             }
         }

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            FacingRight = !FacingRight;
    
            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
