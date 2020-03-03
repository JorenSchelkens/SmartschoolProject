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

        public GoogleGebruiker(string email)
        {
            this.GebruikerInfo = new GebruikerInfo();
            this.GetUserDetails(email);

            this.Refresh();
        }

        private async void GetUserDetails(string email)
        {
            this.GebruikerInfo = await this.GebruikerInfo.Initialize(email);
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