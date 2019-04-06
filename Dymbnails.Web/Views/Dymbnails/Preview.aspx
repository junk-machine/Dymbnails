<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.String>" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
	Dymbnails! Preview
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Preview</h2>
    
    <div class="preview-image center">
        <%= Html.Image("preview", Model, "Preview") %>
    </div>
    <div class="preview-url center">
        <%= Model %>
    </div>

</asp:Content>
