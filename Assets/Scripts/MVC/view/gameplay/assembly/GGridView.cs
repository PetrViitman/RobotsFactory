using UnityEngine;

public class GGridView : GInteractiveRectangleView
{
	public const int STATE_ID_LISTENING = 0;
	public const int STATE_ID_ARTIFICIAL_INTERACTION = 1;
	public const int STATE_ID_INTERACTION = 2;
	public const int STATE_ID_ADJASTMENT = 3;

	public const int ADJUSTMENT_STEPS_NUMBER = 30;

	private int stateId_int;
	private int columnsNumber_int;
	private int rowsNumber_int;
	private GCellView[][] cellViews_gcv_arr_arr;
	private GCellView[][] cellViewsInversion_gcv_arr_arr;
	private int activeCellColumnIndex_int;
	private int activeCellRowIndex_int;
	private GAdjustableValue adjustableValue_gav;
	private float adjustmentDelta_num;
	private float adjustmentOffset_num;

	public GGridView(float aX_num, float aY_num)
		: base(aX_num, aY_num, 0, 0)
	{
		this.stateId_int = GGridView.STATE_ID_LISTENING;
		this.adjustableValue_gav = new GAdjustableValue(GGridView.ADJUSTMENT_STEPS_NUMBER);

		this.activeCellColumnIndex_int = -1;
		this.activeCellRowIndex_int = -1;
	}

	protected override void onModelSet(GModel aModel_gm)
	{
		GGridModel gridModel_ggm = (GGridModel) aModel_gm;

		this.columnsNumber_int = gridModel_ggm.getWidth();
		this.rowsNumber_int = gridModel_ggm.getHeight();

		float cellWidth_num = 100f / this.columnsNumber_int;
		float cellHeight_num = 100f / this.rowsNumber_int;

		this.cellViews_gcv_arr_arr = new GCellView[this.rowsNumber_int][];

		for( int y = 0; y < this.rowsNumber_int; y++ )
		{
			this.cellViews_gcv_arr_arr[y] = new GCellView[this.columnsNumber_int];

			for( int x = 0; x < this.columnsNumber_int; x++ )
			{
				this.cellViews_gcv_arr_arr[y][x] = new GCellView(
					this,
					0,
					0,
					cellWidth_num,
					cellHeight_num);
			}
		}

		this.cellViewsInversion_gcv_arr_arr = this.getInversion();

		this.centralize();
	}

	public override void validate()
	{
		GCellView[][] cellViews_gcv_arr_arr = new GCellView[this.rowsNumber_int][];
		GGridModel gridModel_ggm = (GGridModel) this.getModel();

		for( int y = 0; y < this.rowsNumber_int; y++ )
		{
			cellViews_gcv_arr_arr[y] = new GCellView[this.columnsNumber_int];

			for( int x = 0; x < this.columnsNumber_int; x++ )
			{	
				GCellView targetCellView_gcv = (GCellView) gridModel_ggm.getCell(x, y).getView();
				cellViews_gcv_arr_arr[y][x] = targetCellView_gcv;
			}
		}

		for( int y = 0; y < this.rowsNumber_int; y++ )
		{
			for( int x = 0; x < this.columnsNumber_int; x++ )
			{			
				this.cellViews_gcv_arr_arr[y][x] = cellViews_gcv_arr_arr[y][x];
			}
		}

		this.cellViewsInversion_gcv_arr_arr = this.getInversion();
		this.centralize();
	}

	public void centralize()
	{
		this.setWidth(this.columnsNumber_int * this.cellViews_gcv_arr_arr[0][0].getWidth() * GScreen.getSidesRatio());
		this.setHeight(this.rowsNumber_int * this.cellViews_gcv_arr_arr[0][0].getHeight());

		this.setXY(50f - this.getWidth() / 2f, 0);
	}

	private GCellView[][] getInversion()
	{
		GCellView[][] cellViews_gcv_arr_arr = new GCellView[this.rowsNumber_int][];

		for(int y = 0; y < this.rowsNumber_int; y++)
		{
			cellViews_gcv_arr_arr[y] = new GCellView[this.columnsNumber_int];

			for(int x = 0; x < this.columnsNumber_int; x++)
			{
				cellViews_gcv_arr_arr[y][x] = this.cellViews_gcv_arr_arr[x][y];
			}
		}

		return cellViews_gcv_arr_arr;
	}

