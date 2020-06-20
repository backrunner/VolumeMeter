using AudioSwitcher.AudioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumeMeter
{
    public class VolumeChangedObserver : IObserver<DeviceVolumeChangedArgs>
    {
        private IDisposable unsubscriber;
        public delegate void VolumeChanged(object sender, DeviceVolumeChangedArgs args);
        public event VolumeChanged VolumeChangedEvent;
        public virtual void Subscribe(IObservable<DeviceVolumeChangedArgs> notification)
        {
            if (notification != null)
            {
                unsubscriber = notification.Subscribe(this);
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

        public void OnNext(DeviceVolumeChangedArgs value)
        {
            VolumeChangedEvent(this, value);
        }
    }
}
