public class GTemplateDescriptor110111011 : GTemplateDescriptor
{
	private static readonly int[][] IDS_MAP = new int[3][]
	{
		new int[] { GTemplateDescriptor.CELL_ID_A1, GTemplateDescriptor.CELL_ID_A2, GTemplateDescriptor.CELL_ID_00 },
		new int[] { GTemplateDescriptor.CELL_ID_B1, GTemplateDescriptor.CELL_ID_B2, GTemplateDescriptor.CELL_ID_B3 },
		new int[] { GTemplateDescriptor.CELL_ID_00, GTemplateDescriptor.CELL_ID_C2, GTemplateDescriptor.CELL_ID_C3 },
	};

	public override int[][] getIdsMap()
	{
		return GTemplateDescriptor110111011.IDS_MAP;
	}

	public override int getId()
	{
		return GTemplateDescriptor.TEMPLATE_ID_3;
	}
}