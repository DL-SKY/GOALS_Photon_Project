using DllSky.Services;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

namespace GOALS.GlobalManagers
{
    public class PhotonManager : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();            
        }

        private void OnDestroy()
        {

        }


        public override void OnConnected()
        {
            Debug.LogError($"OnConnected() : {PhotonNetwork.CloudRegion} : {PhotonNetwork.IsMasterClient}");
        }

        public override void OnConnectedToMaster()
        {
            Debug.LogError($"OnConnectedToMaster() : {PhotonNetwork.CloudRegion} : {PhotonNetwork.IsMasterClient}");

            PhotonNetwork.JoinLobby();            
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogError($"OnDisconnected() : {PhotonNetwork.CloudRegion} : {PhotonNetwork.IsMasterClient}");
        }

        public override void OnCreatedRoom()
        {
            Debug.LogError($"OnCreatedRoom() : {PhotonNetwork.CloudRegion} : {PhotonNetwork.IsMasterClient}");
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.LogError($"OnCreateRoomFailed() : {PhotonNetwork.CloudRegion} : {PhotonNetwork.IsMasterClient}");
        }

        public override void OnJoinedRoom()
        {
            Debug.LogError($"OnJoinedRoom() : {PhotonNetwork.CloudRegion} : {PhotonNetwork.IsMasterClient}");
        }

        public override void OnLeftRoom()
        {
            Debug.LogError($"OnLeftRoom() : {PhotonNetwork.CloudRegion} : {PhotonNetwork.IsMasterClient}");
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            Debug.LogError($"OnRoomListUpdate() : roomList {roomList.Count}");
        }

        public override void OnJoinedLobby()
        {
            Debug.LogError($"OnJoinedLobby() : {PhotonNetwork.CloudRegion} : {PhotonNetwork.IsMasterClient}");

            if (PhotonNetwork.IsConnectedAndReady)
                OnCreateRoom();
            else
                Debug.LogError($"OnConnectedToMaster() : PhotonNetwork.IsConnected = false");
        }


        private void OnCreateRoom()
        {
            var options = new RoomOptions
            {
                IsVisible = true,
            };
            PhotonNetwork.JoinOrCreateRoom("001", options, TypedLobby.Default);

            //Debug.LogError($"{PhotonNetwork.get}");
        }
    }
}
