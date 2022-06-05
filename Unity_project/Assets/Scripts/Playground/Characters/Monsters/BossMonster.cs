
using Pathfinding;
using UnityEngine;
using Playground.Weapons;

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
        public float pathUpdateSeconds = 0.5f;

        [Header("Physics")]
        public float nextWaypointDistance = 3f;
        private bool FacingRight = true;

        private Path path;
        private int currentWaypoint = 0;
        Seeker seeker;
        Rigidbody2D rb;
        
         public void Start()
         {
             // le seeker c'est un composante du package A* 
             seeker = GetComponent<Seeker>();
             // j'ai pas reussi a reutiliser le CC2D ducoup je recupere le rigidbody pour faire 
             // faire bouger le monstre 
             rb = GetComponent<Rigidbody2D>();

             target = GetTarget();
             InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
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
         private void OnPathComplete(Path p)
         { 
             if (!p.error)
             {
                 path = p;
                 currentWaypoint = 0;
             }
         }
    
         private void FixedUpdate()
         {
             if (TargetInDistance())
             {
                 PathFollow();
             }
         }
         

         private void UpdatePath()
         {
             if (TargetInDistance() && seeker.IsDone())
             {
                 //calcule du path entre les positions du monstres 
                 // on passe la methode OnPathComplete en parametre pour verifier l'existance d'un path,
                 seeker.StartPath(rb.position, target.position, OnPathComplete);
             }
         }
         
         private void PathFollow()
         {
             if (path == null)
             {
                 return;
             }
     
             // condition pour l'arret du monstre qd il est a proximite du joueur 
             if (currentWaypoint >= path.vectorPath.Count)
             {
                 return;
             }
             
             // calcul de la direction 
             Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
             Vector2 force = direction * speed * Time.deltaTime*1.3f;
             
             // Movement
             rb.AddForce(force*1.5f);
             
             // flip le monstre pour qu'il regarde du bon cote 
             if (force.x > 0 && !FacingRight) 
                 Flip();
             else if (force.x <0 && FacingRight) 
                 Flip();

             // on augmente le currentwaypoint qui l'arret du monstre lorsque superieur ou egal au max
             // -> a modifier avec le calcule de la distance directement prcq sert a rien 
        
             float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]); 
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