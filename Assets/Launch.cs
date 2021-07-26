using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


    public class Launch: Photon.PunBehaviour
    {
		public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;
		public InputField name;
		static string playerNamePrefKey = "PlayerName";
		public Text _Connecting;
		bool flag = true;
        #region Public Variables


        #endregion


        #region Private Variables


        /// <summary>
        /// This client's version number. Users are separated from each other by gameversion (which allows you to make breaking changes).
        /// </summary>
        string _gameVersion = "1";
		
		[Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players and so new room will be created")]
		public byte MaxPlayersPerRoom = 4;


        #endregion


        #region MonoBehaviour CallBacks


        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        void Awake()
        {

			PhotonNetwork.logLevel = Loglevel;
            // #Critical
            // we don't join the lobby. There is no need to join a lobby to get the list of rooms.
            PhotonNetwork.autoJoinLobby = false;


            // #Critical
            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
            PhotonNetwork.automaticallySyncScene = true;
        }


        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
        void Start()
        {
            //Connect();
			PhotonNetwork.ConnectUsingSettings(_gameVersion);
        }
		
		void Update()
		{
			if (PhotonNetwork.connected &&flag)
            {
                // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnPhotonRandomJoinFailed() and we'll create one.
                // PhotonNetwork.JoinRandomRoom();
				// print("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
				// flag=false;
				
            }
			if(PhotonNetwork.room.PlayerCount >1 && PhotonNetwork.isMasterClient)
			{
				LoadArena();
			}
			
		}
			
			
		void LoadArena()
		{
			if (!PhotonNetwork.isMasterClient)
			{
				Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
			}
			else
			{
				Debug.Log("PhotonNetwork : Loading Level : " + PhotonNetwork.room.PlayerCount);
				PhotonNetwork.LoadLevel("Arena");
			}
			
		}

        #endregion


        #region Public Methods


        /// <summary>
        /// Start the connection process.
        /// - If already connected, we attempt joining a random room
        /// - if not yet connected, Connect this application instance to Photon Cloud Network
        /// </summary>
        public void Connect()
        {

			PhotonNetwork.playerName = name.text + " "; // force a trailing space string in case value is an empty string, else playerName would not be updated.


            PlayerPrefs.SetString(playerNamePrefKey,name.text);
            // we check if we are connected or not, we join if we are, else we initiate the connection to the server.
            if (PhotonNetwork.connected)
            {
                // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnPhotonRandomJoinFailed() and we'll create one.
                //PhotonNetwork.JoinRandomRoom();
				print("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
				
            }
            else
            {
                // #Critical, we must first and foremost connect to Photon Online Server.
                //PhotonNetwork.ConnectUsingSettings(_gameVersion);
				print("lololllllllllllllllllllllllllllllllllllllllllllllllllll");
				
            }
        }
		public void Join()
		{
			RoomOptions roomOptions = new RoomOptions();
			TypedLobby typedLobby = new TypedLobby("room1",LobbyType.Default);
			PhotonNetwork.JoinOrCreateRoom("room1",roomOptions,typedLobby);
			print("joined");
		}
		public override void OnPhotonRandomJoinFailed (object[] codeAndMsg)
		{
			Debug.Log("DemoAnimator/Launcher:OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
			// #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
			//PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
		}

		public override void OnJoinedRoom()
		{
			Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
			_Connecting.gameObject.SetActive(true);
				print("*****************************");
		}
		
		 public override void OnConnectedToMaster()
		 {
			 Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN");
		 }


		public override void OnDisconnectedFromPhoton()
		{
			Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");
		}


    #endregion


    }

