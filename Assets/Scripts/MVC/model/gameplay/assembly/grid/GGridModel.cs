using UnityEngine;
using System.Collections.Generic;

public class GGridModel : GModel
{
	public const int ACTION_TYPE_ID_UP = 0;
	public const int ACTION_TYPE_ID_DOWN = 1;
	public const int ACTION_TYPE_ID_LEFT = 2;
	public const int ACTION_TYPE_ID_RIGHT = 3;

	public static readonly int[] ACTION_IDS = 
	{
		GGridModel.ACTION_TYPE_ID_UP,
		GGridModel.ACTION_TYPE_ID_DOWN,
		GGridModel.ACTION_TYPE_ID_LEFT,
		GGridModel.ACTION_TYPE_ID_RIGHT,
	};

	public static readonly string[] ACTION_NAMES = 
	{
		"DOWN",
		"UP",
		"RIGHT",
		"LEFT",
	};

	private GCellModel[][] cells_gcm_arr_arr;
	private GCellModel[] cellsReferences_gcm_arr;
	private int width_int;
	private int height_int;

	public GGridModel(int[][] aIds_int_arr_arr)
		: base()
	{
		this.width_int = aIds_int_arr_arr.Length;
		this.height_int = aIds_int_arr_arr[0].Length;
		
		GCellModel[][] cells_gcm_arr_arr = new GCellModel[this.height_int][];
		this.cellsReferences_gcm_arr = new GCellModel[this.width_int * this.height_int];

		for( int y = 0; y < this.height_int; y++)
		{
			cells_gcm_arr_arr[y] = new GCellModel[this.width_int];

			for( int x = 0; x < this.width_int; x++)
			{
				int cellTypeId_int = aIds_int_arr_arr[y][x];
				GCellModel cellModel_gcm = new GCellModel(cellTypeId_int);
				
				cells_gcm_arr_arr[y][x] = cellModel_gcm;
				this.cellsReferences_gcm_arr[y*this.height_int + x] = cellModel_gcm;
			}
		}

		this.cells_gcm_arr_arr = cells_gcm_arr_arr;
	}

	protected override void onViewSet(GView aView_gv)
	{
		GGridView gridView_ggv = (GGridView) aView_gv;

		for(int y = 0; y < this.height_int; y++)
		{
			for(int x = 0; x < this.width_int; x++)
			{
				this.cells_gcm_arr_arr[y][x].setView(gridView_ggv.getCellViews()[y][x]);
			}
		}
	}

	public int getWidth()
	{
		return this.width_int;
	}

	public int getHeight()
	{
		return this.height_int;
	}

	public void pushRowRight(int aRowIndex_int)
	{
		int width_int = this.width_int;
		GCellModel lastCell_gcm = this.cells_gcm_arr_arr[aRowIndex_int][width_int-1];

		for(int x = width_int-1; x > 0; x--)
		{
			this.cells_gcm_arr_arr[aRowIndex_int][x] = 
			this.cells_gcm_arr_arr[aRowIndex_int][x-1];
		}

		this.cells_gcm_arr_arr[aRowIndex_int][0] = lastCell_gcm;
	}

	public void pushRowLeft(int aRowIndex_int)
	{
		int width_int = this.width_int;
		GCellModel firstCell_gcm = this.cells_gcm_arr_arr[aRowIndex_int][0];

		for(int x = 0; x < width_int-1; x++)
		{
			this.cells_gcm_arr_arr[aRowIndex_int][x] = 
			this.cells_gcm_arr_arr[aRowIndex_int][x + 1];
		}

		this.cells_gcm_arr_arr[aRowIndex_int][width_int-1] = firstCell_gcm;
	}

	public void pushRow(int aRowIndex_int, int aLength_int)
	{
		if(aLength_int > 0)
		{
			for(int i = 0; i < aLength_int; i++)
			{
				this.pushRowRight(aRowIndex_int);
			}
		}
		else
		{
			for(int i = 0; i > aLength_int; i--)
			{
				this.pushRowLeft(aRowIndex_int);
			}
		}

		this.validateView();
	}

	public void pushColumnUp(int aColumnIndex_int)
	{
		int height_int = this.height_int;
		GCellModel topCell_gcm = this.cells_gcm_arr_arr[0][aColumnIndex_int];

		for(int y = 0; y < height_int - 1; y++)
		{
			this.cells_gcm_arr_arr[y][aColumnIndex_int] = 
			this.cells_gcm_arr_arr[y+1][aColumnIndex_int];
		}

		this.cells_gcm_arr_arr[height_int-1][aColumnIndex_int] = topCell_gcm;
	}

	public void pushColumnDown(int aColumnIndex_int)
	{
		int height_int = this.height_int;
		GCellModel bottomCell_gcm = this.cells_gcm_arr_arr[height_int-1][aColumnIndex_int];

		for(int y = height_int-1; y > 0; y--)
		{
			this.cells_gcm_arr_arr[y][aColumnIndex_int] = 
			this.cells_gcm_arr_arr[y-1][aColumnIndex_int];
		}

		this.cells_gcm_arr_arr[0][aColumnIndex_int] = bottomCell_gcm;
	}

	public void pushColumn(int aColumnIndex_int, int aLength_int)
	{
		if(aLength_int > 0)
		{
			for(int i = 0; i < aLength_int; i++)
			{
				this.pushColumnDown(aColumnIndex_int);
			}
		}
		else
		{
			for(int i = 0; i > aLength_int; i--)
			{
				this.pushColumnUp(aColumnIndex_int);
			}
		}

		this.validateView();
	}

