using UnityEngine;

public class MinionMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    private Vector3 _target;
    private bool _moving;

    public void MoveTo(Vector3 pos)
    {
        _target = pos;
        _moving = true;
    }

    private void Update()
    {
        if (!_moving) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            _target,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, _target) < 0.1f)
            _moving = false;
    }
}