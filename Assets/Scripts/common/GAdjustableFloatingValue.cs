using UnityEngine;

public class GAdjustableFloatingValue : GAdjustableValue
{
	private const int FLOATING_DIRECTION_ID_DOWN = 0;
	private const int FLOATING_DIRECTION_ID_UP = 1;

	private int floatingDirectionId_int;

	public GAdjustableFloatingValue(int aStepsNumber_int)
		: base(aStepsNumber_int)
	{
		this.floatingDirectionId_int = GAdjustableFloatingValue.FLOATING_DIRECTION_ID_DOWN;
	}

	public float getFloatingValue()
	{
		switch(this.floatingDirectionId_int)
		{
			case GAdjustableFloatingValue.FLOATING_DIRECTION_ID_DOWN:
				return 0.5f - this.getValue();
			case GAdjustableFloatingValue.FLOATING_DIRECTION_ID_UP:
				return -0.5f + this.getValue();
			default:
				return 0f;
		}
	}

	public override void update()
	{
		base.update();

		if(this.isFull())
		{
			switch(this.floatingDirectionId_int)
			{
				case GAdjustableFloatingValue.FLOATING_DIRECTION_ID_DOWN:
				{
					this.floatingDirectionId_int = GAdjustableFloatingValue.FLOATING_DIRECTION_ID_UP;
				}
				break;
				case GAdjustableFloatingValue.FLOATING_DIRECTION_ID_UP:
				{
					this.floatingDirectionId_int = GAdjustableFloatingValue.FLOATING_DIRECTION_ID_DOWN;
				}
				break;
			}

			this.resetValue();
		}
	}

	public override void randomize()
	{
		base.randomize();

		switch (Random.Range(0, 1))
		{
			case 0:
			{
				this.floatingDirectionId_int = GAdjustableFloatingValue.FLOATING_DIRECTION_ID_DOWN;
			}
			break;

			case 1:
			{
				this.floatingDirectionId_int = GAdjustableFloatingValue.FLOATING_DIRECTION_ID_UP;
			}
			break;
		}
	}
}