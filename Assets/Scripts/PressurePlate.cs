using System.Collections;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private float releaseDelay = 1.0f;

    [SerializeField]
    private Transform buttonVisual;

    [SerializeField]
    private float pressDepth = 0.05f;

    [SerializeField]
    private float pressSpeed = 5f;

    private bool isPressed = false;
    private Coroutine releaseCoroutine = null;
    private Vector3 buttonUpPosition;
    private Vector3 buttonDownPosition;

    public bool IsPressed => this.isPressed;

    void Start()
    {
        if (this.buttonVisual != null)
        {
            this.buttonUpPosition = this.buttonVisual.localPosition;
            this.buttonDownPosition = this.buttonUpPosition + Vector3.down * this.pressDepth;
        }
    }

    void Update()
    {
        if (this.buttonVisual == null) return;

        Vector3 targetPosition = this.isPressed ? this.buttonDownPosition : this.buttonUpPosition;
        this.buttonVisual.localPosition = Vector3.Lerp(
            this.buttonVisual.localPosition,
            targetPosition,
            this.pressSpeed * Time.deltaTime
        );
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (this.releaseCoroutine != null)
            {
                this.StopCoroutine(this.releaseCoroutine);
                this.releaseCoroutine = null;
            }
            this.isPressed = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.releaseCoroutine = this.StartCoroutine(this.ReleaseAfterDelay());
        }
    }

    IEnumerator ReleaseAfterDelay()
    {
        yield return new WaitForSeconds(this.releaseDelay);
        this.isPressed = false;
        this.releaseCoroutine = null;
    }
}