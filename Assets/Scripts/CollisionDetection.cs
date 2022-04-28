using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    ScoreManager sm;

    public delegate void DeathDelegate();
    public event DeathDelegate deathEvent;

    private void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Score"))
        {
            sm.IncrementScore(100);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Collision"))
        {
            if (deathEvent != null)
            {
                deathEvent();
            }
        }
    }
}
