using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    //We use Transform when we want to move/change rotation of an item
    public Transform target;
    public Transform hoop;

    //How fast the camera snaps to the target
    [Range(0.0f, 1.0f)]
    float smoothSpeed = 0.125f;
    float hoopSmoothSpeed = 5f;
    public Vector3 offset;
    public Vector3 hoopOffset;

    float hoopZoomInSpeed = 0.08f;
    float zoomSpeed = 0.02f;
    float maxCameraSize = 8; //We need to confine this to the size of a screen some how
    float minCameraSize = 3;

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
        Vector3 desiredPosition;
        Vector3 smoothedPosition;

        if (BasketBall.zoomIn == false)
        {
            
            desiredPosition = target.position + offset;
            smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

            //Check if the ball is moving up
            if (target.position.y > transform.position.y && GetComponent<Camera>().orthographicSize <= maxCameraSize)
            {
                //ball is moving up
                GetComponent<Camera>().orthographicSize += zoomSpeed;

                cameraBounds.x += zoomSpeed * 2;
                cameraBounds.y += zoomSpeed;
            }

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
        else
        {
            Debug.Log("We have entered this part");
            //Zoom into the hoops position and should slow mo as well, not yet implemented
            desiredPosition = hoop.position + hoopOffset;
            smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, hoopSmoothSpeed);
            transform.position = smoothedPosition;
            if (GetComponent<Camera>().orthographicSize >= minCameraSize)
            {
                GetComponent<Camera>().orthographicSize -= hoopZoomInSpeed;
            }
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
