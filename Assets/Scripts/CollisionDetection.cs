using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private UIManager sm;

    private bool isCollided = false;

    public delegate void DeathDelegate();

    public event DeathDelegate deathEvent;

    private void Start()
    {
        sm = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Score"))
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
}