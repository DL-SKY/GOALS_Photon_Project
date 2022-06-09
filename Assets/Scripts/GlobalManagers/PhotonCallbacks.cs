﻿using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GOALS.GlobalManagers
{
    public class PhotonCallbacks : MonoBehaviourPunCallbacks
    {
        public event Action EventOnConnectedToMaster;
        public event Action<DisconnectCause> EventOnDisconnected;

        public event Action EventOnJoinedLobby;
        public event Action EventOnLeftLobby;

        public event Action EventOnCreatedRoom;
        public event Action<short, string> EventOnCreateRoomFailed;
        public event Action EventOnJoinedRoom;
        public event Action<short, string> EventOnJoinRoomFailed;
        public event Action EventOnLeftRoom;
        public event Action<Player> EventOnPlayerEnteredRoom;
        public event Action<Player> EventOnPlayerLeftRoom;
        public event Action<List<RoomInfo>> EventOnRoomListUpdate;
        public event Action<Hashtable> EventOnRoomPropertiesUpdate;


        public override void OnConnectedToMaster()
        {
            Debug.Log($"OnConnectedToMaster() // CloudRegion : {PhotonNetwork.CloudRegion} // IsMasterClient : {PhotonNetwork.IsMasterClient}");
            EventOnConnectedToMaster?.Invoke();
        }        

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log($"OnDisconnected() // CloudRegion : {PhotonNetwork.CloudRegion} // IsMasterClient : {PhotonNetwork.IsMasterClient}");
            EventOnDisconnected?.Invoke(cause);
        }


        public override void OnJoinedLobby()
        {
            Debug.Log($"OnJoinedLobby() // CloudRegion : {PhotonNetwork.CloudRegion} // IsMasterClient : {PhotonNetwork.IsMasterClient}");
            EventOnJoinedLobby?.Invoke();
        }

        public override void OnLeftLobby()
        {
            Debug.Log($"OnLeftLobby() // CloudRegion : {PhotonNetwork.CloudRegion} // IsMasterClient : {PhotonNetwork.IsMasterClient}");
            EventOnLeftLobby?.Invoke();
        }


        public override void OnCreatedRoom()
        {
            Debug.Log($"OnCreatedRoom() // CloudRegion : {PhotonNetwork.CloudRegion} // IsMasterClient : {PhotonNetwork.IsMasterClient}");
            EventOnCreatedRoom?.Invoke();
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log($"OnCreateRoomFailed() // CloudRegion : {PhotonNetwork.CloudRegion} // IsMasterClient : {PhotonNetwork.IsMasterClient}");
            EventOnCreateRoomFailed?.Invoke(returnCode, message);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log($"OnJoinedRoom() // CloudRegion : {PhotonNetwork.CloudRegion} // IsMasterClient : {PhotonNetwork.IsMasterClient}");
            EventOnJoinedRoom?.Invoke();
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log($"OnJoinRoomFailed() // CloudRegion : {PhotonNetwork.CloudRegion} // IsMasterClient : {PhotonNetwork.IsMasterClient}");
            EventOnJoinRoomFailed?.Invoke(returnCode, message);
        }

        public override void OnLeftRoom()
        {
            Debug.Log($"OnLeftRoom() // CloudRegion : {PhotonNetwork.CloudRegion} // IsMasterClient : {PhotonNetwork.IsMasterClient}");
            EventOnLeftRoom?.Invoke();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log($"OnPlayerEnteredRoom() // CloudRegion : {PhotonNetwork.CloudRegion} // IsMasterClient : {PhotonNetwork.IsMasterClient}");
            EventOnPlayerEnteredRoom?.Invoke(newPlayer);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log($"OnPlayerLeftRoom() // CloudRegion : {PhotonNetwork.CloudRegion} // IsMasterClient : {PhotonNetwork.IsMasterClient}");
            EventOnPlayerLeftRoom?.Invoke(otherPlayer);
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            Debug.Log($"OnRoomListUpdate() // CloudRegion : {PhotonNetwork.CloudRegion} // IsMasterClient : {PhotonNetwork.IsMasterClient}");
            EventOnRoomListUpdate?.Invoke(roomList);
        }

        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            Debug.Log($"OnRoomPropertiesUpdate() // CloudRegion : {PhotonNetwork.CloudRegion} // IsMasterClient : {PhotonNetwork.IsMasterClient}");
            EventOnRoomPropertiesUpdate?.Invoke(propertiesThatChanged);
        }
    }
}
