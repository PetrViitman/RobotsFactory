using UnityEngine;

public class GGameController : GController
{
	private GRoundController roundController_grc;
	private GGameplayController gameplayController_ggc;
	private GStatisticController statisticController_gsc;
	private GMenuController menuController_gmc;

	public GGameController(GGameModel aGameModel_ggm, GGameView aGameView_ggv)
		: base(aGameModel_ggm, aGameView_ggv)
	{
		this.roundController_grc = new GRoundController(new GRoundModel());
		this.gameplayController_ggc = new GGameplayController(new GGameplayModel(), new GGameplayView());
		this.statisticController_gsc = new GStatisticController(new GStatisticModel(), new GStatisticView());
		this.menuController_gmc = new GMenuController(new GMenuModel(), new GMenuView());
	}

	public GRoundController getRoundController()
	{
		return this.roundController_grc;
	}

	public GGameplayController getGameplayController()
	{
		return this.gameplayController_ggc;
	}

	public GStatisticController getStatisticController()
	{
		return this.statisticController_gsc;
	}

	public GMenuController getMenuController()
	{
		return this.menuController_gmc;
	}

	public void startTransition(int aTargetGameState_int)
	{
		GGameModel gameModel_ggm = (GGameModel) this.getModel();
		GGameView gameView_ggv = (GGameView) this.getView();
		GTransitionView transitionView_gtv = gameView_ggv.getTransitionView();

		transitionView_gtv.setTargetGameStateId(aTargetGameState_int);
		transitionView_gtv.setStateId(GTransitionView.STATE_ID_INTRO);
		gameModel_ggm.setIsTransitionInProgress(true);
	}

	public void startTransition()
	{
		GGameModel gameModel_ggm = (GGameModel) this.getModel();
		this.startTransition(gameModel_ggm.getStateId());
	}

	public int getModelStateId()
	{
		GGameModel aGameModel_ggm = (GGameModel) this.getModel();
		return aGameModel_ggm.getStateId();
	}

	//EVENTS LISTENERS...

	public void onGameReady()
	{
		this.getMenuController().onGameReady();
	}

	//SCREEN...
	public void onScreenAdjusted()
	{
		GGameModel gameModel_ggm = (GGameModel) this.getModel();
		
		switch(gameModel_ggm.getStateId())
		{
			case GGameModel.GAME_STATE_ID_GAMEPLAY:
			{
				this.gameplayController_ggc.onScreenAdjusted();
			}
			break;
			case GGameModel.GAME_STATE_ID_STATISTICS:
			{
				this.statisticController_gsc.onScreenAdjusted();
			}
			break;
			case GGameModel.GAME_STATE_ID_MENU_PAUSE:
			{
				this.menuController_gmc.onScreenAdjusted();
			}
			break;
		}
	}
	//...SCREEN

	//TRANSITION...
	public void onTransition()
	{
		GGameModel gameModel_ggm = (GGameModel) this.getModel();
		GGameView gameView_ggv = (GGameView) this.getView();
		GTransitionView transitionView_gtv = gameView_ggv.getTransitionView();
		gameModel_ggm.setStateId(transitionView_gtv.getTargetGameStateId());

		switch(gameModel_ggm.getStateId())
		{
			case GGameModel.GAME_STATE_ID_GAMEPLAY:
			{
				this.gameplayController_ggc.onTransition();
			}
			break;
			case GGameModel.GAME_STATE_ID_STATISTICS:
			{
				this.statisticController_gsc.onTransition();
			}
			break;
			case GGameModel.GAME_STATE_ID_MENU_PAUSE:
			{
				this.menuController_gmc.onTransition();
			}
			break;
		}

		switch(gameModel_ggm.getPreviousStateId())
		{
			case GGameModel.GAME_STATE_ID_STATISTICS:
			{
				this.statisticController_gsc.onTransitionEnd();
			}
			break;
		}
	}

	public void onTransitionEnd()
	{
		GGameModel gameModel_ggm = (GGameModel) this.getModel();
		gameModel_ggm.setIsTransitionInProgress(false);
	}
	//...TRANSITION

	//BUTTONS...
	public void onBackButtonClicked()
	{
		GGameModel gameModel_ggm = (GGameModel) this.getModel();
		this.startTransition(gameModel_ggm.getPreviousStateId());
	}
	//...BUTTONS

	//ASSEMBLY...
	public void onRobotAssembled()
	{
		//TODO...
		GRoundModel roundModel_grm = (GRoundModel) GMain.getGameController().getRoundController().getModel();
		if(roundModel_grm.getRemainingTimeInSeconds() <= 1f)
		{
			return;
		}
		//...TODO
		this.gameplayController_ggc.onRobotAssembled();
		GGameModel gameModel_ggm = (GGameModel) this.getModel();
		gameModel_ggm.incrementScore();
	}
	//...ASSEMBLY

	//ROUND...
	public void onRoundSecondPassed()
	{
		
	}

	public void onRoundCompleted()
	{
		this.getGameplayController().onRoundCompleted();

		GStatisticModel statisticModel_gsm = (GStatisticModel) this.statisticController_gsc.getModel();

		if(statisticModel_gsm.getAssembledRobotsNumber() == 0)
		{
			this.onGameOver();
			this.startTransition(GGameModel.GAME_STATE_ID_MENU_PAUSE);
		}
		else
		{
			this.getStatisticController().onRoundCompleted();
			this.startTransition(GGameModel.GAME_STATE_ID_STATISTICS);
		}
	}
	//...ROUND

	public void onGameOver()
	{
		GGameModel.setRoundIndex(0);

		GGameModel gameModel_ggm = (GGameModel) this.getModel();
		gameModel_ggm.setScore(0);
	}

	public void onNextRoundRequired()
	{
		this.roundController_grc.restart();
		GGameModel.incrementRoundIndex();
		this.gameplayController_ggc.onNextRoundRequired();
	}
	//...EVENTS LISTENERS

	public override void update()
	{
		GGameModel gameModel_ggm = (GGameModel) this.getModel();
		GGameplayModel gameplayModel_ggm = (GGameplayModel) this.gameplayController_ggc.getModel();

		switch(gameModel_ggm.getStateId())
		{
			case GGameModel.GAME_STATE_ID_GAMEPLAY:
			{
				this.gameplayController_ggc.update();
				
				if(gameplayModel_ggm.getStateId() == GGameplayModel.GAMEPLAY_STATE_ID_ASSEMBLE)
				{
					this.roundController_grc.update();
				}
			}
			break;
			case GGameModel.GAME_STATE_ID_STATISTICS:
			{
				this.statisticController_gsc.update();
			}
			break;
		}
	}
}