	public override void setXY(float aX_num, float aY_num)
	{
		base.setXY(aX_num, aY_num);

		GCellView cellView_gcv = this.cellViews_gcv_arr_arr[0][0];
		float cellWidth_num = cellView_gcv.getWidth() * GScreen.getSidesRatio();
		float cellHeight_num = cellView_gcv.getHeight();

		for(int y = 0; y < this.rowsNumber_int; y++)
		{
			for(int x = 0; x < this.columnsNumber_int; x++)
			{
				this.cellViews_gcv_arr_arr[y][x].setXY(
					aX_num + x * cellWidth_num,
					aY_num + y *cellHeight_num);
			}
		}
	}

	public override void setX(float aX_num)
	{
		this.setXY(aX_num, this.getY());
	}

	public override void setY(float aY_num)
	{
		this.setXY(this.getX(), aY_num);
	}

	public int getColumnsNumber()
	{
		return this.columnsNumber_int;
	}

	public int getRowsNumber()
	{
		return this.rowsNumber_int;
	}

	public GCellView[][] getCellViews()
	{
		return this.cellViews_gcv_arr_arr;
	}

	public void setStateId(int aStateId_int)
	{
		this.stateId_int = aStateId_int;
	}

	public int getStateId()
	{
		return this.stateId_int;
	}

	public GCellView getActiveCell()
	{
		if(this.activeCellRowIndex_int == -1)
		{
			return null;
		}

		if(this.activeCellColumnIndex_int == -1)
		{
			return null;
		}

		return this.cellViews_gcv_arr_arr[this.activeCellRowIndex_int][this.activeCellColumnIndex_int];
	}

