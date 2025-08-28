using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Vector2 _targetPosition;
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    [Range(1, 20)]
    private int _speed = 5;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //this._rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_targetPosition != null)
        {
            
            Vector3 direction = this._targetPosition - this._rb.position;
            this.transform.position = this.transform.position + (direction.normalized * Time.deltaTime * _speed);
        }

        if (Vector2.Distance(this._targetPosition, this._rb.position) < 0.3f)
        {
            this.transform.position = _targetPosition;
            this.enabled = false;
        }
    }

    public float SetPosition(Vector2 target)
    {
        this._targetPosition = target;
        Vector2 direction = this._targetPosition - this._rb.position;
        return Vector2.Distance(_targetPosition, this.transform.position) / _speed;
    }
    
    public void WillDieIn(float seconds)
    {
        Debug.Log("Will destroy in " + seconds);
        Invoke("DestroyCharacter", seconds);
    }

    public void DestroyCharacter()
    {
        Destroy(this.gameObject);
    }
}
