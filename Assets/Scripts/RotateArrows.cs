using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 90f; 

    void Update()
    {
        this.transform.Rotate(0f, this.rotationSpeed * Time.deltaTime, 0f);
    }
}