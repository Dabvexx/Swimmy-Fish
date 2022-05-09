using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpRotation = 5f;
    private AudioSource source;
    [SerializeField] private AudioClip jump;

    /*
    [SerializeField] private float minAngle = 5f;
    [SerializeField] private float maxAngle = 5f;
    */
    private Rigidbody2D rb;

    private IEnumerator coroutine;

    private void Awake()
    {
        FindObjectOfType<CollisionDetection>().deathEvent += StopInput;
        FindObjectOfType<LivesManager>().resetEvent += ResetLevel;

        source = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        coroutine = HandleInput();

        //StartCoroutine(coroutine);
    }

    private IEnumerator HandleInput()
    {
        for (; ; )
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (rb.gravityScale == 0)
                {
                    rb.gravityScale = 2.2f;
                }

                rb.AddTorque(jumpRotation);
                rb.velocity = new Vector2(0f, jumpForce);
                source.PlayOneShot(jump);
            }

            yield return null;
        }
    }

    private void StopInput()
    {
        StopCoroutine(coroutine);
    }

    private void ResetLevel()
    {
        transform.position = new Vector3(-5, 0, 0);

        transform.rotation = Quaternion.identity;
        rb.gravityScale = 0;
        StartCoroutine(coroutine);
    }
}