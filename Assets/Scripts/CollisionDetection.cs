using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private ScoreManager sm;

    private bool isCollided = false;

    public delegate void DeathDelegate();

    public event DeathDelegate deathEvent;

    private void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
        FindObjectOfType<LivesManager>().resetEvent += ResetLevel;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ScoreField"))
        {
            Debug.Log("Score");

            sm.IncrementScore(1);

            collision.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Collision") && !isCollided)
        {
            // Stop the event from being triggered multiple times.
            isCollided = true;

            Debug.Log("Died");

            deathEvent?.Invoke();
        }
    }

    private void ResetLevel()
    {
        isCollided = false;
    }
}