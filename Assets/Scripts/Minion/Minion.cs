using UnityEngine;

[RequireComponent(typeof(MinionMovement))]
public class Minion : MonoBehaviour
{
    [SerializeField] private MinionMovement movement;
    
    public MinionMovement Movement => movement;

    private Job _currentJob;
    
    private MinionState _state;

    private void Update()
    {
        if (_currentJob == null)
        {
            AssignJob();
            return;
        }
        
        var done = _currentJob.Execute(this);
        
        if (done)
            _currentJob = null;
    }

    private void AssignJob()
    {
        _currentJob = JobManager.Instance.GetJob();
    }
}