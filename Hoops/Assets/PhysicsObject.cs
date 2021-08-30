using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    //If we want to change the strength of gravity
    public float gravityModifier = 1f;

    public float minGroundNormalY = 0.65f;

    protected Rigidbody2D rb2d;
    protected Vector2 velocity;

    protected const float minMoveDistance = 0.001f;

    //All variables required for Casting
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    //Extra buffer to prevent passing through another collider
    protected const float shellRadius = 0.01f;

    // When the object first becomes active, this function is called
    void OnEnable() 
    {
        //Assign the RigidBody2D of this object to variable rb2d
        rb2d = GetComponent<Rigidbody2D> ();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Not checking collisions against triggers
        contactFilter.useTriggers = false;

        //Getting mask from layer collision matrix from inside unity from this gameObject
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() 
    {
        //Works out current speed
        velocity = velocity + gravityModifier * Physics2D.gravity * Time.deltaTime;

        //Works out distance travelled at current speed
        Vector2 deltaPosition = velocity * Time.deltaTime;

        //For some reason up means down and down means up??
        Vector2 move = Vector2.up * deltaPosition.y;

        Movement(move);
    }

    void Movement(Vector2 move)
    {
        float distance = move.magnitude;

        //If the move distance in the next frame is larger than the minMove value, then we check for collision. If the object were to collide with something its move would be 0 and thus smaller than minMove, meaning no need to check continually
        if (distance > minMoveDistance) 
        {
            //Cast the next frame, and check if it collides with anything
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);

            //Ensures the list is empty; not using last frame data
            hitBufferList.Clear();

            for (int i = 0; i < count;i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            //We are now going to compare the normal of the ground to determine the angle at which the ball hits the ground
            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
            }
                        
        }
        rb2d.position = rb2d.position + move;
    }
}
