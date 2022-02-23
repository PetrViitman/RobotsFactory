using UnityEngine;

public class GAtlas
{
	private GAtlasSector[] sectors_gas_arr;
	public const int ATLAS_SEGMENT_PADDING = 5;

	public GAtlas(Texture2D aTexture_t2d, string[] aDescriptors_str_arr)
	{
		int descriptorsCount_int = aDescriptors_str_arr.Length;
		int textureWidth_int = aTexture_t2d.width;
		int textureHeight_int = aTexture_t2d.height;

		this.sectors_gas_arr = new GAtlasSector[descriptorsCount_int];

		//CUTTING TEXTURE TO SECTORS...
		for( int i = 0; i < descriptorsCount_int; i++ )
		{
			//PARSING PARAMETERS...
			string descriptor_str = aDescriptors_str_arr[i];
			string[] descriptorParams_str_arr = descriptor_str.Split(' ');
			
			string id_str = descriptorParams_str_arr[0];
			int x_int = int.Parse(descriptorParams_str_arr[1]);
			int y_int = int.Parse(descriptorParams_str_arr[2]);
			int width_int = int.Parse(descriptorParams_str_arr[3]);
			int height_int = int.Parse(descriptorParams_str_arr[4]);
			//...PARSING PARAMETERS

			Rect rectangle_r = new Rect(
				x_int + GAtlas.ATLAS_SEGMENT_PADDING,
				textureHeight_int - y_int - GAtlas.ATLAS_SEGMENT_PADDING,
				width_int - GAtlas.ATLAS_SEGMENT_PADDING * 2,
				-height_int + GAtlas.ATLAS_SEGMENT_PADDING * 2);

			Sprite sprite_s = Sprite.Create(
				aTexture_t2d, 
				rectangle_r, 
				new Vector2(0.0f, 0.0f),
				1f);

			this.sectors_gas_arr[i] = new GAtlasSector(id_str, sprite_s);
		}
		//...CUTTING TEXTURE TO SECTORS
	}

	public Sprite getSprite(string aId_str)
	{
		for( int i = 0; i < this.sectors_gas_arr.Length; i++ )
		{
			GAtlasSector sector_gas = this.sectors_gas_arr[i];

			if(sector_gas.hasMatchingId(aId_str))
			{
				return sector_gas.getSprite();
			}
		}

		return null;
	}
}