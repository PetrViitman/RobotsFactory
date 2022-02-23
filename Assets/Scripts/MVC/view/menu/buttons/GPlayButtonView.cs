public class GPlayButtonView : GButtonView
{
	public GPlayButtonView(float aX_num, float aY_num, float aWidth_num, float aHeight_num)
		: base(aX_num, aY_num, aWidth_num, aHeight_num, "P", "play")
	{	

	}

	protected override void onButtonPressed()
	{
		GMain.getGameController().getMenuController().onPlayButtonClicked();
	}
}