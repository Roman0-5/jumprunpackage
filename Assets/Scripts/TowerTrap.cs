using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerTrap : MonoBehaviour
{
    private bool playerInRange = false;
    private InputAction interactAction;

    [SerializeField]
    private GameObject wrongChoiceText;

    [SerializeField]
    private GameObject winText;

    [SerializeField]
    private Transform respawnPoint;

    [SerializeField]
    private float messageDisplayTime = 3f;

    public static bool hasKey = false;

    void Start()
    {
        this.interactAction = InputSystem.actions.FindAction("Interact");

        if (this.wrongChoiceText != null)
        {
            this.wrongChoiceText.SetActive(false);
        }
        if (this.winText != null)
        {
            this.winText.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (this.interactAction.WasPressedThisFrame() && this.playerInRange)
        {
            if (hasKey)
            {
                this.StartCoroutine(this.ShowMessageAndRespawn(this.winText));
            }
            else
            {
                this.StartCoroutine(this.ShowMessageAndRespawn(this.wrongChoiceText));
            }
        }
    }

    IEnumerator ShowMessageAndRespawn(GameObject message)
    {
        if (message != null)
        {
            message.SetActive(true);
        }

        yield return new WaitForSeconds(this.messageDisplayTime);

        if (message != null)
        {
            message.SetActive(false);
        }

        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            CharacterController controller = player.GetComponentInChildren<CharacterController>();
            if (controller == null)
            {
                controller = player.GetComponentInParent<CharacterController>();
            }

            if (controller != null)
            {
                controller.enabled = false;
                player.transform.position = this.respawnPoint.position;
                controller.enabled = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.playerInRange = false;
        }
    }
}