using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomPrefabScript : MonoBehaviour
{

    TextMeshProUGUI [ ] _roomInfo;
    Button button;

    private void Awake ( )
    {
        _roomInfo = gameObject.GetComponentsInChildren<TextMeshProUGUI> ( );
        button = gameObject.GetComponentInChildren<Button> ( );
        button.onClick.AddListener ( OnClick );
    }

    void OnClick ( )
    {
        button.onClick.RemoveAllListeners ( );
        if ( !PhotonNetwork.isMasterClient )
        {
            PhotonNetwork.JoinRoom ( GetRoomName ( ) );
        }
    }

    public void SetRoomName(string roomName)
    {
        _roomInfo [ 0 ].text = roomName;
    }

    public void SetMaxPlayer(int playerAmount)
    {        
        _roomInfo [ 1 ].text = playerAmount.ToString ( );
    }

    public string GetRoomName()
    {
        return _roomInfo [ 0 ].text;
    }

    public int GetMaxPlayer()
    {
        return int.Parse(_roomInfo [ 1 ].text);
    }

}
