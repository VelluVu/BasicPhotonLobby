using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : Photon.PunBehaviour
{

    public static NetworkManager instance = null;
    public string version = "0.1";
    private RoomInfo[] roomsList;
    private List<GameObject> roomPrefabs = new List<GameObject> ( );
    public GameObject roomPrefab;

    private void Awake ( )
    {
        instance = this;
        DontDestroyOnLoad ( this );
    }

    private void Start ( )
    {
        PhotonNetwork.logLevel = PhotonLogLevel.ErrorsOnly;
        PhotonNetwork.ConnectUsingSettings ( version );
    }

    public override void OnConnectedToPhoton ( )
    {
        Debug.Log ( "Connected Photon" );
    }

    public override void OnCreatedRoom ( )
    {
        Debug.Log ( "Created Room" );       
        SceneManager.LoadScene ( "Game" );
             
    }

    public override void OnConnectedToMaster ( )
    {
        Debug.Log ( "Connected to Master" );
        PhotonNetwork.JoinLobby ( );
    }

    public override void OnJoinedLobby ( )
    {
        Debug.Log ( "Joined to Lobby" );
        RefreshRoomList ( );
        
    }

    public override void OnJoinedRoom ( )
    {
        Debug.Log ( "Joined to " + PhotonNetwork.room );
        SceneManager.LoadScene ( "Game" );
    }

    public override void OnReceivedRoomListUpdate ( )
    {
        roomsList = PhotonNetwork.GetRoomList ( );
        Debug.Log ( "Added Room" );

        if ( roomsList != null )
        {
            for ( int i = 0 ; i < roomsList.Length ; i++ )
            {
                Debug.Log ( roomsList [ i ].Name );

                if ( !PhotonNetwork.isMasterClient )
                {
                    GameObject roomObj = Instantiate ( roomPrefab, GameObject.Find ( "Content" ).transform );
                    RoomPrefabScript roomData = roomObj.GetComponent<RoomPrefabScript> ( );
                    roomData.SetRoomName ( roomsList [ i ].Name );
                    roomData.SetMaxPlayer ( roomsList [ i ].MaxPlayers );
                    roomPrefabs.Add ( roomObj );
                }
                else
                {
                    GameObject roomObj = PhotonNetwork.Instantiate ( "RoomInfo", GameObject.Find ( "Content" ).transform.position, Quaternion.identity, 0 );
                    RoomPrefabScript roomData = roomObj.GetComponent<RoomPrefabScript> ( );
                    roomData.SetRoomName ( roomsList [ i ].Name );
                    roomData.SetMaxPlayer ( roomsList [ i ].MaxPlayers );
                    roomPrefabs.Add ( roomObj );
                }
            }
        }       
    }

    /// <summary>
    /// Refresh roomlist by clicking refresh button
    /// </summary>
    public void RefreshRoomList()
    {
        if (roomPrefabs.Count >0)
        {
            for ( int i = 0 ; i < roomPrefabs.Count ; i++ )
            {
                Destroy ( roomPrefabs [ i ] );
            }

            roomPrefabs.Clear ( );

        }
         
        if ( roomsList != null )
        {
            for ( int i = 0 ; i < roomsList.Length ; i++ )
            {
                Debug.Log ( roomsList [ i ].Name );
                
                if ( !PhotonNetwork.isMasterClient )
                {
                    GameObject roomObj = Instantiate ( roomPrefab, GameObject.Find ( "Content" ).transform );
                    RoomPrefabScript roomData = roomObj.GetComponent<RoomPrefabScript> ( );
                    roomData.SetRoomName ( roomsList [ i ].Name );
                    roomData.SetMaxPlayer ( roomsList [ i ].MaxPlayers );
                    roomPrefabs.Add ( roomObj );
                }
                else
                {
                    GameObject roomObj = PhotonNetwork.Instantiate ( "RoomInfo", GameObject.Find ( "Content" ).transform.position, Quaternion.identity, 0 );
                    RoomPrefabScript roomData = roomObj.GetComponent<RoomPrefabScript> ( );
                    roomData.SetRoomName ( roomsList [ i ].Name );
                    roomData.SetMaxPlayer ( roomsList [ i ].MaxPlayers );
                    roomPrefabs.Add ( roomObj );
                }
            }
        }
    }

    private void OnGUI ( )
    {
        GUILayout.Label ( PhotonNetwork.connectionStateDetailed.ToString ( ) );
    }

    public override void OnPhotonJoinRoomFailed ( object [ ] codeAndMsg )
    {
        Debug.Log ( codeAndMsg [ 1 ] );
    }
}
