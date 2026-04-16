using System.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private Vector3 topPosition;

    [SerializeField]
    private Vector3 bottomPosition;

    [SerializeField]
    private float moveSpeed = 2f;

    [SerializeField]
    private float waitDelay = 3f;

    private bool moving = false;
    private bool atTop = true;
    private Vector3 targetPosition;
    private Vector3 lastPosition;
    private Vector3 velocity;

    void Start()
    {
        this.transform.localPosition = this.topPosition;
        this.lastPosition = this.transform.position;
        this.targetPosition = this.topPosition;
    }

    void FixedUpdate()
    {
        this.velocity = (this.transform.position - this.lastPosition) / Time.fixedDeltaTime;
        this.lastPosition = this.transform.position;

        if (!this.moving)
        {
            return;
        }

        this.transform.localPosition = Vector3.MoveTowards(
            this.transform.localPosition,
            this.targetPosition,
            this.moveSpeed * Time.fixedDeltaTime
        );

        if (Vector3.Distance(this.transform.localPosition, this.targetPosition) < 0.01f)
        {
            this.transform.localPosition = this.targetPosition;
            this.moving = false;
            this.atTop = !this.atTop;
        }
    }

    public Vector3 GetVelocity()
    {
        return this.velocity;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !this.moving)
        {
            this.StartCoroutine(this.WaitAndMove());
        }
    }

    IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(this.waitDelay);

        if (this.atTop)
        {
            this.targetPosition = this.bottomPosition;
        }
        else
        {
            this.targetPosition = this.topPosition;
        }

        this.moving = true;
    }
}