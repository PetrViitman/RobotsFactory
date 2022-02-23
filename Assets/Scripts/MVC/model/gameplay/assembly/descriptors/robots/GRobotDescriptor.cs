public class GRobotDescriptor
{
	public virtual GTemplateDescriptor getTemplateDescriptor()
	{
		return null;
	}

	public int[][] getIdsMap()
	{
		return this.getTemplateDescriptor().getIdsMap();
	}

	public int getTemplateId()
	{
		return this.getTemplateDescriptor().getId();
	}

	public virtual GRobotDetailView generateDetailView(int aCellIndex_int)
	{
		return null;
	}
} 