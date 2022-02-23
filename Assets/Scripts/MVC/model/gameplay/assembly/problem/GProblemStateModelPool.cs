using UnityEngine;

public class GProblemStateModelPool
{
	private GProblemStateModel[] states_gpsm_arr;
	private int statesNumber_int;

	public GProblemStateModelPool(int aMaximalStatesNumber_int)
	{
		this.states_gpsm_arr = new GProblemStateModel[aMaximalStatesNumber_int];
		
		for(int i = 0; i < aMaximalStatesNumber_int; i++)
		{
			this.states_gpsm_arr[i] = new GProblemStateModel();
		}

		this.drop();
	}

	public void drop()
	{
		this.statesNumber_int = 0;
	}

	public void add(int[][] aIdsMap_int_arr_arr, int[] aAction_int_arr)
	{
		if(this.isFull())
		{
			return;
		}

		GProblemStateModel state_gpsm = this.states_gpsm_arr[this.statesNumber_int];
		
		state_gpsm.setAction(
			aAction_int_arr[0],
			aAction_int_arr[1],
			aAction_int_arr[2]);

		state_gpsm.copyIdsMap(aIdsMap_int_arr_arr);

		this.statesNumber_int++;
	}

	public int length()
	{
		return this.statesNumber_int;
	}

	public GProblemStateModel getState(int aStateIndex_int)
	{
		return this.states_gpsm_arr[aStateIndex_int];
	}

	public GProblemStateModel getLastState()
	{
		return this.states_gpsm_arr[this.statesNumber_int - 1];
	}

	public bool isEmpty()
	{
		return this.statesNumber_int == 0;
	}

	public bool isFull()
	{
		return this.statesNumber_int == this.states_gpsm_arr.Length - 1;
	}

	public void deleteFirst()
	{
		GProblemStateModel[] states_gpsm_arr = this.states_gpsm_arr;

		for( int i = 0; i < this.statesNumber_int - 1; i++ )
		{
			states_gpsm_arr[i].copy(states_gpsm_arr[i + 1]);
		}

		this.statesNumber_int--;
	}

	private void deleteRange(int aFirstStateIndex_int, int aLastStateIndex_int)
	{
		//Debug.Log("["+aFirstStateIndex_int+ " ... "+aLastStateIndex_int+ "]");

		GProblemStateModel[] states_gpsm_arr = this.states_gpsm_arr;

		for( int i = aFirstStateIndex_int; i <= aLastStateIndex_int; i++ )
		{
			for( int j = aFirstStateIndex_int + 1; j < this.statesNumber_int; j++ )
			{
				states_gpsm_arr[j].copy(states_gpsm_arr[j + 1]);
				this.statesNumber_int--;
			}
		}
	}

	public void deleteExcessives()
	{
		for( int i = 0; i < this.statesNumber_int - 1; i++ )
		{
			for( int j = i + 1; j < this.statesNumber_int; j++ )
			{
				int[][] originalIdsMap_int_arr_arr = this.states_gpsm_arr[i].getIdsMap();
				int[][] otherIdsMap_int_arr_arr = this.states_gpsm_arr[j].getIdsMap();
				bool isEqual_bl = true;

				for( int y = 0; y < 3; y++ )
				{
					for( int x = 0; x < 3; x++ )
					{
						if(originalIdsMap_int_arr_arr[y][x] != otherIdsMap_int_arr_arr[y][x])
						{
							isEqual_bl = false;
							break;
						}
					}

					if(!isEqual_bl)
					{
						break;
					}
				}

				if(isEqual_bl)
				{
					this.deleteRange(i, j);
					break;
				}
			}
		}
	}

	public void print()
	{
		if(this.statesNumber_int == 0)
		{
			Debug.Log("NO ACTIONS...");
			return;
		}

		Debug.Log("---");
		for( int i = 0; i < this.statesNumber_int; i++ )
		{
			int[] action_int_arr = this.states_gpsm_arr[i].getAction();
			Debug.Log("{ " + action_int_arr[0] + " " + action_int_arr[1] + " " + action_int_arr[2] + " }");
		}
	}
}