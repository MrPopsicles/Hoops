using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCorrect : MonoBehaviour
{
    //stating these as public allows me to change them in the Unity UI
    //force of pull back power
    public float power = 10f;
    public Rigidbody2D rb;

    //minMax Values
    public Vector2 minPower;
    public Vector2 maxPower;

    lineTrajectory lt;

    Camera cam;

    //force * power will give us the resulting strength of drag
    Vector2 force;
    Vector3 mouseStartPoint;
    Vector3 mouseEndPoint;
    Vector3 rendererStartPoint;
    Vector3 rendererEndPoint;

    //We are going to use this vector to store the initial difference between MouseInput and rb.position
    Vector3 translation;
    //We are going to use this vector to store the difference between the final render point and rb.position
    Vector3 translation2;

    private void Start()
    {
        cam = Camera.main;
        lt = GetComponent<lineTrajectory>();
        rendererStartPoint = rb.position;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;

        //We need this so that the line is not hiding behind other elements
        rendererStartPoint.z = 5;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //the cam.screen part converts the pixel output of Input and converts it to world coordinates
            mouseStartPoint = cam.ScreenToWorldPoint(Input.mousePosition);

            //When the mouse is first pressed down, we need to update our translation
            translation.x = mouseStartPoint.x - rb.position.x;
            translation.y = mouseStartPoint.y - rb.position.y;
        }

        //While leftMouse is pressed down
        if (Input.GetMouseButton(0))
        {
            //Works out the rendererEndPoint Position
            rendererEndPoint = cam.ScreenToWorldPoint(Input.mousePosition) - translation;
            translation2.x = rb.position.x - rendererEndPoint.x;
            translation2.y = rb.position.y - rendererEndPoint.y;
            rendererEndPoint = rendererEndPoint + translation2 + translation2;
            rendererEndPoint.z = 5;

            lt.RenderLine(rendererStartPoint, rendererEndPoint);
        }

        if (Input.GetMouseButtonUp(0))
        {
            rb.constraints = RigidbodyConstraints2D.None;

            mouseEndPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            force = new Vector2(Mathf.Clamp(mouseStartPoint.x - mouseEndPoint.x, minPower.x, maxPower.x), Mathf.Clamp(mouseStartPoint.y - mouseEndPoint.y, minPower.y, maxPower.y));
            rb.AddForce(force * power, ForceMode2D.Impulse);

            lt.EndLine();
        }


    }
}
