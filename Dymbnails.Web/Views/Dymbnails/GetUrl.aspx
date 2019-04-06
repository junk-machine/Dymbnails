<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<Dymbnails.Web.Models.Dymbnails.GetUrlModel>" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
	Dymbnails! Create Url
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create Url</h2>
    
    <% using (var form = Html.BeginForm("GetUrl", "Dymbnails", FormMethod.Post)) { %>
        <%= Html.Hidden("dymbnailId", Model.DymbnailId) %>
        <fieldset>
        <legend>Image Parameters</legend>
        <% foreach (var item in Model.Variables) { %>
            <label for="<%= item.Name %>"><%= Html.Encode(item.Title) %></label>
            <%= Html.TextBox(item.Name) %>
            <legend><%= Html.Encode(item.Description) %></legend>
        <% }%>
        </fieldset>
        
        <div class="right"><input type="submit" value="Done" /></div>
        <div class="clear"></div>
    <% } %>

</asp:Content>
