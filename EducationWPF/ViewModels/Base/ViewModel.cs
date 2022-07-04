using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EducationWPF.ViewModels.Base
{
    internal abstract class ViewModel : INotifyPropertyChanged, IDisposable
    {


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }


        protected virtual bool Set<T>(ref T failed, T value, [CallerMemberName] string PropertName = null)
        {
            if (Equals(failed, value)) return false;
            failed = value;
            OnPropertyChanged(PropertName);
            return true;
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
