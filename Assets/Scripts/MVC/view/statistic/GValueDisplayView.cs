using UnityEngine;

public class GValueDisplayView : GRectangleView
{
	private const int DISPLAY_STATE_ID_WAITING = 0;
	private const int DISPLAY_STATE_ID_SHOWING = 1;
	private const int DISPLAY_STATE_ID_HIDING = 2;

	private const int VALUE_SHOWING_HIDING_DURATION_IN_FRAMES = 15;

	private int stateId_int;
	private string value_str;
	private string targetValue_str;
	private GAdjustableValue alpha_gav;
	private float padding_num;

	private GColor color_gc;

	public GValueDisplayView(float aX_num, float aY_num, float aWidth_num, float aHeight_num)
		: base(aX_num, aY_num, aWidth_num, aHeight_num)
	{
		this.alpha_gav = new GAdjustableValue(GValueDisplayView.VALUE_SHOWING_HIDING_DURATION_IN_FRAMES);
		this.stateId_int = GValueDisplayView.DISPLAY_STATE_ID_SHOWING;
		this.value_str = "";
		this.padding_num = 0f;
		this.color_gc = new GColor(255, 255, 255);
	}

	public void setShowingHidingDurationInFrames(int aShowingHidingDurationInFrames_int)
	{
		this.alpha_gav.resetValue(aShowingHidingDurationInFrames_int);
	}

	public void setRGB(int aRed_int, int aGreen_int, int aBlue_int)
	{
		this.color_gc.setRGB(aRed_int, aGreen_int, aBlue_int);
	}

	public GColor getColor()
	{
		return this.color_gc;
	}

	public void setTargetValue(string aValue_str)
	{
		if(aValue_str == this.value_str)
		{
			return;
		}

		this.targetValue_str = aValue_str;
		this.stateId_int = GValueDisplayView.DISPLAY_STATE_ID_HIDING;
		this.alpha_gav.resetValue();
	}

	public string getValue()
	{
		return this.value_str;
	}

	public void setPadding(float aPadding_num)
	{
		this.padding_num = aPadding_num;
	}

	public float getPadding()
	{
		return this.padding_num;
	}

	public float getAlpha()
	{
		switch(this.stateId_int)
		{
			case GValueDisplayView.DISPLAY_STATE_ID_SHOWING:
			{
				return this.alpha_gav.getValue();
			}
			case GValueDisplayView.DISPLAY_STATE_ID_HIDING:
			{
				return 1f - this.alpha_gav.getValue();
			}
			default:
			{
				return 1f;
			}
		}
	}

	public void update()
	{
		GAdjustableValue alpha_gav = this.alpha_gav;

		switch(this.stateId_int)
		{
			case GValueDisplayView.DISPLAY_STATE_ID_SHOWING:
			case GValueDisplayView.DISPLAY_STATE_ID_HIDING:
			{
				alpha_gav.update();
			}
			break;
		}

		if(alpha_gav.isFull())
			{
				switch(this.stateId_int)
				{
					case GValueDisplayView.DISPLAY_STATE_ID_SHOWING:
					{
						this.stateId_int = GValueDisplayView.DISPLAY_STATE_ID_WAITING;
					}
					break;
					case GValueDisplayView.DISPLAY_STATE_ID_HIDING:
					{
						this.stateId_int = GValueDisplayView.DISPLAY_STATE_ID_SHOWING;
						this.value_str = this.targetValue_str;
						this.alpha_gav.resetValue();
					}
					break;
				}
			}
	}
}