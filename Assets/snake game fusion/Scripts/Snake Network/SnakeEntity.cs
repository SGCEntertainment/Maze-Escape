using Fusion;
using UnityEngine;
using System.Collections.Generic;

public class SnakeEntity : SnakeComponent
{
	[HideInInspector]
	public int StepCount { get; set; }

	[HideInInspector]
    public SnakeController Controller { get; private set; }

	[HideInInspector]
	public NetworkTransform NetTransform { get; private set; }

	[HideInInspector]
	public Rigidbody2D Rigidbody2D { get; set; }

	[HideInInspector]
	public NetworkRigidbody2D NetworkRigidbody2D { get; set; }

	public static readonly List<SnakeEntity> Karts = new List<SnakeEntity>();

	private void Awake()
	{
		// Set references before initializing all components
		Controller = GetComponent<SnakeController>();
		NetTransform = GetComponent<NetworkTransform>();
		Rigidbody2D = GetComponent<Rigidbody2D>();
		NetworkRigidbody2D = GetComponent<NetworkRigidbody2D>();

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
