using UnityEngine;

public class GGridController : GController
{
	public GGridController(GGridModel aGridModel_ggm, GGridView aGridView_ggv)
		: base(aGridModel_ggm, aGridView_ggv)
	{

	}

	public void onGridViewRowChanged(int rowIndex_int, int value_int)
	{
		GGridModel gridModel_ggm = (GGridModel) this.getModel();
		gridModel_ggm.pushRow(rowIndex_int, value_int);

		//EXTENDED ACTION DATA FOR UPPER LEVEL INFORMING...
		int actionId_int = GGridModel.ACTION_TYPE_ID_RIGHT;
		int entryIndex_int = rowIndex_int;
		int delta_int = value_int;

		if(delta_int < 0)
		{
			delta_int *= -1;
			actionId_int = GGridModel.ACTION_TYPE_ID_LEFT;
		}
		//...EXTENDED ACTION DATA FOR UPPER LEVEL INFORMING

		GMain.getGameplayController().getAssemblyController().onGridChanged(actionId_int, entryIndex_int, delta_int);
	}

	public void onGridViewColumnChanged(int columnIndex_int, int value_int)
	{
		GGridModel gridModel_ggm = (GGridModel) this.getModel();
		gridModel_ggm.pushColumn(columnIndex_int, value_int);
		
		//EXTENDED ACTION DATA FOR UPPER LEVEL INFORMING...
		int actionId_int = GGridModel.ACTION_TYPE_ID_DOWN;
		int entryIndex_int = columnIndex_int;
		int delta_int = value_int;

		if(delta_int < 0)
		{
			delta_int *= -1;
			actionId_int = GGridModel.ACTION_TYPE_ID_UP;
		}
		//...EXTENDED ACTION DATA FOR UPPER LEVEL INFORMING

		GMain.getGameplayController().getAssemblyController().onGridChanged(actionId_int, entryIndex_int, delta_int);
	}
}