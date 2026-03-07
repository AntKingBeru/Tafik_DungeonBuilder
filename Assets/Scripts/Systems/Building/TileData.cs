public class TileData
{
    public bool IsCleared;
    public RoomInstance Room;
    public SurfaceType TileSurfaceType;

    public TileData(SurfaceType surface)
    {
        TileSurfaceType = surface;
        IsCleared = false;
        Room = null;
    }
}