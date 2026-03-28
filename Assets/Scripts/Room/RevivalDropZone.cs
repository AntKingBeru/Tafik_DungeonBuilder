using UnityEngine;

public class RevivalDropZone : MonoBehaviour
{
    [SerializeField] private RevivalRoom room;

    public void ReceiveCorpse(Corpse corpse)
    {
        room.Revive(corpse);
    }
}