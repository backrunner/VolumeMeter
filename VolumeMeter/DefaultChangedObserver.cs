using AudioSwitcher.AudioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumeMeter
{
    public class DefaultChangedObserver : IObserver<DefaultDeviceChangedArgs>
    {
        private IDisposable unsubscriber;
        public delegate void DefaultDeviceChanged(object sender, DefaultDeviceChangedArgs args);
        public event DefaultDeviceChanged DefaultDeviceChangedEvent;

        public virtual void Subscribe(IObservable<DefaultDeviceChangedArgs> args)
        {
            if (args != null)
            {
                unsubscriber = args.Subscribe(this);
            }
        }
        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }
        public void OnCompleted()
        {
            this.Unsubscribe();
        }

        public void OnError(Exception error)
        {
            throw error;
        }

        public void OnNext(DefaultDeviceChangedArgs value)
        {
            DefaultDeviceChangedEvent(this, value);
        }
    }
}
