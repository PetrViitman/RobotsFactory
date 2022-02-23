using UnityEngine;

public class GAssemblyController : GController
{
	private GGridController gridController_ggc;

	public GAssemblyController(GAssemblyModel aAssemblyModel_gam)
		: base(aAssemblyModel_gam)
	{

	}

	public void onGridChanged(int aActionId_int, int aEntryIndex_int, int aDelta_int)
	{
		GGridModel gridModel_ggm = (GGridModel) this.gridController_ggc.getModel();
		GAssemblyModel assemblyModel_gam = (GAssemblyModel) this.getModel();
		assemblyModel_gam.trackAction(aActionId_int, aEntryIndex_int, aDelta_int);

		if(gridModel_ggm.isEqualTo(GRobotTemplate.ROBOT_DESCRIPTOR.getIdsMap()))
		{
			GMain.getGameController().onRobotAssembled();
		}
	}

	public GGridController getGridController()
	{
		return this.gridController_ggc;
	}

	public void restart()
	{
		GAssemblyModel assemblyModel_gam = (GAssemblyModel) this.getModel();
		assemblyModel_gam.restart();

		this.gridController_ggc = new GGridController(assemblyModel_gam.getGridModel(), new GGridView(50,0));
	}
}