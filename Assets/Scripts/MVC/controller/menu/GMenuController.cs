using UnityEngine;

public class GMenuController : GController
{
	public GMenuController(GModel aModel_gm, GView aView_gv)
		: base(aModel_gm, aView_gv)
	{
		
	}

	//EVENTS LISTENERS...

	//BUTTONS...
	public void onContinueButtonClicked()
	{
		GMain.getGameController().startTransition(GGameModel.GAME_STATE_ID_GAMEPLAY);
	}

	public void onAutoAssembleButtonClicked()
	{
		GMain.getGameplayController().onAutoAssembleRequired();
		GMain.getGameController().startTransition(GGameModel.GAME_STATE_ID_GAMEPLAY);
	}

	public void onPlayButtonClicked()
	{
		GMain.getGameController().onNextRoundRequired();
		GMain.getGameController().startTransition(GGameModel.GAME_STATE_ID_GAMEPLAY);
	}
	//...BUTTONS


	public void onScreenAdjusted()
	{
		GMenuView view_gmpv = (GMenuView) this.getView();
		view_gmpv.adjust();
	}

	public void onTransition()
	{
		GMenuView view_gmpv = (GMenuView) this.getView();
		view_gmpv.validate();
	}

	public void onGameReady()
	{
		GMenuView view_gmpv = (GMenuView) this.getView();
		view_gmpv.validate();
	}
	//...EVENTS LISTENERS
}