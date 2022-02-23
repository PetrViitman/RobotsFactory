using UnityEngine;

public class GRoundModel : GStatesPoweredModel
{
	public const int ROUND_STATE_ID_INVALID = GStatesPoweredModel.STATE_ID_INVALID;
	public const int ROUND_STATE_ID_PLAYING = 1;
	public const int ROUND_STATE_ID_COMPLETED = 2;

	private float remainingTimeInSeconds_num = 100;
	private int previousTickRemainingTimeInSeconds_int;
	private GRoundDescriptor roundDescriptor_grd;
	private int assembleStepsNumber_int;

	public GRoundModel()
		: base()
	{

	}

	public void setDescriptor(GRoundDescriptor aRoundDescriptor_grd)
	{
		this.roundDescriptor_grd = aRoundDescriptor_grd;
		this.remainingTimeInSeconds_num = roundDescriptor_grd.getDurationInSeconds();
	
		if(aRoundDescriptor_grd.isProdressiveAssemblyDifficultyRequired())
		{
			this.assembleStepsNumber_int = 0;
		}
		else
		{
			this.assembleStepsNumber_int = roundDescriptor_grd.getAssembleSepsNumber();
		}
	}

	public int getAssembleSepsNumber()
	{
		return this.assembleStepsNumber_int;
	}

	public void incrementAssembleStepsNumberIfRequired()
	{
		if(this.assembleStepsNumber_int < this.roundDescriptor_grd.getAssembleSepsNumber())
		{
			this.assembleStepsNumber_int++;
		}
	}

	public GRoundDescriptor getDescriptor()
	{
		return this.roundDescriptor_grd;
	}

	public void decrementRemainingTime()
	{
		this.previousTickRemainingTimeInSeconds_int = this.getFormatedRemainingTimeInSeconds();
		this.remainingTimeInSeconds_num -= Time.deltaTime;

		if(this.remainingTimeInSeconds_num < 0)
		{
			this.remainingTimeInSeconds_num = 0;
		}

		if(this.getFormatedRemainingTimeInSeconds() != this.previousTickRemainingTimeInSeconds_int)
		{
			GMain.getGameController().getRoundController().onSecondPassed();
		}
	}

	public float getRemainingTimeInSeconds()
	{
		return this.remainingTimeInSeconds_num;
	}

	public int getFormatedRemainingTimeInSeconds()
	{
		return (int) Mathf.Floor(this.remainingTimeInSeconds_num);
	}

	public string getFormatedRemainingTimeAsString()
	{
		string remainingTime_str = "";
		int remainingTimeInSeconds_int = this.getFormatedRemainingTimeInSeconds();

		int minutesNumber_int = remainingTimeInSeconds_int / 60;
		int secondsNumber_int = (int) Mathf.Floor(remainingTimeInSeconds_int % 60); 

		remainingTime_str += minutesNumber_int + ":";
		if(secondsNumber_int < 10)
		{
			remainingTime_str += "0";
		}
		remainingTime_str += secondsNumber_int;

		return remainingTime_str;
	}
}