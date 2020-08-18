using UnityEditor;
using UnityEngine;

namespace VisualPinball.Unity.Editor.Inspectors
{
	[CustomEditor(typeof(PinMameAuthoring))]
	[CanEditMultipleObjects]
	public class PinMameInspector : UnityEditor.Editor
	{
		private PinMameAuthoring _pinMameAuthoring;

		private void OnEnable()
		{
			_pinMameAuthoring = (PinMameAuthoring) target;
			_pinMameAuthoring.Init();
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("Start Game")) {
				_pinMameAuthoring.StartGame();
			}

			if (GUILayout.Button("Stop Game")) {
				_pinMameAuthoring.PinMame.StopGame();
			}
			EditorGUILayout.EndHorizontal();
		}
	}
}
