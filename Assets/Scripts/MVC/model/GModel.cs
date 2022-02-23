public class GModel
{
	private GView view_gv;
	private GController controller_gc;

	public GModel()
	{

	}

	public void setView(GView aView_gv)
	{
		if(this.view_gv == null)
		{
			this.view_gv = aView_gv;
			this.onViewSet(aView_gv);
			this.view_gv.setModel(this);
			this.validateView();
		}
	}

	protected virtual void onViewSet(GView aView_gv)
	{
		
	}

	public void setController(GController aController_gc)
	{
		this.controller_gc = aController_gc;
	}

	public GController getController()
	{
		return this.controller_gc;
	}

	public GView getView()
	{
		return this.view_gv;
	}

	protected void validateView()
	{
		if(this.view_gv != null)
		{
			this.view_gv.validate();
		}
	}
}