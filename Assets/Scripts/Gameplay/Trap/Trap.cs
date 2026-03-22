using UnityEngine;

public class Trap : MonoBehaviour
{
    protected RoomInstance Room;

    public void Initialize(RoomInstance owningRoom)
    {
        Room = owningRoom;
    }
}