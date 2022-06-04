using Pathfinding;
using Playground.Weapons;
using UnityEngine;
using Random = System.Random;

namespace Playground.Characters.Monsters
{
    public class AMonster : Monster
    {
        public AMonster() : base(MonstersNames.AMonster, WeaponsNames.Stick, 100, 1)
        {
        }
        //TODO 
        //faire en sortes que les monstres ne reste pas stuck sous la plateforme 
        // update les conditions d'attaques 

        //MULTI ONLY 
        // faire switch la cible du monstre en fonction de la postion du joueur le +proche

        [Header("Pathfinding")]
        private Transform target;
        public float activateDistance = 50f;
        public float pathUpdateSeconds = 0.5f;

        [Header("Physics")]
        private float nextWaypointDistance = 1f; //3f 
        public float jumpCheckOffset = 0.1f;
        private int waitJump = 0;


        private bool jumpEnabled = false;
        private Vector3[] jumpPosition;
        private bool FacingRight = true;

        private Path path;
        private int currentWaypoint = 0;
        RaycastHit2D isGrounded;
        Seeker seeker;
        Rigidbody2D rb;

        private Vector3 InitialePos;

        private bool IsGravitySwitched = false;
        
         public void Start()
         {
             // le seeker c'est un composante du package A* 
             seeker = GetComponent<Seeker>();
             // j'ai pas reussi a reutiliser le CC2D ducoup je recupere le rigidbody pour faire 
             // faire bouger le monstre 
             rb = GetComponent<Rigidbody2D>();

             target = GetTarget();

             InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
             jumpPosition = GetPostion();

             InitialePos = new Vector3(34, -15,0);
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

         //function used to know the closest player to chase 
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

         //fonction pour savoir si le monstre se situe au bon endroit pour sauter 
         public Vector3[] GetPostion()
         {
             // jumpEnabled = 
     
             GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("EnableJump");
             Vector3[] pos = new Vector3[gameObjects.Length];
             for (int i = 0; i < gameObjects.Length; i++)
             {
                 pos[i] = gameObjects[i].transform.position;
             }
             return pos;
         }

         public bool EnbaleJump()
         {
             foreach (var pos in jumpPosition)
             {
                 if (transform.position.x >= pos.x - 1f
                     && transform.position.x <= pos.x + 1f
                     && transform.position.y >= pos.y - 1f
                     && transform.position.y <= pos.y + 1f
                     && -1*(target.position.x -transform.position.x) <= 1.5f)
                     return true;
             }
             return false;
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

             if (rb.position.x+0.5f >= target.position.x&&
                 rb.position.x-0.5f <= target.position.x&&
                 target.position.y - rb.position.y > 5f )
             {
                 target = GameObject.FindGameObjectWithTag("EnableJump").transform;
                 Debug.Log("jambon");
             }
     
             // verfication de si le joueur est bien sur le soleuh 
             Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);
             isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);
             
             
             // calcul de la direction 
             Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
             Vector2 force = direction * speed * Time.deltaTime*1.3f;
     
              jumpEnabled = EnbaleJump();
              
             // Jump
             float val_y = rb.position.x + (Vector2.up * 1000 * 1.5f).x;
             if (jumpEnabled && isGrounded)
             {
                 if (target.position.y>transform.position.y+0.5f && waitJump <=0)
                 {
                     
                     rb.AddForce(Vector2.up * 1000*1.5f);
                     waitJump = 50;
     
                     if ((target.position.x - transform.position.x) * -1 > 0 && transform.position.y >= val_y/3)
                     {
                         rb.AddForce(Vector2.left*speed);
                     }
                     else if (transform.position.y <= val_y / 3)
                     {
                         rb.AddForce(Vector2.right*speed);
                     }
                 }
                 else
                 {
                     waitJump--;
                 }
             }
              

             // Movement
             rb.AddForce(force*3f);
             
             // flip le monstre pour qu'il regarde du bon cote 
             if (force.x > 0 && !FacingRight)
                 Flip();
             
             else if (force.x<0 && FacingRight)
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