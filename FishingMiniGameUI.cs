using System;
using Godot;

public partial class FishingMiniGameUI : Control
{
	Label fishCount;
	TextureProgressBar progressBar;
	
	AnimationPlayer[] waterAnimations = new AnimationPlayer[5];
	AnimationPlayer fisherWomanAnim;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		fishCount = GetNode<Label>("Label");
		progressBar = GetNode<TextureProgressBar>("TextureProgressBar");
	
		//Water animation
		for (int i = 1; i < waterAnimations.Length + 1; i++)
		{
			waterAnimations[i-1] = GetNode<AnimationPlayer>($"Water{i}/Anim{i}");
			if (waterAnimations[i-1] != null)
			{
				waterAnimations[i-1].Play("Wave");
			}
			else
			{
				GD.PrintErr($"AnimationPlayer not found: Water{i}");
			}
		}
		
		//FisherWoman Animation
		fisherWomanAnim = GetNode<AnimationPlayer>("Fisher/FisherWomanAnim");
		fisherWomanAnim.Play("Fishing");
	}
	
	public override void _Process(double delta)
	{
		
	}
	
	private void OnFishingMiniGameOnFishCaught(long count)
	{
		fishCount.Text = count.ToString();
	}
	private void OnFishingMiniGameOnFishProgress(double progress)
	{
		progressBar.Value = progress * 100f;
	}
}




