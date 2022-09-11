public class RoomConnectivity
{
    public bool CheckRoomCennctivity(Room origin, Room connectedRoom)
    {
        if (connectedRoom.RoomType == RoomType.BOSS && origin.RoomType != RoomType.HARD)
        {
            return false;
        }

        /*if (origin.IsStartingRoom && connectedRoom.RoomType != RoomType.EASY)
        {
            return false;
        }*/

        return true;
    }
}
