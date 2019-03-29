using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomLister : MonoBehaviour
{
   
    public GameObject _roomObject;
    RoomPrefabScript _roomData;
  
    TextMeshProUGUI [ ] texts;

    public void CreateRoomListObject(string roomName, int players)
    {
        Debug.Log ( "Instantiated object to roomlist" );
   
        GameObject roomObject = Instantiate ( _roomObject, transform ) as GameObject;
        _roomData = roomObject.GetComponent<RoomPrefabScript> ( );
        _roomData.SetRoomName ( roomName );
        _roomData.SetMaxPlayer ( players );
        
        RoomOptions roomOptions = new RoomOptions ( );
        roomOptions.MaxPlayers = ( byte ) players;

        PhotonNetwork.CreateRoom ( roomName, roomOptions, TypedLobby.Default );

    }

}
