using UnityEngine;

public class GProblemStateModel
{
	private int[][] idsMap_int_arr_arr;
	private int[] action_int_arr;

	public GProblemStateModel()
	{
		//IDS MAP...
		this.idsMap_int_arr_arr = new int[3][];

		for( int i = 0; i < 3; i++ )
		{
			this.idsMap_int_arr_arr[i] = new int[3];
		}
		//...IDS MAP

		//ACTION...
		this.action_int_arr = new int[3];
		//...ACTION
	}

	public void setAction(int aActionTypeId_int, int aEntryPointIndex, int aDelta_int)
	{
		int[] action_int_arr = this.action_int_arr;

		action_int_arr[0] = aActionTypeId_int;
		action_int_arr[1] = aEntryPointIndex;
		action_int_arr[2] = aDelta_int;
	}

	public int[] getAction()
	{
		return this.action_int_arr;
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

	public int[][] getIdsMap()
	{
		return this.idsMap_int_arr_arr;
	}

	public void copy(GProblemStateModel aProblemStateModel_gpsm)
	{
		int[] action_int_arr = aProblemStateModel_gpsm.getAction();

		this.copyIdsMap(aProblemStateModel_gpsm.getIdsMap());
		this.setAction(
			action_int_arr[0],
			action_int_arr[1],
			action_int_arr[2]);
	}
}