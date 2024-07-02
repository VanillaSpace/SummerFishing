using System;
using Godot;

public partial class FishingMiniGameUI : Control
{
	Label fishCount;
	TextureProgressBar progressBar;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		fishCount = GetNode<Label>("Label");
		progressBar = GetNode<TextureProgressBar>("TextureProgressBar");
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




