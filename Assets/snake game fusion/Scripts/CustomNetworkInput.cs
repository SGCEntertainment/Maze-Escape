using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomNetworkInput : Fusion.Behaviour, INetworkRunnerCallbacks
{
	Vector2 GetInputDirection()
	{
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		return new Vector2(x, y);
	}

	public void OnInput(NetworkRunner runner, NetworkInput input)
	{
		var frameworkInput = new CustomNetworkInputStruct();

		if (Input.GetKey(KeyCode.W))
		{
			frameworkInput.Buttons.Set(NetworkInputPrototype.BUTTON_FORWARD, true);
		}

		if (Input.GetKey(KeyCode.S))
		{
			frameworkInput.Buttons.Set(NetworkInputPrototype.BUTTON_BACKWARD, true);
		}

		if (Input.GetKey(KeyCode.A))
		{
			frameworkInput.Buttons.Set(NetworkInputPrototype.BUTTON_LEFT, true);
		}

		if (Input.GetKey(KeyCode.D))
		{
			frameworkInput.Buttons.Set(NetworkInputPrototype.BUTTON_RIGHT, true);
		}

		input.Set(frameworkInput);






		//var userInput = new CustomNetworkInputStruct()
		//{
		//	direction = GetInputDirection()
		//};

		//input.Set(userInput);
	}

	public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }
	public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
	public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
	public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
	public void OnConnectedToServer(NetworkRunner runner) { }
	public void OnDisconnectedFromServer(NetworkRunner runner) { }
	public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
	public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
	public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
	public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
	public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
	public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
	public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
	public void OnSceneLoadDone(NetworkRunner runner) { }
	public void OnSceneLoadStart(NetworkRunner runner) { }
}

public struct CustomNetworkInputStruct : INetworkInput
{
	public Vector2 direction;

	public const int BUTTON_FORWARD = 3;
	public const int BUTTON_BACKWARD = 4;
	public const int BUTTON_LEFT = 5;
	public const int BUTTON_RIGHT = 6;

	public NetworkButtons Buttons;

	public bool IsUp(int button)
	{
		return Buttons.IsSet(button) == false;
	}

	public bool IsDown(int button)
	{
		return Buttons.IsSet(button);
	}
}
