using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfPoint : MonoBehaviour
{
    //If this counter is >=2, means it should count as a point
    private int counter;
 
    private void OnTriggerEnter2D(Collider2D other)
    {
        counter++;
        if (counter >= 2)
        {
            Debug.Log("Point has been scored");
            counter = 0;
        }
    }

    private void ResetCounter()
    {
        counter = 0;
    }
}
