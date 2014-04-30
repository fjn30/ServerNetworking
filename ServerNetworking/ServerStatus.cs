using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerNetworking
{

    /// <summary> 
    /// This class is used as a container of the server status, using an eventhandler it can provide  
    /// the status to objects listening to this event. (observerer pattern)
    /// </summary> 
    public class ServerStatus
    {
        private String internalServerName = null;
        private Boolean online = false;
        private int maxclients = 0;
        private int onlineclients = 0;

        private List<String> clientIdsConnected = new List<String>();

        public EventHandler<ServerStatus> DataChangedEvent;
        private void InvokeUpdateEvent()
        {
            if (DataChangedEvent != null)
                DataChangedEvent.Invoke(this, this);
        }

        public List<String> GetClientIdsConnected()
        {
            return this.clientIdsConnected;
        }

        public void RefreshClientIdsConnected(ArrayList al)
        {
            clientIdsConnected.Clear();
            foreach (Client c in al)
            {
                clientIdsConnected.Add(c.GetClientID());
            }
            InvokeUpdateEvent();
        }

        public Boolean ServerFull()
        {
            if (this.onlineclients < this.maxclients)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void SetInternalServerName(String name)
        {
            this.internalServerName = name;
        }

        public void SetOnline()
        {
            this.online = true;
            InvokeUpdateEvent();
        }

        public void SetOffline()
        {
            this.online = false;
            InvokeUpdateEvent();
        }

        public void SetMaxClients(int c)
        {
            this.maxclients = c;
        }

        public void IncOnline()
        {
            this.onlineclients = this.onlineclients + 1;
            InvokeUpdateEvent();
        }

        public void DecOnline()
        {
            this.onlineclients = this.onlineclients - 1;
            InvokeUpdateEvent();
        }

        public int GetNumOnlineClients()
        {
            return this.onlineclients;
        }

        public String GetInternalServerName()
        {
            return this.internalServerName;
        }

        public Boolean IsOnline()
        {
            return this.online;
        }

        public int GetMaxClients()
        {
            return this.maxclients;
        }

    }
}

