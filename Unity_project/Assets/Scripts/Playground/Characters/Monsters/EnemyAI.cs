using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Pathfinding;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    //TODO 
    //rendre les deplacements fluides 
    //mettre une logique dans la physique utilise car la je comprend pas 
    //faire monter les monstres sur les plateformes 
    // update les conditions d'attaques 

    //MULTI ONLY 
    // faire switch la cible du monstre en fonction de la postion du joueur le +proche

    [Header("Pathfinding")]
    public Transform target;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float speed = 200f;
    public float nextWaypointDistance = 3f; 
    public float jumpCheckOffset = 0.1f;
    private int waitJump = 0;

    [Header("Custom Behavior")]
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;

    private Path path;
    private int currentWaypoint = 0;
    RaycastHit2D isGrounded;
    Seeker seeker;
    Rigidbody2D rb;

    public void Start()
    {
        // le seeker c'est un composante du package A* 
        seeker = GetComponent<Seeker>();
        // j'ai pas reussi a reutiliser le CC2D ducoup je recupere le rigidbody pour faire 
        // faire bouger le monstre 
        rb = GetComponent<Rigidbody2D>();
        
        target = GameObject.FindGameObjectWithTag("Heros").transform;
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
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

        // verfication de si le joueur est bien sur le soleuh 
        Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);
        isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);
        //
        
        
        
        // calcul de la direction // very useful comme changement xd 
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        // Jump
        if (jumpEnabled && isGrounded)
        {
            if (transform.position.y +0.2f< target.position.y  && waitJump <=0)
            {
                if (transform.position.x < target.position.x)
                {
                   
                    Task task = Task.Delay(1);
                    rb.AddForce(Vector2.up * speed );
                    task.Wait(1);
                    rb.AddForce(Vector2.left*100);
                    waitJump = 50;
                }
                else
                {
                    Task task = Task.Delay(1);
                    rb.AddForce(Vector2.up * speed );
                    task.Wait(1);
                    rb.AddForce(Vector2.right*100);
                    waitJump = 50;
                }
            }
            else
            {
                waitJump--;
            }
        }

        // Movement
        rb.AddForce(force);

        // on augmente le currentwaypoint qui l'arret du monstre lorsque superieur ou egal au max 
        // -> a modifier avec le calcule de la distance directement prcq sert a rien 
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // flip le monstre pour qu'il regarde du bon cote 
        if (directionLookEnabled)
        {
            if (rb.velocity.x > 0.05f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (rb.velocity.x < -0.05f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }
}