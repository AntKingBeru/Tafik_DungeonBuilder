using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class JobManager : MonoBehaviour
{
    public static JobManager Instance { get; private set; }
    
    private readonly List<Job> _jobs = new();

    private void Awake()
    {
        Instance = this;
    }

    public void AddJob(Job job)
    {
        _jobs.Add(job);
    }

    public void RemoveJob(Job job)
    {
        _jobs.Remove(job);
    }

    public Job GetBestJob(Vector3 requestedPos)
    {
        if (_jobs.Count == 0)
            return null;

        return _jobs.
            OrderByDescending(j => j.Priority)
            .ThenBy(j => Vector2.Distance(requestedPos, j.Position))
            .FirstOrDefault();
    }
}