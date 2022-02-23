public class GView : GPoint
{
	private GModel model_gm;
	private GController controller_gc;

	public GView()
		: base()
	{

	}

	public GView(float aX_num, float aY_num)
		: base(aX_num, aY_num)
	{

	}

	public void setModel(GModel aModel_gm)
	{
		if(this.model_gm == null)
		{
			this.model_gm = aModel_gm;
			this.onModelSet(aModel_gm);
		}
	}

	protected GModel getModel()
	{
		return this.model_gm;
	}

	protected virtual void onModelSet(GModel aModel_gm)
	{
		
	}

	public void setController(GController aGController_gc)
	{
		this.controller_gc = aGController_gc;
	}

	protected GController getController()
	{
		return this.controller_gc;
	}

	public virtual void validate()
	{
		
	}
}