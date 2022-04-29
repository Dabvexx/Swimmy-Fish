using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePipes : MonoBehaviour
{
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private float pipeDelay = 2f;

    private float minOffset = -9.5f;
    private float maxOffset = -5.5f;

    private IEnumerator coroutine;

    private void Start()
    {
        FindObjectOfType<CollisionDetection>().deathEvent += StopSpawning;

        coroutine = SpawnPipes();

        StartCoroutine(coroutine);
    }

    private void StopSpawning()
    {
        StopCoroutine(coroutine);
    }

    private IEnumerator SpawnPipes()
    {
        for (; ; )
        {
            Instantiate(pipePrefab, new Vector3(15, Random.Range(minOffset, maxOffset), 0), Quaternion.identity);

            yield return new WaitForSeconds(pipeDelay);
        }
    }
}