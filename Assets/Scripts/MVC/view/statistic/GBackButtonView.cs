public class GBackButtonView : GButtonView
{
	private bool isActive_bl;

	public GBackButtonView(float aX_num, float aY_num, float aWidth_num, float aHeight_num)
		: base(aX_num, aY_num, aWidth_num, aHeight_num, "R", "return")
	{	

	}

	protected override void onButtonPressed()
	{
		GMain.getGameController().getStatisticController().onBackButtonClicked();
	}
}