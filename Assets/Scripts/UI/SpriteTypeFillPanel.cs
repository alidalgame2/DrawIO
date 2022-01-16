using Sirenix.OdinInspector;
using UnityEngine;


    public class SpriteTypeFillPanel : FillPanel
    {
        public FillDirection fillDirection;
        public float testFillAmount;
        [HideIf("useNativeSize")]
        public float maxWidth;
        public float minWidth;
        public bool useNativeSize;

        private float multiplier;

        public void Start()
        {
            CalculateValues();
        }

        [Button("Set Fill")] 
        public void SetFill()
        {
            CalculateValues();
            SetFill(testFillAmount);
        }
        public override void SetFill(float fillAmount)
        {
            base.SetFill(fillAmount);
            fillAmount = Mathf.Clamp(fillAmount, 0, maxValue);
            fillAmount = (fillAmount / maxValue) * multiplier + minWidth;
            fillAmount = Mathf.Clamp(fillAmount, minWidth, maxWidth);
            
            if(fillDirection == FillDirection.Horizontal)
            {
                fillImage.rectTransform.sizeDelta = new Vector2(fillAmount, fillImage.rectTransform.sizeDelta.y);
            }
            else
            {
                fillImage.rectTransform.sizeDelta = new Vector2(fillImage.rectTransform.sizeDelta.x, fillAmount);
            }
        }
        [Button("Calculate Values")]
        private void CalculateValues()
        {
            if (useNativeSize)
            {
                if (fillDirection == FillDirection.Horizontal)
                {
                    maxWidth = fillImage.sprite.texture.width;
                }
                else
                {
                    maxWidth = fillImage.sprite.texture.height;
                }
            }
            multiplier = maxWidth - minWidth;
        }
    }

    public enum FillDirection
    {
        Horizontal,
        Vertical
    }


