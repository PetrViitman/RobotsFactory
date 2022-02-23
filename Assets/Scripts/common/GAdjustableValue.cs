using UnityEngine;

public class GAdjustableValue
{
	private float valuePerStep_num = 10f;
	private float value_num = 0f;

	public GAdjustableValue(int aStepsNumber_int)
		: base()
	{
		this.resetValue(aStepsNumber_int);
	}

	public void resetValue()
	{
		this.value_num = 0;
	}

	public void resetValue(int aStepsNumber_int)
	{
		this.valuePerStep_num = 1.0f / aStepsNumber_int;
		this.resetValue();
	}

	public virtual void update()
	{
		this.value_num += this.valuePerStep_num;

		if(this.value_num > 1)
		{
			this.value_num = 1;
		}
	}

	public float getValue()
	{
		return this.value_num;
	}

	public bool isFull()
	{
		return this.value_num == 1;
	}

	public virtual void randomize()
	{
		this.value_num = Random.Range(0.0f, 1.0f);
	}

	public void copy(GAdjustableValue aAdjustableValue_gav)
	{
		this.value_num = aAdjustableValue_gav.getValue();
		this.valuePerStep_num = aAdjustableValue_gav.valuePerStep_num;
	}
}