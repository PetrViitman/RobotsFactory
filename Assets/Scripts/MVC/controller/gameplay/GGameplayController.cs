using UnityEngine;

public class GGameplayController : GController
{
	private GAssemblyController assemblyController_gac;

	public GGameplayController(GGameplayModel aGameplayModel_ggm, GGameplayView aGameplayView_ggv)
		: base(aGameplayModel_ggm, aGameplayView_ggv)
	{
		GGameplayModel gameplayModel_ggm = (GGameplayModel) this.getModel();
		gameplayModel_ggm.setStateId(GGameplayModel.GAMEPLAY_STATE_ID_ASSEMBLE);

		this.assemblyController_gac = new GAssemblyController(new GAssemblyModel());
	}

	public GAssemblyController getAssemblyController()
	{
		return this.assemblyController_gac;
	}

	//EVENTS LISTENERS...
	//ASSEMBLY...
	public void onRobotAssembled()
	{
		GGameplayModel model_ggm = (GGameplayModel) this.getModel();
		model_ggm.setStateId(GGameplayModel.GAMEPLAY_STATE_ID_COMPLETED);

		GMain.getGameController().getStatisticController().onRobotAssembled();
		GMain.getGameController().startTransition(GGameModel.GAME_STATE_ID_STATISTICS);
	}
	//...ASSEMBLY

	//SCREEN...
	public void onScreenAdjusted()
	{
		if(this.assemblyController_gac != null)
		{
			GGameplayView view_ggv = (GGameplayView) this.getView();
			view_ggv.adjust();
		}
	}
	//...SCREEN

	//TEST...
	public void onTestActionButtonClicked()
	{

	}
	//...TEST

	//AUTO ASSEMBLE...
	public void onAutoAssembleRequired()
	{
		GGameplayModel gameplayModel_ggm = (GGameplayModel) this.getModel();
		GAssemblyModel assemblyModel_gam = (GAssemblyModel) this.assemblyController_gac.getModel();
		GSolutionModel solutionModel_gsm = assemblyModel_gam.getSolution();

		if(solutionModel_gsm == null)
		{
			gameplayModel_ggm.setStateId(GGameplayModel.GAMEPLAY_STATE_ID_HINT_REWIND);
		}
		else
		{
			gameplayModel_ggm.setStateId(GGameplayModel.GAMEPLAY_STATE_ID_HINT);
		}
	}
	//...AUTO ASSEMBLE

	//QUIT...
	public void onQuitButtonClicked()
	{
		GMain.getGameController().startTransition(GGameModel.GAME_STATE_ID_MENU_PAUSE);
	}
	//...QUIT
	//...BUTTONS

	//TRANSITION...
	public void onTransition()
	{
		GGameplayModel gameplayModel_ggm = (GGameplayModel) this.getModel();
		GAssemblyModel assemblyModel_gam = (GAssemblyModel) this.assemblyController_gac.getModel();

		switch (gameplayModel_ggm.getStateId())
		{
			case GGameplayModel.GAMEPLAY_STATE_ID_HINT_REWIND_TRANSITION:
			{
				assemblyModel_gam.restore();
				assemblyModel_gam.getSolution();
				gameplayModel_ggm.setStateId(GGameplayModel.GAMEPLAY_STATE_ID_HINT);
			}
			break;
			case GGameplayModel.GAMEPLAY_STATE_ID_COMPLETED:
			{
				GMain.getGameController().getRoundController().onNextRobotAssemblyRequired();
				this.getAssemblyController().restart();
				gameplayModel_ggm.setStateId(GGameplayModel.GAMEPLAY_STATE_ID_ASSEMBLE);
				GGameView.setRandomInterfaceColor();
			}
			break;
		}

		this.onScreenAdjusted();
	}

	public void onTransitionEnd()
	{
		
	}
	//...TRANSITION

	//ROUND...
	public void onTimeToRefreshCounter(string aFormatedRemainingTime_str)
	{
		GGameplayView view_ggv = (GGameplayView) this.getView();
		view_ggv.setDisplayValue(aFormatedRemainingTime_str);
	}

	public void onRoundCompleted()
	{
		GGameplayModel gameplayModel_ggm = (GGameplayModel) this.getModel();
		gameplayModel_ggm.setStateId(GGameplayModel.GAMEPLAY_STATE_ID_COMPLETED);
	}

	public void onNextRoundRequired()
	{
		this.getAssemblyController().restart();
	}
	//...ROUND
	//...EVENTS LISTENERS

	public override void update()
	{
		GGameplayModel gameplayModel_ggm = (GGameplayModel) this.getModel();
		GGridView gridView_ggv = (GGridView) this.assemblyController_gac.getGridController().getView();
		GAssemblyModel assemblyModel_gam = (GAssemblyModel) this.assemblyController_gac.getModel();

		switch(gameplayModel_ggm.getStateId())
		{	
			case GGameplayModel.GAMEPLAY_STATE_ID_HINT_REWIND:
			{
				if(gridView_ggv.getStateId() == GGridView.STATE_ID_LISTENING)
				{
					int[] action_int_arr = assemblyModel_gam.getLastPlayerAction();

					if(action_int_arr != null)
					{
						gridView_ggv.undoActionArtificially(action_int_arr);
					}
					else
					{
						if(assemblyModel_gam.playerActionsOverflowed())
						{
							gameplayModel_ggm.setStateId(GGameplayModel.GAMEPLAY_STATE_ID_HINT_REWIND_TRANSITION);
							GMain.getGameController().startTransition();
						}
						else
						{
							gameplayModel_ggm.setStateId(GGameplayModel.GAMEPLAY_STATE_ID_HINT);
						}
					}
				}
			}
			break;
			case GGameplayModel.GAMEPLAY_STATE_ID_HINT:
			{
				GSolutionModel solutionModel_gsm = assemblyModel_gam.getSolution();
				
				if(gridView_ggv.getStateId() == GGridView.STATE_ID_LISTENING)
				{
					if(!solutionModel_gsm.isEmpty())
					{
						int[] action_int_arr = solutionModel_gsm.getAction(0);

						gridView_ggv.applyActionArtificially(action_int_arr);
						solutionModel_gsm.deleteFirst();
					}
					else
					{
						gameplayModel_ggm.setStateId(GGameplayModel.GAMEPLAY_STATE_ID_ASSEMBLE);
					}
				}
			}
			break;
		}
	}
}