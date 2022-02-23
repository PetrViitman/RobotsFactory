public class GTestActionButtonView : GButtonView
{
	private bool isActive_bl;

	public GTestActionButtonView(float aX_num, float aY_num, float aWidth_num, float aHeight_num)
		: base(aX_num, aY_num, aWidth_num, aHeight_num, "test")
	{	

	}

	protected override void onInteractionStart()
	{
		GMain.getGameplayController().onTestActionButtonClicked();
	}
}