using EmeraldRush.Model.SettingsManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.ViewModels.Menu
{
    class SettingsViewModel : BaseViewModel
    {
        private string adventurerName;
        public string AdventurerName
        {
            get { return adventurerName; }
            set { SetProperty(ref adventurerName, value); settingsManager?.UpdateValue(SettingsKey.NAME, value); }
        }

        private bool decisionBoxDisapears;
        public bool DecisionBoxDisapears
        {
            get { return decisionBoxDisapears; }
            set { SetProperty(ref decisionBoxDisapears, value); settingsManager?.UpdateValue(SettingsKey.DECISION_BOX, value); }
        }

        private bool vibration;
        public bool Vibration
        {
            get { return vibration; }
            set { SetProperty(ref vibration, value); settingsManager?.UpdateValue(SettingsKey.VIBRATION, value); }
        }

        private int musicLevel;
        public int MusicLevel
        {
            get {
                return musicLevel;
            }
            set {
                MusicLevelLabel = (musicLevel > 0 ? musicLevel.ToString() : "MUSIC OFF");
                SetProperty(ref musicLevel, value);
                settingsManager?.UpdateValue(SettingsKey.MUSIC_LEVEL, value);
            }
        }

        private string musicLevelLabel;
        public string MusicLevelLabel
        {
            get { return musicLevelLabel; }
            set { SetProperty(ref musicLevelLabel, value); }
        }

        public double MinMusicValue = 0;
        public double MaxMusicValue = 5;


        private SettingManager settingsManager;

        public SettingsViewModel()
        {
            settingsManager = new SettingManager();
        }

        public void LoadData()
        {
            this.MusicLevel = settingsManager.GetValue(SettingsKey.MUSIC_LEVEL, 3);
            this.Vibration = settingsManager.GetValue(SettingsKey.VIBRATION, true);
            this.DecisionBoxDisapears = settingsManager.GetValue(SettingsKey.DECISION_BOX, false);
            this.AdventurerName = settingsManager.GetValue(SettingsKey.NAME, "Adventurer");
        }

        public void SaveData()
        {
            settingsManager.UpdateValue(SettingsKey.MUSIC_LEVEL, MusicLevel);
            settingsManager.UpdateValue(SettingsKey.NAME, AdventurerName);
            settingsManager.UpdateValue(SettingsKey.VIBRATION, Vibration);
            settingsManager.UpdateValue(SettingsKey.DECISION_BOX, DecisionBoxDisapears);
        }

    }
}
