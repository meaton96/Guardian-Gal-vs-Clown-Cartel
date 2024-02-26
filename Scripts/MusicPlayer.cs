using Godot;
using System;

public partial class MusicPlayer : AudioStreamPlayer
{
	private double _timeBegin;
	private double _timeDelay;

	const int VU_COUNT = 16;
	const double FREQ_MAX = 11050.0;

	const int WIDTH = 400;
	const int HEIGHT = 100;

	const int MIN_DB = 60;

//	AudioEffectInstance audioInstance;

	public override void _Ready()
	{
		//_timeBegin = Time.GetTicksUsec();
	//	_timeDelay = AudioServer.GetTimeToNextMix() + AudioServer.GetOutputLatency();

		//GD.Print($"Stream: {Stream}");
		//GD.Print($"beat count: {Stream._GetBeatCount()}");
		//GD.Print($"BBM: {Stream._GetBpm()}");


	//	audioInstance = AudioServer.GetBusEffectInstance(0, 0);



		//Play();

		//Draw();



	}

	public override void _Process(double delta)
	{

		//https://docs.godotengine.org/en/stable/tutorials/audio/sync_with_audio.html#introduction
		//double time = (Time.GetTicksUsec() - _timeBegin) / 1000000.0d;
		//time = Math.Max(0.0d, time - _timeDelay);
		//GD.Print(string.Format("Time is: {0}", time));
	//	PrintBusVolume();
	}




}