	public GCellModel getCell(int aColumnIndex_int, int aRowIndex_int)
	{
		if(
			aRowIndex_int < 0 ||
			aRowIndex_int >= this.height_int ||
			aColumnIndex_int < 0 ||
			aColumnIndex_int >= this.width_int
		)
		{
			return null;
		}

		return this.cells_gcm_arr_arr[aRowIndex_int][aColumnIndex_int];
	}

	public int getCellId(int aColumnIndex_int, int aRowIndex_int)
	{
		return this.cells_gcm_arr_arr[aRowIndex_int][aColumnIndex_int].getTypeId();
	}

	public void applyAction(int aActionId_int, int aEntryIndex_int, int aDelta_int)
	{
		switch(aActionId_int)
		{
			case GGridModel.ACTION_TYPE_ID_UP:
			{
				this.pushColumn(aEntryIndex_int, -aDelta_int);
			}
			break;
			case GGridModel.ACTION_TYPE_ID_DOWN:
			{
				this.pushColumn(aEntryIndex_int, aDelta_int);
			}
			break;
			case GGridModel.ACTION_TYPE_ID_LEFT:
			{
				this.pushRow(aEntryIndex_int, -aDelta_int);
			}
			break;
			case GGridModel.ACTION_TYPE_ID_RIGHT:
			{
				this.pushRow(aEntryIndex_int, aDelta_int);
			}
			break;
		}
	}
	
	public void applyActions(List<int[]> aActions_int_arr_arr)
	{
		for(int i = 0; i < aActions_int_arr_arr.Count; i++)
		{
			int[] action_int_arr = aActions_int_arr_arr[i];

			this.applyAction(
				action_int_arr[0],
				action_int_arr[1],
				action_int_arr[2]);
		}
	}

	public void undoActions(List<int[]> aActions_int_arr_arr)
	{
		for(int i = aActions_int_arr_arr.Count-1; i >= 0; i--)
		{
			int[] action_int_arr = aActions_int_arr_arr[i];

			this.applyAction(
				action_int_arr[0],
				action_int_arr[1],
				-action_int_arr[2]);
		}
	}
	

	public void applyRandomActions(int aActionsNumber_int)
	{
		for(int i = 0; i < aActionsNumber_int; i++)
		{
			int actionId_int = GGridModel.ACTION_IDS[Random.Range(0, GGridModel.ACTION_IDS.Length)];
			int entryIndex_int = Random.Range(1, this.width_int-1);
			int delta_int = Random.Range(1, this.width_int);
			if(delta_int == 0)
			{
				delta_int = 1;
			}
			this.applyAction(actionId_int, entryIndex_int, delta_int);
		}
	}


	public int[][] getIdsMap()
	{
		int[][] cellsIds_int_arr_arr = new int[this.height_int][];
		
		for(int y = 0; y < this.height_int; y++)
		{
			cellsIds_int_arr_arr[y] = new int[this.width_int];
			for(int x = 0; x < this.width_int; x++)
			{
				cellsIds_int_arr_arr[y][x] = this.cells_gcm_arr_arr[y][x].getTypeId();
			}
		}

		return cellsIds_int_arr_arr;
	}

	public void adjust(int[][] aIdsMap_int_arr_arr)
	{
		GCellModel[] cellsReferences_gcm_arr = new GCellModel[this.cellsReferences_gcm_arr.Length];

		for(int i = 0; i <  cellsReferences_gcm_arr.Length; i++)
		{
			cellsReferences_gcm_arr[i] = this.cellsReferences_gcm_arr[i];
		}

		for(int y = 0; y <  this.height_int; y++)
		{
			for(int x = 0; x < this.width_int; x++)
			{
				for(int i = 0; i < cellsReferences_gcm_arr.Length; i++)
				{
					GCellModel cellModel_gcm = cellsReferences_gcm_arr[i];

					if(cellModel_gcm == null)
					{
						continue;
					}

					if(cellModel_gcm.getTypeId() == aIdsMap_int_arr_arr[y][x])
					{
						this.cells_gcm_arr_arr[y][x] = cellModel_gcm;
						cellsReferences_gcm_arr[i] = null;
						break;
					}
				}
			}
		}

		this.validateView();
	}

	public string getHashSumm()
	{
		string hashSumm_str = "";

		for(int y = 0; y < this.height_int; y++)
		{
			for(int x = 0; x < this.width_int; x++)
			{
				hashSumm_str += this.cells_gcm_arr_arr[y][x].getTypeId();
			}
		}

		return hashSumm_str;
	}

	public bool isEqualTo(int[][] aIdsMap_int_arr_arr)
	{
		for(int y = 0; y < this.height_int; y++)
		{
			for(int x = 0; x < this.width_int; x++)
			{
				if(this.cells_gcm_arr_arr[y][x].getTypeId() != aIdsMap_int_arr_arr[y][x])
				{
					return false;
				}
			}
		}

		return true;
	}

	public void print()
	{
		Debug.Log("---");
		for(int y = 0; y < this.height_int; y++)
		{
			string str_str = "";

			for(int x = 0; x < this.width_int; x++)
			{
				str_str += this.cells_gcm_arr_arr[y][x].getTypeId()+" ";
			}

			Debug.Log(str_str);
		}
	}
}