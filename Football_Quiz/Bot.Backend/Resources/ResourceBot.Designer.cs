﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bot.Backend.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ResourceBot {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResourceBot() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Bot.Backend.Resources.ResourceBot", typeof(ResourceBot).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Добро пожаловать в футбольную викторину.
        /// </summary>
        public static string HelloString {
            get {
                return ResourceManager.GetString("HelloString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Начальное меню.
        /// </summary>
        public static string MainMenu {
            get {
                return ResourceManager.GetString("MainMenu", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Начать игру.
        /// </summary>
        public static string StartGame {
            get {
                return ResourceManager.GetString("StartGame", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Позиция в рейтинге.
        /// </summary>
        public static string Statistics {
            get {
                return ResourceManager.GetString("Statistics", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Тематическая игра.
        /// </summary>
        public static string ThematicGame {
            get {
                return ResourceManager.GetString("ThematicGame", resourceCulture);
            }
        }
    }
}