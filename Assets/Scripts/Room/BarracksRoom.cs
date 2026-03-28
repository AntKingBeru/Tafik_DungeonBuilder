using UnityEngine;

public class BarracksRoom : MonoBehaviour
{
    [SerializeField] private int bonus = 5;

    private void Start()
    {
        MinionManager.Instance.AddBarracksBonus(bonus);
    }
}