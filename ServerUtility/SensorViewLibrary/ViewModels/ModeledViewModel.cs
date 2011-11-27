using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Command;
using Livet.Messaging;
using Livet.Messaging.File;
using Livet.Messaging.Window;

namespace SensorViewLibrary.ViewModels
{
    public class ModeledViewModel<T> : ViewModel
    {
        /*コマンド、プロパティの定義にはそれぞれ 
         * 
         *  ldcom   : DelegateCommand(パラメータ無)
         *  ldcomn  : DelegateCommand(パラメータ無・CanExecute無)
         *  ldcomp  : DelegateCommand(型パラメータ有)
         *  ldcompn : DelegateCommand(型パラメータ有・CanExecute無)
         *  lprop   : 変更通知プロパティ
         *  
         * を使用してください。
         */

        /*ViewModelからViewを操作したい場合は、
         * Messengerプロパティからメッセージ(各種InteractionMessage)を発信してください。
         */

        /*
         * UIDispatcherを操作する場合は、DispatcherHelperのメソッドを操作してください。
         * UIDispatcher自体はApp.xaml.csでインスタンスを確保してあります。
         */

        /*
         * Modelからの変更通知などの各種イベントをそのままViewModelで購読する事はメモリリークの
         * 原因となりやすく推奨できません。ViewModelHelperの各静的メソッドの利用を検討してください。
         */

        private T model;
        private T beforeModel;
        public T Model
        {
            get
            {
                return this.model;
            }
            set
            {
                this.beforeModel = this.model;
                this.model = value;
                OnModelChanged(new ModelChangedArgs<T>() { before = this.beforeModel, current = this.model });
            }
        }

        public delegate void ModelChangeEventHandler(object sender, ModelChangedArgs<T> e);

        public event ModelChangeEventHandler ModelChanged;
        protected void OnModelChanged(ModelChangedArgs<T> e)
        {
            if (this.ModelChanged != null)
            {
                this.ModelChanged(this, e);
            }
        }
    }

    public class ModelChangedArgs<T>
        : EventArgs
    {
        public T before;
        public T current;

    }
}
