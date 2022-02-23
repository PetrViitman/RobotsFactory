using UnityEngine;

public class GFireflyView: GView
{
	public const float FIREFLY_SIZE = 2f;

	private const int ALPHA_TRANSITION_DURATION_IN_FRAMES = 50;
	private const int LIFETIME_DURATION_IN_FRAMES = 250;

	private const int FIREFLY_STATE_ID_SHOW = 0;
	private const int FIREFLY_STATE_ID_WAIT = 1;
	private const int FIREFLY_STATE_ID_HIDE = 2;

	private const float FLIGHT_SPEED = 0.05f;
	
	private int stateId_int;
	private GAdjustableValue alpha_gav;
	private int lifetimeFramesCount_int;
	private float angle_num;

	public GFireflyView()
		: base()
	{	
		this.alpha_gav = new GAdjustableValue(GFireflyView.ALPHA_TRANSITION_DURATION_IN_FRAMES);
	}

	public void reset()
	{
		this.alpha_gav.resetValue();
		this.lifetimeFramesCount_int = 0;
		this.stateId_int = GFireflyView.FIREFLY_STATE_ID_SHOW;
		this.angle_num = Random.Range(0, 360) * Mathf.PI / 180f;
	}

	public float getAlpha()
	{
		switch (this.stateId_int)
		{
			case GFireflyView.FIREFLY_STATE_ID_SHOW:
			{
				return this.alpha_gav.getValue();
			}
			case GFireflyView.FIREFLY_STATE_ID_HIDE:
			{
				return 1f - this.alpha_gav.getValue();
			}
		}

		return 1f;
	}

	public void copy(GFireflyView aFireflyView_gfv)
	{
		this.setX(aFireflyView_gfv.getX());
		this.setY(aFireflyView_gfv.getY());
		this.alpha_gav.copy(aFireflyView_gfv.alpha_gav);
		this.stateId_int = aFireflyView_gfv.stateId_int;
		this.angle_num = aFireflyView_gfv.angle_num;
		this.lifetimeFramesCount_int = aFireflyView_gfv.lifetimeFramesCount_int;
	}

	public void update()
	{
		GAdjustableValue alpha_gav = this.alpha_gav;

		switch (this.stateId_int)
		{
			case GFireflyView.FIREFLY_STATE_ID_SHOW:
			{
				alpha_gav.update();

				if(alpha_gav.isFull())
				{
					this.stateId_int = GFireflyView.FIREFLY_STATE_ID_WAIT;
				}
			}
			break;
			case GFireflyView.FIREFLY_STATE_ID_WAIT:
			{
				this.lifetimeFramesCount_int++;

				if(this.lifetimeFramesCount_int == GFireflyView.LIFETIME_DURATION_IN_FRAMES)
				{
					this.alpha_gav.resetValue();
					this.stateId_int = GFireflyView.FIREFLY_STATE_ID_HIDE;
				}
			}
			break;
			case GFireflyView.FIREFLY_STATE_ID_HIDE:
			{
				alpha_gav.update();
			}
			break;
		}

		this.setXY(
			this.getX() + GFireflyView.FLIGHT_SPEED * Mathf.Cos(this.angle_num),
			this.getY() + GFireflyView.FLIGHT_SPEED * Mathf.Sin(this.angle_num) * GScreen.getSidesRatio());
	
		this.angle_num += 0.01f;
	}

	public bool isNotRequired()
	{
		float halfSize_num = GFireflyView.FIREFLY_SIZE / 2f;

		return (
			(
				this.stateId_int == GFireflyView.FIREFLY_STATE_ID_HIDE
				&& alpha_gav.isFull()
			)
			|| this.getX() < -halfSize_num * GScreen.getSidesRatio()
			|| this.getX() > 100 + halfSize_num * GScreen.getSidesRatio()
			|| this.getY() < -halfSize_num
			|| this.getY() > 100 + halfSize_num);
	}
}