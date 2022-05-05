using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGenerator : MonoBehaviour
{
    [SerializeField] private GameObject pipePrefab;

    // Use bounds of scoreprefab to determind gap size
    [SerializeField] private GameObject scorePrefab;

    [SerializeField] private float pipeDelay = 2f;

    [SerializeField] private float variationIntensity = 1.2f;

    public List<GameObject> pipes = new List<GameObject>();

    private ScoreManager sm;

    private float minOffset = -9.5f;
    private float maxOffset = -5.5f;

    private IEnumerator coroutine;

    // Start with an offset near the middle of the screen
    private float prevOffset = -7f;

    private GameObject empty;

    private void Start()
    {
        empty = new GameObject("Pipe");

        sm = FindObjectOfType<ScoreManager>();

        FindObjectOfType<CollisionDetection>().deathEvent += StopSpawning;

        FindObjectOfType<LivesManager>().resetEvent += ResetLevel;

        coroutine = SpawnPipes();

        StartCoroutine(coroutine);
    }

    private IEnumerator SpawnPipes()
    {
        for (; ; )
        {
            //var offset = Random.Range(minOffset, maxOffset);
            var pipeSpeed = CalculatePipeSpeed();
            var offset = CalculatePipeOffset(prevOffset, pipeDelay, pipeSpeed, GetPipeGap(), variationIntensity);
            var pipe = Instantiate(empty, new Vector3(15, offset, 0), Quaternion.identity);

            // Add the pipemover and set its speed
            pipe.AddComponent<PipeMover>();
            pipe.GetComponent<PipeMover>().speed = pipeSpeed;

            ConstructPipe(offset, pipe);

            pipes.Add(pipe);

            prevOffset = offset;

            yield return new WaitForSeconds(pipeDelay);
        }
    }

    // The pipes should start spawning in faster as well as making the gap smaller.
    // To create possible scenarios, Take into account the distance between the pipes based on pipeDelay
    // And calculate how high the gap can go
    // Smaller differences in how the pipes are spaced appart leads to smaller offsets.
    // Smaller gaps in pipes leads to smaller offsets.
    // Offsets are based on what the previous pipes offset as well.
    private float CalculatePipeOffset(float prevOffset, float pipeDelay, float pipeSpeed, float pipeGap, float intensity)
    {
        // 5/2 the gap of the pipe multiplied by the difference between the delay of the pipe and its speed
        // times an intensity variable to create more dramatic results
        float variation = ((pipeGap / 2.5f) * (pipeDelay / pipeSpeed)) * intensity;

        // Maybe Use sin and cosin to create a bias for going up and down and only staying in one direction for a moment.

        // +- variation to the last pipes offset
        // Clamp offset to not create absurd scenarios
        float minOffset = Mathf.Clamp(prevOffset + variation, -9.5f, -4.5f);
        float maxOffset = Mathf.Clamp(prevOffset - variation, -9.5f, -4.5f);

        // Generate the offset.
        float offset = Random.Range(minOffset, maxOffset);

        /*
        Debug.Log("Variation: " + variation);
        Debug.Log("Min Offset: " + minOffset);
        Debug.Log("Max Offset: " + maxOffset);
        Debug.Log("Pipe Offset: " + offset);
        */

        return offset;
    }

    private void ConstructPipe(float offset, GameObject parent)
    {
        // Create a pipe by combining 2 pipe prefabs as well as adjusting a scoring segment.
        // all stored on an empty game object with the pipe mover script.
        float gap = GetPipeGap();

        // Add children to gameobject.
        Instantiate(pipePrefab, parent.transform.position, Quaternion.identity, parent.transform);
        Instantiate(scorePrefab, new Vector3(parent.transform.position.x + 1, parent.transform.position.y + (gap + 12) / 2, parent.transform.position.z), Quaternion.identity, parent.transform);

        // Flip second child pipe
        var flip = Instantiate(pipePrefab, new Vector3(parent.transform.position.x, parent.transform.position.y + (gap + 12), parent.transform.position.z), Quaternion.identity, parent.transform);

        flip.transform.localScale = new Vector3(1, -10, 1);
    }

    private float GetPipeGap()
    {
        // make the pipes get slightly closer toget till you reach a max threashhold
        // Instantiate prefab at 12 + (gap / 2) after calculating how tall the box colliders bounds is

        // Use bounds to get the gap size
        float gap = 3f / sm.GetDifficultyFromScore();

        // Add 12 to make the pipes perfectly flush with eachother
        return Mathf.Clamp(gap, 2.3f, 4f);
    }

    private float CalculatePipeSpeed()
    {
        // Speed = 1 / score to get a fraction
        // Base speed * that fraction
        var speed = 3f * sm.GetDifficultyFromScore();

        return Mathf.Clamp(speed, 3f, 15f);
    }

    private void StopSpawning()
    {
        StopCoroutine(coroutine);
    }

    private void ResetLevel()
    {
        // Delete all pipes
        foreach (var pipe in pipes)
        {
            Destroy(pipe);
        }

        pipes.Clear();

        StartCoroutine(coroutine);
    }
}