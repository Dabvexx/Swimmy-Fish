using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMover : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] private PipeGenerator pg;

    private IEnumerator coroutine;

    private void Start()
    {
        pg = FindObjectOfType<PipeGenerator>();

        FindObjectOfType<CollisionDetection>().deathEvent += StopMoving;

        coroutine = MovePipes();

        StartCoroutine(coroutine);
    }

    private IEnumerator MovePipes()
    {
        for (; ; )
        {
            if (transform.position.x < -15)
            {
                FindObjectOfType<CollisionDetection>().deathEvent -= StopMoving;
                pg.Pipes.Remove(gameObject);
                Destroy(gameObject);
            }

            transform.Translate(new Vector3(-speed * Time.deltaTime, 0f, 0f));

            yield return null;
        }
    }

    private void StopMoving()
    {
        FindObjectOfType<CollisionDetection>().deathEvent -= StopMoving;
        StopCoroutine(coroutine);
    }
}