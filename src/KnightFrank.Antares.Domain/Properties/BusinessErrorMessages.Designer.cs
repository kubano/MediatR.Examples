﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KnightFrank.Antares.Domain.Properties {
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
    internal class BusinessErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal BusinessErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("KnightFrank.Antares.Domain.Properties.BusinessErrorMessages", typeof(BusinessErrorMessages).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} with given id &apos;{1}&apos; does not exist in the database..
        /// </summary>
        internal static string Entity_Not_Exists {
            get {
                return ResourceManager.GetString("Entity_Not_Exists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Enum type {0} with given item id &apos;{1}&apos; does not exist in the database..
        /// </summary>
        internal static string EnumType_Item_Not_Exists {
            get {
                return ResourceManager.GetString("EnumType_Item_Not_Exists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Inconsistent address country with address form definition..
        /// </summary>
        internal static string Inconsistent_Address_Country_Id {
            get {
                return ResourceManager.GetString("Inconsistent_Address_Country_Id", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} configuration is inconsistent..
        /// </summary>
        internal static string Inconsistent_Dynamic_Configuration {
            get {
                return ResourceManager.GetString("Inconsistent_Dynamic_Configuration", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to One or more attendees are not on the applicant list..
        /// </summary>
        internal static string Missing_Applicant_Id {
            get {
                return ResourceManager.GetString("Missing_Applicant_Id", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The &apos;{0}&apos; property value format is inappropriate..
        /// </summary>
        internal static string Property_Format_Is_Invalid {
            get {
                return ResourceManager.GetString("Property_Format_Is_Invalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The &apos;{0}&apos; property value should be empty..
        /// </summary>
        internal static string Property_Should_Be_Empty {
            get {
                return ResourceManager.GetString("Property_Should_Be_Empty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The &apos;{0}&apos; property value is required..
        /// </summary>
        internal static string Property_Should_Not_Be_Empty {
            get {
                return ResourceManager.GetString("Property_Should_Not_Be_Empty", resourceCulture);
            }
        }
    }
}
