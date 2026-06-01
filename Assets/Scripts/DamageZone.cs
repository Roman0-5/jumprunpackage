using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField]
    private float damagePerSecond = 20.0f;
    [SerializeField]
    private Enemy enemy;

    private Character playerInZone = null;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        var character = other.GetComponentInChildren<Character>();
        if (character == null)
            character = other.GetComponentInParent<Character>();

        this.playerInZone = character;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            this.playerInZone = null;
    }

    void Update()
    {
        Debug.Log($"DamageZone Weltposition: {this.transform.position}");
        if (this.playerInZone == null)
            return;

        if (this.enemy != null && this.enemy.IsSquashing())
            return;

        this.playerInZone.InflictDamage(this.damagePerSecond * Time.deltaTime);
        Debug.Log($"Schaden! Health jetzt: {this.playerInZone.getCurrentHealth()}");
    }
}