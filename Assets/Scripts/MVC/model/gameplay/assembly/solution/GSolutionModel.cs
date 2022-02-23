using UnityEngine;

public class GSolutionModel
{	
	public const int DEFAULT_MAXIMAL_SOLUTION_LENGTH = 20;

	private int[][] idsMap_int_arr_arr;
	private int[][] actions_int_arr_arr;
	private int length_int;

	public GSolutionModel(int aMaximalSolutionLength_int)
	{
		//IDS MAP...
		this.idsMap_int_arr_arr = new int[3][];

		for( int i = 0; i < 3; i++ )
		{
			this.idsMap_int_arr_arr[i] = new int[3];
		}
		//...IDS MAP

		//ACTIONS...
		this.actions_int_arr_arr = new int[aMaximalSolutionLength_int][];

		for( int i = 0; i < aMaximalSolutionLength_int; i++ )
		{
			this.actions_int_arr_arr[i] = new int[3];
			this.actions_int_arr_arr[i][0] = 0;
			this.actions_int_arr_arr[i][1] = 0;
			this.actions_int_arr_arr[i][2] = 0;
		}
		//...ACTIONS

		this.drop();
	}

	public void addAction(int aActionTypeId_int, int aEntryPointIndex, int aDelta_int)
	{
		if(this.length_int == this.actions_int_arr_arr.Length)
		{
			return;
		}

		int[] action_int_arr = this.actions_int_arr_arr[this.length_int];

		action_int_arr[0] = aActionTypeId_int;
		action_int_arr[1] = aEntryPointIndex;
		action_int_arr[2] = aDelta_int;

		this.length_int++;
	}


	public void addReversiveActionsRange(GProblemStateModelPool aProblemStateModelPool_gpsmp, int aFirstStateIndex_int, int aLastStateIndex_int)
	{
		for( int i = aLastStateIndex_int; i >= aFirstStateIndex_int; i-- )
		{
			GProblemStateModel problemStateModel_gpsm = aProblemStateModelPool_gpsmp.getState(i);
			int[] action_int_arr = problemStateModel_gpsm.getAction();

			this.addAction(
				action_int_arr[0],
				action_int_arr[1],
				-action_int_arr[2]);
		}
	}

	public int[] getAction(int aActionIndex_int)
	{
		return this.actions_int_arr_arr[aActionIndex_int];
	}

	public int[][] getActions()
	{
		return this.actions_int_arr_arr;
	}

	public void drop()
	{
		this.length_int = 0;
	}

	public int length()
	{
		return this.length_int;
	}

	public void copyIdsMap(int[][] aIdsMap_int_arr_arr)
	{
		for(int y = 0; y < 3; y++)
		{
			for(int x = 0; x < 3; x++)
			{
				this.idsMap_int_arr_arr[y][x] = aIdsMap_int_arr_arr[y][x];
			}
		}
	}

	public void copyIdsMap(GGridModel aGridModel_ggm)
	{
		for(int y = 0; y < 3; y++)
		{
			for(int x = 0; x < 3; x++)
			{
				this.idsMap_int_arr_arr[y][x] = aGridModel_ggm.getCellId(x, y);
			}
		}
	}

	public int[][] getIdsMap()
	{
		return this.idsMap_int_arr_arr;
	}


	public void copy(GSolutionModel aSolution_gsm)
	{
		this.length_int = aSolution_gsm.length();

		//COPYING IDS MAP...
		int[][] idsMap_int_arr_arr = aSolution_gsm.getIdsMap();

		for(int y = 0; y < 3; y++)
		{
			for(int x = 0; x < 3; x++)
			{
				this.idsMap_int_arr_arr[y][x] = idsMap_int_arr_arr[y][x];
			}
		}
		//...COPYING IDS MAP

		//COPYING ACTIONS...
		int[][] actions_int_arr_arr = aSolution_gsm.getActions();

		for(int i = 0; i < this.length_int; i++)
		{
			for(int j = 0; j < 3; j++)
			{
				this.actions_int_arr_arr[i][j] = actions_int_arr_arr[i][j];
			}
		}
		//...COPYING ACTIONS
	}

	public void deleteFirst()
	{
		for( int i = 0; i < this.length_int - 1; i++ )
		{
			for(int j = 0; j < 3; j++)
			{
				this.actions_int_arr_arr[i][j] = this.actions_int_arr_arr[i + 1][j];
			}
		}

		this.length_int--;
	}

	public bool isEmpty()
	{
		return this.length_int == 0;
	}

	public int getMatchesNumber(int[][] aIdsMap_int_arr_arr)
	{
		int matchesNumber_int = 0;

		for(int y = 0; y < 3; y++)
		{
			for(int x = 0; x < 3; x++)
			{
				if(this.idsMap_int_arr_arr[y][x] == aIdsMap_int_arr_arr[y][x])
				{
					matchesNumber_int++;
				}
			}
		}

		return matchesNumber_int;
	}
}