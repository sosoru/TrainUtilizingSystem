﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LightSwitchApplication.Implementation
{
    
    #region NeededParts
    [global::System.Runtime.Serialization.DataContract(Namespace = "http://schemas.datacontract.org/2004/07/ApplicationData.Implementation")]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public partial class NeededParts :
        global::LightSwitchApplication.NeededParts.DetailsClass.IImplementation
    {
        partial void OnNeededParts_AvailablePartsChanged()
        {
            this.___OnPropertyChanged("NeededParts_AvailableParts");
            this.___OnPropertyChanged("AvailableParts");
        }
        
        partial void OnIdChanged()
        {
            this.___OnPropertyChanged("Id");
        }
        
        partial void OnBoardChanged()
        {
            this.___OnPropertyChanged("Board");
        }
        
        partial void OnValueChanged()
        {
            this.___OnPropertyChanged("Value");
        }
        
        partial void OnDeviceChanged()
        {
            this.___OnPropertyChanged("Device");
        }
        
        partial void OnPartsChanged()
        {
            this.___OnPropertyChanged("Parts");
        }
        
        partial void OnQtyChanged()
        {
            this.___OnPropertyChanged("Qty");
        }
        
        partial void OnRowVersionChanged()
        {
            this.___OnPropertyChanged("RowVersion");
        }
        
        global::System.Collections.IEnumerable global::LightSwitchApplication.NeededParts.DetailsClass.IImplementation.NeededBoardsCollection
        {
            get
            {
                return this.NeededBoardsCollection;
            }
        }
        
        internal global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRefCollection<global::LightSwitchApplication.Implementation.NeededBoards> __NeededBoardsCollection
        {
            get
            {
                if (this.___NeededBoardsCollection == null)
                {
                    this.___NeededBoardsCollection = new global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRefCollection<global::LightSwitchApplication.Implementation.NeededBoards>(
                        this,
                        "NeededBoardsCollection",
                        () => this._NeededBoardsCollection,
                        e => global::System.Object.Equals(e.NeededBoards_NeededParts, this.Id));
                }
                return this.___NeededBoardsCollection;
            }
        }
        
        private global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRefCollection<global::LightSwitchApplication.Implementation.NeededBoards> ___NeededBoardsCollection;
        
        global::Microsoft.LightSwitch.Internal.IEntityImplementation global::LightSwitchApplication.NeededParts.DetailsClass.IImplementation.AvailableParts
        {
            get
            {
                return this.AvailableParts;
            }
            set
            {
                this.AvailableParts = (global::LightSwitchApplication.Implementation.AvailableParts)value;
            }
        }
        
        private global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRef<global::LightSwitchApplication.Implementation.AvailableParts> __AvailableParts
        {
            get
            {
                if (this.___AvailableParts == null)
                {
                    this.___AvailableParts = new global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRef<global::LightSwitchApplication.Implementation.AvailableParts>(
                        this,
                        "AvailableParts",
                        new string[] { "NeededParts_AvailableParts" },
                        e => global::System.Object.Equals(e.Id, this.NeededParts_AvailableParts),
                        () => this._AvailableParts,
                        e => this._AvailableParts = e);
                }
                return this.___AvailableParts;
            }
        }
        
        private global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRef<global::LightSwitchApplication.Implementation.AvailableParts> ___AvailableParts;
        
    }
    #endregion
    
    #region AvailableParts
    [global::System.Runtime.Serialization.DataContract(Namespace = "http://schemas.datacontract.org/2004/07/ApplicationData.Implementation")]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public partial class AvailableParts :
        global::LightSwitchApplication.AvailableParts.DetailsClass.IImplementation
    {
        partial void OnIdChanged()
        {
            this.___OnPropertyChanged("Id");
        }
        
        partial void OnCategoryChanged()
        {
            this.___OnPropertyChanged("Category");
        }
        
        partial void OnNameChanged()
        {
            this.___OnPropertyChanged("Name");
        }
        
        partial void OnLongNameChanged()
        {
            this.___OnPropertyChanged("LongName");
        }
        
        partial void OnCommentChanged()
        {
            this.___OnPropertyChanged("Comment");
        }
        
        partial void OnPartsUriChanged()
        {
            this.___OnPropertyChanged("PartsUri");
        }
        
        partial void OnPartsImageUriChanged()
        {
            this.___OnPropertyChanged("PartsImageUri");
        }
        
        partial void OnStockChanged()
        {
            this.___OnPropertyChanged("Stock");
        }
        
        partial void OnRowVersionChanged()
        {
            this.___OnPropertyChanged("RowVersion");
        }
        
        global::System.Collections.IEnumerable global::LightSwitchApplication.AvailableParts.DetailsClass.IImplementation.PriceInfoCollection
        {
            get
            {
                return this.PriceInfoCollection;
            }
        }
        
        internal global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRefCollection<global::LightSwitchApplication.Implementation.PriceInfo> __PriceInfoCollection
        {
            get
            {
                if (this.___PriceInfoCollection == null)
                {
                    this.___PriceInfoCollection = new global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRefCollection<global::LightSwitchApplication.Implementation.PriceInfo>(
                        this,
                        "PriceInfoCollection",
                        () => this._PriceInfoCollection,
                        e => global::System.Object.Equals(e.AvailableParts_PriceInfo, this.Id));
                }
                return this.___PriceInfoCollection;
            }
        }
        
        private global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRefCollection<global::LightSwitchApplication.Implementation.PriceInfo> ___PriceInfoCollection;
        
        global::System.Collections.IEnumerable global::LightSwitchApplication.AvailableParts.DetailsClass.IImplementation.NeededPartsCollection
        {
            get
            {
                return this.NeededPartsCollection;
            }
        }
        
        internal global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRefCollection<global::LightSwitchApplication.Implementation.NeededParts> __NeededPartsCollection
        {
            get
            {
                if (this.___NeededPartsCollection == null)
                {
                    this.___NeededPartsCollection = new global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRefCollection<global::LightSwitchApplication.Implementation.NeededParts>(
                        this,
                        "NeededPartsCollection",
                        () => this._NeededPartsCollection,
                        e => global::System.Object.Equals(e.NeededParts_AvailableParts, this.Id));
                }
                return this.___NeededPartsCollection;
            }
        }
        
        private global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRefCollection<global::LightSwitchApplication.Implementation.NeededParts> ___NeededPartsCollection;
        
    }
    #endregion
    
    #region PriceInfo
    [global::System.Runtime.Serialization.DataContract(Namespace = "http://schemas.datacontract.org/2004/07/ApplicationData.Implementation")]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public partial class PriceInfo :
        global::LightSwitchApplication.PriceInfo.DetailsClass.IImplementation
    {
        partial void OnPriceInfo_StoreInfoChanged()
        {
            this.___OnPropertyChanged("PriceInfo_StoreInfo");
            this.___OnPropertyChanged("StoreInfo");
        }
        
        partial void OnAvailableParts_PriceInfoChanged()
        {
            this.___OnPropertyChanged("AvailableParts_PriceInfo");
            this.___OnPropertyChanged("AvailableParts");
        }
        
        partial void OnIdChanged()
        {
            this.___OnPropertyChanged("Id");
        }
        
        partial void OnUnitChanged()
        {
            this.___OnPropertyChanged("Unit");
        }
        
        partial void OnPriceChanged()
        {
            this.___OnPropertyChanged("Price");
        }
        
        partial void OnQtyChanged()
        {
            this.___OnPropertyChanged("Qty");
        }
        
        partial void OnRowVersionChanged()
        {
            this.___OnPropertyChanged("RowVersion");
        }
        
        global::Microsoft.LightSwitch.Internal.IEntityImplementation global::LightSwitchApplication.PriceInfo.DetailsClass.IImplementation.StoreInfo
        {
            get
            {
                return this.StoreInfo;
            }
            set
            {
                this.StoreInfo = (global::LightSwitchApplication.Implementation.StoreInfo)value;
            }
        }
        
        private global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRef<global::LightSwitchApplication.Implementation.StoreInfo> __StoreInfo
        {
            get
            {
                if (this.___StoreInfo == null)
                {
                    this.___StoreInfo = new global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRef<global::LightSwitchApplication.Implementation.StoreInfo>(
                        this,
                        "StoreInfo",
                        new string[] { "PriceInfo_StoreInfo" },
                        e => global::System.Object.Equals(e.Id, this.PriceInfo_StoreInfo),
                        () => this._StoreInfo,
                        e => this._StoreInfo = e);
                }
                return this.___StoreInfo;
            }
        }
        
        private global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRef<global::LightSwitchApplication.Implementation.StoreInfo> ___StoreInfo;
        
        global::Microsoft.LightSwitch.Internal.IEntityImplementation global::LightSwitchApplication.PriceInfo.DetailsClass.IImplementation.AvailableParts
        {
            get
            {
                return this.AvailableParts;
            }
            set
            {
                this.AvailableParts = (global::LightSwitchApplication.Implementation.AvailableParts)value;
            }
        }
        
        private global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRef<global::LightSwitchApplication.Implementation.AvailableParts> __AvailableParts
        {
            get
            {
                if (this.___AvailableParts == null)
                {
                    this.___AvailableParts = new global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRef<global::LightSwitchApplication.Implementation.AvailableParts>(
                        this,
                        "AvailableParts",
                        new string[] { "AvailableParts_PriceInfo" },
                        e => global::System.Object.Equals(e.Id, this.AvailableParts_PriceInfo),
                        () => this._AvailableParts,
                        e => this._AvailableParts = e);
                }
                return this.___AvailableParts;
            }
        }
        
        private global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRef<global::LightSwitchApplication.Implementation.AvailableParts> ___AvailableParts;
        
    }
    #endregion
    
    #region StoreInfo
    [global::System.Runtime.Serialization.DataContract(Namespace = "http://schemas.datacontract.org/2004/07/ApplicationData.Implementation")]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public partial class StoreInfo :
        global::LightSwitchApplication.StoreInfo.DetailsClass.IImplementation
    {
        partial void OnIdChanged()
        {
            this.___OnPropertyChanged("Id");
        }
        
        partial void OnStoreNameChanged()
        {
            this.___OnPropertyChanged("StoreName");
        }
        
        partial void OnStoreUriChanged()
        {
            this.___OnPropertyChanged("StoreUri");
        }
        
        partial void OnRowVersionChanged()
        {
            this.___OnPropertyChanged("RowVersion");
        }
        
        global::System.Collections.IEnumerable global::LightSwitchApplication.StoreInfo.DetailsClass.IImplementation.PriceInfoCollection
        {
            get
            {
                return this.PriceInfoCollection;
            }
        }
        
        internal global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRefCollection<global::LightSwitchApplication.Implementation.PriceInfo> __PriceInfoCollection
        {
            get
            {
                if (this.___PriceInfoCollection == null)
                {
                    this.___PriceInfoCollection = new global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRefCollection<global::LightSwitchApplication.Implementation.PriceInfo>(
                        this,
                        "PriceInfoCollection",
                        () => this._PriceInfoCollection,
                        e => global::System.Object.Equals(e.PriceInfo_StoreInfo, this.Id));
                }
                return this.___PriceInfoCollection;
            }
        }
        
        private global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRefCollection<global::LightSwitchApplication.Implementation.PriceInfo> ___PriceInfoCollection;
        
    }
    #endregion
    
    #region NeededBoards
    [global::System.Runtime.Serialization.DataContract(Namespace = "http://schemas.datacontract.org/2004/07/ApplicationData.Implementation")]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public partial class NeededBoards :
        global::LightSwitchApplication.NeededBoards.DetailsClass.IImplementation
    {
        partial void OnNeededBoards_NeededPartsChanged()
        {
            this.___OnPropertyChanged("NeededBoards_NeededParts");
            this.___OnPropertyChanged("NeededParts");
        }
        
        partial void OnIdChanged()
        {
            this.___OnPropertyChanged("Id");
        }
        
        partial void OnBoardNameChanged()
        {
            this.___OnPropertyChanged("BoardName");
        }
        
        partial void OnCountChanged()
        {
            this.___OnPropertyChanged("Count");
        }
        
        partial void OnRowVersionChanged()
        {
            this.___OnPropertyChanged("RowVersion");
        }
        
        global::Microsoft.LightSwitch.Internal.IEntityImplementation global::LightSwitchApplication.NeededBoards.DetailsClass.IImplementation.NeededParts
        {
            get
            {
                return this.NeededParts;
            }
            set
            {
                this.NeededParts = (global::LightSwitchApplication.Implementation.NeededParts)value;
            }
        }
        
        private global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRef<global::LightSwitchApplication.Implementation.NeededParts> __NeededParts
        {
            get
            {
                if (this.___NeededParts == null)
                {
                    this.___NeededParts = new global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRef<global::LightSwitchApplication.Implementation.NeededParts>(
                        this,
                        "NeededParts",
                        new string[] { "NeededBoards_NeededParts" },
                        e => global::System.Object.Equals(e.Id, this.NeededBoards_NeededParts),
                        () => this._NeededParts,
                        e => this._NeededParts = e);
                }
                return this.___NeededParts;
            }
        }
        
        private global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRef<global::LightSwitchApplication.Implementation.NeededParts> ___NeededParts;
        
    }
    #endregion
    
    #region Table1Item
    [global::System.Runtime.Serialization.DataContract(Namespace = "http://schemas.datacontract.org/2004/07/ApplicationData.Implementation")]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public partial class Table1Item :
        global::LightSwitchApplication.Table1Item.DetailsClass.IImplementation
    {
        partial void OnIdChanged()
        {
            this.___OnPropertyChanged("Id");
        }
        
        partial void OnRowVersionChanged()
        {
            this.___OnPropertyChanged("RowVersion");
        }
        
    }
    #endregion
    
    #region Table2Item
    [global::System.Runtime.Serialization.DataContract(Namespace = "http://schemas.datacontract.org/2004/07/ApplicationData.Implementation")]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public partial class Table2Item :
        global::LightSwitchApplication.Table2Item.DetailsClass.IImplementation
    {
        partial void OnIdChanged()
        {
            this.___OnPropertyChanged("Id");
        }
        
        partial void OnRowVersionChanged()
        {
            this.___OnPropertyChanged("RowVersion");
        }
        
    }
    #endregion
    
    #region ApplicationDataObjectContext
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public partial class ApplicationDataObjectContext
    {
        protected override global::Microsoft.LightSwitch.Internal.IEntityImplementation CreateEntityImplementation<T>()
        {
            if (typeof(T) == typeof(global::LightSwitchApplication.NeededParts))
            {
                return new global::LightSwitchApplication.Implementation.NeededParts();
            }
            if (typeof(T) == typeof(global::LightSwitchApplication.AvailableParts))
            {
                return new global::LightSwitchApplication.Implementation.AvailableParts();
            }
            if (typeof(T) == typeof(global::LightSwitchApplication.PriceInfo))
            {
                return new global::LightSwitchApplication.Implementation.PriceInfo();
            }
            if (typeof(T) == typeof(global::LightSwitchApplication.StoreInfo))
            {
                return new global::LightSwitchApplication.Implementation.StoreInfo();
            }
            if (typeof(T) == typeof(global::LightSwitchApplication.NeededBoards))
            {
                return new global::LightSwitchApplication.Implementation.NeededBoards();
            }
            if (typeof(T) == typeof(global::LightSwitchApplication.Table1Item))
            {
                return new global::LightSwitchApplication.Implementation.Table1Item();
            }
            if (typeof(T) == typeof(global::LightSwitchApplication.Table2Item))
            {
                return new global::LightSwitchApplication.Implementation.Table2Item();
            }
            return null;
        }
    }
    #endregion
    
    #region DataServiceImplementationFactory
    [global::System.ComponentModel.Composition.PartCreationPolicy(global::System.ComponentModel.Composition.CreationPolicy.Shared)]
    [global::System.ComponentModel.Composition.Export(typeof(global::Microsoft.LightSwitch.Internal.IDataServiceFactory))]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public class DataServiceFactory :
        global::Microsoft.LightSwitch.ClientGenerated.Implementation.DataServiceFactory
    {
    
        protected override global::Microsoft.LightSwitch.IDataService CreateDataService(global::System.Type dataServiceType)
        {
            if (dataServiceType == typeof(global::LightSwitchApplication.ApplicationData))
            {
                return new global::LightSwitchApplication.ApplicationData();
            }
            return base.CreateDataService(dataServiceType);
        }
    
        protected override global::Microsoft.LightSwitch.Internal.IDataServiceImplementation CreateDataServiceImplementation<TDataService>(TDataService dataService)
        {
            if (typeof(TDataService) == typeof(global::LightSwitchApplication.ApplicationData))
            {
                return new global::LightSwitchApplication.Implementation.ApplicationDataObjectContext(global::Microsoft.LightSwitch.ClientGenerated.Implementation.DataServiceContext.CreateServiceUri("../ApplicationData.svc"));
            }
            return base.CreateDataServiceImplementation(dataService);
        }
    }
    #endregion
    
    [global::System.ComponentModel.Composition.PartCreationPolicy(global::System.ComponentModel.Composition.CreationPolicy.Shared)]
    [global::System.ComponentModel.Composition.Export(typeof(global::Microsoft.LightSwitch.Internal.ITypeMappingProvider))]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public class __TypeMapping
        : global::Microsoft.LightSwitch.Internal.ITypeMappingProvider
    {
        global::System.Type global::Microsoft.LightSwitch.Internal.ITypeMappingProvider.GetImplementationType(global::System.Type definitionType)
        {
            if (typeof(global::LightSwitchApplication.NeededParts) == definitionType)
            {
                return typeof(global::LightSwitchApplication.Implementation.NeededParts);
            }
            if (typeof(global::LightSwitchApplication.AvailableParts) == definitionType)
            {
                return typeof(global::LightSwitchApplication.Implementation.AvailableParts);
            }
            if (typeof(global::LightSwitchApplication.PriceInfo) == definitionType)
            {
                return typeof(global::LightSwitchApplication.Implementation.PriceInfo);
            }
            if (typeof(global::LightSwitchApplication.StoreInfo) == definitionType)
            {
                return typeof(global::LightSwitchApplication.Implementation.StoreInfo);
            }
            if (typeof(global::LightSwitchApplication.NeededBoards) == definitionType)
            {
                return typeof(global::LightSwitchApplication.Implementation.NeededBoards);
            }
            if (typeof(global::LightSwitchApplication.Table1Item) == definitionType)
            {
                return typeof(global::LightSwitchApplication.Implementation.Table1Item);
            }
            if (typeof(global::LightSwitchApplication.Table2Item) == definitionType)
            {
                return typeof(global::LightSwitchApplication.Implementation.Table2Item);
            }
            return null;
        }
    }
}