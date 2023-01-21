using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTile : MonoBehaviour
{
    [SerializeField] Image mapTileImage;
    [SerializeField] List<MapConnectionPoint> tileConnectionPoints;
    [SerializeField] Room refRoom;
    [SerializeField] private Sprite regBorder;
    [SerializeField] private Sprite playerBorder;
    [SerializeField] private GameObject playerFaceIcon;
    [SerializeField] private GameObject specialRoomIcon;
    [SerializeField] private GameObject emergencyExitIcon;
    [SerializeField] private GameObject npcIcon;
    [SerializeField] private GameObject chestIcon;

    public Image MapTileImage { get => mapTileImage; set => mapTileImage = value; }
    public List<MapConnectionPoint> TileConnectionPoints { get => tileConnectionPoints; set => tileConnectionPoints = value; }
    public Room RefRoom { get => refRoom; }
    public GameObject PlayerFaceIcon { get => playerFaceIcon; }
    public GameObject SpecialRoomIcon { get => specialRoomIcon; }
    public GameObject EmergencyExitIcon { get => emergencyExitIcon; }
    public GameObject NpcIcon { get => npcIcon; }
    public GameObject ChestIcon { get => chestIcon; }

    public void CacheRoom(Room givenRoom)
    {
        refRoom = givenRoom;
    }
    public void OnEnterSub()
    {
        RefRoom.OnEntered.AddListener(UpdateIconsOnEnter);
    }
    public MapConnectionPoint GetConnectionPointFromExitLocation(ExitLocationInfo givenLocation)
    {
        foreach (var item in TileConnectionPoints)
        {
            if (item.LocationInfo.EquateLoc(givenLocation))
            {
                return item;
            }
        }
        return null;
    }

    public void SetNodeSize(int x, int y)
    {
        ((RectTransform)mapTileImage.transform).sizeDelta = new Vector2(((RectTransform)mapTileImage.transform).sizeDelta.x * x, ((RectTransform)mapTileImage.transform).sizeDelta.y * y);
    }

    public void UpdateIconsOnEnter()
    {
        DisableIcons();

        if (refRoom.HasChest)
        {
            specialRoomIcon.SetActive(false);
            chestIcon.SetActive(true);
        }
        else if (refRoom.HasPortal)
        {
            specialRoomIcon.SetActive(false);
            emergencyExitIcon.SetActive(true);
        }
        else if (refRoom is NpcRoom)
        {
            specialRoomIcon.SetActive(false);
            npcIcon.SetActive(true);
        }
    }

    public void DisableIcons()
    {
        chestIcon.SetActive(false);
        emergencyExitIcon.SetActive(false);
        npcIcon.SetActive(false);
    }

    public void PlayerVisualsOn()
    {
        playerFaceIcon.SetActive(true);
        mapTileImage.sprite = playerBorder;

    }
    public void PlayerVisualsOff()
    {
        playerFaceIcon.SetActive(false);
        mapTileImage.sprite = regBorder;
    }
}
