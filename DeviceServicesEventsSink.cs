using System;
using MbnApi;

namespace MBN_DotNet
{
    public delegate void OnOpenCommandSessionCompleteHandler(IMbnDeviceService deviceService, int status, uint requestId);
    public delegate void OnQueryCommandCompleteHandler(IMbnDeviceService deviceService, uint responseId, byte[] deviceServiceData, int status, uint requestId);
    public delegate void OnCloseCommandSessionCompleteHandler(IMbnDeviceService deviceService, int status, uint requestId);
    public delegate void OnSetCommandCompleteHandler(IMbnDeviceService deviceService, uint responseID, byte[] deviceServiceData, int status, uint requestID);

    class DeviceServicesEventsSink : IMbnDeviceServicesEvents, IDisposable
    {
        private WeakReference<OnOpenCommandSessionCompleteHandler> m_OnOpenCommandSessionCallback;
        private WeakReference<OnQueryCommandCompleteHandler> m_OnQueryCommandCallback;
        private WeakReference<OnCloseCommandSessionCompleteHandler> m_OnCloseCommandCallback;
        private WeakReference<OnSetCommandCompleteHandler> m_OnSetCommandCallback;
        private IConnectionPoint m_ConnectionPoint;
        private uint m_AdviseCookie;

        public DeviceServicesEventsSink(
            OnOpenCommandSessionCompleteHandler onOpenDataSessionCallback,
            OnQueryCommandCompleteHandler onQueryCommandCallback,
            OnCloseCommandSessionCompleteHandler onCloseCommandCallback,
            OnSetCommandCompleteHandler onSetCommandCallback,
            IConnectionPoint connectionPoint)
        {
            m_OnOpenCommandSessionCallback = new WeakReference<OnOpenCommandSessionCompleteHandler>(onOpenDataSessionCallback);
            m_OnQueryCommandCallback = new WeakReference<OnQueryCommandCompleteHandler>(onQueryCommandCallback);
            m_OnCloseCommandCallback = new WeakReference<OnCloseCommandSessionCompleteHandler>(onCloseCommandCallback);
            m_OnSetCommandCallback = new WeakReference<OnSetCommandCompleteHandler>(onSetCommandCallback);
            m_ConnectionPoint = connectionPoint;
            m_ConnectionPoint.Advise(this, out m_AdviseCookie);
        }

        ~DeviceServicesEventsSink()
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

        // This will be called back when open command session operation is complete
        public void OnOpenCommandSessionComplete(IMbnDeviceService deviceService, int status, uint requestId)
        {
            // Invoke main page thread to show UI
            OnOpenCommandSessionCompleteHandler callback;
            if (m_OnOpenCommandSessionCallback.TryGetTarget(out callback))
            {
                callback.Invoke(deviceService, status, requestId);
            }
        }

        // This will be called back when query command operation is complete
        public void OnQueryCommandComplete(IMbnDeviceService deviceService, uint responseId, Array deviceServiceData, int status, uint requestId)
        {
            // Invoke main page thread to show UI
            OnQueryCommandCompleteHandler callback;
            if (m_OnQueryCommandCallback.TryGetTarget(out callback))
            {
                callback.Invoke(deviceService, responseId, (byte[])deviceServiceData, status, requestId);
            }
        }

        // This will be called back when close command session operation is complete
        public void OnCloseCommandSessionComplete(IMbnDeviceService deviceService, int status, uint requestId)
        {
            // Invoke main page thread to show UI
            OnCloseCommandSessionCompleteHandler callback;
            if (m_OnCloseCommandCallback.TryGetTarget(out callback))
            {
                callback.Invoke(deviceService, status, requestId);
            }
        }

        public void OnSetCommandComplete(IMbnDeviceService deviceService, uint responseID, Array deviceServiceData, int status, uint requestID)
        {
            // Invoke main page thread to show UI
            OnSetCommandCompleteHandler callback;
            if (m_OnSetCommandCallback.TryGetTarget(out callback))
            {
                callback.Invoke(deviceService, responseID, (byte[])deviceServiceData, status, requestID);
            }
        }

        public void OnQuerySupportedCommandsComplete(IMbnDeviceService deviceService, Array commandIDList, int status, uint requestID)
        {
            Console.WriteLine("OnQuerySupportedCommandsComplete");
        }

        public void OnEventNotification(IMbnDeviceService deviceService, uint eventID, Array deviceServiceData)
        {
            Console.WriteLine("OnEventNotification");
        }

        public void OnOpenDataSessionComplete(IMbnDeviceService deviceService, int status, uint requestID)
        {
            Console.WriteLine("OnOpenDataSessionComplete");
        }

        public void OnCloseDataSessionComplete(IMbnDeviceService deviceService, int status, uint requestID)
        {
            Console.WriteLine("OnCloseDataSessionComplete");
        }

        public void OnWriteDataComplete(IMbnDeviceService deviceService, int status, uint requestID)
        {
            Console.WriteLine("OnWriteDataComplete");
        }

        public void OnReadData(IMbnDeviceService deviceService, Array deviceServiceData)
        {
            Console.WriteLine("OnReadData");
        }

        public void OnInterfaceStateChange(string InterfaceID, MBN_DEVICE_SERVICES_INTERFACE_STATE stateChange)
        {
            Console.WriteLine("OnInterfaceStateChange");
        }
    }
}
