public class GQuitButtonView : GButtonView
{
	private bool isActive_bl;

	public GQuitButtonView(float aX_num, float aY_num, float aWidth_num, float aHeight_num)
		: base(aX_num, aY_num, aWidth_num, aHeight_num, "**")
	{	

	}

	protected override void onButtonPressed()
	{
		//TODO...
		GRoundModel roundModel_grm = (GRoundModel) GMain.getGameController().getRoundController().getModel();
		if(roundModel_grm.getRemainingTimeInSeconds() <= 1f)
		{
			return;
		}
		//...TODO

		GMain.getGameplayController().onQuitButtonClicked();
	}
}