using System;
using Prism.Mvvm;

namespace VipaksTestTask
{
    public abstract class ViewModel : BindableBase, IDisposable
    {
        public void Dispose()
        {
            DisposeInner();
        }

        protected virtual void DisposeInner()
        {
            
        }
    }
}
