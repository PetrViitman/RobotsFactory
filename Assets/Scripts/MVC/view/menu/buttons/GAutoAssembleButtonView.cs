public class GAutoAssembleButtonView : GButtonView
{
	private bool isActive_bl;

	public GAutoAssembleButtonView(float aX_num, float aY_num, float aWidth_num, float aHeight_num)
		: base(aX_num, aY_num, aWidth_num, aHeight_num, "A", "auto assemble")
	{	

	}

	protected override void onButtonPressed()
	{
		GMain.getGameController().getMenuController().onAutoAssembleButtonClicked();
	}
}