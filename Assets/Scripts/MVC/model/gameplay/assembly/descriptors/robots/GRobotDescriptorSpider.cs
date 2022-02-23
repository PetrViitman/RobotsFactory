public class GRobotDescriptorSpider : GRobotDescriptor
{
	public GRobotDescriptorSpider()
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
			case GTemplateDescriptor.CELL_ID_A1: return new GRobotArmSpiderTopLeftView();
			case GTemplateDescriptor.CELL_ID_A2: return new GRobotHeadSpiderView();
			case GTemplateDescriptor.CELL_ID_A3: return new GRobotArmSpiderTopRightView();

			case GTemplateDescriptor.CELL_ID_B1: return new GRobotArmSpiderMiddleLeftView();
			case GTemplateDescriptor.CELL_ID_B2: return new GRobotBodyView();
			case GTemplateDescriptor.CELL_ID_B3: return new GRobotArmSpiderMiddleRightView();

			case GTemplateDescriptor.CELL_ID_C1: return new GRobotLegCurveLeftView();
			case GTemplateDescriptor.CELL_ID_C2: return new GRobotPelvicView();
			case GTemplateDescriptor.CELL_ID_C3: return new GRobotLegCurveRightView();
		}

		return null;
	}
} 