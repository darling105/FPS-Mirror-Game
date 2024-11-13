using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerHandler : NetworkBehaviour
{
    [SerializeField] GameObject[] allPlayers;
    [SerializeField] int playerToLoad = Random.Range(0,2);

    System.Action actionOnInitialisation;
    private void Start()
    {
        if (isLocalPlayer)
        {
            CmdPlayerInitialisationOnServer(playerToLoad);
        }
    }

    [Command]
    public void CmdPlayerInitialisationOnServer(int aPlayNum)
    {
        playerToLoad = aPlayNum;
        actionOnInitialisation?.Invoke();
    }
    public void OnInitialiseOnServer(System.Action aActionOnInitialisation)
    {
        actionOnInitialisation = aActionOnInitialisation;
    }
    public void LoadPlayerToAllClient()
    {
        RPCLoadPlayerToAllClient(playerToLoad);
    }

    public void LoadPlayerToTargetConnection(NetworkConnectionToClient conn)
    {
        TRPCLoadPlayerToTargetConnection(conn, playerToLoad);
    }
    [ClientRpc]
    public void RPCLoadPlayerToAllClient(int aPlayNum)
    {
        spawnPlayer(aPlayNum);
    }

    [TargetRpc]
    public void TRPCLoadPlayerToTargetConnection(NetworkConnectionToClient conn, int aPlayNum)
    {
        spawnPlayer(aPlayNum);
    }
    bool isPlayerSpawn;
    void spawnPlayer(int aPlayNum)
    {
        if (isPlayerSpawn)
        return;
            GameObject tPlayerToLoad = Instantiate(allPlayers[aPlayNum], transform);
            tPlayerToLoad.transform.localPosition = tPlayerToLoad.transform.localEulerAngles = Vector3.zero;
            isPlayerSpawn = true;
        

    }
}
