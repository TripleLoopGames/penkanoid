using System;

public class LoginStatus
{
    private bool? servicesActivated;
    public bool ServicesActivated
    {
        get
        {
            return this.servicesActivated ?? false;
        }
        set
        {
            this.servicesActivated = value;
        }
    }

    private bool? loggedIn;
    public bool LoggedIn
    {
        get
        {
            return this.loggedIn ?? false;
        }
        set
        {
            this.loggedIn = value;
        }
    }

    private bool? refusedLogIn;
    public bool RefusedLogIn
    {
        get
        {
            return this.refusedLogIn ?? false;
        }
        set
        {
            this.refusedLogIn = value;
        }
    }

    public LoginStatus Copy()
    {
        LoginStatus copied = new LoginStatus()
        {
            ServicesActivated = this.ServicesActivated,
            RefusedLogIn = this.RefusedLogIn,
            LoggedIn = this.LoggedIn,
        };
        return copied;
    }


    public LoginStatus Merge(LoginStatus loginStatus)
    {
        if(loginStatus.GetRealRefusedLogIn() != null)
        {
            this.RefusedLogIn = loginStatus.RefusedLogIn;
        }
        if (loginStatus.GetRealLoggedIn() != null)
        {
            this.LoggedIn = loginStatus.LoggedIn;
        }
        if (loginStatus.GetRealServicesActivated() != null)
        {
            this.ServicesActivated = loginStatus.ServicesActivated;
        }
        return this;
    }

    public bool? GetRealRefusedLogIn()
    {
        return this.refusedLogIn;
    }

    public bool? GetRealLoggedIn()
    {
        return this.loggedIn;
    }

    public bool? GetRealServicesActivated()
    {
        return this.servicesActivated;
    }
}
