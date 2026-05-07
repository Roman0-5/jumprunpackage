using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform patrolPointA;
    [SerializeField]
    private Transform patrolPointB;
    [SerializeField]
    private float moveSpeed = 1.5f;
    [SerializeField]
    private float squashDuration = 0.3f;
    [SerializeField]
    private float stunDuration = 0.5f;
    [SerializeField]
    private Vector3 squashFactor = new Vector3(1.4f, 0.4f, 1.4f);
    [SerializeField]
    private AudioSource squashSound;

    private Transform currentTarget;
    private Vector3 originalScale;
    private Vector3 squashedScale;
    private bool isSquashing = false;

    void Start()
    {
        this.originalScale = this.transform.localScale;
        this.squashedScale = new Vector3(
            this.originalScale.x * this.squashFactor.x,
            this.originalScale.y * this.squashFactor.y,
            this.originalScale.z * this.squashFactor.z
        );
        this.currentTarget = this.patrolPointB;
    }

    void Update()
    {
        if (this.isSquashing)
        {
            return;
        }

        Vector3 direction = this.currentTarget.position - this.transform.position;
        direction.y = 0.0f;

        if (direction.sqrMagnitude < 0.05f)
        {
            this.currentTarget = this.currentTarget == this.patrolPointA
                ? this.patrolPointB
                : this.patrolPointA;
            return;
        }

        Vector3 moveDirection = direction.normalized;
        this.transform.position += moveDirection * this.moveSpeed * Time.deltaTime;

        this.transform.forward = moveDirection;
    }

    void OnTriggerEnter(Collider other)
    {
        if (this.isSquashing)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            this.Squash();
        }
    }

    void Squash()
    {
        this.isSquashing = true;

        if (this.squashSound != null)
        {
            this.squashSound.Play();
        }

        Sequence squashSequence = DOTween.Sequence();
        // Schnell zusammendruecken
        squashSequence.Append(this.transform.DOScale(this.squashedScale, this.squashDuration * 0.4f)
            .SetEase(Ease.OutQuad));
        // Sofort wieder hochfedern
        squashSequence.Append(this.transform.DOScale(this.originalScale, this.squashDuration * 0.6f)
            .SetEase(Ease.OutBack));
        // Kurz geschockt dastehen
        squashSequence.AppendInterval(this.stunDuration);
        squashSequence.OnComplete(() =>
        {
            this.isSquashing = false;
        });
    }
}