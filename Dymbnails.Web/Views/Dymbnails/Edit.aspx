<%@ Page Title="" ValidateRequest="false" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<Dymbnails.Entities.Dymbnail>" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
	Dymbnails! Edit
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>
    
    <% using (var form = Html.BeginForm("Edit", "Dymbnails", FormMethod.Post)) { %>
        <%= Html.Hidden("id", Model.ID) %>
        <fieldset>
            <legend>Information</legend>
            
            <label for="txtTitle">Title:</label>
            <%= Html.TextBox("txtTitle", Model.Title, new { maxlength = 80 })%>
            
            <label for="txtDescription">Description:</label>
            <%= Html.TextArea("txtDescription", Model.Description, 4, 85, null) %>
        </fieldset>
        
        <fieldset>
            <legend>Content</legend>
            <%= Html.TextArea("txtContent", Model.Content, 25, 85, null) %>
        </fieldset>
        
        <div class="right"><input type="submit" value="Next" /></div>
        <div class="clear"></div>
    <% } %>

</asp:Content>
