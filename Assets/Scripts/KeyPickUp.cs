using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TowerTrap.hasKey = true;
            this.gameObject.SetActive(false);
        }
    }
}