public class GStatisticButtonView : GButtonView
{
	private bool isActive_bl;

	public GStatisticButtonView(float aX_num, float aY_num, float aWidth_num, float aHeight_num)
		: base(aX_num, aY_num, aWidth_num, aHeight_num, "R", "robots")
	{	

	}

	protected override void onButtonPressed()
	{
		GMain.getGameController().startTransition(GGameModel.GAME_STATE_ID_STATISTICS);
	}
}