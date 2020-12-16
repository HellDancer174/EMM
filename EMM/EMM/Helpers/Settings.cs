using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace EMM.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        #endregion


        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault("Password", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Password", value);
            }
        }

        public static string Username
        {
            get
            {
                return AppSettings.GetValueOrDefault("Username", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Username", value);
            }
        }

        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault("AccessToken", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("AccessToken", value);
            }
        }
        public static DateTime DefaultDateTime
        {
            get
            {
                return AppSettings.GetValueOrDefault("DefaultDateTime", new DateTime(1837, 11, 11, 00, 00, 00));
            }
        }

        public static IEnumerable<string> StationsNames { get; set; }

        public static IDictionary<string, int> LocomotivesTypes { get; set; }
        public static DateTime AccessTokenExpires
        {
            get
            {
                return AppSettings.GetValueOrDefault("AccessTokenExpires", new DateTime(1837, 11, 11, 00, 00, 00));
            }
            set
            {
                AppSettings.AddOrUpdateValue("AccessTokenExpires", value);
            }
        }
        public static string Position
        {
            get
            {
                return AppSettings.GetValueOrDefault("Position", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Position", value);
            }

        }
        public static int QualificationСlass
        {
            get
            {
                return AppSettings.GetValueOrDefault("QualificationСlass", -1);
            }
            set
            {
                AppSettings.AddOrUpdateValue("QualificationСlass", value);
            }

        }
        public static bool Combination
        {
            get
            {
                return AppSettings.GetValueOrDefault("Combination", false);
            }
            set
            {
                AppSettings.AddOrUpdateValue("Combination", value);
            }

        }
        public static IDictionary<string, double> PositionRates { get; set; }

        public static double Rate
        {
            get
            {
                return AppSettings.GetValueOrDefault("Rate", 0d);
            }
            set
            {
                AppSettings.AddOrUpdateValue("Rate", value);
            }
        }


    }
}
