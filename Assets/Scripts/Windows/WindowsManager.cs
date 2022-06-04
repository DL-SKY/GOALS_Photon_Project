using DllSky.Components.Services;
using GOALS.Windows.Enums;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GOALS.Windows
{
    public class WindowsManager : AutoLocatorComponent
    {
        [SerializeField]
        private List<WindowsLayer> _editorLayers = new List<WindowsLayer>();

        private Dictionary<EnumWindowLayer, Transform> _runtimeLayers = new Dictionary<EnumWindowLayer, Transform>();


        protected override void CustomAwake()
        {
            base.CustomAwake();

            DoCacheLayers(_editorLayers);
        }


        private void DoCacheLayers(List<WindowsLayer> list)
        {
            _runtimeLayers.Clear();
            foreach (var layer in list)
                if (!_runtimeLayers.ContainsKey(layer.key))
                    _runtimeLayers.Add(layer.key, layer.value);
        }


#if UNITY_EDITOR
        [ContextMenu("Create NA Layer Object")]
        private void CreateNewNALayerObject()
        {
            CreateNewLayerObject(EnumWindowLayer.NA, "New NA Layer");
        }

        [ContextMenu("Create ALL Default Layers")]
        private void CreateAllDefaultLayers()
        {
            var layers = new EnumWindowLayer[] 
            { 
                EnumWindowLayer.Main,
                EnumWindowLayer.Dialogs,
                EnumWindowLayer.Loading,
                EnumWindowLayer.Special,
                EnumWindowLayer.Errors,
            };

            foreach (var layer in layers)
                CreateNewLayerObject(layer, $"{layer}Layer");
        }

        private void CreateNewLayerObject(EnumWindowLayer layerType, string layerName)
        {
            var newLayer = new GameObject();
            newLayer.transform.SetParent(transform);
            newLayer.name = layerName;

            var components = new Type[] { typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster) };
            foreach (var component in components)
                newLayer.AddComponent(component);

            var presetActions = new Action<GameObject, EnumWindowLayer>[] { OnCanvasPreset, OnScalerPreset, OnRaycasterPreset };
            foreach (var preset in presetActions)
                preset.Invoke(newLayer, layerType);

            var layer = new WindowsLayer(layerType, newLayer.transform);
            AddEditorLayer(layer);
        }

        private void OnCanvasPreset(GameObject layer, EnumWindowLayer layerType)
        {
            var canvas = layer.GetComponent<Canvas>();
            if (canvas == null)
                return;

            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = (int)layerType;
        }

        private void OnScalerPreset(GameObject layer, EnumWindowLayer layerType)
        {
            var canvasScaler = layer.GetComponent<CanvasScaler>();
            if (canvasScaler == null)
                return;

            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920, 1080);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        }

        private void OnRaycasterPreset(GameObject layer, EnumWindowLayer layerType)
        {
            var graphicRaycaster = layer.GetComponent<GraphicRaycaster>();
            if (graphicRaycaster == null)
                return;
        }

        private void AddEditorLayer(WindowsLayer layer)
        {
            _editorLayers.Add(layer);
        }
#endif
    }
}
