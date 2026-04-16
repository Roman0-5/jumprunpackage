using UnityEngine;

public class BlockVisibility : MonoBehaviour
{
    [SerializeField]
    private PressurePlate button;

    [SerializeField]
    private MeshRenderer blockRenderer;

    void Start()
    {
        if (this.blockRenderer == null)
        {
            this.blockRenderer = this.GetComponent<MeshRenderer>();
        }


        this.blockRenderer.enabled = false;
    }

    void Update()
    {
        if (this.button != null)
        {
            this.blockRenderer.enabled = this.button.IsPressed;
        }
    }
}