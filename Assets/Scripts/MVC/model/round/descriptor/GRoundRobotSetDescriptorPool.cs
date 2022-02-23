using UnityEngine;

public class GRoundRobotDescriptorPool
{
	private const int MAXIMAL_ROUND_ROBOTS_DESCRIPTORS_NUMBER = 20;
	private GRoundRobotSetDescriptor[] descriptors_grrd_arr;
	private int descriptorsNumber_int;
	private int previousRandomGeneratedIndex_int;

	public GRoundRobotDescriptorPool()
	{
		this.descriptors_grrd_arr = new GRoundRobotSetDescriptor[GRoundRobotDescriptorPool.MAXIMAL_ROUND_ROBOTS_DESCRIPTORS_NUMBER];
		this.descriptorsNumber_int = 0;
		this.previousRandomGeneratedIndex_int = -1;
	}

	public void add(GRobotDescriptor aRobotDescriptor_grd, int aRobotsNumber_int)
	{
		this.descriptors_grrd_arr[this.descriptorsNumber_int] = new GRoundRobotSetDescriptor(aRobotDescriptor_grd, aRobotsNumber_int);
		this.descriptorsNumber_int++;
	}

	public void reset()
	{
		for( int i = 0; i < this.descriptorsNumber_int; i++ )
		{
			this.descriptors_grrd_arr[i].reset();
		}
	}

	public GRobotDescriptor getNextRandomRobotDescriptor()
	{
		/*
		this.previousRandomGeneratedIndex_int++;

		if(this.previousRandomGeneratedIndex_int == this.descriptorsNumber_int)
		{
			this.previousRandomGeneratedIndex_int = 0;
		}

		Debug.Log(this.previousRandomGeneratedIndex_int);

		return this.descriptors_grrd_arr[this.previousRandomGeneratedIndex_int].getRobotDescriptor();
		*/

		int randomIndex_int = Random.Range(0, this.descriptorsNumber_int);
		
		if(randomIndex_int == this.previousRandomGeneratedIndex_int)
		{
			randomIndex_int++;
			if(randomIndex_int >= this.descriptorsNumber_int)
			{
				randomIndex_int = 0;
			}
		}

		this.previousRandomGeneratedIndex_int = randomIndex_int;

		GRoundRobotSetDescriptor descriptor_grrd = descriptors_grrd_arr[randomIndex_int];

		if(descriptor_grrd.isEmpty())
		{
			for( int i = 0; i < this.descriptorsNumber_int; i++ )
			{
				descriptor_grrd = this.descriptors_grrd_arr[i];

				if(!descriptor_grrd.isEmpty())
				{
					break;
				}
			}

			if(descriptor_grrd.isEmpty())
			{
				this.reset();
			}
		}

		descriptor_grrd.decrementRobotsNumber();

		return descriptor_grrd.getRobotDescriptor();
	}
}