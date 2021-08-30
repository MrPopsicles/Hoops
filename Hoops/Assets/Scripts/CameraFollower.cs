using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    //We use Transform when we want to move/change rotation of an item
    public Transform target;

    //How fast the camera snaps to the target
    [Range(0.0f, 1.0f)]
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    Vector2 cameraBounds;

    public void Start()
    {
        cameraBounds = new Vector2(transform.position.x, transform.position.y);
    }

    //LateUpdate is basically update, but it happens after the Update() function. 
    //We do this because if we have an update() for camera and update() for the object, 
    //They'll fight for which update should go first, lateUpdate takes the ambiguity out of it
    private void LateUpdate()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        //check if the position it is trying to choose is out of bound
        if (IsOutOfBound(smoothedPosition, cameraBounds) == false)
        {
            transform.position = smoothedPosition;
        }
        else
        {
            transform.position = BoundedCamera(smoothedPosition, cameraBounds);
        }
    }

    //Works out if the given coordinates is a valid coordinate for the camera
    private bool IsOutOfBound(Vector2 firstVector, Vector2 secondVector)
    {
        bool isOutOfBound = false;
        
        //if any of the values returned are negative, automatically out of bound
        if (firstVector.x - secondVector.x > 0)
        {
            if (firstVector.y - secondVector.y > 0)
            {
                return isOutOfBound;
            }
        }
        isOutOfBound = true;
        return isOutOfBound;
    }

    //Work out the new position of the camera as it is bounded by an edge
    private Vector3 BoundedCamera(Vector3 desiredPosition, Vector3 cameraBounds)
    {
        Vector3 finalCamPosition;
        finalCamPosition = desiredPosition;
        //Bounded by x
        if (desiredPosition.x - cameraBounds.x < 0)
        {
            finalCamPosition.x = cameraBounds.x;
        }

        //Bounded by y
        if (desiredPosition.y - cameraBounds.y < 0)
        {
            finalCamPosition.y = cameraBounds.y;
        }

        return finalCamPosition;
    }
}
