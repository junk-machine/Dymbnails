<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<Dymbnails.Web.Models.Dymbnails.IndexModel>" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
	Dymbnails! Collection
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Collection</h2>

    <% if (Model.Items.Count == 0) { %>
        <p>Sorry, we don't have anything yet.</p>
    <%}%>

    <% foreach (var item in Model.Items) { %>
        <div class="dymbnail-item">
            <div class="preview">
                <%= Html.Image(item.ID.ToString(), 
                    DymbnailsHelper.GetUrl(1, "image-id", item.ID),
                    item.Title) %>
            </div>
            
            <div class="title">
                <h3><%= Html.ActionLink(Html.Encode(item.Title), "GetUrl", new { id=item.ID } ) %></h3>
            </div>
            <div class="description">
                <p><%= Html.Encode(item.Description) %></p>
            </div>
            <div class="rating">
                <%= Html.ActionLink("Edit", "Edit", new { id=item.ID })%>>
                <span class="up"><%= item.Rating.Up %></span>
                /
                <span class="down"><%= item.Rating.Down %></span>
            </div>
            
            <div class="clear"></div>
        </div>
    <% } %>
</asp:Content>

