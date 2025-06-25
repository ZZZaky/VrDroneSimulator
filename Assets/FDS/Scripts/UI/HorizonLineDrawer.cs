using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FDS.UI
{
    [RequireComponent(typeof(RawImage))]
    public class HorizonLineDrawer : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private Drone.Drone drone;

        [SerializeField]
        private float textureScale;

        [SerializeField]
        private Color lineColor = Color.white;

        [SerializeField]
        private float yOffsetScale;

        [SerializeField]
        private float maxYOffset;

        private RawImage _img;
        private Texture2D _texture;

        private int _widthPixels;
        private int _heightPixels;
        private Color _transparent = new Color(0, 0, 0, 0);

        #endregion

        #region LifeCycle

        private void Awake()
        {
            _img = GetComponent<RawImage>();
            _widthPixels = (int)(_img.rectTransform.sizeDelta.x * textureScale);
            _heightPixels = (int)(_img.rectTransform.sizeDelta.y * textureScale);
            _texture = new Texture2D(_widthPixels, _heightPixels);
            _texture.filterMode = FilterMode.Point;
            _img.texture = _texture;
        }

        private void Update()
        {
            var droneAngles = drone.ReadCurrentAngles();
            var angleTan = Mathf.Tan(-droneAngles.Roll * Mathf.Deg2Rad);
            var yOffset = (int)Mathf.Clamp(Mathf.Sin(-droneAngles.Pitch * Mathf.Deg2Rad) * yOffsetScale, -maxYOffset, maxYOffset);

            for(int x = 0; x < _widthPixels; x++)
            {
                var currentX = (x - _widthPixels / 2);
                var currentY = (int)(angleTan * currentX + _heightPixels / 2 + yOffset);
                for(int y = 0; y < _heightPixels; y++)
                {
                    if (y == currentY)
                        _texture.SetPixel(x, y, lineColor);
                    else if(y == currentY + 1 || y == currentY - 1)
                        _texture.SetPixel(x, y, Color.black);
                    else
                        _texture.SetPixel(x, y, _transparent);
                }
            }
            _texture.Apply();
        }

        #endregion
    }
}
