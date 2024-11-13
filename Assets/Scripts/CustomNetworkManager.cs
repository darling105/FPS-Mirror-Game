using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CustomNetworkManager : NetworkManager
{
    NetworkConnectionToClient currentJoinedPlayer;
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        currentJoinedPlayer = conn;
        conn.identity.GetComponent<PlayerHandler>().OnInitialiseOnServer(OnPlayerInitialisation);
    }
    void OnPlayerInitialisation()
    {
        currentJoinedPlayer.identity.GetComponent<PlayerHandler>().LoadPlayerToAllClient();
        PlayerHandler[] tAllPlayerHandleOnServer = FindObjectsOfType<PlayerHandler>();
        for (int i = 0; i < tAllPlayerHandleOnServer.Length; i++)
        {
            tAllPlayerHandleOnServer[i].LoadPlayerToTargetConnection(currentJoinedPlayer);
        }
    }
}
