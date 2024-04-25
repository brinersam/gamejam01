using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class CheckPointMover : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private bool _movesInitially = false;
    [SerializeField] private bool _multiUse = true;
    [SerializeField] private bool _goesIdle = false;
    [SerializeField] private Collider2D[] _checkPointList;
    [SerializeField] private ContactFilter2D _CF;
    [SerializeField] private float _distanceCheckEveryxSec = 5;

    private Collider2D[] _arrToucher = new Collider2D[1];

    private Collider2D _collider;
    private float _distCheckCD = 0;
    private int _idxGoalCP = 0;
    private bool _isMoving = false;
    private bool _reversing = false;
    private Vector3 _moveVector;

    public  bool IsMoving => _isMoving;

    private void Awake()
    {
        if (_checkPointList.Length == 0)
            Debug.LogWarning("No checkpoints set @", gameObject);
    }

    private void Start()
    {   
        _collider = GetComponent<Collider2D>();

        if (_checkPointList.Length == 0)
            return;

        transform.position = _checkPointList[0].gameObject.transform.position;

        if (_movesInitially)
            StartMoving();

        System_Ticker.Instance.OnSecond += SubscribeTicker;
    }

    public void ResetTrap(bool andMove)
    {
        _idxGoalCP = 0;
        if (andMove)
            StartMoving();
    }

    public void StartMoving()
    {
        _isMoving = true;
        ProceedToNextCheckPoint();
    }

    private void FixedUpdate()
    {
        if (_isMoving == true)
            transform.position = Move();
    }

    private void SubscribeTicker()
    {
        if (_isMoving == true)
            _distCheckCD += 1;
    }

    private Vector3 Move()
    {
        if (_distCheckCD >= _distanceCheckEveryxSec)
        {
            _distCheckCD = 0;
            if (_collider.OverlapCollider(_CF, _arrToucher) > 0)
            {
                Debug.DrawLine(transform.position, _checkPointList[_idxGoalCP].gameObject.transform.position, Color.green, 0.5f);
                ProceedToNextCheckPoint();
            }
            else
            {
                Debug.DrawLine(transform.position, _checkPointList[_idxGoalCP].gameObject.transform.position, Color.red, 0.5f);
            }
        }

        return Vector2.MoveTowards(transform.position, _checkPointList[_idxGoalCP].transform.position, _speed * Time.deltaTime);
    }

    private void ProceedToNextCheckPoint()
    {
        transform.position = _checkPointList[_idxGoalCP].gameObject.transform.position;

        int newIdx = AdvanceIdx(_idxGoalCP);

        if (newIdx >= _checkPointList.Length) // last checkpoint
        {
            if (!_multiUse)
            {
                this.enabled = false;
                System_Ticker.Instance.OnSecond -= SubscribeTicker;
                return;
            }
            newIdx -= 2;

            if (_goesIdle)
            {
                transform.position = _checkPointList[0].gameObject.transform.position;
                _isMoving = false;
                return;
            }

            _reversing = true;
            
        }
        else if (newIdx <= 0)
        {
            _reversing = false;
            newIdx += 2;
        }

        _idxGoalCP = newIdx;
    }

    private int AdvanceIdx(int idx)
    {
        if (_reversing)
            return idx - 1;
            
        return idx + 1;
    }
}
