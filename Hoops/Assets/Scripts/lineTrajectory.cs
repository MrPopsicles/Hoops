using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This Prevents awake from being called before the lineRenderer component is woken up. Basically stops it from causing an error
[RequireComponent(typeof(LineRenderer))]
public class lineTrajectory : MonoBehaviour
{
    LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void RenderLine(Vector3 startPoint, Vector3 endPoint)
    {
        //positionCount is used to set/get the number of vertices, a line has 2 verticies for a straight line
        lr.positionCount = 2;

        //create new variable called points which store values of type vector3. The size of this array will be 2
        Vector3[] points = new Vector3[2];
        points[0] = startPoint;
        points[1] = endPoint;

        lr.SetPositions(points);
    }

    //Function will remove the line from view
    public void EndLine()
    {
        //If a line has 0 vertices, it doesnt exist, thus removing the line
        lr.positionCount = 0;
    }
}