	public void update()
	{
		switch(this.stateId_int)
		{
			case GGridView.STATE_ID_LISTENING:
			{
				for(int y = 0; y < this.rowsNumber_int; y++)
				{
					for(int x = 0; x < this.columnsNumber_int; x++)
					{
						GCellView cellView_gcv = this.cellViews_gcv_arr_arr[y][x];
						cellView_gcv.update();

						if(cellView_gcv.isActive())
						{
							this.activeCellColumnIndex_int = x;
							this.activeCellRowIndex_int = y;
							this.setStateId(GGridView.STATE_ID_INTERACTION);
							return;
						}
					}
				}
			}
			break;
			case GGridView.STATE_ID_ARTIFICIAL_INTERACTION:
			{
				GCellView cellView_gcv = this.getActiveCell();
				this.adjustableValue_gav.update();

				switch(cellView_gcv.getDirectionId())
				{
					case GCellView.DIRECTION_ID_HORIZONTAL:
					{
						float cellWidth_num = cellView_gcv.getWidth() * GScreen.getSidesRatio();
						float delta_num = this.adjustableValue_gav.getValue() * this.adjustmentDelta_num * cellWidth_num;

						this.setRowXByActiveCell(this.adjustmentOffset_num + delta_num);
					}
					break;
					case GCellView.DIRECTION_ID_VERTICAL:
					{
						float cellHeight_num = cellView_gcv.getHeight();
						float delta_num = this.adjustableValue_gav.getValue() * this.adjustmentDelta_num * cellHeight_num;

						this.setColumnYByActiveCell(this.adjustmentOffset_num + delta_num);
					}
					break;
				}

				if(this.adjustableValue_gav.isFull())
				{
					this.setStateId(GGridView.STATE_ID_ADJASTMENT);
				}
			}
			break;
			case GGridView.STATE_ID_INTERACTION:
			{
				GCellView cellView_gcv = this.getActiveCell();
				cellView_gcv.update();
				if(!cellView_gcv.isActive())
				{
					this.setStateId(GGridView.STATE_ID_ADJASTMENT);
					this.adjustableValue_gav.resetValue();

					switch(cellView_gcv.getDirectionId())
					{
						case GCellView.DIRECTION_ID_HORIZONTAL:
						{
							float cellWidth_num = cellView_gcv.getWidth() * GScreen.getSidesRatio();
							float deltaX_num = cellView_gcv.getX() - this.getX();
							float offsetX_num = -(deltaX_num/cellWidth_num) % 1f;

							if(offsetX_num > 0.5f)
							{
								offsetX_num = offsetX_num - 1f;
							}
							else if (offsetX_num < -0.5f)
							{
								offsetX_num = offsetX_num + 1f;
							}
						
							this.adjustmentDelta_num = deltaX_num/cellWidth_num;
							this.adjustmentOffset_num = offsetX_num;
						}
						break;
						case GCellView.DIRECTION_ID_VERTICAL:
						{
							float cellHeight_num = cellView_gcv.getHeight();
							float deltaY_num = cellView_gcv.getY() - this.getY();
							float offsetY_num = -(deltaY_num/cellHeight_num) % 1f;
							if(offsetY_num > 0.5f)
							{
								offsetY_num = offsetY_num - 1f;
							}
							else if (offsetY_num < -0.5f)
							{
								offsetY_num = offsetY_num + 1f;
							}

							this.adjustmentDelta_num = deltaY_num/cellHeight_num;
							this.adjustmentOffset_num = offsetY_num;
						}
						break;
					}
				}
			}
			break;
			case GGridView.STATE_ID_ADJASTMENT:
			{
				this.adjustableValue_gav.update();
				float adjustableValueProgress_num = this.adjustableValue_gav.getValue();
				GCellView cellView_gcv = this.getActiveCell();

				switch(cellView_gcv.getDirectionId())
				{
					case GCellView.DIRECTION_ID_HORIZONTAL:
					{
						float cellWidth_num = cellView_gcv.getWidth() * GScreen.getSidesRatio();
						float initialX_num = this.getX() + this.adjustmentDelta_num * cellWidth_num;
						float offsetLength_num = this.adjustmentOffset_num * cellWidth_num;
						
						this.setRowXByActiveCell(initialX_num + offsetLength_num * adjustableValueProgress_num);
					}
					break;
					case GCellView.DIRECTION_ID_VERTICAL:
					{
						float cellHeight_num = cellView_gcv.getHeight();
						float initialY_num = this.getY() + this.adjustmentDelta_num * cellHeight_num;
						float offsetLength_num = this.adjustmentOffset_num * cellHeight_num;
						
						this.setColumnYByActiveCell(initialY_num + offsetLength_num * adjustableValueProgress_num);
					}
					break;
				}

				if(this.adjustableValue_gav.isFull())
				{
					GGridController gridController_gc = (GGridController) this.getController();

					switch(cellView_gcv.getDirectionId())
					{
						case GCellView.DIRECTION_ID_HORIZONTAL:
						{
							int gridModelPushValue_int = (int)Mathf.Round(this.adjustmentDelta_num) - this.activeCellColumnIndex_int;
							
							if(gridModelPushValue_int != 0)
							{
								gridController_gc.onGridViewRowChanged(this.activeCellRowIndex_int, gridModelPushValue_int);
							}
						}
						break;
						case GCellView.DIRECTION_ID_VERTICAL:
						{
							int gridModelPushValue_int = (int)Mathf.Round(this.adjustmentDelta_num) - this.activeCellRowIndex_int;
							
							if(gridModelPushValue_int != 0)
							{
								gridController_gc.onGridViewColumnChanged(this.activeCellColumnIndex_int, gridModelPushValue_int);
							}
						}
						break;
					}

					this.centralize();
					this.activeCellColumnIndex_int = -1;
					this.activeCellRowIndex_int = -1;
					this.setStateId(GGridView.STATE_ID_LISTENING);
				}
			}
			break;
		}

		//FLOATING...
		for(int y = 0; y < this.rowsNumber_int; y++)
		{
			for(int x = 0; x < this.columnsNumber_int; x++)
			{
				this.cellViews_gcv_arr_arr[y][x].updateFloating();
			}
		}
		//...FLOATING
	}

	public void setRowXByActiveCell(float aX_num)
	{
		GCellView[] row_cv_arr = this.cellViews_gcv_arr_arr[this.activeCellRowIndex_int];
		float activeCellPositionX_num = this.getActiveCell().getX();

		for(int i = 0; i < row_cv_arr.Length; i++)
		{
			GCellView cellView_gcv = row_cv_arr[i];
			float offset_num = cellView_gcv.getX() - activeCellPositionX_num;
			cellView_gcv.setX(aX_num + offset_num);
		}
	}

