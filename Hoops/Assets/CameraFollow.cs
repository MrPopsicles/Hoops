using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //We use Transform when we want to move/change rotation of an item
    public Transform target;

    //How fast the camera snaps to the target
    [Range(0.0f, 1.0f)]
    public float smoothSpeed = 0.125f;

    public Vector3 offset;

    //LateUpdate is basically update, but it happens after the Update() function. 
    //We do this because if we have an update() for camera and update() for the object, 
    //They'll fight for which update should go first, lateUpdate takes the ambiguity out of it
    private void LateUpdate()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
