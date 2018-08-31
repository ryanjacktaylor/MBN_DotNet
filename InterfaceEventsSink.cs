using System;
using MbnApi;

namespace MBN_DotNet
{
    public delegate void OnEmergencyModeChangeHandler(IMbnInterface newInterface);
    public delegate void OnHomeProviderAvailableHandler(IMbnInterface newInterface);
    public delegate void OnInterfaceCapabilityAvailableHandler(IMbnInterface newInterface);
    public delegate void OnPreferredProvidersChangeHandler(IMbnInterface newInterface);
    public delegate void OnReadyStateChangeHandler(IMbnInterface newInterface);
    public delegate void OnScanNetworkCompleteHandler(IMbnInterface newInterface, uint requestID, int status);
    public delegate void OnSetPreferredProvidersCompleteHandler(IMbnInterface newInterface, uint requestID, int status);
    public delegate void OnSubscriberInformationChangeHandler(IMbnInterface newInterface);

    class InterfaceEventsSink : IMbnInterfaceEvents, IDisposable
    {
        private WeakReference<OnEmergencyModeChangeHandler> m_OnEmergencyModeChangeCallback;
        private WeakReference<OnHomeProviderAvailableHandler> m_OnHomeProviderAvailableCallback;
        private WeakReference<OnInterfaceCapabilityAvailableHandler> m_OnInterfaceCapabilityAvailableCallback;
        private WeakReference<OnPreferredProvidersChangeHandler> m_OnPreferredProvidersChangeCallback;
        private WeakReference<OnReadyStateChangeHandler> m_OnReadyStateChangeCallback;
        private WeakReference<OnScanNetworkCompleteHandler> m_OnScanNetworkCompleteCallback;
        private WeakReference<OnSetPreferredProvidersCompleteHandler> m_OnSetPreferredProvidersCompleteCallback;
        private WeakReference<OnSubscriberInformationChangeHandler> m_OnSubscriberInformationChangeCallback;

        private IConnectionPoint m_ConnectionPoint;
        private uint m_AdviseCookie;

        public InterfaceEventsSink(

            OnEmergencyModeChangeHandler onEmergencyModeChangeCallback,
            OnHomeProviderAvailableHandler onHomeProviderAvailableCallback,
            OnInterfaceCapabilityAvailableHandler onInterfaceCapabilityAvailableCallback,
            OnPreferredProvidersChangeHandler onPreferredProvidersChangeCallback,
            OnReadyStateChangeHandler onReadyStateChangeCallback,
            OnScanNetworkCompleteHandler onScanNetworkCompleteCallback,
            OnSetPreferredProvidersCompleteHandler onSetPreferredProvidersCompleteCallback,
            OnSubscriberInformationChangeHandler onSubscriberInformationChangeCallback,
            IConnectionPoint connectionPoint)
        {
            m_OnEmergencyModeChangeCallback = new WeakReference<OnEmergencyModeChangeHandler>(onEmergencyModeChangeCallback);
            m_OnHomeProviderAvailableCallback = new WeakReference<OnHomeProviderAvailableHandler>(onHomeProviderAvailableCallback);
            m_OnInterfaceCapabilityAvailableCallback = new WeakReference<OnInterfaceCapabilityAvailableHandler>(onInterfaceCapabilityAvailableCallback);
            m_OnPreferredProvidersChangeCallback = new WeakReference<OnPreferredProvidersChangeHandler>(onPreferredProvidersChangeCallback);
            m_OnReadyStateChangeCallback = new WeakReference<OnReadyStateChangeHandler>(onReadyStateChangeCallback);
            m_OnScanNetworkCompleteCallback = new WeakReference<OnScanNetworkCompleteHandler>(onScanNetworkCompleteCallback);
            m_OnSetPreferredProvidersCompleteCallback = new WeakReference<OnSetPreferredProvidersCompleteHandler>(onSetPreferredProvidersCompleteCallback);
            m_OnSubscriberInformationChangeCallback = new WeakReference<OnSubscriberInformationChangeHandler>(onSubscriberInformationChangeCallback);

            m_ConnectionPoint = connectionPoint;
            m_ConnectionPoint.Advise(this, out m_AdviseCookie);
        }

        ~InterfaceEventsSink()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (m_AdviseCookie != 0)
            {
                m_ConnectionPoint.Unadvise(m_AdviseCookie);
                m_AdviseCookie = 0;
            }
        }

        public void OnInterfaceCapabilityAvailable(IMbnInterface newInterface)
        {
            throw new NotImplementedException();
        }

        public void OnSubscriberInformationChange(IMbnInterface newInterface)
        {
            OnSubscriberInformationChangeHandler callback;
            if (m_OnSubscriberInformationChangeCallback.TryGetTarget(out callback))
            {
                callback.Invoke(newInterface);
            }
        }

        public void OnReadyStateChange(IMbnInterface newInterface)
        {
            OnReadyStateChangeHandler callback;
            if (m_OnReadyStateChangeCallback.TryGetTarget(out callback))
            {
                callback.Invoke(newInterface);
            }
        }

        public void OnEmergencyModeChange(IMbnInterface newInterface)
        {
            OnEmergencyModeChangeHandler callback;
            if (m_OnEmergencyModeChangeCallback.TryGetTarget(out callback))
            {
                callback.Invoke(newInterface);
            }
        }

        public void OnHomeProviderAvailable(IMbnInterface newInterface)
        {
            OnHomeProviderAvailableHandler callback;
            if (m_OnHomeProviderAvailableCallback.TryGetTarget(out callback))
            {
                callback.Invoke(newInterface);
            }
        }

        public void OnPreferredProvidersChange(IMbnInterface newInterface)
        {
            OnPreferredProvidersChangeHandler callback;
            if (m_OnPreferredProvidersChangeCallback.TryGetTarget(out callback))
            {
                callback.Invoke(newInterface);
            }
        }

        public void OnSetPreferredProvidersComplete(IMbnInterface newInterface, uint requestID, int status)
        {
            OnSetPreferredProvidersCompleteHandler callback;
            if (m_OnSetPreferredProvidersCompleteCallback.TryGetTarget(out callback))
            {
                callback.Invoke(newInterface, requestID, status);
            }
        }

        public void OnScanNetworkComplete(IMbnInterface newInterface, uint requestID, int status)
        {
            OnScanNetworkCompleteHandler callback;
            if (m_OnScanNetworkCompleteCallback.TryGetTarget(out callback))
            {
                callback.Invoke(newInterface, requestID, status);
            }
        }
    }
}
