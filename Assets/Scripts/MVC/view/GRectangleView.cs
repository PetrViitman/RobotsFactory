using UnityEngine;

public class GRectangleView : GView
{
	private float width_num;
	private float height_num;

	public GRectangleView(float aX_num, float aY_num, float aWidth_num, float aHeight_num)
		: base(aX_num, aY_num)
	{
		this.setWidth(aWidth_num);
		this.setHeight(aHeight_num);
	}

	public void setWidth(float aWidth_num)
	{
		this.width_num = aWidth_num;
	}

	public float getWidth()
	{
		return this.width_num;
	}

	public void setHeight(float aHeight_num)
	{
		this.height_num = aHeight_num;
	}

	public float getHeight()
	{
		return this.height_num;
	}

	public bool isCollision(GPoint aPoint_gp)
	{
		float screenSidesRatio_num = GScreen.getSidesRatio();
		float pointX_num = aPoint_gp.getX();
		float pointY_num = aPoint_gp.getY();

		if(
			pointX_num < this.getX()
			|| pointX_num > this.getX() + this.getWidth() * screenSidesRatio_num
			|| pointY_num < this.getY()
			|| pointY_num > this.getY() + this.getHeight()
		)
		{
			return false;
		}
		return true;
	}
}