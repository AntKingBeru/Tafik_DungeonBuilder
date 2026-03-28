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

    public Job GetAvailableJob()
    {
        return _jobs.FirstOrDefault(job => !job.IsReserved);
    }

    public void CompleteJob(Job job)
    {
        _jobs.Remove(job);
    }
    
    public Job RequestJob(Minion minion)
    {
        foreach (var job in _jobs)
        {
            if (!job.IsReserved)
            {
                job.Reserve(minion);
                return job;
            }
        }

        return null;
    }
}