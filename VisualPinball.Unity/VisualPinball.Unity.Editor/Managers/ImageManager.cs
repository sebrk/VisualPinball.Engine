using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VisualPinball.Unity.VPT;
using VisualPinball.Unity.VPT.Table;

namespace VisualPinball.Unity.Editor.Managers
{
	/// <summary>
	/// Editor UI for VPX images, equivalent to VPX's "Image Manager" window
	/// </summary>
	public class ImageManager : ManagerWindow<ImageListData>
	{
		protected override string DataTypeName => "Image";

		[MenuItem("Visual Pinball/Image Manager", false, 102)]
		public static void ShowWindow()
		{
			GetWindow<ImageManager>("Image Manager");
		}

		protected override void OnDataDetailGUI()
		{
			SliderField("Alpha Mask", ref _selectedItem.TextureData.AlphaTestValue, 0, 255);

			var unityTex = _table.GetTexture(_selectedItem.Name);
			var rect = GUILayoutUtility.GetRect(new GUIContent(""), GUIStyle.none);
			float aspect = (float)unityTex.height / unityTex.width;
			rect.width = Mathf.Min(unityTex.width, rect.width);
			rect.height = rect.width * aspect;
			GUI.DrawTexture(rect, unityTex);
		}

		protected override void RenameExistingItem(ImageListData listItem, string newName)
		{
			string oldName = listItem.TextureData.Name;

			// give each editable item a chance to update its fields
			string undoName = "Rename Image";
			foreach (var item in _table.GetComponentsInChildren<IEditableItemBehavior>()) {
				item.HandleTextureRenamed(undoName, oldName, newName);
			}
			Undo.RecordObject(_table, undoName);
			OnDataChanged(undoName, listItem);

			listItem.TextureData.Name = newName;
		}

		protected override void CollectData(List<ImageListData> data)
		{
			foreach (var t in _table.Textures) {
				data.Add(new ImageListData { TextureData = t });
			}
		}

		protected override void OnDataChanged(string undoName, ImageListData data)
		{
			// NOTE/TODO: adding the sidecar to the undo stack is nasty, there's a massive amount of
			// data serialized there, but that's where the texture props live. we'll have to do
			// rethink how we're storing and serializing things like the textures
			Undo.RecordObject(_table.GetComponentInChildren<TableSidecar>(), undoName);
		}
	}
}