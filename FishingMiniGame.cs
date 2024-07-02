using System;
using Godot;

public partial class FishingMiniGame : Node2D
{
	public Node2D fish;
	public Node2D topPivot;
	public Node2D bottomPivot;
	public Node2D hook;
	
	//Fish
	public float fishTimer;
	public float fishPosition;
	public float fishTargetPos;
	[Export] public float fishThinkTime = 1f;
	[Export] public float fishMoveSpeed = 2f;
	
	//hook
	float hookPosition;
	[Export] float hookPullPower = 0.5f;
	float hookVelocity;
	[Export] float hookGravity = 0.01f;
	[Export] float hookSize = 0.2f;
	
	[Export] float hookProgress;
	[Export] float hookPower = 0.2f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		fish = GetNode<Node2D>("Fish");
		topPivot = GetNode<Node2D>("TopPivot");
		bottomPivot = GetNode<Node2D>("BottomPivot");
		hook = GetNode<Node2D>("Hook");

		fish.GlobalPosition = CalculatePosition(1f);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float timeDelta = (float)delta;
		ProcessFish(timeDelta);
		ProcessHook(timeDelta);
		DetectProgress(timeDelta);
	}

	void ProcessFish(float timeDelta)
	{
		fishTimer -= timeDelta;
		if (fishTimer < 0f)
		{
			fishTimer = fishThinkTime * GD.Randf();
			fishTargetPos = GD.Randf();
		}

		fishPosition = Mathf.Lerp(fishPosition, fishTargetPos, fishMoveSpeed * timeDelta);
		fish.GlobalPosition = CalculatePosition(fishPosition);
	}
	
	void ProcessHook(float timeDelta)
	{
		if(Input.IsActionPressed("hook_pull"))
		{
			hookVelocity += hookPullPower * timeDelta;
		}
		
		hookVelocity -= hookGravity * timeDelta;
		hookPosition += hookVelocity;
		hookPosition = Mathf.Clamp(hookPosition, hookSize/2f, 1f - hookSize/2f);
		hook.GlobalPosition = CalculatePosition(hookPosition);
	}
	
	void DetectProgress(float deltaTime)
	{
		float HookTopBoundary = hookPosition + hookSize/2f;
		float hookBottomBoundry = hookPosition - hookSize/2f;
	}
	
	Vector2 CalculatePosition(float normalizedPos)
	{
		normalizedPos = 1f - normalizedPos;
		Vector2 newPos = bottomPivot.GlobalPosition - topPivot.GlobalPosition;
		newPos *= normalizedPos;
		newPos += topPivot.GlobalPosition;
		return newPos;
	}
}
