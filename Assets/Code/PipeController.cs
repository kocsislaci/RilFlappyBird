using UnityEngine;

public class PipeController : MonoBehaviour
{
    private Transform _upperPipe;
    private Transform _lowerPipe;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _upperPipe = transform.GetChild(0);
        _lowerPipe = transform.GetChild(1);
    }
    
    public void StartMovement(float velocity)
    {
        _rigidbody2D.velocity = Vector2.left * velocity;
    }

    public void SetPipeConfiguration(float height, float gap)
    {
        _upperPipe.localPosition = new Vector2(0f, height + gap / 2);
        _lowerPipe.localPosition = new Vector2(0f, height - gap / 2);
    }
}
