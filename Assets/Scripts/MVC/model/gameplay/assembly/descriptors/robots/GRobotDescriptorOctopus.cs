public class GRobotDescriptorOctopus : GRobotDescriptor
{
	public GRobotDescriptorOctopus()
		: base()
	{

	}

	public override GTemplateDescriptor getTemplateDescriptor()
	{
		return GRobotTemplate.TEMPLATE_DESCRIPTOR_111_111_111;
	}

	public override GRobotDetailView generateDetailView(int aCellIndex_int)
	{
		switch (aCellIndex_int)
		{
			case GTemplateDescriptor.CELL_ID_A1: return new GRobotTentacleTopLeftView();
			case GTemplateDescriptor.CELL_ID_A2: return new GRobotHeadRoundView();
			case GTemplateDescriptor.CELL_ID_A3: return new GRobotTentacleTopRightView();

			case GTemplateDescriptor.CELL_ID_B1: return new GRobotTentacleMiddleLeftView();
			case GTemplateDescriptor.CELL_ID_B2: return new GRobotBodyView();
			case GTemplateDescriptor.CELL_ID_B3: return new GRobotTentacleMiddleRightView();

			case GTemplateDescriptor.CELL_ID_C1: return new GRobotTentacleBottomLeftView();
			case GTemplateDescriptor.CELL_ID_C2: return new GRobotPelvicTentacleView();
			case GTemplateDescriptor.CELL_ID_C3: return new GRobotTentacleBottomRightView();
		}

		return null;
	}
} 