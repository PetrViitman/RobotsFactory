using UnityEngine;

public class GAssemblyModel : GModel
{
	private const int MAXIMAL_PLAYER_TRACKED_ACTIONS_NUMBER = 5000;//overflow is buggy and needs to be fixed

	private GSolverModel solver_gsm = null;
	private GProblemModel problemModel_gpm;
	private GGridModel gridModel_ggm;
	private GSolutionModel solution_gsm;

	private GProblemStateModelPool playerActions_gpsmp;
	private bool playerActionsOverflowed_bl = false;

	private int iterator = 0;

	public GAssemblyModel()
		: base()
	{
		this.solver_gsm = new GSolverModel(
			GSolverModel.DEFAULT_MAXIMAL_DIFICULTY_LEVEL,
			GSolutionModelPool.DEFAULT_MAXIMAL_SOLUTIONS_NUMBER,
			GSolutionModel.DEFAULT_MAXIMAL_SOLUTION_LENGTH);

		playerActions_gpsmp = new GProblemStateModelPool(GAssemblyModel.MAXIMAL_PLAYER_TRACKED_ACTIONS_NUMBER);
	}

	public void trackAction(int aActionId_int, int aEntryPoint_int, int aDelta_int)
	{
		if(playerActions_gpsmp.isFull())
		{
			playerActions_gpsmp.deleteFirst();
			this.playerActionsOverflowed_bl = true;
		}

		int[][] idsMap_int_arr_arr = this.gridModel_ggm.getIdsMap();
		int[] action_int_arr = new int[]{aActionId_int, aEntryPoint_int, aDelta_int};

		GRoundModel roundModel_grm = (GRoundModel) GMain.getGameController().getRoundController().getModel();
		GRoundDescriptor roundDescriptor_grd = roundModel_grm.getDescriptor();

		if(
			playerActions_gpsmp.isEmpty() &&
			roundModel_grm.getAssembleSepsNumber() >= this.problemModel_gpm.getStates().length()//if assembly starts not from the very begining
			)
		{
			playerActions_gpsmp.add(this.problemModel_gpm.getInitialIdsMap(), new int[]{0,0,0});
		}

		playerActions_gpsmp.add(idsMap_int_arr_arr, action_int_arr);
		playerActions_gpsmp.deleteExcessives();

		if(playerActions_gpsmp.isEmpty())
		{
			playerActions_gpsmp.add(this.problemModel_gpm.getInitialIdsMap(), new int[]{0,0,0});
		}

		//playerActions_gpsmp.print();
	}

	public GSolutionModel getSolution()
	{
		if(this.solution_gsm == null)
		{
			this.solution_gsm = this.solver_gsm.getSolution(this);
		}

		return this.solution_gsm;
	}

	public GProblemModel getProblemModel()
	{
		return this.problemModel_gpm;
	}

	public GGridModel getGridModel()
	{
		return this.gridModel_ggm;
	}

	public GProblemStateModelPool getPlayerActions()
	{
		return this.playerActions_gpsmp;
	}

	public int[] getLastPlayerAction()
	{
		if(this.playerActions_gpsmp.length() <= 1)
		{
			return null;
		}

		return this.playerActions_gpsmp.getLastState().getAction();
	}

	public void restore()
	{
		this.playerActions_gpsmp.drop();


		GRoundModel roundModel_grm = (GRoundModel) GMain.getGameController().getRoundController().getModel();
		GProblemStateModelPool problemStateModelPool_gpsmp = this.problemModel_gpm.getStates();

		int requiredStepIndex_int = roundModel_grm.getAssembleSepsNumber() - 1;
		
		if(requiredStepIndex_int >= problemStateModelPool_gpsmp.length())
		{
			requiredStepIndex_int = problemStateModelPool_gpsmp.length() - 1;
		}

		GProblemStateModel problemStateModel_gpsm = problemStateModelPool_gpsmp.getState(requiredStepIndex_int);

		this.gridModel_ggm.adjust(problemStateModel_gpsm.getIdsMap());
		this.playerActionsOverflowed_bl = false;
	}

	public bool playerActionsOverflowed()
	{
		return this.playerActionsOverflowed_bl;
	}

	public void restart()
	{
		GProblemModel problemModel_gpm = GStorage.getProblem(GRobotTemplate.ROBOT_DESCRIPTOR.getTemplateDescriptor(), this.iterator);
		GGridModel gridModel_ggm = new GGridModel(problemModel_gpm.getInitialIdsMap());

		this.problemModel_gpm = problemModel_gpm;
		this.gridModel_ggm = gridModel_ggm;
		this.solution_gsm = null;

		this.iterator++;
		this.restore();
	}
}