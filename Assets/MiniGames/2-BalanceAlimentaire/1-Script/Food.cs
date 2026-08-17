using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Food : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Rigidbody2D _rigidbody;
    private FoodData _data;

    public FoodData Data
    {
        get => _data;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        if (_spriteRenderer == null)
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Initialize(FoodData data)
    {
        _data = data;

        if (_spriteRenderer != null)
            _spriteRenderer.sprite = data.Sprite;

        ResetPhysics();
    }

    public void ResetPhysics()
    {
        _rigidbody.linearVelocity = Vector2.zero;
        _rigidbody.angularVelocity = 0f;
        _rigidbody.simulated = true;
    }

    public void SetPaused(bool isPaused)
    {
        _rigidbody.simulated = !isPaused;
    }
}