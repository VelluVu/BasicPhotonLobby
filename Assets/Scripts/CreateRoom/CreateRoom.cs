using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour
{

    public InputField inputText;
    public InputField inputText2;
    string _roomName;
    public RoomLister addRoom;
    int maxPlayers;

    public void NameRoom ( )
    {
        if ( inputText.text != null && inputText.text.Length > 1 )
        {
            _roomName = inputText.text;
            Debug.Log ( "Room name " + _roomName );           
        }
    }

    public void MaxPlayerInput ( )
    {
        if ( inputText2.text.Length == 1 &&
            ( inputText2.text == "1" ||
            inputText2.text == "2" ||
            inputText2.text == "3" ||
            inputText2.text == "4" ||
            inputText2.text == "5" ) )
        {
            maxPlayers = int.Parse ( inputText2.text );
            Debug.Log ( maxPlayers );
        }
    }

    public void MakeRoom ( )
    {
        if ( _roomName != null && (maxPlayers > 0 || maxPlayers <= 5))
        {
            Debug.Log ( maxPlayers );
            Debug.Log ( "Made Room " + _roomName );
            addRoom.CreateRoomListObject ( _roomName , maxPlayers );
        }
    }

}
