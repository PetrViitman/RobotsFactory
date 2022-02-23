public class GTemplateDescriptor
{
	//GRID CELLS POSSIBLE IDS...
	public const int CELL_ID_00 = 0;

	public const int CELL_ID_A1 = 1;
	public const int CELL_ID_A2 = 2;
	public const int CELL_ID_A3 = 3;

	public const int CELL_ID_B1 = 4;
	public const int CELL_ID_B2 = 5;
	public const int CELL_ID_B3 = 6;

	public const int CELL_ID_C1 = 7;
	public const int CELL_ID_C2 = 8;
	public const int CELL_ID_C3 = 9;
	//...GRID CELLS POSSIBLE IDS

	//TEMPLATES POSSIBLE IDS...
	public const int TEMPLATE_ID_1 = 0;
	public const int TEMPLATE_ID_2 = 1;
	public const int TEMPLATE_ID_3 = 2;
	//...TEMPLATES POSSIBLE IDS

	public virtual int[][] getIdsMap()
	{
		return null;
	}

	public virtual int getId()
	{
		return -1;
	}
}