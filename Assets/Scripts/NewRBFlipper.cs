using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class NewRBFlipper : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Transform objectToFlip;
    Vector2 lastVelocity;
    public UnityEvent OnFlip;
    public bool Disabled;
    private Vector2 startingScale;
    [SerializeField] private bool leftyObject;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingScale = objectToFlip.localScale;
    }

    private void Update()
    {
        if (lastVelocity != rb.velocity && !Disabled)
        {
            if (rb.velocity.x > 0)
            {
                FlipRight();
                OnFlip?.Invoke();
            }
            else if (rb.velocity.x < 0)
            {
                FlipLeft();
                OnFlip?.Invoke();
            }
            lastVelocity = rb.velocity;
        }
    }

    public void FlipRight()
    {
        if (leftyObject)
        {
            objectToFlip.localScale = new Vector3(-1 * startingScale.x, startingScale.y, 1);
        }
        else
        {
            objectToFlip.localScale = startingScale;
        }
    }
    public void FlipLeft()
    {
        if (leftyObject)
        {
            objectToFlip.localScale = startingScale;
        }
        else
        {
            objectToFlip.localScale = new Vector3(-1 * startingScale.x, startingScale.y, 1);
        }

    }

}
