using UnityEngine;

public class GSolverModel : GModel
{
	public const int DEFAULT_MAXIMAL_DIFICULTY_LEVEL = 2;

	private GSolutionModelPool solutionPool_gsp;
	private int maximalDifficultyLevel_int;

	public GSolverModel(int aMaximalDifficultyLevel_int, int aMaximalSolutionsNumber_int, int aMaximalSolutionlength_int)
		: base()
	{
		this.solutionPool_gsp = new GSolutionModelPool(aMaximalSolutionsNumber_int, aMaximalSolutionlength_int);
		this.maximalDifficultyLevel_int = aMaximalDifficultyLevel_int;
	}

	public int getMaximalDifficultyLevel()
	{
		return this.maximalDifficultyLevel_int;
	}

	public GSolutionModel getSolution(GAssemblyModel aAssemblyModel_gam)
	{
		GGridModel gridModel_ggm = aAssemblyModel_gam.getGridModel();
		GProblemModel problemModel_gpm = aAssemblyModel_gam.getProblemModel();
		GProblemStateModelPool states_gpsmp = problemModel_gpm.getStates();
		GSolutionModel solution_gsm;

		//CHECKING INSTANT SOLUTION...
		solution_gsm = this.getSolution(gridModel_ggm, problemModel_gpm.getFinalIdsMap());

		if(solution_gsm != null)
		{
			return solution_gsm;
		}
		//...CHECKING INSTANT SOLUTION

		//SEARCHING SOLUTION FROM CURRENT STATE TO ANY AVAILABLE PROBLEM STATE...
		for(int i = 0; i < states_gpsmp.length(); i++)
		{
			gridModel_ggm.adjust(gridModel_ggm.getIdsMap());
			solution_gsm = this.getSolution(gridModel_ggm, states_gpsmp.getState(i).getIdsMap());
			if(solution_gsm != null)
			{
				//CONCATING SOLUTION AND REST OF STATES SIQUENCE...
				solution_gsm.addReversiveActionsRange(states_gpsmp, 0, i);
				solution_gsm.copyIdsMap(problemModel_gpm.getFinalIdsMap());
				//...CONCATING SOLUTION AND REST OF STATES SIQUENCE

				return solution_gsm;
			}
		}
		//...SEARCHING SOLUTION FROM CURRENT STATE TO ANY AVAILABLE PROBLEM STATE

		return null;
	}

	public GSolutionModel getSolution(GGridModel aGridModel_ggm, GProblemModel aProblemModel_gpm)
	{
		GGridModel gridModel_ggm = new GGridModel(aGridModel_ggm.getIdsMap());
		GProblemStateModelPool states_gpsmp = aProblemModel_gpm.getStates();
		GSolutionModel solution_gsm;

		for(int i = 0; i < states_gpsmp.length(); i++)
		{
			gridModel_ggm.adjust(aGridModel_ggm.getIdsMap());
			solution_gsm = this.getSolution(gridModel_ggm, states_gpsmp.getState(i).getIdsMap());
			if(solution_gsm != null)
			{
				//Debug.Log("SOULUTION FOUND: " + i);
				return solution_gsm;
			}
		}

		//Debug.Log("SOULUTION NOT FOUND" );
		return null;
	}

	public GSolutionModel getSolution(GGridModel aGridModel_ggm, int[][] aDesiredIdsMap_int_arr_arr)
	{	
		GGridModel testGridModel_ggm = new GGridModel(aGridModel_ggm.getIdsMap());
		GSolutionModelPool solutionPool_gsp = this.solutionPool_gsp;
		bool solutionFound_bl = false;

		//ADDING FIRST EMPTY SOLUTION...
		solutionPool_gsp.drop();
		solutionPool_gsp.add(testGridModel_ggm);
		//...ADDING FIRST EMPTY SOLUTION

		//SEARCHING SOLUTIONS...
		while(
			!solutionFound_bl &&
			!solutionPool_gsp.isFull()
			)
		{
			int length_int = solutionPool_gsp.length();

			for( int solutionIndex_int = 0; solutionIndex_int < length_int; solutionIndex_int++ )
			{
				//GETTING NEW SOLUTIONS...
				for( int actionId_int = 0; actionId_int < GGridModel.ACTION_IDS.Length; actionId_int++ )
				{
					for( int entryPointIndex_int = 0; entryPointIndex_int < testGridModel_ggm.getWidth(); entryPointIndex_int++ )
					{
						for( int delta_int = 1; delta_int < testGridModel_ggm.getWidth(); delta_int++ )
						{
							GSolutionModel solution_gsm = solutionPool_gsp.getSolution(solutionIndex_int);

							testGridModel_ggm.adjust(solution_gsm.getIdsMap());

							//ADDING NEW STEP...
							testGridModel_ggm.applyAction(
								GGridModel.ACTION_IDS[actionId_int],
								entryPointIndex_int,
								delta_int);
							//...ADDING NEW STEP


							//ADDING NEW SOLUTION TO SOLUTIONS POOL...
							solutionPool_gsp.add(
									testGridModel_ggm, 
									solution_gsm,
									actionId_int,
									entryPointIndex_int,
									delta_int);
							//...ADDING NEW SOLUTION TO SOLUTIONS POOL

							//CEHCKING SOLUTION...
							if(testGridModel_ggm.isEqualTo(aDesiredIdsMap_int_arr_arr))
							{
								solutionFound_bl = true;
								//Debug.Log("SOLUTIONS: "+solutionPool_gsp.length());
								return solutionPool_gsp.getLastSolution();
							}
							//...CHECKING SOLUTION

							if(solutionPool_gsp.isFull())
							{
								return null;
							}
						}
					}
				}
				//...GETTING NEW SOLUTIONS

				//DELETING EXCESSIVE SOLUTION...
				solutionPool_gsp.delete(solutionIndex_int);
				//...DELETING EXCESSIVE SOLUTION
			}
		}
		//...SEARCHING SOLUTIONS

		return null;
	}
}