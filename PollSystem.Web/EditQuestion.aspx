<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditQuestion.aspx.cs" Inherits="PollSystem.Web.EditQuestion" %>
<asp:Content ID="ContentEditQuestion" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Edit Question</h1>

    <asp:TextBox runat="server" ID="TextBoxQueryText" TextMode="MultiLine" Rows="3" Columns="30" /> <br />
    <asp:LinkButton ID="LinkButtonSaveQuestionText" 
                    Text="Save Question Text"
                    runat="server" 
                    OnClientClick="return confirm('Are you sure that you want to save the changes to this questions?')"
                    OnClick="LinkButtonSaveQuestionText_Click"/>
    <table>
        <thead>
            <tr>
                <th>Answer</th>
                <th>Votes</th>
                <th>Commands</th>
            </tr>
        </thead>
        <asp:Repeater ID="RepeaterAnswers" runat="server" ItemType="PollSystem.OpenAccess.Identity.Answer">
            <ItemTemplate>
               
                <tr>
                    <td><%#: Item.AnswerText %> </td>
                    <td><%#: Item.Votes %></td>
                    <td>
                        <a href="EditAnswer.aspx?AnswerId=<%#: Item.Id %>">Edit...</a>
                        <asp:LinkButton 
                            ID="LinkButtonDeleteAnswer"
                            Text="Delete..." 
                            runat="server" 
                            CommandName="Delete" 
                            CommandArgument="<%#:Item.Id %>"
                            OnCommand="LinkButtonDeleteAnswer_Command"
                            OnClientClick="return confirm('Are sure that you want to delete the selected answer?');"/>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>


</asp:Content>
