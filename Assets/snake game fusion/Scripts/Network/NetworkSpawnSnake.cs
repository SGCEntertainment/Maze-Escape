using Fusion.Sockets;
using UnityEngine;
using Fusion;
using System.Collections.Generic;
using System;
using TMPro;

public class NetworkSpawnSnake : MonoBehaviour, INetworkRunnerCallbacks
{
    private static NetworkSpawnSnake instance;
    public static NetworkSpawnSnake Instance
    {
        get
        {
            if(!instance)
            {
                instance = FindObjectOfType<NetworkSpawnSnake>();
            }

            return instance;
        }
    }

    SnakeInputHandler localSnakeInputHandler;

    [SerializeField] NetworkSnake networkSnakePrefab;
    public Transform spawnPoint;

    [Space(10)]
    [SerializeField] GameObject loadingGO;
    [SerializeField] GameObject resultGO;

    [Space(10)]
    [SerializeField] TextMeshProUGUI resText;

    private void Start()
    {
        loadingGO.SetActive(true);
    }

    public void Restart()
    {
        resultGO.SetActive(false);
        NetworkSnakeCharacterController[] networkSnakeCharacterControllers = FindObjectsOfType<NetworkSnakeCharacterController>();

        foreach(NetworkSnakeCharacterController player in networkSnakeCharacterControllers)
        {
            player.GetComponent<SnakeInputHandler>().direction = Vector3.zero;
            ResetPlayerPosition(player.transform);
        }
    }

    public void ResetPlayerPosition(Transform _playerTransform)
    {
        _playerTransform.position = spawnPoint.position;
        _playerTransform.GetComponent<SnakeInputHandler>().direction = Vector3.zero;
    }

    public void ShowResult(GameObject collision)
    {
        resultGO.SetActive(true);
        resText.text = collision.GetComponent<NetworkSnake>().HasInputAuthority ? "YOU WIN" : "YOU LOSE";
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        if (runner.Topology == SimulationConfig.Topologies.Shared)
        {
            Debug.Log("OnConnectedToServer, starting player prefab as local player");
            runner.Spawn(networkSnakePrefab, spawnPoint.position, Quaternion.Euler(Vector3.zero), runner.LocalPlayer);
        }
        else
        {
            Debug.Log("OnConnectedToServer");
        }
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        loadingGO.SetActive(false);

        if (runner.IsServer)
        {
            Debug.Log("OnPlayerJoined we a server. Spawning player");
            runner.Spawn(networkSnakePrefab, spawnPoint.position, Quaternion.Euler(Vector3.zero), player);
        }
        else
        {
            Debug.Log("OnPlayerJoined");
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input) 
    { 
        if(!localSnakeInputHandler && NetworkSnake.Local)
        {
            localSnakeInputHandler = NetworkSnake.Local.GetComponent<SnakeInputHandler>();
        }

        if(localSnakeInputHandler)
        {
            input.Set(localSnakeInputHandler.GetNetworkInput());
        }
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) {}

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }

    public void OnDisconnectedFromServer(NetworkRunner runner) { }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }

    public void OnSceneLoadDone(NetworkRunner runner) { }

    public void OnSceneLoadStart(NetworkRunner runner) { }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
}
