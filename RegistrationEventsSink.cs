using System;
using MbnApi;

namespace MBN_DotNet
{
    public delegate void OnRegisterModeAvailableHandler(IMbnRegistration newInterface);
    public delegate void OnRegisterStateChangeHandler(IMbnRegistration newInterface);
    public delegate void OnPacketServiceStateChangeHandler(IMbnRegistration newInterface);
    public delegate void OnSetRegisterModeCompleteHandler(IMbnRegistration newInterface, uint requestID, int status);

    class RegistrationEventsSink : IMbnRegistrationEvents, IDisposable
    {

        private WeakReference<OnRegisterModeAvailableHandler> m_OnRegisterModeAvailableCallback;
        private WeakReference<OnRegisterStateChangeHandler> m_OnRegisterStateChangeCallback;
        private WeakReference<OnPacketServiceStateChangeHandler> m_onPacketServiceStateChangeCallback;
        private WeakReference<OnSetRegisterModeCompleteHandler> m_onSetRegisterModeCompleteCallback;
        private IConnectionPoint m_ConnectionPoint;
        private uint m_AdviseCookie;

        public RegistrationEventsSink(
            OnRegisterModeAvailableHandler registeModeAvailableCallback,
            OnRegisterStateChangeHandler registerStateCallback,
            OnPacketServiceStateChangeHandler packetServiceStateCallback,
            OnSetRegisterModeCompleteHandler setRegisterModeCallback,
            IConnectionPoint connectionPoint)
        {
            m_OnRegisterModeAvailableCallback = new WeakReference<OnRegisterModeAvailableHandler>(registeModeAvailableCallback);
            m_OnRegisterStateChangeCallback = new WeakReference<OnRegisterStateChangeHandler>(registerStateCallback);
            m_onPacketServiceStateChangeCallback = new WeakReference<OnPacketServiceStateChangeHandler>(packetServiceStateCallback);
            m_onSetRegisterModeCompleteCallback = new WeakReference<OnSetRegisterModeCompleteHandler>(setRegisterModeCallback);
            m_ConnectionPoint = connectionPoint;
            m_ConnectionPoint.Advise(this, out m_AdviseCookie);
        }


        ~RegistrationEventsSink()
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

        public void OnRegisterModeAvailable(IMbnRegistration newInterface)
        {
            //Console.WriteLine("OnRegisterModeAvailable");

            // Invoke main page thread to show UI
            OnRegisterModeAvailableHandler callback;
            if (m_OnRegisterModeAvailableCallback.TryGetTarget(out callback))
            {
                callback.Invoke(newInterface);
            }
        }

        public void OnRegisterStateChange(IMbnRegistration newInterface)
        {
            //Console.WriteLine("OnRegisterStateChange");

            // Invoke main page thread to show UI
            OnRegisterStateChangeHandler callback;
            if (m_OnRegisterStateChangeCallback.TryGetTarget(out callback))
            {
                callback.Invoke(newInterface);
            }
        }

        public void OnPacketServiceStateChange(IMbnRegistration newInterface)
        {
            //Console.WriteLine("OnPacketServiceStateChange");

            // Invoke main page thread to show UI
            OnPacketServiceStateChangeHandler callback;
            if (m_onPacketServiceStateChangeCallback.TryGetTarget(out callback))
            {
                callback.Invoke(newInterface);
            }
        }

        public void OnSetRegisterModeComplete(IMbnRegistration newInterface, uint requestID, int status)
        {
            //Console.WriteLine("OnSetRegisterModeComplete");

            // Invoke main page thread to show UI
            OnSetRegisterModeCompleteHandler callback;
            if (m_onSetRegisterModeCompleteCallback.TryGetTarget(out callback))
            {
                callback.Invoke(newInterface, requestID, status);
            }
        }
    }
}
