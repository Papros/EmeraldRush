using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EmeraldRush.Model.SettingsManager
{
    class SettingManager
    {
        public SettingManager()
        {

        }
    
        public void UpdateValue(SettingsKey key, object value)
        {
            Application.Current.Properties[key.ToString()] = value;
        }

        public T GetValue<T>(SettingsKey key, T defaultValue )
        {
            try
            {
                return (T)Application.Current.Properties[key.ToString()];

            }catch(Exception)
            {
                return defaultValue;
            }
        }
    }
}
