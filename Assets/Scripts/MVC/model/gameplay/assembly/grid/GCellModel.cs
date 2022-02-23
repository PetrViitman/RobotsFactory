public class GCellModel : GModel
{
	private int cellTypeId_int = 0;

	public GCellModel(int aCellTypeId_int)
		: base()
	{
		this.cellTypeId_int = aCellTypeId_int;
	}

	public int getTypeId()
	{
		return this.cellTypeId_int;
	}
}