using UnityEngine;

public class GStatisticModel : GStatesPoweredModel
{
	public const int STATISTIC_STATE_ID_INCREMENTATION = 0;
	public const int STATISTIC_STATE_ID_DECREMENTATION = 1;
	public const int STATISTIC_STATE_ID_DISPLAY = 2;
	public const int STATISTIC_STATE_ID_WAITING_CONTINUATION = 3;
	public const int STATISTIC_STATE_ID_WAITING_GAMEOVER = 4;

	private const int DELAY_DURATION_IN_FRAMES = 60;

	private int assembledRobotsNumber_int;
	private GAdjustableValue delay_gav;

	public GStatisticModel()
		: base()
	{
		this.assembledRobotsNumber_int = 0;
		this.delay_gav = new GAdjustableValue(GStatisticModel.DELAY_DURATION_IN_FRAMES);
	}

	public void setAssembledRobotsNumber(int aAssembledRobotsNumber_int)
	{
		this.assembledRobotsNumber_int = aAssembledRobotsNumber_int;
	}

	public int getAssembledRobotsNumber()
	{
		return this.assembledRobotsNumber_int;
	}

	public void incrementAssembledRobotsNumber()
	{
		//TEMPORARELY BLOCKED BASED ON VIEW LIMIT...
		if(this.assembledRobotsNumber_int >= GStatisticView.MAXIMAL_ROBOTS_NUMBER)
		{
			return;
		}
		//...TEMPORARELY BLOCKED BASED ON VIEW LIMIT

		this.assembledRobotsNumber_int++;
		this.validateView();
	}

	public void decrementAssembledRobotsNumber()
	{
		this.assembledRobotsNumber_int--;

		if(this.assembledRobotsNumber_int < 0)
		{
			this.assembledRobotsNumber_int = 0;
		}
	}

	public void update()
	{
		switch(this.getStateId())
		{
			case GStatisticModel.STATISTIC_STATE_ID_WAITING_CONTINUATION:
			case GStatisticModel.STATISTIC_STATE_ID_WAITING_GAMEOVER:
			{
				this.delay_gav.update();
				if(this.delay_gav.isFull())
				{
					GStatisticController controller_gsc = (GStatisticController) this.getController();
					controller_gsc.onWaitingFinished();
					this.delay_gav.resetValue();
				}
			}
			break;
		}
	}
}
