using UnityEngine;

public class GRoundController : GController
{
	public GRoundController(GModel aModel_gm)
		: base(aModel_gm)
	{
		this.restart();
	}

	public void restart()
	{
		GRoundModel roundModel_grm = (GRoundModel) this.getModel();

		roundModel_grm.setDescriptor(GProgressDescriptor.getRoundDescriptor(GGameModel.getRoundIndex()));
		roundModel_grm.setStateId(GRoundModel.ROUND_STATE_ID_PLAYING);
		this.onNextRobotAssemblyRequired();
		//TODO...
		roundModel_grm.getDescriptor().getDescriptorsPool().reset();
		//...TODO
	}

	//EVENTS LISTENERS...
	public void onSecondPassed()
	{
		GRoundModel roundModel_grm = (GRoundModel) this.getModel();

		GMain.getGameController().onRoundSecondPassed();
		GMain.getGameplayController().onTimeToRefreshCounter(roundModel_grm.getFormatedRemainingTimeAsString());
	}

	public void onNextRobotAssemblyRequired()
	{
		GRoundModel roundModel_grm = (GRoundModel) this.getModel();
		GRoundDescriptor roundDescriptor_grd = roundModel_grm.getDescriptor();
		GRobotDescriptor robotDescriptor_grd = roundDescriptor_grd.getDescriptorsPool().getNextRandomRobotDescriptor();

		roundModel_grm.incrementAssembleStepsNumberIfRequired();
		GRobotTemplate.setActualRobotDescriptor(robotDescriptor_grd);
	}
	//...EVENTS LISTENERS

	public override void update()
	{
		GRoundModel roundModel_grm = (GRoundModel) this.getModel();

		switch(roundModel_grm.getStateId())
		{
			case GRoundModel.ROUND_STATE_ID_PLAYING:
			{
				roundModel_grm.decrementRemainingTime();

				if(roundModel_grm.getRemainingTimeInSeconds() <= 0)
				{
					roundModel_grm.setStateId(GRoundModel.ROUND_STATE_ID_COMPLETED);
					GMain.getGameController().onRoundCompleted();
				}
			}
			break;
		}
	}
}