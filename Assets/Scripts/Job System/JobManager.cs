using UnityEngine;
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

    public Job GetJob()
    {
        if (_jobs.Count == 0)
            return null;

        var job = _jobs[0];
        _jobs.RemoveAt(0);
        return job;
    }
}