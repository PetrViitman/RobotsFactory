public class GSolutionModelPool
{	
	public const int DEFAULT_MAXIMAL_SOLUTIONS_NUMBER = 3000;

	private GSolutionModel[] solutions_gsm_arr;
	private int solutionsNumber_int;

	public GSolutionModelPool(int aMaximalSolutionsNumber_int, int aMaximalSolutionlength_int)
	{
		this.solutions_gsm_arr = new GSolutionModel[aMaximalSolutionsNumber_int];
		
		for(int i = 0; i < aMaximalSolutionsNumber_int; i++)
		{
			this.solutions_gsm_arr[i] = new GSolutionModel(aMaximalSolutionlength_int);
		}

		this.drop();
	}

	public void drop()
	{
		this.solutionsNumber_int = 0;
	}

	public void add()
	{
		if(this.isFull())
		{
			return;
		}

		this.solutions_gsm_arr[this.solutionsNumber_int].drop();

		this.solutionsNumber_int++;
	}

	public void add(GSolutionModel aPath_gp)
	{
		if(this.isFull())
		{
			return;
		}

		this.solutions_gsm_arr[this.solutionsNumber_int].copy(aPath_gp);
		this.solutionsNumber_int++;
	}

	public void add(GGridModel gridMod_ggm)
	{
		if(this.isFull())
		{
			return;
		}

		int pathIndex_int = this.solutionsNumber_int;

		this.solutions_gsm_arr[pathIndex_int].drop();
		this.solutions_gsm_arr[pathIndex_int].copyIdsMap(gridMod_ggm);
		this.solutionsNumber_int++;
	}

	public void add(GGridModel gridMod_ggm, GSolutionModel aSolution_gsm , int aActionTypeId_int, int aEntrypoint_int, int aDelta_int)
	{
		if(this.isFull())
		{
			return;
		}

		int pathIndex_int = this.solutionsNumber_int;

		this.solutions_gsm_arr[pathIndex_int].copy(aSolution_gsm);
		this.solutions_gsm_arr[pathIndex_int].copyIdsMap(gridMod_ggm);
		this.solutions_gsm_arr[pathIndex_int].addAction(aActionTypeId_int, aEntrypoint_int, aDelta_int);

		this.solutionsNumber_int++;
	}

	public void delete(int aSolutionIndex_int)
	{
		this.solutions_gsm_arr[aSolutionIndex_int].copy(this.solutions_gsm_arr[this.solutionsNumber_int-1]);

		this.solutionsNumber_int--;
		if(this.solutionsNumber_int < 0)
		{
			this.solutionsNumber_int = 0;
		}
	}

	public int length()
	{
		return this.solutionsNumber_int;
	}

	public GSolutionModel getSolution(int aSolutionIndex_int)
	{
		return this.solutions_gsm_arr[aSolutionIndex_int];
	}

	public GSolutionModel getLastSolution()
	{
		return this.getSolution(this.solutionsNumber_int - 1);
	}

	public bool isFull()
	{
		return this.solutionsNumber_int == this.solutions_gsm_arr.Length - 1;
	}

	public GSolutionModel getColsestSolution(int[][] aIdsMap_int_arr_arr)
	{
		int colsestPathIndex_int = 0;
		int highestMatchesNumber_int = 0;

		for(int i = 0; i < this.solutionsNumber_int; i++)
		{
			int matchesNumber_int = this.solutions_gsm_arr[i].getMatchesNumber(aIdsMap_int_arr_arr);
			if(matchesNumber_int > highestMatchesNumber_int)
			{
				highestMatchesNumber_int = matchesNumber_int;
				colsestPathIndex_int = i;
			}
		}

		return this.solutions_gsm_arr[colsestPathIndex_int];
	}
}