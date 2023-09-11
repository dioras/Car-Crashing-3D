using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Gaia
{
	public class Utils : MonoBehaviour
	{
		public static string GetGaiaAssetDirectory()
		{
			string text = Path.Combine(Application.dataPath, GaiaConstants.AssetDir);
			return text.Replace('\\', '/');
		}

		public static string GetGaiaAssetDirectory(GaiaConstants.FeatureType featureType)
		{
			string text = Path.Combine(Application.dataPath, GaiaConstants.AssetDir);
			text = Path.Combine(text, featureType.ToString());
			return text.Replace('\\', '/');
		}

		public static List<string> GetGaiaStampsList(GaiaConstants.FeatureType featureType)
		{
			return new List<string>(Directory.GetFiles(Utils.GetGaiaAssetDirectory(featureType), "*.jpg"));
		}

		public static string GetGaiaAssetPath(GaiaConstants.FeatureType featureType, string assetName)
		{
			string text = Utils.GetGaiaAssetDirectory(featureType);
			text = Path.Combine(Utils.GetGaiaAssetDirectory(featureType), assetName);
			return text.Replace('\\', '/');
		}

		public static string GetGaiaStampAssetPath(GaiaConstants.FeatureType featureType, string assetName)
		{
			string text = Utils.GetGaiaAssetDirectory(featureType);
			text = Path.Combine(Utils.GetGaiaAssetDirectory(featureType), "Data");
			text = Path.Combine(text, assetName);
			return text.Replace('\\', '/');
		}

		public static string GetGaiaStampPath(Texture2D source)
		{
			string text = string.Empty;
			string fileName = Path.GetFileName(text);
			text = Path.Combine(Path.GetDirectoryName(text), "Data");
			text = Path.Combine(text, fileName);
			text = Path.ChangeExtension(text, ".bytes");
			return text.Replace('\\', '/');
		}

		public static bool CheckValidGaiaStampPath(Texture2D source)
		{
			string text = string.Empty;
			if (Path.GetExtension(text).ToLower() != ".jpg")
			{
				return false;
			}
			string fileName = Path.GetFileName(text);
			text = Path.Combine(Path.GetDirectoryName(text), "Data");
			text = Path.Combine(text, fileName);
			text = Path.ChangeExtension(text, ".bytes");
			text = text.Replace('\\', '/');
			return File.Exists(text);
		}

		public static void CreateGaiaAssetDirectories()
		{
		}

		public static T[] GetAtPath<T>(string path)
		{
			ArrayList arrayList = new ArrayList();
			T[] array = new T[arrayList.Count];
			for (int i = 0; i < arrayList.Count; i++)
			{
				array[i] = (T)((object)arrayList[i]);
			}
			return array;
		}

		public static void MakeTextureNormal(Texture2D texture)
		{
			if (texture == null)
			{
				return;
			}
		}

		public static void MakeTextureReadable(Texture2D texture)
		{
			if (texture == null)
			{
				return;
			}
		}

		public static void CompressToSingleChannelFileImage(float[,] input, string imageName, TextureFormat imageStorageFormat = TextureFormat.RGBA32, bool exportPNG = true, bool exportJPG = true)
		{
			int length = input.GetLength(0);
			int length2 = input.GetLength(1);
			Texture2D texture2D = new Texture2D(length, length2, imageStorageFormat, false);
			Color color = default(Color);
			color.a = 1f;
			color.r = (color.g = (color.b = 0f));
			for (int i = 0; i < length; i++)
			{
				for (int j = 0; j < length2; j++)
				{
					color.r = (color.b = (color.g = input[i, j]));
					texture2D.SetPixel(i, j, color);
				}
			}
			texture2D.Apply();
			if (exportJPG)
			{
				Utils.ExportJPG(imageName, texture2D);
			}
			if (exportPNG)
			{
				Utils.ExportPNG(imageName, texture2D);
			}
			UnityEngine.Object.DestroyImmediate(texture2D);
		}

		public static void CompressToMultiChannelFileImage(float[,,] input, string imageName, TextureFormat imageStorageFormat = TextureFormat.RGBA32, bool exportPNG = true, bool exportJPG = true)
		{
			int length = input.GetLength(0);
			int length2 = input.GetLength(1);
			int length3 = input.GetLength(2);
			int num = (length3 + 3) / 4;
			for (int i = 0; i < num; i++)
			{
				Texture2D texture2D = new Texture2D(length, length, imageStorageFormat, false);
				Color color = default(Color);
				int num2 = i * 4;
				for (int j = 0; j < length; j++)
				{
					for (int k = 0; k < length2; k++)
					{
						color.r = ((num2 >= length3) ? 0f : input[j, k, num2]);
						color.g = ((num2 + 1 >= length3) ? 0f : input[j, k, num2 + 1]);
						color.b = ((num2 + 2 >= length3) ? 0f : input[j, k, num2 + 2]);
						color.a = ((num2 + 3 >= length3) ? 0f : input[j, k, num2 + 3]);
						texture2D.SetPixel(j, k, color);
					}
				}
				texture2D.Apply();
				if (exportJPG)
				{
					byte[] bytes = texture2D.EncodeToJPG();
					Utils.WriteAllBytes(imageName + i + ".jpg", bytes);
				}
				if (exportPNG)
				{
					byte[] bytes2 = texture2D.EncodeToPNG();
					Utils.WriteAllBytes(imageName + i + ".png", bytes2);
				}
				UnityEngine.Object.DestroyImmediate(texture2D);
			}
		}

		public static float[,] ConvertTextureToArray(Texture2D texture)
		{
			float[,] array = new float[texture.width, texture.height];
			for (int i = 0; i < texture.width; i++)
			{
				for (int j = 0; j < texture.height; j++)
				{
					array[i, j] = texture.GetPixel(i, j).grayscale;
				}
			}
			return array;
		}

		public static float[,] DecompressFromSingleChannelFileImage(string fileName, int width, int height, TextureFormat imageStorageFormat = TextureFormat.RGBA32, bool channelR = true, bool channelG = false, bool channelB = false, bool channelA = false)
		{
			float[,] array = null;
			if (File.Exists(fileName))
			{
				byte[] data = Utils.ReadAllBytes(fileName);
				Texture2D texture2D = new Texture2D(width, height, imageStorageFormat, false);
				texture2D.LoadImage(data);
				array = new float[width, height];
				for (int i = 0; i < width; i++)
				{
					for (int j = 0; j < height; j++)
					{
						array[i, j] = texture2D.GetPixel(i, j).r;
					}
				}
				UnityEngine.Object.DestroyImmediate(texture2D);
			}
			else
			{
				UnityEngine.Debug.LogError("Unable to find " + fileName);
			}
			return array;
		}

		public static float[,] DecompressFromSingleChannelTexture(Texture2D importTexture, bool channelR = true, bool channelG = false, bool channelB = false, bool channelA = false)
		{
			if (importTexture == null || importTexture.width <= 0 || importTexture.height <= 0)
			{
				UnityEngine.Debug.LogError("Unable to import from texture");
				return null;
			}
			float[,] array = new float[importTexture.width, importTexture.height];
			if (channelR)
			{
				for (int i = 0; i < importTexture.width; i++)
				{
					for (int j = 0; j < importTexture.height; j++)
					{
						array[i, j] = importTexture.GetPixel(i, j).r;
					}
				}
			}
			else if (channelG)
			{
				for (int k = 0; k < importTexture.width; k++)
				{
					for (int l = 0; l < importTexture.height; l++)
					{
						array[k, l] = importTexture.GetPixel(k, l).g;
					}
				}
			}
			else if (channelB)
			{
				for (int m = 0; m < importTexture.width; m++)
				{
					for (int n = 0; n < importTexture.height; n++)
					{
						array[m, n] = importTexture.GetPixel(m, n).b;
					}
				}
			}
			if (channelA)
			{
				for (int num = 0; num < importTexture.width; num++)
				{
					for (int num2 = 0; num2 < importTexture.height; num2++)
					{
						array[num, num2] = importTexture.GetPixel(num, num2).a;
					}
				}
			}
			return array;
		}

		public static void ExportJPG(string fileName, Texture2D texture)
		{
			byte[] bytes = texture.EncodeToJPG();
			Utils.WriteAllBytes(fileName + ".jpg", bytes);
		}

		public static void ExportPNG(string fileName, Texture2D texture)
		{
			byte[] bytes = texture.EncodeToPNG();
			Utils.WriteAllBytes(fileName + ".png", bytes);
		}

		public static float[,] LoadRawFile(string fileName)
		{
			if (!File.Exists(fileName))
			{
				UnityEngine.Debug.LogError("Could not locate heightmap file : " + fileName);
				return null;
			}
			float[,] array = null;
			using (FileStream fileStream = File.OpenRead(fileName))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					int num = Mathf.CeilToInt(Mathf.Sqrt((float)(fileStream.Length / 2L)));
					array = new float[num, num];
					for (int i = 0; i < num; i++)
					{
						for (int j = 0; j < num; j++)
						{
							array[i, j] = (float)binaryReader.ReadUInt16() / 65535f;
						}
					}
				}
				fileStream.Close();
			}
			return array;
		}

		public static Mesh CreateMesh(float[,] heightmap, Vector3 targetSize)
		{
			int num = heightmap.GetLength(0);
			int num2 = heightmap.GetLength(1);
			Vector3 b = Vector3.zero - targetSize / 2f;
			Vector2 b2 = new Vector2(1f / (float)(num - 1), 1f / (float)(num2 - 1));
			int i;
			for (i = 1; i < 100; i++)
			{
				if (num / i * (num2 / i) < 65000)
				{
					break;
				}
			}
			targetSize = new Vector3(targetSize.x / (float)(num - 1) * (float)i, targetSize.y, targetSize.z / (float)(num2 - 1) * (float)i);
			num = (num - 1) / i + 1;
			num2 = (num2 - 1) / i + 1;
			Vector3[] array = new Vector3[num * num2];
			Vector2[] array2 = new Vector2[num * num2];
			Vector3[] array3 = new Vector3[num * num2];
			Color[] array4 = new Color[num * num2];
			int[] array5 = new int[(num - 1) * (num2 - 1) * 6];
			for (int j = 0; j < num2; j++)
			{
				for (int k = 0; k < num; k++)
				{
					array4[j * num + k] = Color.black;
					array3[j * num + k] = Vector3.up;
					array[j * num + k] = Vector3.Scale(targetSize, new Vector3((float)k, heightmap[k * i, j * i], (float)j)) + b;
					array2[j * num + k] = Vector2.Scale(new Vector2((float)(k * i), (float)(j * i)), b2);
				}
			}
			int num3 = 0;
			for (int l = 0; l < num2 - 1; l++)
			{
				for (int m = 0; m < num - 1; m++)
				{
					array5[num3++] = l * num + m;
					array5[num3++] = (l + 1) * num + m;
					array5[num3++] = l * num + m + 1;
					array5[num3++] = (l + 1) * num + m;
					array5[num3++] = (l + 1) * num + m + 1;
					array5[num3++] = l * num + m + 1;
				}
			}
			Mesh mesh = new Mesh();
			mesh.vertices = array;
			mesh.colors = array4;
			mesh.normals = array3;
			mesh.uv = array2;
			mesh.triangles = array5;
			mesh.RecalculateBounds();
			mesh.RecalculateNormals();
			return mesh;
		}

		public static Bounds GetBounds(GameObject go)
		{
			Bounds result = new Bounds(go.transform.position, Vector3.zero);
			foreach (Renderer renderer in go.GetComponentsInChildren<Renderer>())
			{
				result.Encapsulate(renderer.bounds);
			}
			foreach (Collider collider in go.GetComponentsInChildren<Collider>())
			{
				result.Encapsulate(collider.bounds);
			}
			return result;
		}

		private Vector3 Rotate90LeftXAxis(Vector3 input)
		{
			return new Vector3(input.x, -input.z, input.y);
		}

		private Vector3 Rotate90RightXAxis(Vector3 input)
		{
			return new Vector3(input.x, input.z, -input.y);
		}

		private Vector3 Rotate90LeftYAxis(Vector3 input)
		{
			return new Vector3(-input.z, input.y, input.x);
		}

		private Vector3 Rotate90RightYAxis(Vector3 input)
		{
			return new Vector3(input.z, input.y, -input.x);
		}

		private Vector3 Rotate90LeftZAxis(Vector3 input)
		{
			return new Vector3(input.y, -input.x, input.z);
		}

		private Vector3 Rotate90RightZAxis(Vector3 input)
		{
			return new Vector3(-input.y, input.x, input.z);
		}

		public static bool Math_ApproximatelyEqual(float a, float b, float threshold)
		{
			return a == b || Mathf.Abs(a - b) < threshold;
		}

		public static bool Math_ApproximatelyEqual(float a, float b)
		{
			return Utils.Math_ApproximatelyEqual(a, b, float.Epsilon);
		}

		public static bool Math_IsPowerOf2(int value)
		{
			return (value & value - 1) == 0;
		}

		public static float Math_Clamp(float min, float max, float value)
		{
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		public static float Math_Modulo(float value, float mod)
		{
			return value - mod * (float)Math.Floor((double)(value / mod));
		}

		public static int Math_Modulo(int value, int mod)
		{
			return (int)((float)value - (float)mod * (float)Math.Floor((double)((float)value / (float)mod)));
		}

		public static float Math_InterpolateLinear(float value1, float value2, float fraction)
		{
			return value1 * (1f - fraction) + value2 * fraction;
		}

		public static float Math_InterpolateSmooth(float value1, float value2, float fraction)
		{
			if (fraction < 0.5f)
			{
				fraction = 2f * fraction * fraction;
			}
			else
			{
				fraction = 1f - 2f * (fraction - 1f) * (fraction - 1f);
			}
			return value1 * (1f - fraction) + value2 * fraction;
		}

		public static float Math_Distance(float x1, float y1, float x2, float y2)
		{
			return Mathf.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
		}

		public static float Math_InterpolateSmooth2(float v1, float v2, float fraction)
		{
			float num = fraction * fraction;
			fraction = 3f * num - 2f * fraction * num;
			return v1 * (1f - fraction) + v2 * fraction;
		}

		public static float Math_InterpolateCubic(float v0, float v1, float v2, float v3, float fraction)
		{
			float num = v3 - v2 - (v0 - v1);
			float num2 = v0 - v1 - num;
			float num3 = v2 - v0;
			float num4 = fraction * fraction;
			return num * fraction * num4 + num2 * num4 + num3 * fraction + v1;
		}

		public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angle)
		{
			Vector3 vector = point - pivot;
			vector = Quaternion.Euler(angle) * vector;
			point = vector + pivot;
			return point;
		}

		public static string FixFileName(string sourceFileName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in sourceFileName)
			{
				if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '_')
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		public static FileStream OpenRead(string path)
		{
			return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		public static string ReadAllText(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Argument_EmptyPath");
			}
			string result;
			using (StreamReader streamReader = new StreamReader(path, Encoding.UTF8, true, 1024))
			{
				result = streamReader.ReadToEnd();
			}
			return result;
		}

		public static void WriteAllText(string path, string contents)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Argument_EmptyPath");
			}
			if (path == null)
			{
				throw new ArgumentNullException("contents");
			}
			if (contents.Length == 0)
			{
				throw new ArgumentException("Argument_EmptyContents");
			}
			using (StreamWriter streamWriter = new StreamWriter(path, false, Encoding.UTF8, 1024))
			{
				streamWriter.Write(contents);
			}
		}

		public static byte[] ReadAllBytes(string path)
		{
			byte[] result;
			using (FileStream fileStream = Utils.OpenRead(path))
			{
				long length = fileStream.Length;
				if (length > 2147483647L)
				{
					throw new IOException("Reading more than 2GB with this call is not supported");
				}
				int num = 0;
				int i = (int)length;
				byte[] array = new byte[length];
				while (i > 0)
				{
					int num2 = fileStream.Read(array, num, i);
					if (num2 == 0)
					{
						throw new IOException("Unexpected end of stream");
					}
					num += num2;
					i -= num2;
				}
				result = array;
			}
			return result;
		}

		public static void WriteAllBytes(string path, byte[] bytes)
		{
			using (Stream stream = File.Create(path))
			{
				stream.Write(bytes, 0, bytes.Length);
			}
		}

		public static void CreateAsset<T>() where T : ScriptableObject
		{
		}

		public static string GetAssetPath(UnityEngine.Object uo)
		{
			return string.Empty;
		}

		public static string WrapScriptableObject(ScriptableObject so)
		{
			return string.Empty;
		}

		public static void UnwrapScriptableObject(string path, string newpath)
		{
		}

		public static string WrapGameObjectAsPrefab(GameObject go)
		{
			return string.Empty;
		}

		public static bool IsInLayerMask(GameObject obj, LayerMask mask)
		{
			return (mask.value & 1 << obj.layer) > 0;
		}

		public static bool IsSameTexture(Texture2D tex1, Texture2D tex2, bool checkID = false)
		{
			if (tex1 == null || tex2 == null)
			{
				return false;
			}
			if (checkID)
			{
				return tex1.GetInstanceID() == tex2.GetInstanceID();
			}
			return !(tex1.name != tex2.name) && tex1.width == tex2.width && tex1.height == tex2.height;
		}

		public static bool IsSameGameObject(GameObject go1, GameObject go2, bool checkID = false)
		{
			if (go1 == null || go2 == null)
			{
				return false;
			}
			if (checkID)
			{
				return go1.GetInstanceID() == go2.GetInstanceID();
			}
			return !(go1.name != go2.name);
		}

		public static string GetAssetPath(string fileName)
		{
			return string.Empty;
		}

		public static string GetAssetPath(string name, string type)
		{
			return string.Empty;
		}

		public static GaiaSettings GetGaiaSettings()
		{
			return Utils.GetAsset("GaiaSettings.asset", typeof(GaiaSettings)) as GaiaSettings;
		}

		public static UnityEngine.Object GetAsset(string fileNameOrPath, Type assetType)
		{
			return null;
		}

		public static GameObject GetAssetPrefab(string name)
		{
			return null;
		}

		public static ScriptableObject GetAssetScriptableObject(string name)
		{
			return null;
		}

		public static Texture2D GetAssetTexture2D(string name)
		{
			return null;
		}

		public static Type GetType(string TypeName)
		{
			Type type = Type.GetType(TypeName);
			if (type != null)
			{
				return type;
			}
			if (TypeName.Contains("."))
			{
				string assemblyString = TypeName.Substring(0, TypeName.IndexOf('.'));
				try
				{
					Assembly assembly = Assembly.Load(assemblyString);
					if (assembly == null)
					{
						return null;
					}
					type = assembly.GetType(TypeName);
					if (type != null)
					{
						return type;
					}
				}
				catch (Exception)
				{
				}
			}
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			if (callingAssembly != null)
			{
				type = callingAssembly.GetType(TypeName);
				if (type != null)
				{
					return type;
				}
			}
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for (int i = 0; i < assemblies.GetLength(0); i++)
			{
				type = assemblies[i].GetType(TypeName);
				if (type != null)
				{
					return type;
				}
			}
			AssemblyName[] referencedAssemblies = callingAssembly.GetReferencedAssemblies();
			foreach (AssemblyName assemblyRef in referencedAssemblies)
			{
				Assembly assembly2 = Assembly.Load(assemblyRef);
				if (assembly2 != null)
				{
					type = assembly2.GetType(TypeName);
					if (type != null)
					{
						return type;
					}
				}
			}
			return null;
		}
	}
}
