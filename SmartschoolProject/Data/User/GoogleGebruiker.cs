using DefaultDomain.Classes;
using System;

namespace SmartschoolProject.Data.User
{
    public class GoogleGebruiker
    {
        public GebruikerInfo GebruikerInfo { get; set; }

        public event EventHandler StateChanged;

        public GoogleGebruiker()
        {
            this.GebruikerInfo = new GebruikerInfo();
        }

        public void Refresh()
        {
            this.StateHasChanged();
        }

        private void StateHasChanged()
        {
            this.StateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}