	public void setColumnYByActiveCell(float aY_num)
	{
		GCellView[] column_cv_arr = this.cellViewsInversion_gcv_arr_arr[this.activeCellColumnIndex_int];
		float activeCellPositionY_num = this.getActiveCell().getY();

		for(int i = 0; i < column_cv_arr.Length; i++)
		{
			GCellView cellView_gcv = column_cv_arr[i];
			float offset_num = cellView_gcv.getY() - activeCellPositionY_num;
			cellView_gcv.setY(aY_num + offset_num);
		}
	}


/*
	setRowX(aRowIndex_int, aX_num)
	{
		let column_cv_arr = this.cellViews_gcv_arr_arr[aRowIndex_int];

		for(let i = 0; i < column_cv_arr.length; i++)
		{
			column_cv_arr[i].setX(aX_num);
		}
	}

	setColumnY(aColumnIndex_int, aY_num)
	{
		let column_cv_arr = this.cellViewsInversion_gcv_arr_arr[aColumnIndex_int];

		for(let i = 0; i < column_cv_arr.length; i++)
		{
			column_cv_arr[i].setY(aY_num);
		}
	}*/

	public void applyActionArtificially(int[] aAction_int_arr)
	{
		this.applyActionArtificially(
			aAction_int_arr[0],
			aAction_int_arr[1],
			aAction_int_arr[2]);
	}

	public void undoActionArtificially(int[] aAction_int_arr)
	{
		this.applyActionArtificially(
			aAction_int_arr[0],
			aAction_int_arr[1],
			-aAction_int_arr[2]);
	}

	public void applyActionArtificially(int aActionTypeId_int, int aEntryPointIndex_int, int aDelta_int)
	{
		if(this.stateId_int != GGridView.STATE_ID_LISTENING)
		{
			return;
		}

		switch(aActionTypeId_int)
		{
			case GGridModel.ACTION_TYPE_ID_UP:
			case GGridModel.ACTION_TYPE_ID_DOWN:
			{
				this.activeCellRowIndex_int = 0;
				this.activeCellColumnIndex_int = aEntryPointIndex_int;

				GCellView cellView_gcv = this.getActiveCell();
				cellView_gcv.setDirectionId(GCellView.DIRECTION_ID_VERTICAL);

				this.adjustmentOffset_num = cellView_gcv.getY();
				this.adjustmentDelta_num = aDelta_int;
			}
			break;
			case GGridModel.ACTION_TYPE_ID_LEFT:
			case GGridModel.ACTION_TYPE_ID_RIGHT:
			{
				this.activeCellRowIndex_int = aEntryPointIndex_int;
				this.activeCellColumnIndex_int = 0;

				GCellView cellView_gcv = this.getActiveCell();
				cellView_gcv.setDirectionId(GCellView.DIRECTION_ID_HORIZONTAL);

				this.adjustmentOffset_num = cellView_gcv.getX();
				this.adjustmentDelta_num = aDelta_int;
			}
			break;
		}

		switch(aActionTypeId_int)
		{
			case GGridModel.ACTION_TYPE_ID_UP:
			case GGridModel.ACTION_TYPE_ID_LEFT:
			{
				this.adjustmentDelta_num *= -1;
			}
			break;
		}

		this.adjustableValue_gav.resetValue();
		this.setStateId(GGridView.STATE_ID_ARTIFICIAL_INTERACTION);
	}

	public float getActiveRowRightBorderX()
	{
		GCellView cellView_gcv = this.cellViews_gcv_arr_arr[this.activeCellRowIndex_int][this.columnsNumber_int-1];
		float cellWidth_num = cellView_gcv.getWidth() * GScreen.getSidesRatio();

		return cellView_gcv.getX() + cellWidth_num;
	}

	public float getActiveRowLeftBorderX()
	{
		return this.cellViews_gcv_arr_arr[this.activeCellRowIndex_int][0].getX();
	}

	public float getActiveColumnBottomBorderY()
	{
		GCellView cellView_gcv = this.cellViews_gcv_arr_arr[this.rowsNumber_int-1][this.activeCellColumnIndex_int];
		float cellHeight_num = cellView_gcv.getHeight();

		return cellView_gcv.getY() + cellHeight_num;
	}

	public float getActiveColumnTopBorderY()
	{
		GCellView cellView_gcv = this.cellViews_gcv_arr_arr[0][this.activeCellColumnIndex_int];

		return cellView_gcv.getY();
	}

	public int getActiveRowIndex()
	{
		return this.activeCellRowIndex_int;
	}

	public int getActiveColumnIndex()
	{
		return this.activeCellColumnIndex_int;
	}

	protected override void onEnabledStateChanged(bool aIsEnabled_bl)
	{
		for(int y = 0; y < this.rowsNumber_int; y++)
		{
			for(int x = 0; x < this.columnsNumber_int; x++)
			{
				this.cellViews_gcv_arr_arr[y][x].setEnabled(aIsEnabled_bl);
			}
		}
	}
}