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

    public Job GetBestJob(Minion minion)
    {
        return _jobs
            .Where (j => j.IsValid() && !j.IsReserved)
            .OrderByDescending(j => j.Priority)
            .ThenBy(j => Vector2.Distance(minion.transform.position, j.Position))
            .FirstOrDefault();
    }
}