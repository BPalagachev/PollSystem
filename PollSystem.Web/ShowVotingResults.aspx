<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShowVotingResults.aspx.cs" Inherits="PollSystem.Web.ShowVotingResults" %>
<asp:Content ID="ShowVotingResults" 
    ContentPlaceHolderID="MainContent"
     runat="server">
    <h1>Voting Results</h1>

    <h2>Question: <asp:Literal Id="LabelQuestion" runat="server" Mode="Encode" /></h2>
    <ul>
        <asp:Repeater 
            ID="RepeaterAnswers"
            runat="server" 
            ItemType="PollSystem.OpenAccess.Identity.Answer">
            <ItemTemplate>
                <li>
                    <%#: Item.AnswerText %> ---> <%#: Item.Votes %>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</asp:Content>
