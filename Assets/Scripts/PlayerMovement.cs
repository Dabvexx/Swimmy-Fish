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

    private void Awake()
    {
        FindObjectOfType<CollisionDetection>().deathEvent += StopInput;
        FindObjectOfType<LivesManager>().resetEvent += ResetLevel;

        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        coroutine = HandleInput();

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
                if (rb.isKinematic)
                {
                    // TODO: CHANGE THIS FROM BEING KINEAMATIC! PLAY CAN JUST CHEAT BY NOT DOING ANYTHING!
                    rb.isKinematic = false;
                }

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

    private void ResetLevel()
    {
        transform.position = new Vector3(-5, 0, 0);

        rb.isKinematic = true;
        StartCoroutine(coroutine);
    }
}