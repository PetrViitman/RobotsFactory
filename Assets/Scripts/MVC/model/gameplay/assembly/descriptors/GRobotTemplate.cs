using UnityEngine;

public static class GRobotTemplate
{
	//TEMPLATES DESCRIPTORS...
	public static GTemplateDescriptor TEMPLATE_DESCRIPTOR_111_111_111 = new GTemplateDescriptor111111111();
	public static GTemplateDescriptor TEMPLATE_DESCRIPTOR_010_111_111 = new GTemplateDescriptor010111111();
	public static GTemplateDescriptor TEMPLATE_DESCRIPTOR_110_111_011 = new GTemplateDescriptor110111011();
	//...TEMPLATES DESCRIPTORS

	//ROBOTS DESCRIPTORS...
	public static GRobotDescriptorHuman ROBOT_DESCRIPTOR_HUMAN = new GRobotDescriptorHuman();
	public static GRobotDescriptorCrab ROBOT_DESCRIPTOR_CRAB = new GRobotDescriptorCrab();
	public static GRobotDescriptorHuman2 ROBOT_DESCRIPTOR_HUMAN_2 = new GRobotDescriptorHuman2();
	public static GRobotDescriptorOctopus ROBOT_DESCRIPTOR_OCTOPUS = new GRobotDescriptorOctopus();
	public static GRobotDescriptorSpider ROBOT_DESCRIPTOR_SPIDER = new GRobotDescriptorSpider();
	public static GRobotDescriptorBird ROBOT_DESCRIPTOR_BIRD = new GRobotDescriptorBird();

	public static void setActualRobotDescriptor(GRobotDescriptor aRobotDescriptor_grd)
	{
		GRobotTemplate.ROBOT_DESCRIPTOR = aRobotDescriptor_grd;
	}

	public static GRobotDescriptor ROBOT_DESCRIPTOR = null;
	//...ROBOTS DESCRIPTORS
}