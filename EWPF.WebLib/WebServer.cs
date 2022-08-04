using System.Net;
using System.Net.Sockets;

namespace EWPF.WebLib
{
    public class WebServer
    {
        #region Fields

        
        ////hard and cool: headers (4+ OSI)
        //private TcpListener _listener = new TcpListener(new IPEndPoint(IPAddress.Any, 8080));

        //for little crying babies (+ problems with Windows restrictions) 
        private HttpListener _listener;
        private readonly int _port;
        private bool _enabled;
        private readonly object _syncRoot = new object(); //for critical section

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
            Listen();
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
        /// 
        /// </summary>
        private void Listen()
        {

        }
        #endregion

    }
}
