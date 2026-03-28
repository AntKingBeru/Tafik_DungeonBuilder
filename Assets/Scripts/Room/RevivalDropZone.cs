using UnityEngine;

public class RevivalDropZone : MonoBehaviour
{
    public void ReceiveCorpse(Corpse corpse)
    {
        if (!corpse)
            return;
        
        corpse.gameObject.SetActive(false);
        
        RevivalRoom.Instance.ReceiveCorpse(corpse);
        
        Destroy(corpse.gameObject);
    }
}