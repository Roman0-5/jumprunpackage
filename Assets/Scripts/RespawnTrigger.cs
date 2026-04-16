using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    [SerializeField]
    private Transform respawnPoint;

    void OnTriggerEnter(Collider other)
    {
        CharacterController controller = other.gameObject.GetComponent<CharacterController>();

        if (controller != null)
        {
            controller.enabled = false;

            other.gameObject.transform.position = this.respawnPoint.position;

            controller.enabled = true;
        }
    }
}