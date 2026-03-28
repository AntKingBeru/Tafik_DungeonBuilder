using UnityEngine;

[RequireComponent(typeof(MinionMovement))]
public class Minion : MonoBehaviour
{
    [SerializeField] private MinionMovement movement;
    
    public MinionMovement Movement => movement;

    private Job _currentJob;

    private void Update()
    {
        if (_currentJob == null)
        {
            _currentJob = JobManager.Instance.GetBestJob(transform.position);
            return;
        }
        
        var done = _currentJob.Execute(this);
        
        if (done)
        {
            JobManager.Instance.RemoveJob(_currentJob);
            _currentJob = null;
        }
    }
}