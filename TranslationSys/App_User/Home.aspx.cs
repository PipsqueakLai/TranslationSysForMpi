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
            SqlDataSource1.SelectParameters["UserId"].DefaultValue = User.Identity.GetUserId();
            string someScript = "$(document).ready(function () {var sidebar = $('#page-sidebar');sidebar.addClass('page-sidebar-left');sidebar.removeClass('page-sidebar-hide');var content=$('#page-content');content.addClass('page-sidebar-content')})";
            ClientScript.RegisterStartupScript(this.GetType(), "x", someScript, true);
           

        }
    }
}