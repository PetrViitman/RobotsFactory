public class GInteractiveRectangleView : GRectangleView
{
	private bool isActive_bl;
	private bool isEnabled_bl;

	public GInteractiveRectangleView(float aX_num, float aY_num, float aWidth_num, float aHeight_num)
		: base(aX_num, aY_num, aWidth_num, aHeight_num)
	{	
		this.isActive_bl = false;
		this.isEnabled_bl = true;
	}

	protected virtual bool isInteraction()
	{
		return false;
	}

	protected virtual void onInteractionStart()
	{

	}

	protected virtual void onInteraction()
	{

	}

	protected virtual void onInteractionEnd()
	{

	}

	public bool isActive()
	{
		return this.isActive_bl;
	}

	public virtual void update()
	{
		if(
			this.isInteraction() &&
			this.isEnabled()
			)
		{
			if(!this.isActive_bl)
			{
				this.onInteractionStart();
				this.isActive_bl = true;
			}
			else
			{
				this.onInteraction();
			}
		}
		else
		{
			if(this.isActive_bl)
			{
				this.onInteractionEnd();
				this.isActive_bl = false;
			}
		}
	}

	public void setEnabled(bool aIsEnabled_bl)
	{
		this.isEnabled_bl = aIsEnabled_bl;
		this.onEnabledStateChanged(aIsEnabled_bl);
	}

	protected virtual void onEnabledStateChanged(bool aIsEnabled_bl)
	{

	}

	public bool isEnabled()
	{
		return this.isEnabled_bl;
	}
}