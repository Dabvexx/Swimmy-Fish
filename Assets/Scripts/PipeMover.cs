using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMover : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private IEnumerator coroutine;

    // Update is called once per frame
    private void Start()
    {
        coroutine = MovePipes();

        FindObjectOfType<CollisionDetection>().deathEvent += StopMoving;

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