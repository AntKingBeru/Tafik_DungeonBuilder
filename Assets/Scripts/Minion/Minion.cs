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
            var job = JobManager.Instance.GetBestJob(this);
            
            if (job != null && job.TryReserve(this))
                _currentJob = job;

            return;
        }

        if (!_currentJob.IsValid())
        {
            _currentJob.Release();
            _currentJob = null;
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