using System.Net;
using System.Net.Sockets;

namespace EWPF.WebLib
{

    /// <summary>
    /// Main web server class
    /// </summary>
    public class WebServer
    {
        #region Fields

        
        ////hard and cool: headers (4+ OSI)
        //private TcpListener _listener = new TcpListener(new IPEndPoint(IPAddress.Any, 8080));

        //for little crying babies (+ problems with Windows restrictions) 
        private HttpListener _listener;                     //HTTP listener
        private readonly int _port;                         //Server port
        private bool _enabled;                              //If server has been started
        private readonly object _syncRoot = new object();   //for critical section

        #endregion

        #region Properties

        public int Port => _port;

        public bool Enabled
        {
            get => _enabled;
            set
            {
                if(value) 
                    Start();
                else
                    Stop();
            }
        }

        #endregion

        #region Constructors

        public WebServer(int port) => _port = port;

        #endregion

        #region Events

        private event EventHandler<RequestReceiverEventArgs> RequestReceived;

        #endregion

        #region Methods

        /// <summary>
        /// Server start up !No validation!
        /// </summary>
        public void Start()
        {
            if(_enabled) return;
            lock (_syncRoot)
            {
                if (_enabled) return;

                _listener = new HttpListener();
                _listener.Prefixes.Add($"http//*:{_port}"); //exception if url or port is restricted for user
                _listener.Prefixes.Add($"http//+:{_port}"); //both
                _enabled = true;
            }
            ListenAsync();
        }
        
        /// <summary>
        /// Server shut down
        /// </summary>
        public void Stop()
        {
            if(!_enabled) return;
            lock (_syncRoot)
            {
                if (!_enabled) return;

                _listener = null;
                _enabled = false;
            }
        }

        /// <summary>
        /// Incoming connections listening
        /// </summary>
        private async void ListenAsync()
        {
            var listener = _listener;

            listener.Start();

            while (_enabled)
            {
                var context = await listener.GetContextAsync().ConfigureAwait(false);
                ProcessRequest(context);
                

            }

            listener.Stop();
        }


        /// <summary>
        /// Do things with context from http listener
        /// </summary>
        /// <param name="context"></param>
        private void ProcessRequest(HttpListenerContext context)
        {
            RequestReceived?.Invoke(this, new RequestReceiverEventArgs(context));
        }

        #endregion

    }

    public class RequestReceiverEventArgs : EventArgs
    {
        public HttpListenerContext Context { get; }

        public RequestReceiverEventArgs(HttpListenerContext context)
        {
            Context = context;
        }

    }
}
