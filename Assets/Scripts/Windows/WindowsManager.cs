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
        public Action<WindowTemplate> OnShowWindow;
        public Action<WindowTemplate, bool> OnCloseWindow;

        [SerializeField]
        private List<WindowsLayer> _editorLayers = new List<WindowsLayer>();

        private Dictionary<EnumWindowLayer, Transform> _runtimeLayers = new Dictionary<EnumWindowLayer, Transform>();
        private List<WindowTemplate> _windows = new List<WindowTemplate>();


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                GetLastWindow()?.OnClickEsc();
        }


        public T ShowWindow<T>(string pathPrefab, EnumWindowLayer windowLayer, object data = null, bool includeInWindowsList = true) where T : WindowTemplate
        {
            var prefab = Resources.Load<GameObject>(string.Format(pathPrefab));
            var layer = _runtimeLayers.ContainsKey(windowLayer) ? _runtimeLayers[windowLayer] : transform;
            var window = Instantiate(prefab, layer).GetComponent<T>();

            window.OnClose += OnCloseHandler;
            window.Initialize(data);

            if (includeInWindowsList)
                _windows.Add(window);

            OnShowWindow?.Invoke(window);

            return window;
        }

        public WindowTemplate GetLastWindow()
        {
            if (_windows.Count < 1)
                return null;

            return _windows[_windows.Count - 1];
        }


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

        private void OnCloseHandler(WindowTemplate window, bool result)
        {
            window.OnClose -= OnCloseHandler;

            if (_windows.Contains(window))
                _windows.Remove(window);

            OnCloseWindow?.Invoke(window, result);
        }


#if UNITY_EDITOR
        #region EditorOnly
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
        #endregion
#endif
    }
}
