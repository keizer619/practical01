using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentSystemClient.ServiceReference1;

namespace StudentSystemClient
{
    public class DataContainer
    {
        public event EventHandler AcceptedChanges;
        protected virtual void OnAcceptedChanges()
        {
            if ((this.AcceptedChanges != null))
            {
                this.AcceptedChanges(this, EventArgs.Empty);
            }
        }

        public void AcceptChanges()
        {
            this.OnAcceptedChanges();
        }

        public StudentC stc { get; set; }
    }
}
