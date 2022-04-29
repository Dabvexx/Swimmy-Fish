using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMover : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private IEnumerator coroutine;

    private void Start()
    {
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
                Destroy(this.gameObject);
            }

            transform.Translate(new Vector3(-speed * Time.deltaTime, 0f, 0f));

            yield return null;
        }
    }

    private void StopMoving()
    {
        StopCoroutine(coroutine);
    }
}