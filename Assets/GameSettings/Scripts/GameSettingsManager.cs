using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace GameSettings
{
    public class GameSettingsManager : MonoBehaviour
    {
        #region Fields

        [SerializeField] private UniversalRenderPipelineAsset customSettingsPipelineAsset;

        [Header("Buttons")]
        [SerializeField] private SettingsButton qualityButton;
        [SerializeField] private SettingsButton textureQualityButton;
        [SerializeField] private SettingsButton shadowQualityButton;
        [SerializeField] private SettingsButton msaaQualityButton;
        [SerializeField] private SettingsButton vSyncButton;

        private bool _isCustomQualityChoosen = false;

        #endregion

        #region LifeCycle

        private void Start()
        {
            UpdateButtonValues();
        }

        #endregion

        #region PublicMethods

        public void SetQualityLevel(int qualityLevel)
        {
            QualitySettings.SetQualityLevel(qualityLevel);
            _isCustomQualityChoosen = qualityLevel == 3;
            if(qualityLevel < 3)
            {
                QualitySettings.masterTextureLimit = 2 - qualityLevel;
            }
            QualitySettings.vSyncCount = qualityLevel < 1 ? 0 : 1;
            UpdateButtonValues();
        }

        public void ChangeTextureQuality(int qualityLevel)
        {
            CustomSettingsChecker();
            QualitySettings.masterTextureLimit = 2 - qualityLevel;
        }

        public void ChangeShadowQuality(int qualityLevel)
        {
            CustomSettingsChecker();
            customSettingsPipelineAsset.shadowDistance = qualityLevel * 10;
        }

        public void EnableVSync(int value)
        {
            CustomSettingsChecker();
            QualitySettings.vSyncCount = value;
        }

        public void ChangeMSAAQuality(int qualityLevel)
        {
            CustomSettingsChecker();
            switch(qualityLevel) 
            {
                case 0:
                    customSettingsPipelineAsset.msaaSampleCount = (int)MsaaQuality.Disabled;
                    break;
                case 1:
                    customSettingsPipelineAsset.msaaSampleCount = (int)MsaaQuality._2x;
                    break;
                case 2:
                    customSettingsPipelineAsset.msaaSampleCount = (int)MsaaQuality._4x;
                    break;
                case 3:
                    customSettingsPipelineAsset.msaaSampleCount = (int)MsaaQuality._8x;
                    break;
            }
        }

        #endregion

        #region PrivateMethods

        private void CustomSettingsChecker()
        {
            if (!_isCustomQualityChoosen)
            {
                QualitySettings.SetQualityLevel(3);
                qualityButton.SetParameterIndex(3);
                _isCustomQualityChoosen = true;
            }
        }

        private void UpdateButtonValues()
        {
            qualityButton.SetParameterIndex(QualitySettings.GetQualityLevel());
            textureQualityButton.SetParameterIndex(GetTextureQualityIndex());
            UniversalRenderPipelineAsset currentPiplineAsset = (UniversalRenderPipelineAsset)QualitySettings.renderPipeline;
            shadowQualityButton.SetParameterIndex((int)(currentPiplineAsset.shadowDistance / 10));
            msaaQualityButton.SetParameterIndex(GetMSAAIndex(currentPiplineAsset));
            vSyncButton.SetParameterIndex(QualitySettings.vSyncCount);
        }

        private int GetMSAAIndex(UniversalRenderPipelineAsset currentPipeline)
        {
            return (int)Mathf.Log(currentPipeline.msaaSampleCount, 2);
        }

        private int GetTextureQualityIndex()
        {
            return 2 - QualitySettings.masterTextureLimit;
        }

        #endregion

    }
}

