using UnityEngine;

public class GTransitionView : GView
{
	public const int STATE_ID_AWAITING = 0;
	public const int STATE_ID_INTRO = 1;
	public const int STATE_ID_ACTION = 2;
	public const int STATE_ID_OUTRO = 3;

	private int stateId_int = GTransitionView.STATE_ID_AWAITING;
	private int targetGameStateId_int;
	private GAdjustableValue alphaValue_gav;

	public GTransitionView()
		: base()
	{
		this.alphaValue_gav = new GAdjustableValue(10);
	}

	public void setStateId(int aStateId_int)
	{
		this.stateId_int = aStateId_int;
	}

	public int getStateId()
	{
		return this.stateId_int;
	}

	public void setTargetGameStateId(int aStateId_int)
	{
		this.targetGameStateId_int = aStateId_int;
	}

	public int getTargetGameStateId()
	{
		return this.targetGameStateId_int;
	}

	public float getAlpha()
	{
		switch(this.stateId_int)
		{
			case GTransitionView.STATE_ID_INTRO:
			{
				return this.alphaValue_gav.getValue();
			}
			case GTransitionView.STATE_ID_OUTRO:
			{
				return (1 - this.alphaValue_gav.getValue());
			}
			case GTransitionView.STATE_ID_ACTION:
			{
				return 1;
			}
			default:
			{
				return 0;
			}
		}
	}

	public void update()
	{
		if(this.alphaValue_gav.isFull())
		{
			switch(this.stateId_int)
			{
				case GTransitionView.STATE_ID_INTRO:
				{
					this.setStateId(GTransitionView.STATE_ID_ACTION);
				}
				break;
				case GTransitionView.STATE_ID_ACTION:
				{
					this.alphaValue_gav.resetValue();
					GMain.getGameController().onTransition();
					this.setStateId(GTransitionView.STATE_ID_OUTRO);
				}
				break;
				case GTransitionView.STATE_ID_OUTRO:
				{
					this.alphaValue_gav.resetValue();
					this.setStateId(GTransitionView.STATE_ID_AWAITING);
					GMain.getGameController().onTransitionEnd();
				}
				break;
			}
		}

		switch(this.stateId_int)
		{
			case GTransitionView.STATE_ID_INTRO:
			case GTransitionView.STATE_ID_OUTRO:
			{
				this.alphaValue_gav.update();
			}
			break;
		}
	}
}