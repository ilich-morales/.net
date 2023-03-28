using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PageToSync_artistDetails : MultitracksPage
{
    SQL sql = new SQL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var id = HttpContext.Current.Request.QueryString["artistID"];            
            if (id == null)//Parameter artistID not provided on URL
                Response.Redirect("~/");            
            int artistID;
            if (!int.TryParse(id.ToString(), out artistID)) //Parameter provided but unconvertible to int
                Response.Redirect("~/");

            sql.Parameters.Add("@artistID", artistID);
            var ds = sql.ExecuteStoredProcedureDS("GetArtistDetails", false); //Execute SP
            if (ds.Tables.Count == 3 && ds.Tables[0].Rows.Count > 0)
            { //artist fund
                Dtl_Artist.DataSource = ds.Tables[0];
                Dtl_Artist.DataBind();

                DataList dtlSongs = (DataList)Dtl_Artist.Items[0].FindControl("Dtl_Songs");
                dtlSongs.DataSource = ds.Tables[2];
                dtlSongs.DataBind();

                DataList dtlAlbums = (DataList)Dtl_Artist.Items[0].FindControl("Dtl_Albums");
                dtlAlbums.DataSource = ds.Tables[1];
                dtlAlbums.DataBind();
            }
            else //Bad Request
                Response.Redirect("~/");
        }
    }
}