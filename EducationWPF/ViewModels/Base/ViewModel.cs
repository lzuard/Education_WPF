using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Xaml;

namespace EducationWPF.ViewModels.Base
{
    internal abstract class ViewModel : MarkupExtension, INotifyPropertyChanged, IDisposable
    {


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

            ////async call not ready yet
            //var handlers = PropertyChanged;
            //if (handlers != null) return;

            //var invocationList = handlers.GetInvocationList();

            //var arg = new PropertyChangedEventArgs(PropertyName);
            //foreach (var action in invocationList)
            //{
            //    if (action.Target is DispatcherObject dispObj)
            //        dispObj.Dispatcher.Invoke(action, this, arg);
            //    else
            //        action.DynamicInvoke(this, arg);
            //}
        }


        protected virtual bool Set<T>(ref T failed, T value, [CallerMemberName] string PropertName = null)
        {
            if (Equals(failed, value)) return false;
            failed = value;
            OnPropertyChanged(PropertName);
            return true;
        }

        public override object ProvideValue(IServiceProvider sp)
        {
            var value_target_service=sp.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            var root_object_service=sp.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider;

            OnInitialized(
                value_target_service?.TargetObject,
                value_target_service?.TargetProperty,
                root_object_service?.RootObject);

            return this;
        }

        private WeakReference _TargetRef;
        private WeakReference _RootRef;

        public object TargetObject => _TargetRef?.Target;
        public object RootObject => _RootRef?.Target;

        protected virtual void OnInitialized(object Target, object Property, object Root)
        {
            _TargetRef = new WeakReference(Target);
            _RootRef = new WeakReference(Root);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private bool _Disposed;
        protected virtual void Dispose(bool Disposing)
        {
            if (!Disposing || _Disposed) return;
            _Disposed = true;
            //dispose resources
        }
    }
}
