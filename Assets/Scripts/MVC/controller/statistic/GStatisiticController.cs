using UnityEngine;

public class GStatisticController : GController
{
	public GStatisticController(GModel aModel_gm, GView aView_gv)
		: base(aModel_gm, aView_gv)
	{
		GStatisticModel model_gsm = (GStatisticModel) this.getModel();
		model_gsm.setStateId(GStatisticModel.STATISTIC_STATE_ID_DISPLAY);
	}

	public void update()
	{
		GStatisticModel model_gsm = (GStatisticModel) this.getModel();
		model_gsm.update();
	}

	//EVENTS LISTENERS...
	public void onScreenAdjusted()
	{
		GStatisticView statisticView_gsv = (GStatisticView) this.getView();
		statisticView_gsv.adjust();
	}

	public void onRobotAssembled()
	{
		GStatisticModel model_gsm = (GStatisticModel) this.getModel();
		model_gsm.incrementAssembledRobotsNumber();
		model_gsm.setStateId(GStatisticModel.STATISTIC_STATE_ID_INCREMENTATION);
	}

	public void onTransition()
	{
		GStatisticModel model_gsm = (GStatisticModel) this.getModel();
		GStatisticView statisticView_gsv = (GStatisticView) this.getView();
		statisticView_gsv.validate();

		if(model_gsm.getStateId() == GStatisticModel.STATISTIC_STATE_ID_DECREMENTATION)
		{
			statisticView_gsv.hideNextRobot();
		}
	}

	public void onTransitionEnd()
	{
		GStatisticModel model_gsm = (GStatisticModel) this.getModel();
		model_gsm.setStateId(GStatisticModel.STATISTIC_STATE_ID_DISPLAY);
	}

	public void onBackButtonClicked()
	{
		GMain.getGameController().onBackButtonClicked();
	}

	public void onRoundCompleted()
	{
		GStatisticModel model_gsm = (GStatisticModel) this.getModel();
		GStatisticView statisticView_gsv = (GStatisticView) this.getView();

		model_gsm.setStateId(GStatisticModel.STATISTIC_STATE_ID_DECREMENTATION);
	}

	public void onRobotStartHiding()
	{
		GStatisticView statisticView_gsv = (GStatisticView) this.getView();
		statisticView_gsv.setCounterValue(statisticView_gsv.getVisibleRobotsNumber() - 1);
	}

	public void onRobotHidden()
	{
		GStatisticModel model_gsm = (GStatisticModel) this.getModel();
		GStatisticView statisticView_gsv = (GStatisticView) this.getView();
		GRoundController roundController_grc = GMain.getGameController().getRoundController();
		GRoundModel roundModel_grm = (GRoundModel)roundController_grc.getModel();
		GRoundDescriptor roundDescriptor_grd = roundModel_grm.getDescriptor();
		int finalRemainingRobotsNumber_int = model_gsm.getAssembledRobotsNumber() - roundDescriptor_grd.getRequiredAssembledRobotsNumber();

		if(finalRemainingRobotsNumber_int >= 0)
		{
			if(statisticView_gsv.getVisibleRobotsNumber() > finalRemainingRobotsNumber_int)
			{	
				statisticView_gsv.hideNextRobot();
			}
			else
			{
				model_gsm.setAssembledRobotsNumber(finalRemainingRobotsNumber_int);
				GMain.getGameController().onNextRoundRequired();
				model_gsm.setStateId(GStatisticModel.STATISTIC_STATE_ID_WAITING_CONTINUATION);
			}
		}
		else
		{
			if(statisticView_gsv.getVisibleRobotsNumber() > 0)
			{	
				statisticView_gsv.hideNextRobot();
			}
			else
			{
				GMain.getGameController().onGameOver();
				model_gsm.setAssembledRobotsNumber(0);
				model_gsm.setStateId(GStatisticModel.STATISTIC_STATE_ID_WAITING_GAMEOVER);
			}
		}
	}

	public void onWaitingFinished()
	{
		GStatisticModel model_gsm = (GStatisticModel) this.getModel();
		GGameController gameController_ggc = GMain.getGameController();

		switch(model_gsm.getStateId())
		{
			case GStatisticModel.STATISTIC_STATE_ID_WAITING_CONTINUATION:
			{
				gameController_ggc.startTransition(GGameModel.GAME_STATE_ID_GAMEPLAY);
			}
			break;
			case GStatisticModel.STATISTIC_STATE_ID_WAITING_GAMEOVER:
			{
				gameController_ggc.startTransition(GGameModel.GAME_STATE_ID_MENU_PAUSE);
			}
			break;
		}
	}
	//...EVENTS LISTENERS
}