using UnityEngine;

public class GStatisticRobotView : GRectangleView
{
	private const int FLOATING_UP_DOWN_DURATION_IN_FRAMES = 150;
	private const int ALPHA_UP_DOWN_DURATION_IN_FRAMES = 30;

	private const int STATE_ID_SHOWING = 0;
	private const int STATE_ID_HIDING = 1;
	private const int STATE_ID_WAITING_VISIBLE = 2;
	private const int STATE_ID_WAITING_HIDDEN = 3;

	private GAdjustableFloatingValue floatingHeigth_gafv;
	private GAdjustableValue alpha_gav;
	private int stateId_int = GStatisticRobotView.STATE_ID_WAITING_VISIBLE;

	public GStatisticRobotView()
		: base(0f, 0f, 0f, 0f)
	{
		this.floatingHeigth_gafv = new GAdjustableFloatingValue(GStatisticRobotView.FLOATING_UP_DOWN_DURATION_IN_FRAMES);
		this.floatingHeigth_gafv.randomize();
		this.alpha_gav = new GAdjustableValue(GStatisticRobotView.ALPHA_UP_DOWN_DURATION_IN_FRAMES); 
	}

	public float getFloatingHeightValue()
	{
		return this.floatingHeigth_gafv.getFloatingValue();
	}

	public void update()
	{
		this.floatingHeigth_gafv.update();

		switch(this.stateId_int)
		{
			case GStatisticRobotView.STATE_ID_SHOWING:
			{
				this.alpha_gav.update();

				if(this.alpha_gav.isFull())
				{
					this.stateId_int = GStatisticRobotView.STATE_ID_WAITING_VISIBLE;
				}
			}
			break;
			case GStatisticRobotView.STATE_ID_HIDING:
			{
				this.alpha_gav.update();

				if(this.alpha_gav.isFull())
				{
					this.stateId_int = GStatisticRobotView.STATE_ID_WAITING_HIDDEN;
					GMain.getGameController().getStatisticController().onRobotHidden();
				}
			}
			break;
		}
	}

	public void show(bool aIsImmediate_bl)
	{
		if(aIsImmediate_bl)
		{
			this.stateId_int = GStatisticRobotView.STATE_ID_WAITING_VISIBLE;
		}
		else
		{
			this.stateId_int = GStatisticRobotView.STATE_ID_SHOWING;
			this.alpha_gav.resetValue();
		}
	}

	public void hide(bool aIsImmediate_bl)
	{
		if(aIsImmediate_bl)
		{
			this.stateId_int = GStatisticRobotView.STATE_ID_WAITING_HIDDEN;
		}
		else
		{
			this.stateId_int = GStatisticRobotView.STATE_ID_HIDING;
			this.alpha_gav.resetValue();
		}
	}

	public float getAlpha()
	{
		switch(this.stateId_int)
		{
			case GStatisticRobotView.STATE_ID_SHOWING:
			{
				return this.alpha_gav.getValue();
			}
			break;
			case GStatisticRobotView.STATE_ID_HIDING:
			{
				return 1f - this.alpha_gav.getValue();
			}
			break;
			case GStatisticRobotView.STATE_ID_WAITING_VISIBLE:
			{
				return 1f;
			}
			break;
			case GStatisticRobotView.STATE_ID_WAITING_HIDDEN:
			default:
			{
				return 0f;
			}
			break;
		}
	}
}