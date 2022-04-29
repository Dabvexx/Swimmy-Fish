using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpRotation = 5f;

    /*
    [SerializeField] private float minAngle = 5f;
    [SerializeField] private float maxAngle = 5f;
    */
    private Rigidbody2D rb;

    private IEnumerator coroutine;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        coroutine = HandleInput();

        FindObjectOfType<CollisionDetection>().deathEvent += StopInput;

        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    private void Update()
    {
        /*transform.eulerAngles = new Vector3
                                    (
                                        transform.eulerAngles.x,
                                        transform.eulerAngles.y,
                                        minAngle
                                    );
        */
    }

    private IEnumerator HandleInput()
    {
        for (; ; )
        {
            if (Input.GetButtonDown("Jump"))
            {
                /*transform.eulerAngles = new Vector3
                                        (
                                            transform.eulerAngles.x,
                                            transform.eulerAngles.y,
                                            maxAngle
                                        );

                rb.AddTorque(jumpRotation);
                */

                //rb.SetRotation(jumpRotation);

                rb.AddTorque(jumpRotation);
                rb.velocity = new Vector2(0f, jumpForce);
            }

            yield return null;
        }
    }

    private void StopInput()
    {
        StopCoroutine(coroutine);
    }
}