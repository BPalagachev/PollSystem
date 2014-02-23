<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditAnswer.aspx.cs" Inherits="PollSystem.Web.EditAnswer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h1>Edit Question</h1>

    Question: <asp:Literal runat="server" ID="LiteralBoxQueryText" /> <br />
    Answer: <asp:TextBox runat="server" ID="TextBoxAnswerText"/> <br />
    <asp:LinkButton ID="LinkButtonSaveAnswerText" 
                    Text="Save Answer Text"
                    runat="server" 
                    OnClientClick="return confirm('Are you sure that you want to save the changes to this answer?')" 
                    OnClick="LinkButtonSaveAnswerText_Click"/>
       <asp:LinkButton ID="LinkButtonDeleteAnswer" 
                    Text="Delete Answer"
                    runat="server" 
                    OnClientClick="return confirm('Are you sure that you want to delete this answer?')"
                    OnClick="LinkButtonDeleteAnswer_Click" />
    
</asp:Content>
