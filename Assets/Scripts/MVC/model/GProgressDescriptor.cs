public class GProgressDescriptor
{
	public const int ROUNDS_NUMBER = 5;

	public static GRoundDescriptor getRoundDescriptor(int aRoundIndex_int)
	{
		GRoundDescriptor roundDescriptor_grd = new GRoundDescriptor();
		GRoundRobotDescriptorPool descriptors_grrdp = roundDescriptor_grd.getDescriptorsPool();

		switch(aRoundIndex_int)
		{
			case 0:
			{
				roundDescriptor_grd.setDurationInSeconds(90);
				roundDescriptor_grd.setRequiredAssembledRobotsNumber(3);
				roundDescriptor_grd.setAssembleSepsNumber(5);
				roundDescriptor_grd.setProdressiveAssemblyDifficultyMode();
			
				descriptors_grrdp.add(GRobotTemplate.ROBOT_DESCRIPTOR_HUMAN, 1);
				descriptors_grrdp.add(GRobotTemplate.ROBOT_DESCRIPTOR_HUMAN_2, 1);
				descriptors_grrdp.add(GRobotTemplate.ROBOT_DESCRIPTOR_CRAB, 1);
			}
			break;
			case 1:
			{
				roundDescriptor_grd.setDurationInSeconds(120);
				roundDescriptor_grd.setAssembleSepsNumber(10);
				roundDescriptor_grd.setRequiredAssembledRobotsNumber(4);
				
				descriptors_grrdp.add(GRobotTemplate.ROBOT_DESCRIPTOR_BIRD, 1);
				descriptors_grrdp.add(GRobotTemplate.ROBOT_DESCRIPTOR_CRAB, 1);
			}
			break;
			case 2:
			{
				roundDescriptor_grd.setDurationInSeconds(170);
				roundDescriptor_grd.setRequiredAssembledRobotsNumber(5);
				roundDescriptor_grd.setAssembleSepsNumber(12);
				
				descriptors_grrdp.add(GRobotTemplate.ROBOT_DESCRIPTOR_BIRD, 1);
				descriptors_grrdp.add(GRobotTemplate.ROBOT_DESCRIPTOR_SPIDER, 1);
				descriptors_grrdp.add(GRobotTemplate.ROBOT_DESCRIPTOR_OCTOPUS, 1);
			}
			break;
			case 3:
			{
				roundDescriptor_grd.setDurationInSeconds(250);
				roundDescriptor_grd.setAssembleSepsNumber(15);
				roundDescriptor_grd.setRequiredAssembledRobotsNumber(6);
				
				descriptors_grrdp.add(GRobotTemplate.ROBOT_DESCRIPTOR_BIRD, 1);
				descriptors_grrdp.add(GRobotTemplate.ROBOT_DESCRIPTOR_SPIDER, 1);
				descriptors_grrdp.add(GRobotTemplate.ROBOT_DESCRIPTOR_CRAB, 1);
				descriptors_grrdp.add(GRobotTemplate.ROBOT_DESCRIPTOR_OCTOPUS, 1);
			}
			break;
			case 4:
			{
				roundDescriptor_grd.setDurationInSeconds(300);
				roundDescriptor_grd.setAssembleSepsNumber(15);
				roundDescriptor_grd.setRequiredAssembledRobotsNumber(7);
				
				descriptors_grrdp.add(GRobotTemplate.ROBOT_DESCRIPTOR_HUMAN, 1);
				descriptors_grrdp.add(GRobotTemplate.ROBOT_DESCRIPTOR_HUMAN_2, 1);
				descriptors_grrdp.add(GRobotTemplate.ROBOT_DESCRIPTOR_BIRD, 1);
				descriptors_grrdp.add(GRobotTemplate.ROBOT_DESCRIPTOR_CRAB, 1);
				descriptors_grrdp.add(GRobotTemplate.ROBOT_DESCRIPTOR_SPIDER, 1);
				descriptors_grrdp.add(GRobotTemplate.ROBOT_DESCRIPTOR_OCTOPUS, 1);
			}
			break;
		}

		return roundDescriptor_grd;
	}
}