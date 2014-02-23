<%@ Page Title="Results" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserQuestions.aspx.cs" Inherits="PollSystem.Web.UserQuestions" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Questions posted by: <asp:Literal runat="server" ID="LiteralCurrectUser" Mode="Encode" /></h2>

    <asp:GridView ID="GridViewQuestions"
        runat="server"
        AutoGenerateColumns="False"
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
            <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="ShowVotingResults.aspx?QuestionId={0}" Text="Go To" />
            <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="EditQuestion.aspx?QuestionId={0}" />
        </Columns>

        <PagerSettings Mode="NextPreviousFirstLast" Position="TopAndBottom"></PagerSettings>
    </asp:GridView>

</asp:Content>
