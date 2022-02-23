public class GGuideButtonView : GButtonView
{
	private bool isActive_bl;

	public GGuideButtonView(float aX_num, float aY_num, float aWidth_num, float aHeight_num)
		: base(aX_num, aY_num, aWidth_num, aHeight_num, "s", "assembled sample")
	{	

	}

	protected override void onButtonPressed()
	{
		//GMain.getGameplayController().onGuideButtonClicked();
	}
}