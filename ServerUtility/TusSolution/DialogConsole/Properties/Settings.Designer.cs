﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DialogConsole.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Users\\Administrator\\ProgSync\\console_page\\2015fes_blocks.yaml")]
        public string SheetPath {
            get {
                return ((string)(this["SheetPath"]));
            }
            set {
                this["SheetPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("192.168.2.0")]
        public string IpSegment {
            get {
                return ((string)(this["IpSegment"]));
            }
            set {
                this["IpSegment"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("255.255.255.0")]
        public string IpMask {
            get {
                return ((string)(this["IpMask"]));
            }
            set {
                this["IpMask"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("8000")]
        public int IpPort {
            get {
                return ((int)(this["IpPort"]));
            }
            set {
                this["IpPort"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("(9, 0, 0)")]
        public string ParentDeviceID {
            get {
                return ((string)(this["ParentDeviceID"]));
            }
            set {
                this["ParentDeviceID"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Users\\Administrator\\ProgSync\\console_page\\2015fes_routes.yaml")]
        public string RoutePath {
            get {
                return ((string)(this["RoutePath"]));
            }
            set {
                this["RoutePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Users\\Administrator\\ProgSync\\console_page\\2014fes_illuminate.yaml")]
        public string IlluminationPath {
            get {
                return ((string)(this["IlluminationPath"]));
            }
            set {
                this["IlluminationPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://+:8012/")]
        public string http {
            get {
                return ((string)(this["http"]));
            }
            set {
                this["http"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Users\\Administrator\\ProgSync\\console_page\\test_vehicles.yaml")]
        public string VehicleGroupPath {
            get {
                return ((string)(this["VehicleGroupPath"]));
            }
            set {
                this["VehicleGroupPath"] = value;
            }
        }
    }
}
