using System;
using MbnApi;

namespace MBN_DotNet
{
    public delegate void OnConnectCompleteHandler(IMbnConnection connection, uint requestId, int status);
    public delegate void OnDisconnectCompleteHandler(IMbnConnection connection, uint requestId, int status);

    class ConnectionEventsSink : IMbnConnectionEvents, IDisposable
    {
        private WeakReference<OnConnectCompleteHandler> m_ConnectCallback;
        private WeakReference<OnDisconnectCompleteHandler> m_DisconnectCallback;
        private IConnectionPoint m_ConnectionPoint;
        private uint m_AdviseCookie;

        public ConnectionEventsSink(
            OnConnectCompleteHandler connectCallback,
            OnDisconnectCompleteHandler disconnectCallback,
            IConnectionPoint connectionPoint)
        {
            m_ConnectCallback = new WeakReference<OnConnectCompleteHandler>(connectCallback);
            m_DisconnectCallback = new WeakReference<OnDisconnectCompleteHandler>(disconnectCallback);
            m_ConnectionPoint = connectionPoint;
            m_ConnectionPoint.Advise(this, out m_AdviseCookie);
        }

        ~ConnectionEventsSink()
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

        // This will be called back when connect operation is complete
        public void OnConnectComplete(IMbnConnection connection, uint requestId, int status)
        {
            // Invoke main page thread to show UI
            OnConnectCompleteHandler callback;
            if (m_ConnectCallback.TryGetTarget(out callback))
            {
                callback.Invoke(connection, requestId, status);
            }
        }

        // This will be called back when disconnect operation is complete
        public void OnDisconnectComplete(IMbnConnection connection, uint requestId, int status)
        {
            // Invoke main page thread to show UI
            OnDisconnectCompleteHandler callback;
            if (m_DisconnectCallback.TryGetTarget(out callback))
            {
                callback.Invoke(connection, requestId, status);
            }
        }

        // Not implemented for sample
        public void OnConnectStateChange(IMbnConnection connection)
        {
            Console.WriteLine("OnConnectStateChange");
        }
        public void OnVoiceCallStateChange(IMbnConnection connection) { }
    }
}
