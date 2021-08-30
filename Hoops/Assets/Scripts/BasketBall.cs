using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasketBall : MonoBehaviour
{
    public static bool hasBeenShot;

    private Vector2 zeroVector = new Vector2(0, 0);
    private Rigidbody2D rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hasBeenShot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasBeenShot == true && rb.velocity == zeroVector)
        {
            RestartGame();
        }

        if (Input.GetMouseButtonUp(0))
        {
            hasBeenShot = true;
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
