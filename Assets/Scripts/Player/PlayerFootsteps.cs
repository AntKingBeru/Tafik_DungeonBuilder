using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerFootsteps : MonoBehaviour
{
    [Header("Timing")]
    [SerializeField] private float stepInterval = 0.4f;
    
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] stoneSteps;
    [SerializeField] private AudioClip[] woodSteps;
    
    [Header("References")]
    [SerializeField] private PlayerMovement movement;
    
    private float _stepTimer;

    private void Update()
    {
        HandleFootsteps();
    }

    private void HandleFootsteps()
    { 
        if (!movement.IsMoving)
        {
            _stepTimer = 0;
            return;
        }

        _stepTimer += Time.deltaTime;

        if (!(_stepTimer >= stepInterval)) return;
        
        PlayFootstep();
        _stepTimer = 0;
    }

    private void PlayFootstep()
    {
        // var surface = DungeonGrid.Instance.GetSurfaceAtPosition(transform.position);

        // var clip = GetClipForSurface(surface);
        
        // if (clip) audioSource.PlayOneShot(clip);
    }

    // private AudioClip GetClipForSurface(SurfaceType surface)
    // {
    //     return surface switch
    //     {
    //         SurfaceType.Stone => stoneSteps[Random.Range(0, stoneSteps.Length)],
    //         SurfaceType.Wood => woodSteps[Random.Range(0, woodSteps.Length)],
    //         _ => null
    //     };
    // }
}