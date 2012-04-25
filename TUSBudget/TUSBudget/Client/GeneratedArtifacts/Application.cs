//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LightSwitchApplication
{
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "10.0.0.0")]
    public sealed partial class Application
        : global::Microsoft.LightSwitch.Framework.Client.ClientApplication<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass, global::LightSwitchApplication.DataWorkspace>
    {
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Application(global::Microsoft.LightSwitch.Model.IApplicationDefinition applicationDefinition) : base(applicationDefinition)
        {
            global::LightSwitchApplication.Application.DetailsClass.Initialize(this);
        }

        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void Application_Initialize();
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void Application_LoggedIn();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static new global::LightSwitchApplication.Application Current
        {
            get
            {
                return (global::LightSwitchApplication.Application)global::Microsoft.LightSwitch.Framework.Client.ClientApplication<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Current;
            }
        }

        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void NeededPartsSetListDetail_CanRun(ref bool result);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void NeededPartsSetListDetail_Run(ref bool handled);
    
        /// <summary>
        /// Opens the ShowNeededPartsSetListDetail screen.  If the screen is already opened, it is activated and shown.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "10.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void ShowNeededPartsSetListDetail()
        {
            ((global::Microsoft.LightSwitch.Details.Client.IClientApplicationDetails)this.Details).InvokeMethod(this.Details.Methods.ShowNeededPartsSetListDetail);
        }
        
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void AvailablePartsSetListDetail_CanRun(ref bool result);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void AvailablePartsSetListDetail_Run(ref bool handled);
    
        /// <summary>
        /// Opens the ShowAvailablePartsSetListDetail screen.  If the screen is already opened, it is activated and shown.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "10.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void ShowAvailablePartsSetListDetail()
        {
            ((global::Microsoft.LightSwitch.Details.Client.IClientApplicationDetails)this.Details).InvokeMethod(this.Details.Methods.ShowAvailablePartsSetListDetail);
        }
        
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void StoreInfoSetListDetail_CanRun(ref bool result);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void StoreInfoSetListDetail_Run(ref bool handled);
    
        /// <summary>
        /// Opens the ShowStoreInfoSetListDetail screen.  If the screen is already opened, it is activated and shown.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "10.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void ShowStoreInfoSetListDetail()
        {
            ((global::Microsoft.LightSwitch.Details.Client.IClientApplicationDetails)this.Details).InvokeMethod(this.Details.Methods.ShowStoreInfoSetListDetail);
        }
        
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void NeededBoardsSetListDetail_CanRun(ref bool result);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void NeededBoardsSetListDetail_Run(ref bool handled);
    
        /// <summary>
        /// Opens the ShowNeededBoardsSetListDetail screen.  If the screen is already opened, it is activated and shown.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "10.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void ShowNeededBoardsSetListDetail()
        {
            ((global::Microsoft.LightSwitch.Details.Client.IClientApplicationDetails)this.Details).InvokeMethod(this.Details.Methods.ShowNeededBoardsSetListDetail);
        }
        
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "10.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public sealed class DetailsClass
            : global::Microsoft.LightSwitch.Details.Framework.Client.ClientApplicationDetails<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass, global::LightSwitchApplication.Application.DetailsClass.PropertySet, global::LightSwitchApplication.Application.DetailsClass.CommandSet, global::LightSwitchApplication.Application.DetailsClass.MethodSet>
        {

            static DetailsClass()
            {
                var initializeCommandEntry = global::LightSwitchApplication.Application.DetailsClass.CommandSetProperties.ShowNeededPartsSetListDetail;
                var initializeMethodEntry = global::LightSwitchApplication.Application.DetailsClass.MethodSetProperties.ShowNeededPartsSetListDetail;
            }

            [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
            private static readonly global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationDetails<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Entry
                __ApplicationEntry = new global::Microsoft.LightSwitch.Details.Framework.Client.ClientApplicationDetails<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Entry(
                    global::LightSwitchApplication.Application.DetailsClass.__Application_Initialized,
                    global::LightSwitchApplication.Application.DetailsClass.__Application_LoggedIn);
            private static void __Application_Initialized(global::LightSwitchApplication.Application a)
            {
                a.Application_Initialize();
            }
            private static void __Application_LoggedIn(global::LightSwitchApplication.Application a)
            {
                a.Application_LoggedIn();
            }

            public DetailsClass() : base()
            {
            }

            public new global::LightSwitchApplication.Application.DetailsClass.PropertySet Properties
            {
                get
                {
                    return base.Properties;
                }
            }

            public new global::LightSwitchApplication.Application.DetailsClass.CommandSet Commands
            {
                get
                {
                    return base.Commands;
                }
            }

            public new global::LightSwitchApplication.Application.DetailsClass.MethodSet Methods
            {
                get
                {
                    return base.Methods;
                }
            }

            protected override global::Microsoft.LightSwitch.Client.IScreenObject CreateScreen(string screenName, params object[] args)
            {
                switch (screenName)
                {
                    case "NeededPartsSetListDetail":
                        return global::LightSwitchApplication.NeededPartsSetListDetail.CreateInstance();
                    case "AvailablePartsSetListDetail":
                        return global::LightSwitchApplication.AvailablePartsSetListDetail.CreateInstance();
                    case "StoreInfoSetListDetail":
                        return global::LightSwitchApplication.StoreInfoSetListDetail.CreateInstance();
                    case "NeededBoardsSetListDetail":
                        return global::LightSwitchApplication.NeededBoardsSetListDetail.CreateInstance();
                }
            
                return base.CreateScreen(screenName, args);
            }
            
            [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "10.0.0.0")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public sealed class PropertySet
                : global::Microsoft.LightSwitch.Details.Framework.Client.ClientApplicationPropertySet<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>
            {

            }

            [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "10.0.0.0")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public sealed class CommandSet
                : global::Microsoft.LightSwitch.Details.Framework.Client.ClientApplicationCommandSet<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>
            {

                public global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationCommand<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass> ShowNeededPartsSetListDetail
                {
                    get
                    {
                        return (global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationCommand<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>)
                               base.GetItem(global::LightSwitchApplication.Application.DetailsClass.CommandSetProperties.ShowNeededPartsSetListDetail);
                    }
                }

                public global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationCommand<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass> ShowAvailablePartsSetListDetail
                {
                    get
                    {
                        return (global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationCommand<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>)
                               base.GetItem(global::LightSwitchApplication.Application.DetailsClass.CommandSetProperties.ShowAvailablePartsSetListDetail);
                    }
                }

                public global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationCommand<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass> ShowStoreInfoSetListDetail
                {
                    get
                    {
                        return (global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationCommand<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>)
                               base.GetItem(global::LightSwitchApplication.Application.DetailsClass.CommandSetProperties.ShowStoreInfoSetListDetail);
                    }
                }

                public global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationCommand<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass> ShowNeededBoardsSetListDetail
                {
                    get
                    {
                        return (global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationCommand<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>)
                               base.GetItem(global::LightSwitchApplication.Application.DetailsClass.CommandSetProperties.ShowNeededBoardsSetListDetail);
                    }
                }

            }

            [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "10.0.0.0")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public sealed class MethodSet
                : global::Microsoft.LightSwitch.Details.Framework.Client.ClientApplicationMethodSet<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>
            {

                public global::Microsoft.LightSwitch.Details.Framework.ApplicationMethod<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass> ShowNeededPartsSetListDetail
                {
                    get
                    {
                        return (global::Microsoft.LightSwitch.Details.Framework.ApplicationMethod<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>)
                               base.GetItem(global::LightSwitchApplication.Application.DetailsClass.MethodSetProperties.ShowNeededPartsSetListDetail);
                    }
                }

                public global::Microsoft.LightSwitch.Details.Framework.ApplicationMethod<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass> ShowAvailablePartsSetListDetail
                {
                    get
                    {
                        return (global::Microsoft.LightSwitch.Details.Framework.ApplicationMethod<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>)
                               base.GetItem(global::LightSwitchApplication.Application.DetailsClass.MethodSetProperties.ShowAvailablePartsSetListDetail);
                    }
                }

                public global::Microsoft.LightSwitch.Details.Framework.ApplicationMethod<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass> ShowStoreInfoSetListDetail
                {
                    get
                    {
                        return (global::Microsoft.LightSwitch.Details.Framework.ApplicationMethod<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>)
                               base.GetItem(global::LightSwitchApplication.Application.DetailsClass.MethodSetProperties.ShowStoreInfoSetListDetail);
                    }
                }

                public global::Microsoft.LightSwitch.Details.Framework.ApplicationMethod<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass> ShowNeededBoardsSetListDetail
                {
                    get
                    {
                        return (global::Microsoft.LightSwitch.Details.Framework.ApplicationMethod<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>)
                               base.GetItem(global::LightSwitchApplication.Application.DetailsClass.MethodSetProperties.ShowNeededBoardsSetListDetail);
                    }
                }

            }

            [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "10.0.0.0")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal sealed class PropertySetProperties
            {

            }

            [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "10.0.0.0")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal sealed class CommandSetProperties
            {

                public static readonly global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationCommand<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Entry
                    ShowNeededPartsSetListDetail = new global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationCommand<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Entry(
                        "ShowNeededPartsSetListDetail",
                        global::LightSwitchApplication.Application.DetailsClass.CommandSetProperties._ShowNeededPartsSetListDetail_Stub,
                        global::LightSwitchApplication.Application.DetailsClass.CommandSetProperties._ShowNeededPartsSetListDetail_CreateExecutableObject);
                private static void _ShowNeededPartsSetListDetail_Stub(global::Microsoft.LightSwitch.Details.Framework.Base.DetailsCallback<global::LightSwitchApplication.Application.DetailsClass, global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationCommand<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Data> c, global::LightSwitchApplication.Application.DetailsClass d, object sf)
                {
                    c(d, ref d._ShowNeededPartsSetListDetailCommand, sf);
                }
                private static global::Microsoft.LightSwitch.IExecutable _ShowNeededPartsSetListDetail_CreateExecutableObject(global::LightSwitchApplication.Application.DetailsClass d)
                {
                    return ((global::LightSwitchApplication.Application.DetailsClass)d).Methods.ShowNeededPartsSetListDetail.CreateInvocation(new object[0]);
                }

                public static readonly global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationCommand<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Entry
                    ShowAvailablePartsSetListDetail = new global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationCommand<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Entry(
                        "ShowAvailablePartsSetListDetail",
                        global::LightSwitchApplication.Application.DetailsClass.CommandSetProperties._ShowAvailablePartsSetListDetail_Stub,
                        global::LightSwitchApplication.Application.DetailsClass.CommandSetProperties._ShowAvailablePartsSetListDetail_CreateExecutableObject);
                private static void _ShowAvailablePartsSetListDetail_Stub(global::Microsoft.LightSwitch.Details.Framework.Base.DetailsCallback<global::LightSwitchApplication.Application.DetailsClass, global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationCommand<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Data> c, global::LightSwitchApplication.Application.DetailsClass d, object sf)
                {
                    c(d, ref d._ShowAvailablePartsSetListDetailCommand, sf);
                }
                private static global::Microsoft.LightSwitch.IExecutable _ShowAvailablePartsSetListDetail_CreateExecutableObject(global::LightSwitchApplication.Application.DetailsClass d)
                {
                    return ((global::LightSwitchApplication.Application.DetailsClass)d).Methods.ShowAvailablePartsSetListDetail.CreateInvocation(new object[0]);
                }

                public static readonly global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationCommand<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Entry
                    ShowStoreInfoSetListDetail = new global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationCommand<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Entry(
                        "ShowStoreInfoSetListDetail",
                        global::LightSwitchApplication.Application.DetailsClass.CommandSetProperties._ShowStoreInfoSetListDetail_Stub,
                        global::LightSwitchApplication.Application.DetailsClass.CommandSetProperties._ShowStoreInfoSetListDetail_CreateExecutableObject);
                private static void _ShowStoreInfoSetListDetail_Stub(global::Microsoft.LightSwitch.Details.Framework.Base.DetailsCallback<global::LightSwitchApplication.Application.DetailsClass, global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationCommand<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Data> c, global::LightSwitchApplication.Application.DetailsClass d, object sf)
                {
                    c(d, ref d._ShowStoreInfoSetListDetailCommand, sf);
                }
                private static global::Microsoft.LightSwitch.IExecutable _ShowStoreInfoSetListDetail_CreateExecutableObject(global::LightSwitchApplication.Application.DetailsClass d)
                {
                    return ((global::LightSwitchApplication.Application.DetailsClass)d).Methods.ShowStoreInfoSetListDetail.CreateInvocation(new object[0]);
                }

                public static readonly global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationCommand<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Entry
                    ShowNeededBoardsSetListDetail = new global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationCommand<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Entry(
                        "ShowNeededBoardsSetListDetail",
                        global::LightSwitchApplication.Application.DetailsClass.CommandSetProperties._ShowNeededBoardsSetListDetail_Stub,
                        global::LightSwitchApplication.Application.DetailsClass.CommandSetProperties._ShowNeededBoardsSetListDetail_CreateExecutableObject);
                private static void _ShowNeededBoardsSetListDetail_Stub(global::Microsoft.LightSwitch.Details.Framework.Base.DetailsCallback<global::LightSwitchApplication.Application.DetailsClass, global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationCommand<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Data> c, global::LightSwitchApplication.Application.DetailsClass d, object sf)
                {
                    c(d, ref d._ShowNeededBoardsSetListDetailCommand, sf);
                }
                private static global::Microsoft.LightSwitch.IExecutable _ShowNeededBoardsSetListDetail_CreateExecutableObject(global::LightSwitchApplication.Application.DetailsClass d)
                {
                    return ((global::LightSwitchApplication.Application.DetailsClass)d).Methods.ShowNeededBoardsSetListDetail.CreateInvocation(new object[0]);
                }

            }

            [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "10.0.0.0")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal sealed class MethodSetProperties
            {

                public static readonly global::Microsoft.LightSwitch.Details.Framework.ApplicationMethod<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Entry
                    ShowNeededPartsSetListDetail = new global::Microsoft.LightSwitch.Details.Framework.ApplicationMethod<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Entry(
                        "ShowNeededPartsSetListDetail",
                        global::LightSwitchApplication.Application.DetailsClass.MethodSetProperties._ShowNeededPartsSetListDetail_Stub,
                        global::LightSwitchApplication.Application.DetailsClass.MethodSetProperties._ShowNeededPartsSetListDetail_CanInvoke,
                        global::LightSwitchApplication.Application.DetailsClass.MethodSetProperties._ShowNeededPartsSetListDetail_InvokeMethod);
                private static void _ShowNeededPartsSetListDetail_Stub(global::Microsoft.LightSwitch.Details.Framework.Base.DetailsCallback<global::LightSwitchApplication.Application.DetailsClass, global::Microsoft.LightSwitch.Details.Framework.ApplicationMethod<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Data> c, global::LightSwitchApplication.Application.DetailsClass d, object sf)
                {
                    c(d, ref d._ShowNeededPartsSetListDetailMethod, sf);
                }
                private static global::System.Exception _ShowNeededPartsSetListDetail_CanInvoke(global::LightSwitchApplication.Application.DetailsClass d, global::System.Collections.ObjectModel.ReadOnlyCollection<object> args, global::System.Exception ex)
                {
                    bool result = true;
                    d.Application.NeededPartsSetListDetail_CanRun(ref result);
                    return result ? null : ex;
                }
                private static void _ShowNeededPartsSetListDetail_InvokeMethod(global::LightSwitchApplication.Application.DetailsClass d, global::System.Collections.ObjectModel.ReadOnlyCollection<object> args)
                {
                    bool handled = false;
                    d.Application.NeededPartsSetListDetail_Run(ref handled);
                    if (!handled)
                    {
                        d.ShowScreen("LightSwitchApplication:NeededPartsSetListDetail", () => global::LightSwitchApplication.NeededPartsSetListDetail.CreateInstance(), args);
                    }
                }
 
                public static readonly global::Microsoft.LightSwitch.Details.Framework.ApplicationMethod<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Entry
                    ShowAvailablePartsSetListDetail = new global::Microsoft.LightSwitch.Details.Framework.ApplicationMethod<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Entry(
                        "ShowAvailablePartsSetListDetail",
                        global::LightSwitchApplication.Application.DetailsClass.MethodSetProperties._ShowAvailablePartsSetListDetail_Stub,
                        global::LightSwitchApplication.Application.DetailsClass.MethodSetProperties._ShowAvailablePartsSetListDetail_CanInvoke,
                        global::LightSwitchApplication.Application.DetailsClass.MethodSetProperties._ShowAvailablePartsSetListDetail_InvokeMethod);
                private static void _ShowAvailablePartsSetListDetail_Stub(global::Microsoft.LightSwitch.Details.Framework.Base.DetailsCallback<global::LightSwitchApplication.Application.DetailsClass, global::Microsoft.LightSwitch.Details.Framework.ApplicationMethod<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Data> c, global::LightSwitchApplication.Application.DetailsClass d, object sf)
                {
                    c(d, ref d._ShowAvailablePartsSetListDetailMethod, sf);
                }
                private static global::System.Exception _ShowAvailablePartsSetListDetail_CanInvoke(global::LightSwitchApplication.Application.DetailsClass d, global::System.Collections.ObjectModel.ReadOnlyCollection<object> args, global::System.Exception ex)
                {
                    bool result = true;
                    d.Application.AvailablePartsSetListDetail_CanRun(ref result);
                    return result ? null : ex;
                }
                private static void _ShowAvailablePartsSetListDetail_InvokeMethod(global::LightSwitchApplication.Application.DetailsClass d, global::System.Collections.ObjectModel.ReadOnlyCollection<object> args)
                {
                    bool handled = false;
                    d.Application.AvailablePartsSetListDetail_Run(ref handled);
                    if (!handled)
                    {
                        d.ShowScreen("LightSwitchApplication:AvailablePartsSetListDetail", () => global::LightSwitchApplication.AvailablePartsSetListDetail.CreateInstance(), args);
                    }
                }
 
                public static readonly global::Microsoft.LightSwitch.Details.Framework.ApplicationMethod<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Entry
                    ShowStoreInfoSetListDetail = new global::Microsoft.LightSwitch.Details.Framework.ApplicationMethod<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Entry(
                        "ShowStoreInfoSetListDetail",
                        global::LightSwitchApplication.Application.DetailsClass.MethodSetProperties._ShowStoreInfoSetListDetail_Stub,
                        global::LightSwitchApplication.Application.DetailsClass.MethodSetProperties._ShowStoreInfoSetListDetail_CanInvoke,
                        global::LightSwitchApplication.Application.DetailsClass.MethodSetProperties._ShowStoreInfoSetListDetail_InvokeMethod);
                private static void _ShowStoreInfoSetListDetail_Stub(global::Microsoft.LightSwitch.Details.Framework.Base.DetailsCallback<global::LightSwitchApplication.Application.DetailsClass, global::Microsoft.LightSwitch.Details.Framework.ApplicationMethod<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Data> c, global::LightSwitchApplication.Application.DetailsClass d, object sf)
                {
                    c(d, ref d._ShowStoreInfoSetListDetailMethod, sf);
                }
                private static global::System.Exception _ShowStoreInfoSetListDetail_CanInvoke(global::LightSwitchApplication.Application.DetailsClass d, global::System.Collections.ObjectModel.ReadOnlyCollection<object> args, global::System.Exception ex)
                {
                    bool result = true;
                    d.Application.StoreInfoSetListDetail_CanRun(ref result);
                    return result ? null : ex;
                }
                private static void _ShowStoreInfoSetListDetail_InvokeMethod(global::LightSwitchApplication.Application.DetailsClass d, global::System.Collections.ObjectModel.ReadOnlyCollection<object> args)
                {
                    bool handled = false;
                    d.Application.StoreInfoSetListDetail_Run(ref handled);
                    if (!handled)
                    {
                        d.ShowScreen("LightSwitchApplication:StoreInfoSetListDetail", () => global::LightSwitchApplication.StoreInfoSetListDetail.CreateInstance(), args);
                    }
                }
 
                public static readonly global::Microsoft.LightSwitch.Details.Framework.ApplicationMethod<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Entry
                    ShowNeededBoardsSetListDetail = new global::Microsoft.LightSwitch.Details.Framework.ApplicationMethod<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Entry(
                        "ShowNeededBoardsSetListDetail",
                        global::LightSwitchApplication.Application.DetailsClass.MethodSetProperties._ShowNeededBoardsSetListDetail_Stub,
                        global::LightSwitchApplication.Application.DetailsClass.MethodSetProperties._ShowNeededBoardsSetListDetail_CanInvoke,
                        global::LightSwitchApplication.Application.DetailsClass.MethodSetProperties._ShowNeededBoardsSetListDetail_InvokeMethod);
                private static void _ShowNeededBoardsSetListDetail_Stub(global::Microsoft.LightSwitch.Details.Framework.Base.DetailsCallback<global::LightSwitchApplication.Application.DetailsClass, global::Microsoft.LightSwitch.Details.Framework.ApplicationMethod<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Data> c, global::LightSwitchApplication.Application.DetailsClass d, object sf)
                {
                    c(d, ref d._ShowNeededBoardsSetListDetailMethod, sf);
                }
                private static global::System.Exception _ShowNeededBoardsSetListDetail_CanInvoke(global::LightSwitchApplication.Application.DetailsClass d, global::System.Collections.ObjectModel.ReadOnlyCollection<object> args, global::System.Exception ex)
                {
                    bool result = true;
                    d.Application.NeededBoardsSetListDetail_CanRun(ref result);
                    return result ? null : ex;
                }
                private static void _ShowNeededBoardsSetListDetail_InvokeMethod(global::LightSwitchApplication.Application.DetailsClass d, global::System.Collections.ObjectModel.ReadOnlyCollection<object> args)
                {
                    bool handled = false;
                    d.Application.NeededBoardsSetListDetail_Run(ref handled);
                    if (!handled)
                    {
                        d.ShowScreen("LightSwitchApplication:NeededBoardsSetListDetail", () => global::LightSwitchApplication.NeededBoardsSetListDetail.CreateInstance(), args);
                    }
                }
 
            }

            private global::Microsoft.LightSwitch.Details.Framework.ApplicationMethod<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Data _ShowNeededPartsSetListDetailMethod;

            private global::Microsoft.LightSwitch.Details.Framework.ApplicationMethod<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Data _ShowAvailablePartsSetListDetailMethod;

            private global::Microsoft.LightSwitch.Details.Framework.ApplicationMethod<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Data _ShowStoreInfoSetListDetailMethod;

            private global::Microsoft.LightSwitch.Details.Framework.ApplicationMethod<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Data _ShowNeededBoardsSetListDetailMethod;

            private global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationCommand<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Data _ShowNeededPartsSetListDetailCommand;

            private global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationCommand<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Data _ShowAvailablePartsSetListDetailCommand;

            private global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationCommand<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Data _ShowStoreInfoSetListDetailCommand;

            private global::Microsoft.LightSwitch.Details.Framework.Base.ApplicationCommand<global::LightSwitchApplication.Application, global::LightSwitchApplication.Application.DetailsClass>.Data _ShowNeededBoardsSetListDetailCommand;

        }
    }
}
