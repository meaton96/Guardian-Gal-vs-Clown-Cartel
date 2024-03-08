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
		//creates a 4600ms delay
		//
        var delay_effect = new AudioEffectDelay
        {
            FeedbackActive = true,
            FeedbackDelayMs = 0.3f, 	//??
            Tap1DelayMs = 4600,			//actual play delay
            Tap2DelayMs = 4600,
            Dry = 0						//play 0% of the original audio

        };
        var bus_idx = AudioServer.GetBusIndex("Master");
		AudioServer.AddBusEffect(bus_idx, delay_effect);	


		_timeBegin = Time.GetTicksUsec();
		_timeDelay = AudioServer.GetTimeToNextMix() + 
				AudioServer.GetOutputLatency();
		
	}
	public void PlayMusic() {Play();}


	public override void _Process(double delta)
	{
		double time = (Time.GetTicksUsec() - _timeBegin) / 1000000.0d;
		time = Math.Max(0.0d, time - _timeDelay);
		//GD.Print(string.Format("Time is: {0}", time));
	}
}