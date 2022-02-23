public class GController
{
	private GModel model_gm;
	private GView view_gv;

	public GController()
	{

	}

	public GController(GModel aModel_gm)
	{
		this.model_gm = aModel_gm;
		this.model_gm.setController(this);
	}

	public GController(GView aView_gv)
	{
		this.view_gv = aView_gv;
		this.view_gv.setController(this);
	}

	public GController(GModel aModel_gm, GView aView_gv)
	{
		this.model_gm = aModel_gm;
		this.view_gv = aView_gv;

		this.view_gv.setModel(aModel_gm);
		this.view_gv.setController(this);
		this.model_gm.setView(aView_gv);
		this.model_gm.setController(this);
	}


	public GModel getModel()
	{
		return this.model_gm;
	}

	public GView getView()
	{
		return this.view_gv;
	}

	public virtual void update()
	{

	}
}