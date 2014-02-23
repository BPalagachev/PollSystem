<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PollSystem.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ListView runat="server" ID="ListViewPolls" ItemType="PollSystem.OpenAccess.Identity.Question">
        <LayoutTemplate>
            <div id="ItemPlaceholder" runat="server"></div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="poll-question">
                <%#: Item.QueryText %>
                <ul>
                    <asp:Repeater runat="server" 
                                  ItemType="PollSystem.OpenAccess.Identity.Answer"
                                  DataSource="<%# Item.Answers %>">
                        <ItemTemplate>
                            <li>
                                <%#: Item.AnswerText %>
                                <asp:LinkButton runat="server" 
                                                Text="Vote"
                                                CommandName="Vote"
                                                CommandArgument="<%#: Item.Id %>"
                                                OnCommand="Vote_Command" />
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </ItemTemplate>
    </asp:ListView>

</asp:Content>
