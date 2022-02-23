public class GRoundRobotSetDescriptor
{
	private GRobotDescriptor robotDescriptor_grd;
	private int initialRobotsNumber_int;
	private int remainingRobotsNumber_int;

	public GRoundRobotSetDescriptor(GRobotDescriptor aRobotDescriptor_grd, int aremainingRobotsNumber_int)
	{
		this.robotDescriptor_grd = aRobotDescriptor_grd;
		this.initialRobotsNumber_int = aremainingRobotsNumber_int;
		this.remainingRobotsNumber_int = aremainingRobotsNumber_int;
	}

	public GRobotDescriptor getRobotDescriptor()
	{
		return this.robotDescriptor_grd;
	}

	public int getRobotsNumber()
	{
		return this.remainingRobotsNumber_int;
	}

	public void decrementRobotsNumber()
	{
		this.remainingRobotsNumber_int--;
	}

	public bool isEmpty()
	{
		return this.remainingRobotsNumber_int == 0;
	}

	public void reset()
	{
		this.remainingRobotsNumber_int = this.initialRobotsNumber_int;
	}
}