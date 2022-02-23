public class GStatisticRobotViewPool : GView
{
	private GStatisticRobotView[] robotStatisticView_arr_gsrv;
	private int length_int;

	public GStatisticRobotViewPool(int aMaximalRobotsNumber_int)
		: base()
	{

		this.robotStatisticView_arr_gsrv = new GStatisticRobotView[aMaximalRobotsNumber_int];
	}

	public GStatisticRobotView add()
	{
		if(this.isFull())
		{
			return null;
		}

		int index_int = this.length_int;
		GStatisticRobotView[] robotStatisticView_arr_gsrv = this.robotStatisticView_arr_gsrv;
		
		if(robotStatisticView_arr_gsrv[index_int] == null)
		{
			robotStatisticView_arr_gsrv[index_int] = new GStatisticRobotView(); 
		}

		this.length_int++;

		return robotStatisticView_arr_gsrv[index_int];
	}

	public GStatisticRobotView getRobot(int aRobotIndex_int)
	{
		return this.robotStatisticView_arr_gsrv[aRobotIndex_int];
	}

	public void drop()
	{
		this.length_int = 0;
	}

	public void delete(int aIndex_int)
	{
		this.length_int--;
	}

	public bool isFull()
	{
		return this.length_int == robotStatisticView_arr_gsrv.Length;
	}

	public bool isEmpty()
	{
		return this.length_int == 0;
	}

	public int length()
	{
		return this.length_int;
	}

	public void update()
	{
		for( int i = 0; i < this.length_int; i++ )
		{
			this.robotStatisticView_arr_gsrv[i].update();
		}
	}

	public int getVisibleRobotsNumber()
	{
		int robotsNumber_int = 0;

		for( int i = 0; i < this.length_int; i++ )
		{
			if(this.robotStatisticView_arr_gsrv[i].getAlpha() > 0)
			{
				robotsNumber_int++;
			}
		}

		return robotsNumber_int;
	}
}