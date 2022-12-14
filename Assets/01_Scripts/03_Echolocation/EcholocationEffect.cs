using UnityEngine;
using Sirenix.OdinInspector;
using Unity.VisualScripting;


public class EcholocationEffect : MonoBehaviour
{
	private Vector3 ScannerOrigin;
	public Material EffectMaterial;
	private Color Mid= Color.white;
	private Color Trail=Color.white;
	private float alpha;
	public float ScanDistance;
	public bool OverrideDefaultValues;
	
	[ShowIf("@this.OverrideDefaultValues")]public float MaxDistance;
	[ShowIf("@this.OverrideDefaultValues")]public float Speed;
	[ShowIf("@this.OverrideDefaultValues")]public float StartFadingPoint;
	private Camera _camera;
	bool _scanning;

	private void Awake()
	{
		ResetEcholocation();
		if (!OverrideDefaultValues)
		{
			MaxDistance = EchoDefaultValues.current.MaxDistance;
			Speed = EchoDefaultValues.current.Speed;
			StartFadingPoint = EchoDefaultValues.current.StartFadingPoint;
		}
	}

	void Update()
	{

		alpha = ScriptsTools.MapValues(ScanDistance, StartFadingPoint, MaxDistance, 1, 0);
		Mid = new Color(alpha, alpha, alpha,0);
		Trail = new Color(alpha, alpha, alpha,0);
		
		if (ScanDistance > MaxDistance)
		{
			ResetEcholocation();
		}
		
		if(_scanning)
		{
			ScanDistance += Time.deltaTime * Speed;
		}
	}

	public void ResetEcholocation()
	{
		_scanning = false;
		ScanDistance = 0;
	}

	[Button]
	public void TriggerEcholocation(Vector3 position)
	{
		ScannerOrigin = position;
		_scanning = true;
		ScanDistance = 0;
	}
	// End Demo Code

	void OnEnable()
	{
		_camera = Camera.main;
		_camera.depthTextureMode = DepthTextureMode.Depth;
	}

	[ImageEffectOpaque]
	public void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		EffectMaterial.SetVector("_WorldSpaceScannerPos", ScannerOrigin);
		EffectMaterial.SetFloat("_ScanDistance", ScanDistance);
		EffectMaterial.SetVector("_MidColor", Mid);
		EffectMaterial.SetVector("_TrailColor", Trail);
		RaycastCornerBlit(src, dst, EffectMaterial);
	}

	void RaycastCornerBlit(RenderTexture source, RenderTexture dest, Material mat)
	{
		// Compute Frustum Corners
		float camFar = _camera.farClipPlane;
		float camFov = _camera.fieldOfView;
		float camAspect = _camera.aspect;

		float fovWHalf = camFov * 0.5f;

		Vector3 toRight = _camera.transform.right * Mathf.Tan(fovWHalf * Mathf.Deg2Rad) * camAspect;
		Vector3 toTop = _camera.transform.up * Mathf.Tan(fovWHalf * Mathf.Deg2Rad);

		Vector3 topLeft = (_camera.transform.forward - toRight + toTop);
		float camScale = topLeft.magnitude * camFar;

		topLeft.Normalize();
		topLeft *= camScale;

		Vector3 topRight = (_camera.transform.forward + toRight + toTop);
		topRight.Normalize();
		topRight *= camScale;

		Vector3 bottomRight = (_camera.transform.forward + toRight - toTop);
		bottomRight.Normalize();
		bottomRight *= camScale;

		Vector3 bottomLeft = (_camera.transform.forward - toRight - toTop);
		bottomLeft.Normalize();
		bottomLeft *= camScale;

		// Custom Blit, encoding Frustum Corners as additional Texture Coordinates
		RenderTexture.active = dest;

		mat.SetTexture("_MainTex", source);

		GL.PushMatrix();
		GL.LoadOrtho();

		mat.SetPass(0);

		GL.Begin(GL.QUADS);

		GL.MultiTexCoord2(0, 0.0f, 0.0f);
		GL.MultiTexCoord(1, bottomLeft);
		GL.Vertex3(0.0f, 0.0f, 0.0f);

		GL.MultiTexCoord2(0, 1.0f, 0.0f);
		GL.MultiTexCoord(1, bottomRight);
		GL.Vertex3(1.0f, 0.0f, 0.0f);

		GL.MultiTexCoord2(0, 1.0f, 1.0f);
		GL.MultiTexCoord(1, topRight);
		GL.Vertex3(1.0f, 1.0f, 0.0f);

		GL.MultiTexCoord2(0, 0.0f, 1.0f);
		GL.MultiTexCoord(1, topLeft);
		GL.Vertex3(0.0f, 1.0f, 0.0f);

		GL.End();
		GL.PopMatrix();
	}
}
