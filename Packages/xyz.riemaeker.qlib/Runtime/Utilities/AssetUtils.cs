using System.Linq;
using Qlib.Assets;
using UnityEngine;

namespace Qlib.Utilities
{
	public static class AssetUtils
	{
		public static Asset DeserializeAsset(string path, byte[] data)
		{
			var extension = path.Split(".").Last();
			var basename = path.Split("/").Last().Split(".").First();
			
			return extension switch
			{
				"cfg" or "rc" => DeserializeAsset<PlainText>(data),
				"lmp" => DeserializeLump(basename, data),
				_ => DeserializeAsset<GenericAsset>(data)
			};
		}

		public static T DeserializeAsset<T>(byte[] data) where T : Asset
		{
			var asset = ScriptableObject.CreateInstance<T>();
			asset.Deserialize(data); 
			return asset;
		}

		private static Asset DeserializeLump(string basename, byte[] data)
		{
			return basename switch
			{
				"palette" => DeserializeAsset<Palette>(data),
				"colormap" => DeserializeAsset<ColorMap>(data),
				"pop" => DeserializeAsset<ProofOfPurchase>(data),
				_ => DeserializeAsset<Picture>(data)
			};
		}
	}
}