using UnityEngine;

public class GMouseModel : GModel
{
	private bool isDown_bl = false;
	private GPoint downPoint_gp = null;
	private GPoint position_gp = null;
	private int interationInputIndex_int;

	public GMouseModel()
		: base()
	{
		this.downPoint_gp = new GPoint();
		this.position_gp = new GPoint();
	}

	public void setX(float aX_num)
	{
		this.position_gp.setX(aX_num);
	}

	public void setY(float aY_num)
	{
		this.position_gp.setY(aY_num);
	}

	public void setXY(float aX_num, float aY_num)
	{
		this.position_gp.setXY(aX_num, aY_num);
	}

	public float getX()
	{
		return this.position_gp.getX();
	}

	public float getY()
	{
		return this.position_gp.getY();
	}

	public void setIsDown(bool aIsDown_bl)
	{
		this.isDown_bl = aIsDown_bl;

		if(!aIsDown_bl)
		{
			this.interationInputIndex_int = 0;
		}
	}

	public bool isDown()
	{
		return this.isDown_bl;
	}

	public bool isJustClicked()
	{
		return this.isDown_bl && this.interationInputIndex_int <= 3;
	}

	public void setDownXY(float aX_num, float aY_num)
	{
		this.downPoint_gp.setXY(aX_num, aY_num);
	}

	public void incrementInteractionInpuIndex()
	{
		this.interationInputIndex_int++;
	}

	public GPoint getDownPoint()
	{
		return this.downPoint_gp;
	}

	public float getDeltaX()
	{
		return Mathf.Abs((float)(this.getX() - this.downPoint_gp.getX()));
	}

	public float getDeltaY()
	{
		return Mathf.Abs((float)(this.getY() - this.downPoint_gp.getY()));
	}
}