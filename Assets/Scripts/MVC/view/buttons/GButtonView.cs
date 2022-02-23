public class GButtonView : GInteractiveRectangleView
{
	private const int STATE_ID_LISTENING = 0;
	private const int STATE_ID_INTRO = 1;
	private const int STATE_ID_OUTRO = 2;

	private const int INTRO_OUTRO_DURATION_IN_FRAMES = 10;

	private string caption_str;
	private string subcaption_str;
	private bool isRequired_bl;
	private bool isActive_bl;
	private int stateId_int;
	private GAdjustableValue alpha_gav;


	public GButtonView(float aX_num, float aY_num, float aWidth_num, float aHeight_num, string aCaption_str, string aSubaption_str)
		: base(aX_num, aY_num, aWidth_num, aHeight_num)
	{	
		this.caption_str = aCaption_str;
		this.subcaption_str = aSubaption_str;
		this.alpha_gav = new GAdjustableValue(GButtonView.INTRO_OUTRO_DURATION_IN_FRAMES);
		this.isRequired_bl = true;
	}

	public GButtonView(float aX_num, float aY_num, float aWidth_num, float aHeight_num, string aCaption_str)
		: this(aX_num, aY_num, aWidth_num, aHeight_num, aCaption_str, "")
	{	

	}

	public string getCaption()
	{
		return this.caption_str;
	}

	public string getSubcaption()
	{
		return this.subcaption_str;
	}

	public void setIsRequired(bool aIsRequired_bl)
	{
		this.isRequired_bl = aIsRequired_bl;
	}

	public bool isRequired()
	{
		return this.isRequired_bl;
	}

	protected override bool isInteraction()
	{
		GGameController gameController_ggc = GMain.getGameController();
		GGameModel gameModel_ggm = (GGameModel) gameController_ggc.getModel();
		
		if(gameModel_ggm.isTransitionInProgress())
		{
			return false;
		}

		GMouseModel mouseModel_gmm = (GMouseModel) GMain.getMouseController().getModel();

		if(
			!mouseModel_gmm.isJustClicked()
			|| this.stateId_int != GButtonView.STATE_ID_LISTENING
			)
		{
			return false;
		}

		return this.isCollision(mouseModel_gmm.getDownPoint());
	}

	public float getAlpha()
	{
		float value_num = this.alpha_gav.getValue();

		switch(this.stateId_int)
		{
			case GButtonView.STATE_ID_INTRO: return 1f - value_num;
			case GButtonView.STATE_ID_OUTRO: return value_num;
			default: return 1f;
		}
	}

	protected override void onInteractionStart()
	{
		this.stateId_int = GButtonView.STATE_ID_INTRO;
		this.alpha_gav.resetValue();
	}

	protected virtual void onButtonPressed()
	{

	}

	public override void update()
	{
		if(!this.isRequired_bl)
		{
			return;
		}

		base.update();

		GAdjustableValue alpha_gav = this.alpha_gav;

		if(this.stateId_int != GButtonView.STATE_ID_LISTENING)
		{
			alpha_gav.update();

			if(alpha_gav.isFull())
			{
				switch(this.stateId_int)
				{
					case GButtonView.STATE_ID_INTRO:
					{
						this.onButtonPressed();
						alpha_gav.resetValue();
						this.stateId_int = GButtonView.STATE_ID_OUTRO;
					}
					break;
					case GButtonView.STATE_ID_OUTRO:
					{
						this.stateId_int = GButtonView.STATE_ID_LISTENING;
					}
					break;
				}
			}
		}
	}
}