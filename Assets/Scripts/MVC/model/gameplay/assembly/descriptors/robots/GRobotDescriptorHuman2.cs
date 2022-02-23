public class GRobotDescriptorHuman2 : GRobotDescriptor
{
	public GRobotDescriptorHuman2()
		: base()
	{

	}

	public override GTemplateDescriptor getTemplateDescriptor()
	{
		return GRobotTemplate.TEMPLATE_DESCRIPTOR_110_111_011;
	}


	public override GRobotDetailView generateDetailView(int aCellIndex_int)
	{
		switch (aCellIndex_int)
		{
			case GTemplateDescriptor.CELL_ID_A1: return new GRobotHandUpView();
			case GTemplateDescriptor.CELL_ID_A2: return new GRobotHeadView();
			case GTemplateDescriptor.CELL_ID_A3: return null;

			case GTemplateDescriptor.CELL_ID_B1: return new GRobotArmUpLeftView();
			case GTemplateDescriptor.CELL_ID_B2: return new GRobotBodyView();
			case GTemplateDescriptor.CELL_ID_B3: return new GRobotArmRightView();

			case GTemplateDescriptor.CELL_ID_C1: return null;
			case GTemplateDescriptor.CELL_ID_C2: return new GRobotLegsView();
			case GTemplateDescriptor.CELL_ID_C3: return new GRobotFistRightView();
		}

		return null;
	}
} 