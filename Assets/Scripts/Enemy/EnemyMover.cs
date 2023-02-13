using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    private const int Perpendicular = 90;
    private const float OffsetError = 0.01f;

    [SerializeField] private float _speed;
    [SerializeField] private int _minAngleOfIncidence;
    [SerializeField] private int _maxAngleOfIncidence;

    private Vector3 _currentTargetPosition;
    private int _startingRayPositionIndicator = -1;
    private int _angleOfIncidence;

    private void Start()
    {
        _angleOfIncidence = Random.Range(_minAngleOfIncidence, _maxAngleOfIncidence);

        DetermineNextPoint();
    }

    private void Update()
    {
        if (transform.position == _currentTargetPosition)
            DetermineNextPoint();

        transform.position = Vector2.MoveTowards(transform.position, _currentTargetPosition, _speed * Time.deltaTime);
    }

    private void DetermineNextPoint()
    {
        Vector2 possibleVector1 = new Vector2(Mathf.Cos(_angleOfIncidence * _startingRayPositionIndicator * Mathf.Deg2Rad), Mathf.Sin(_angleOfIncidence * _startingRayPositionIndicator * Mathf.Deg2Rad));
        Vector2 possibleVector2 = new Vector2(Mathf.Cos((_angleOfIncidence * _startingRayPositionIndicator + 180) * Mathf.Deg2Rad), Mathf.Sin((_angleOfIncidence * _startingRayPositionIndicator + 180) * Mathf.Deg2Rad));

        Vector2 startingVector1 = new Vector2(transform.position.x + OffsetError * _startingRayPositionIndicator, transform.position.y + OffsetError * _startingRayPositionIndicator);
        Vector2 startingVector2 = new Vector2(transform.position.x - OffsetError * _startingRayPositionIndicator, transform.position.y - OffsetError * _startingRayPositionIndicator);

        RaycastHit2D ray1 = Physics2D.Raycast(startingVector1, possibleVector1);
        RaycastHit2D ray2 = Physics2D.Raycast(startingVector2, possibleVector2);

        if (ray1.distance > ray2.distance)
            _currentTargetPosition = ray1.point;
        else
            _currentTargetPosition = ray2.point;

        _angleOfIncidence = Perpendicular - _angleOfIncidence;
        _startingRayPositionIndicator *= -1;
    }
}
