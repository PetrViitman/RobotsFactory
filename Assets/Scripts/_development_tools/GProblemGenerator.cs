using UnityEngine;

public class GProblemGenerator
{
	private const int PATHS_NUMBER_DIFFICULTY_LEVEL_1 = 50; //14
	private const int PATHS_NUMBER_DIFFICULTY_LEVEL_2 = 500; //172
	private const int PATHS_NUMBER_DIFFICULTY_LEVEL_3 = 2500; //1256
	private const int PATHS_NUMBER_DIFFICULTY_LEVEL_4 = 600000; //28258
	private const int PATHS_NUMBER_DIFFICULTY_LEVEL_5 = 10000000; //857753

	private GSolverModel solver_gsm = null;

	public GProblemGenerator()
	{
		this.solver_gsm = new GSolverModel(
			4,
			GProblemGenerator.PATHS_NUMBER_DIFFICULTY_LEVEL_4,
			GSolutionModel.DEFAULT_MAXIMAL_SOLUTION_LENGTH);
	}

	public void generate(GRobotDescriptor aRobotDescriptor_grd, int aDificultyLevel_int, int aNumberOfGenerations_int)
	{
		int numberOfGenerations_int = 0;

		while( numberOfGenerations_int < aNumberOfGenerations_int )
		{
			GProblemModel problemModel_gpm = new GProblemModel(aRobotDescriptor_grd.getIdsMap(), aDificultyLevel_int, this.solver_gsm);
			GStorage.saveProblemStatesPool(problemModel_gpm.getStates(), aRobotDescriptor_grd.getTemplateId());
			
			numberOfGenerations_int++;
		}
	}
}