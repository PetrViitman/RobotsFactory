public class GPoint
{
	private float x_num = 0f;
	private float y_num = 0f;

	public GPoint(float aX_num, float aY_num)
	{
		this.x_num = aX_num;
		this.y_num = aY_num;
	}

	public GPoint()
		: this(0, 0)
	{
		
	}

	public virtual void setX(float aX_num)
	{
		this.x_num = aX_num;
	}

	public virtual void setY(float aY_num)
	{
		this.y_num = aY_num;
	}

	public float getX()
	{
		return this.x_num;
	}

	public float getY()
	{
		return this.y_num;
	}

	public virtual void setXY(float aX_num, float aY_num)
	{
		this.x_num = aX_num;
		this.y_num = aY_num;
	}
}