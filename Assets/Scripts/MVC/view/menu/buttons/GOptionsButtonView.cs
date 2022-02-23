public class GOptionsButtonView : GButtonView
{
	private bool isActive_bl;

	public GOptionsButtonView(float aX_num, float aY_num, float aWidth_num, float aHeight_num)
		: base(aX_num, aY_num, aWidth_num, aHeight_num, "o", "options")
	{	

	}

	protected override void onButtonPressed()
	{
		//GMain.getGameplayController().onAutoAssembleButtonClicked();
	}
}