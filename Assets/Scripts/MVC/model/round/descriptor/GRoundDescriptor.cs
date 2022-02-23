public class GRoundDescriptor
{
	private int requiredAssembledRobotsNumber_int;
	private int durationInSeconds_int;
	private int assembleStepsNumber_int;
	private GRoundRobotDescriptorPool descriptorsPool_grrdp;
	private bool isProdressiveAssemblyDifficultyRequired_bl;

	public GRoundDescriptor()
	{
		this.descriptorsPool_grrdp = new GRoundRobotDescriptorPool();
		this.isProdressiveAssemblyDifficultyRequired_bl = false;
	}

	public GRoundRobotDescriptorPool getDescriptorsPool()
	{
		return this.descriptorsPool_grrdp;
	}

	public void setDurationInSeconds(int aDurationInSeconds_int)
	{
		this.durationInSeconds_int = aDurationInSeconds_int;
	}

	public int getDurationInSeconds()
	{
		return this.durationInSeconds_int;
	}

	public void setAssembleSepsNumber(int aAssembleStepsNumber_int)
	{
		this.assembleStepsNumber_int = aAssembleStepsNumber_int;
	}

	public int getAssembleSepsNumber()
	{
		return this.assembleStepsNumber_int;
	}

	public void setRequiredAssembledRobotsNumber(int aRequiredAssembledRobotsNumber_int)
	{
		this.requiredAssembledRobotsNumber_int = aRequiredAssembledRobotsNumber_int;
	}

	public int getRequiredAssembledRobotsNumber()
	{
		return this.requiredAssembledRobotsNumber_int;
	}

	public bool isProdressiveAssemblyDifficultyRequired()
	{
		return this.isProdressiveAssemblyDifficultyRequired_bl;
	}

	public void setProdressiveAssemblyDifficultyMode()
	{
		this.isProdressiveAssemblyDifficultyRequired_bl = true;
	}
}