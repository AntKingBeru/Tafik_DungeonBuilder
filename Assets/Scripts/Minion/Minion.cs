using UnityEngine;

[RequireComponent(typeof(MinionMovement))]
[RequireComponent(typeof(Combat))]
public class Minion : MonoBehaviour, IDamageable
{
    public MinionCarry Carry { get; private set; }

    private MinionMovement _movement;
    private Combat _combat;

    private Job _currentJob;

    private void Awake()
    {
        _movement = GetComponent<MinionMovement>();
        _combat = GetComponent<Combat>();
        Carry = GetComponent<MinionCarry>();
    }

    private void Update()
    {
        _currentJob ??= JobManager.Instance.RequestJob(this);

        _currentJob?.Execute(this);
    }

    public void MoveTo(Vector3 pos) => _movement.MoveTo(pos);

    public void Attack(Enemy enemy)
    {
        _combat.TryAttack(enemy);
    }

    public void TakeDamage(DamageData damage)
    {
        Destroy(gameObject);
    }
}