using UnityEngine;

public class GRobotDescriptorCrab : GRobotDescriptor
{
	public GRobotDescriptorCrab()
		: base()
	{

	}

	public override GTemplateDescriptor getTemplateDescriptor()
	{
		return GRobotTemplate.TEMPLATE_DESCRIPTOR_010_111_111;
	}


	public override GRobotDetailView generateDetailView(int aCellIndex_int)
	{
		switch (aCellIndex_int)
		{
			case GTemplateDescriptor.CELL_ID_00: return new GRobotClawView();
			case GTemplateDescriptor.CELL_ID_A2: return new GRobotHeadWideView();
			case GTemplateDescriptor.CELL_ID_A3: return new GRobotClawView();

			case GTemplateDescriptor.CELL_ID_B1: return new GRobotArmUpLeftView();
			case GTemplateDescriptor.CELL_ID_B2: return new GRobotBodyView();
			case GTemplateDescriptor.CELL_ID_B3: return new GRobotArmUpRightView();

			case GTemplateDescriptor.CELL_ID_C1: return new GRobotLegCurveLeftView();
			case GTemplateDescriptor.CELL_ID_C2: return new GRobotPelvicView();
			case GTemplateDescriptor.CELL_ID_C3: return new GRobotLegCurveRightView();
		}

		return null;
	}
} 