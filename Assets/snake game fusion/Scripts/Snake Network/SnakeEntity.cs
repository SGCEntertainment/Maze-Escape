using Fusion;
using System.Collections.Generic;

public class SnakeEntity : SnakeComponent
{
	[UnityEngine.HideInInspector]
    public SnakeController Controller { get; private set; }

	[UnityEngine.HideInInspector]
	public NetworkTransform NetTransform { get; private set; }

	public static readonly List<SnakeEntity> Karts = new List<SnakeEntity>();

	private void Awake()
	{
		// Set references before initializing all components
		Controller = GetComponent<SnakeController>();
		NetTransform = GetComponent<NetworkTransform>();

		// Initializes all KartComponents on or under the Kart prefab
		var components = GetComponentsInChildren<SnakeComponent>();
		foreach (var component in components)
		{
			component.Init(this);
		}
	}

	public override void Spawned()
	{
		base.Spawned();
		Karts.Add(this);
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		base.Despawned(runner, hasState);
		Karts.Remove(this);
	}
}
