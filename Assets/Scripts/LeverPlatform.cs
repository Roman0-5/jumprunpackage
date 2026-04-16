using UnityEngine;

public class LeverPlatform : MonoBehaviour
{
    [SerializeField]
    private Lever lever;

    [SerializeField]
    private Vector3 start;

    [SerializeField]
    private Vector3 end;

    [SerializeField]
    private float platformSpeed = 1f;

    private float progress = 0f; 
    private int direction = 1;   
    private Vector3 lastPosition;
    private Vector3 velocity;

    void Start()
    {
        this.transform.localPosition = this.start;
        this.lastPosition = this.transform.position;
    }

    void FixedUpdate()
    {
        this.velocity = (this.transform.position - this.lastPosition) / Time.fixedDeltaTime;
        this.lastPosition = this.transform.position;

        if (this.lever != null && this.lever.IsOn)
        {
       
            this.progress += Time.fixedDeltaTime * this.platformSpeed * this.direction;

            if (this.progress >= 1f)
            {
                this.progress = 1f;
                this.direction = -1;
            }
            else if (this.progress <= 0f)
            {
                this.progress = 0f;
                this.direction = 1;
            }
        }
        else
        {
            if (this.progress > 0f)
            {
                this.progress -= Time.fixedDeltaTime * this.platformSpeed;
                if (this.progress <= 0f)
                {
                    this.progress = 0f;
                }
            }
            this.direction = 1;
        }

        this.transform.localPosition = Vector3.Lerp(this.start, this.end, this.progress);
    }

    public Vector3 GetVelocity()
    {
        return this.velocity;
    }
}