//------------------------------------------------------------------------------
// <auto-generated>
//    このコードはテンプレートから生成されました。
//
//    このファイルを手動で変更すると、アプリケーションで予期しない動作が発生する可能性があります。
//    このファイルに対する手動の変更は、コードが再生成されると上書きされます。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]

namespace EagleBom.Implementation
{
    #region コンテキスト
    
    /// <summary>
    /// 使用できるメタデータ ドキュメントはありません。
    /// </summary>
    public partial class EagleBomObjectContext : ObjectContext
    {
        #region コンストラクター
    
        /// <summary>
        /// アプリケーション構成ファイルの 'EagleBomObjectContext' セクションにある接続文字列を使用して新しい EagleBomObjectContext オブジェクトを初期化します。
        /// </summary>
        public EagleBomObjectContext() : base("name=EagleBomObjectContext", "EagleBomObjectContext")
        {
            OnContextCreated();
        }
    
        /// <summary>
        /// 新しい EagleBomObjectContext オブジェクトを初期化します。
        /// </summary>
        public EagleBomObjectContext(string connectionString) : base(connectionString, "EagleBomObjectContext")
        {
            OnContextCreated();
        }
    
        /// <summary>
        /// 新しい EagleBomObjectContext オブジェクトを初期化します。
        /// </summary>
        public EagleBomObjectContext(EntityConnection connection) : base(connection, "EagleBomObjectContext")
        {
            OnContextCreated();
        }
    
        #endregion
    
        #region 部分メソッド
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet プロパティ
    
        /// <summary>
        /// 使用できるメタデータ ドキュメントはありません。
        /// </summary>
        public ObjectSet<EagleBomPartsItem> EagleBomParts
        {
            get
            {
                if ((_EagleBomParts == null))
                {
                    _EagleBomParts = base.CreateObjectSet<EagleBomPartsItem>("EagleBomParts");
                }
                return _EagleBomParts;
            }
        }
        private ObjectSet<EagleBomPartsItem> _EagleBomParts;

        #endregion
        #region AddTo メソッド
    
        /// <summary>
        /// EagleBomParts EntitySet に新しいオブジェクトを追加するための非推奨のメソッドです。代わりに、関連付けられている ObjectSet&lt;T&gt; プロパティの .Add メソッドを使用してください。
        /// </summary>
        public void AddToEagleBomParts(EagleBomPartsItem eagleBomPartsItem)
        {
            base.AddObject("EagleBomParts", eagleBomPartsItem);
        }

        #endregion
    }
    

    #endregion
    
    #region エンティティ
    
    /// <summary>
    /// 使用できるメタデータ ドキュメントはありません。
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EagleBom", Name="EagleBomPartsItem")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class EagleBomPartsItem : EntityObject
    {
        #region ファクトリ メソッド
    
        /// <summary>
        /// 新しい EagleBomPartsItem オブジェクトを作成します。
        /// </summary>
        /// <param name="id">Id プロパティの初期値。</param>
        /// <param name="qty">Qty プロパティの初期値。</param>
        public static EagleBomPartsItem CreateEagleBomPartsItem(global::System.Int32 id, global::System.Int32 qty)
        {
            EagleBomPartsItem eagleBomPartsItem = new EagleBomPartsItem();
            eagleBomPartsItem.Id = id;
            eagleBomPartsItem.Qty = qty;
            return eagleBomPartsItem;
        }

        #endregion
        #region プリミティブ プロパティ
    
        /// <summary>
        /// 使用できるメタデータ ドキュメントはありません。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// 使用できるメタデータ ドキュメントはありません。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Board
        {
            get
            {
                return _Board;
            }
            set
            {
                OnBoardChanging(value);
                ReportPropertyChanging("Board");
                _Board = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Board");
                OnBoardChanged();
            }
        }
        private global::System.String _Board;
        partial void OnBoardChanging(global::System.String value);
        partial void OnBoardChanged();
    
        /// <summary>
        /// 使用できるメタデータ ドキュメントはありません。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Qty
        {
            get
            {
                return _Qty;
            }
            set
            {
                OnQtyChanging(value);
                ReportPropertyChanging("Qty");
                _Qty = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Qty");
                OnQtyChanged();
            }
        }
        private global::System.Int32 _Qty;
        partial void OnQtyChanging(global::System.Int32 value);
        partial void OnQtyChanged();
    
        /// <summary>
        /// 使用できるメタデータ ドキュメントはありません。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Value
        {
            get
            {
                return _Value;
            }
            set
            {
                OnValueChanging(value);
                ReportPropertyChanging("Value");
                _Value = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Value");
                OnValueChanged();
            }
        }
        private global::System.String _Value;
        partial void OnValueChanging(global::System.String value);
        partial void OnValueChanged();
    
        /// <summary>
        /// 使用できるメタデータ ドキュメントはありません。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Device
        {
            get
            {
                return _Device;
            }
            set
            {
                OnDeviceChanging(value);
                ReportPropertyChanging("Device");
                _Device = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Device");
                OnDeviceChanged();
            }
        }
        private global::System.String _Device;
        partial void OnDeviceChanging(global::System.String value);
        partial void OnDeviceChanged();
    
        /// <summary>
        /// 使用できるメタデータ ドキュメントはありません。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Parts
        {
            get
            {
                return _Parts;
            }
            set
            {
                OnPartsChanging(value);
                ReportPropertyChanging("Parts");
                _Parts = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Parts");
                OnPartsChanged();
            }
        }
        private global::System.String _Parts;
        partial void OnPartsChanging(global::System.String value);
        partial void OnPartsChanged();

        #endregion
    
    }

    #endregion
    
}