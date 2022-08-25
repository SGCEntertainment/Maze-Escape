using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Fusion.Sockets;
using UnityEngine;
using System.Linq;
using Fusion;
using System;

public class NetworkRunnerHandler : MonoBehaviour
{
    NetworkRunner networkRunner;

    private void Awake()
    {
        networkRunner = GetComponent<NetworkRunner>();
    }

    private void Start()
    {
        var clientTask = InitializeNetworkRunner(networkRunner, GameMode.AutoHostOrClient, NetAddress.Any(), SceneManager.GetActiveScene().buildIndex, null);
    }

    protected virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode gameMode, NetAddress netAddress, SceneRef sceneRef, Action<NetworkRunner> initialize)
    {
        var sceneObjectProvider = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();
        if(sceneObjectProvider == null)
        {
            sceneObjectProvider = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        runner.ProvideInput = true;

        return runner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            Address = netAddress,
            Scene = sceneRef,
            SessionName = "snake",
            Initialized = initialize,
            SceneManager = sceneObjectProvider
        });
    }
}
