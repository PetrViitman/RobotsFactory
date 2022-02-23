public class GContinueButtonView : GButtonView
{
	private bool isActive_bl;

	public GContinueButtonView(float aX_num, float aY_num, float aWidth_num, float aHeight_num)
		: base(aX_num, aY_num, aWidth_num, aHeight_num, "C", "continue")
	{	

	}

	protected override void onButtonPressed()
	{
		GMain.getGameController().getMenuController().onContinueButtonClicked();
	}
}