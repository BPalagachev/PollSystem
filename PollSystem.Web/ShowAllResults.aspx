<%@ Page Title="Results" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShowAllResults.aspx.cs" Inherits="PollSystem.Web.ShowAllResults" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridViewQuestions"
        runat="server"
        AutoGenerateColumns="False"
        OnSelectedIndexChanged="GridViewQuestions_SelectedIndexChanged"
        DataKeyNames="Id"
        AllowPaging="True"
        AllowCustomPaging="True"
        OnPageIndexChanging="GridViewQuestions_PageIndexChanging"
        PagerSettings-Mode="NextPreviousFirstLast"
        PagerSettings-NextPageText="Next"
        PagerSettings-PreviousPageText="Previous"
        PagerSettings-FirstPageText="First"
        PagerSettings-LastPageText="Last"
        PagerSettings-Position="TopAndBottom">
        <Columns>
            <asp:BoundField HeaderText="Question" DataField="QueryText" />
            <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="ShowVotingResults.aspx?QuestionId={0}" Text="View" />
            <asp:CommandField ShowSelectButton="True" HeaderText="Select" />
            <asp:TemplateField HeaderText="Posted By">
                <ItemTemplate>
                    <asp:HyperLink runat="server" Text='<%# Eval("PostedBy.UserName") %>' NavigateUrl='<%# Eval("PostedBy.Id", "~/UserQuestions?UserId={0}") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

        <PagerSettings Mode="NextPreviousFirstLast" Position="TopAndBottom"></PagerSettings>
    </asp:GridView>


    <h2>Question:
            <asp:Literal ID="LabelQuestion" runat="server" Mode="Encode" /></h2>
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
