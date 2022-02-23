using UnityEngine;

public class GProblemModel : GModel
{
	private const int MAXIMAL_CALCULATION_LENGTH = 50;
	private const int MAXIMAL_STATES_NUMBER = 20;

	private GProblemStateModelPool states_gpsmp;
	private int[][] initialIdsMap_int_arr_arr;
	private int[][] finalIdsMap_int_arr_arr;

	public GProblemModel(GTemplateDescriptor aTemplateDescriptor_gtd, GProblemStateModelPool aStatesPool_gpsmp)
		: base()
	{
		this.initialIdsMap_int_arr_arr = aStatesPool_gpsmp.getLastState().getIdsMap();
		this.states_gpsmp = aStatesPool_gpsmp;
		this.finalIdsMap_int_arr_arr = aTemplateDescriptor_gtd.getIdsMap();
	}

	public GProblemModel(int[][] aFinalIdsMap_int_arr_arr, int aIterationsNumber_int, GSolverModel aSolver_gsm)
		: base()
	{
		this.states_gpsmp = new GProblemStateModelPool(GProblemModel.MAXIMAL_STATES_NUMBER);
		this.finalIdsMap_int_arr_arr = aFinalIdsMap_int_arr_arr;

		GGridModel gridModel_ggm = new GGridModel(aFinalIdsMap_int_arr_arr);
		GProblemStateModelPool states_gpsmp = this.states_gpsmp;

		int calculationLength_int = 0;

		while(states_gpsmp.length() < aIterationsNumber_int)
		{	
			if(states_gpsmp.isEmpty())
			{
				gridModel_ggm.adjust(aFinalIdsMap_int_arr_arr);
			}
			else
			{
				gridModel_ggm.adjust(states_gpsmp.getLastState().getIdsMap());
			}

			int[] randomAction_int_arr = this.generateRandomAction();

			gridModel_ggm.applyAction(
					randomAction_int_arr[0],
					randomAction_int_arr[1],
					randomAction_int_arr[2]);

			bool actionIsValid_bl = true;

			//CHECKING INSTANT SOLUTION...
			if(!states_gpsmp.isEmpty())
			{
				GSolutionModel solution_gsm =  aSolver_gsm.getSolution(gridModel_ggm, aFinalIdsMap_int_arr_arr);

				if(solution_gsm != null && solution_gsm.length() < states_gpsmp.length())
				{
					actionIsValid_bl = false;
				}
			}
			//...CHECKING INSTANT SOLUTION

			//CHECKING PROBLEM STEP IN SOLVABLE RANGE...
			for( int i = 1; i <= aSolver_gsm.getMaximalDifficultyLevel(); i++)
			{
				int previousStateIndex_int = states_gpsmp.length() - i;
				int solutionToPreviousStateLength_int = i;

				if(previousStateIndex_int <= 0)
				{
					continue;
				}
				
				int[][] previousIdsMap_int_arr_arr = states_gpsmp.getState(previousStateIndex_int).getIdsMap();
				
				GSolutionModel solution_gsm = aSolver_gsm.getSolution(gridModel_ggm, previousIdsMap_int_arr_arr);
				
				if(solution_gsm.length() != solutionToPreviousStateLength_int)
				{
					actionIsValid_bl = false;
					break;
				}
			}
			//...CHECKING PROBLEM STEP IN SOLVABLE RANGE

			//SKIP IF ACTION IS NOT VALID...
			if(!actionIsValid_bl)
			{
				//EXITING POSSIBLE ENDLESS LOOP...
				calculationLength_int++;
				if(calculationLength_int > GProblemModel.MAXIMAL_CALCULATION_LENGTH)
				{
					calculationLength_int = 0;
					states_gpsmp.drop();
				}
				//...EXITING POSSIBLE ENDLESS LOOP

				continue;
			}
			//...SKIP IF ACTION IS NOT VALID

			//CHECKING PROBLEM STEP IN UNSOLVABLE RANGE...
			for( int i = states_gpsmp.length() - aSolver_gsm.getMaximalDifficultyLevel() - 1; i >= 0; i--)
			{
				int[][] previousIdsMap_int_arr_arr = states_gpsmp.getState(i).getIdsMap();
				
				GSolutionModel solution_gsm = aSolver_gsm.getSolution(gridModel_ggm, previousIdsMap_int_arr_arr);

				if(
					solution_gsm != null &&
					solution_gsm.length() < i
					)
				{
					actionIsValid_bl = false;
					break;
				}
			}
			//...CHECKING PROBLEM STEP IN UNSOLVABLE RANGE
			
			//ADDING PROBLEM STATE TO POOL...
			if(actionIsValid_bl)
			{
				states_gpsmp.add(gridModel_ggm.getIdsMap(), randomAction_int_arr);
			}
			//...ADDING PROBLEM STATE TO POOL
			
			//EXITING POSSIBLE ENDLESS LOOP...
			calculationLength_int++;
			if(calculationLength_int > GProblemModel.MAXIMAL_CALCULATION_LENGTH)
			{
				calculationLength_int = 0;
				states_gpsmp.drop();
			}
			//...EXITING POSSIBLE ENDLESS LOOP
		}
		
		this.initialIdsMap_int_arr_arr = states_gpsmp.getLastState().getIdsMap();
	}


	private int[] generateRandomAction()
	{
		int[] previousAction_int_arr = null;

		if(this.states_gpsmp.length() > 0)
		{
			previousAction_int_arr = this.states_gpsmp.getState(this.states_gpsmp.length() - 1).getAction();
		}

		//ACTION TYPE ID...
		int actionTypeId_int = Random.Range(0, GGridModel.ACTION_IDS.Length);
		//...ACTION TYPE ID

		//ENTRY POINT INDEX...
		int entryPointIndex_int = Random.Range(0, 3);
		if(
			previousAction_int_arr != null &&
			previousAction_int_arr[1] == entryPointIndex_int
			)
		{
			entryPointIndex_int++;

			if(entryPointIndex_int > 2)
			{
				entryPointIndex_int = 0;
			}
		}
		//...ENTRY POINT INDEX

		//DELTA...
		int delta_int = Random.Range(1, 3);
		//...DELTA

		return new int[]{actionTypeId_int, entryPointIndex_int, delta_int};
	}

	public int[][] getInitialIdsMap()
	{
		return this.initialIdsMap_int_arr_arr;
	}

	public int[][] getFinalIdsMap()
	{
		return this.finalIdsMap_int_arr_arr;
	}

	public GProblemStateModelPool getStates()
	{
		return this.states_gpsmp;
	}


/*
	public void print()
	{
		//Debug.Log("ACTIONS:");
		List<int[]> actions_int_arr_arr = this.actions_int_arr_arr;

		for( int i = actions_int_arr_arr.Count-1; i >= 0; i-- )
		{
			int[] action_int_arr = actions_int_arr_arr[i];

			//Debug.Log("#"+(i-actions_int_arr_arr.Count)+" { act: " + GGridModel.ACTION_NAMES[action_int_arr[0]] + " ent: "+ action_int_arr[1] + " del: "+(action_int_arr[2]) + " }" );
		}
	}*/
}