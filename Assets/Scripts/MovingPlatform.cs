using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speedOfPlatform;
    [SerializeField] private Vector2[] points;
    private Rigidbody2D _platformerRigidBody;
    
    private int _index = 0;
    private Vector2 _direction;
    
    private bool _playerOnPlatform = false;
    private Rigidbody2D _rigidOnPlatform;

    private void Start()
    {
        _platformerRigidBody = GetComponent<Rigidbody2D>();
        _direction = (points[1] - points[0]).normalized;
        _platformerRigidBody.velocity = _direction * speedOfPlatform;
    }

    private void Update()
    {

        Debug.Log(Vector2.Distance(transform.position, (points[(_index + 1) % points.Length])));
        if (Vector2.Distance(transform.position, (points[(_index + 1) % points.Length])) < 0.1f)
        {
            _index++;
            if (_index == points.Length)
            {
                _index = 0;
            }
            _direction = (points[(_index + 1) % points.Length] - points[_index]).normalized;
            print(_direction);
            _platformerRigidBody.velocity = _direction * speedOfPlatform;

        }
        
        if (!(_rigidOnPlatform == null))
        {
            var velocity =_rigidOnPlatform.velocity;
            _rigidOnPlatform.velocity = velocity + _platformerRigidBody.velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _rigidOnPlatform = PlayersManager.Manager.ActivePlayerScript.PlayerRigidBody;
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        _rigidOnPlatform = null;
    }
}