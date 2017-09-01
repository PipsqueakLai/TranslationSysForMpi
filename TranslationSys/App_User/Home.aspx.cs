using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;

namespace TranslationSys
{
    public partial class Home : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string userID = Context.GetOwinContext().Authentication.User.Identity.GetUserId();
            SqlDataSource1.SelectParameters["UserId"].DefaultValue = User.Identity.GetUserId();

        }
    }
}